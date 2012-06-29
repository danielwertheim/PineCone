#--------------------------------------
# Dependencies
#--------------------------------------
require 'albacore'
#--------------------------------------
# Debug
#--------------------------------------
#ENV.each {|key, value| puts "#{key} = #{value}" }
#--------------------------------------
# Environment vars
#--------------------------------------
@env_solutionname = 'PineCone'
@env_solutionfolderpath = "../Source"

@env_projectnamePineCone = 'PineCone'

@env_buildfolderpath = 'build'
@env_version = "3.4.1"
@env_buildversion = @env_version + (ENV['env_buildnumber'].to_s.empty? ? "" : ".#{ENV['env_buildnumber'].to_s}")
@env_buildconfigname = ENV['env_buildconfigname'].to_s.empty? ? "Release" : ENV['env_buildconfigname'].to_s
@env_buildname = "#{@env_solutionname}-v#{@env_buildversion}-#{@env_buildconfigname}"
#--------------------------------------
# Reusable vars
#--------------------------------------
pineConeOutputPath = "#{@env_buildfolderpath}/#{@env_projectnamePineCone}"
#--------------------------------------
# Albacore flow controlling tasks
#--------------------------------------
task :ci => [:installNuGetPackages, :buildIt, :copyPineCone, :testIt, :zipIt, :packIt]

task :local => [:installNuGetPackages, :buildIt, :copyPineCone, :testIt, :zipIt, :packIt]
#--------------------------------------
task :testIt => [:unittests]

task :zipIt => [:zipPineCone]

task :packIt => [:packPineConeNuGet]
#--------------------------------------
# Albacore tasks
#--------------------------------------
task :installNuGetPackages do
	FileList["#{@env_solutionfolderpath}/**/packages.config"].each { |filepath|
		sh "NuGet.exe i #{filepath} -o #{@env_solutionfolderpath}/packages"
	}
end

assemblyinfo :versionIt do |asm|
	sharedAssemblyInfoPath = "#{@env_solutionfolderpath}/SharedAssemblyInfo.cs"

	asm.input_file = sharedAssemblyInfoPath
	asm.output_file = sharedAssemblyInfoPath
	asm.version = @env_version
	asm.file_version = @env_buildversion  
end

task :ensureCleanBuildFolder do
	FileUtils.rm_rf(@env_buildfolderpath)
	FileUtils.mkdir_p(@env_buildfolderpath)
end

msbuild :buildIt => [:ensureCleanBuildFolder, :versionIt] do |msb|
	msb.properties :configuration => @env_buildconfigname
	msb.targets :Clean, :Build
	msb.solution = "#{@env_solutionfolderpath}/#{@env_solutionname}.sln"
end

task :copyPineCone do
	FileUtils.mkdir_p(pineConeOutputPath)
	FileUtils.cp_r(FileList["#{@env_solutionfolderpath}/Projects/#{@env_projectnamePineCone}/bin/#{@env_buildconfigname}/**"], pineConeOutputPath)
end

nunit :unittests do |nunit|
	nunit.command = "nunit-console.exe"
	nunit.options "/framework=v4.0.30319","/xml=#{@env_buildfolderpath}/NUnit-Report-#{@env_solutionname}-UnitTests.xml"
	nunit.assemblies FileList["#{@env_solutionfolderpath}/Tests/#{@env_solutionname}.**UnitTests/bin/#{@env_buildconfigname}/#{@env_solutionname}.**UnitTests.dll"]
end

zip :zipPineCone do |zip|
	zip.directories_to_zip pineConeOutputPath
	zip.output_file = "#{@env_buildname}.zip"
	zip.output_path = @env_buildfolderpath
end

exec :packPineConeNuGet do |cmd|
	cmd.command = "NuGet.exe"
	cmd.parameters = "pack #{@env_projectnamePineCone}.nuspec -version #{@env_version} -basepath #{pineConeOutputPath} -outputdirectory #{@env_buildfolderpath}"
end