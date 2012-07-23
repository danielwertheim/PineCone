using System;
using System.Linq;
using NUnit.Framework;
using PineCone.Structures.Schemas.Configuration;

namespace PineCone.Tests.UnitTests.Structures.Schemas.Configuration
{
    [TestFixture]
    public class StructureTypeConfigurationsTests : UnitTestBase
    {
        [Test]
        public void IsEmpty_WhenNoSpecificConfigurationExists_ReturnsFalse()
        {
            var configs = new StructureTypeConfigurations();

            Assert.IsTrue(configs.IsEmpty);
        }

        [Test]
        public void IsEmpty_WhenSpecificConfigurationExists_ReturnsTrue()
        {
            var configs = new StructureTypeConfigurations();
            configs.NewForType<Dummy>();

            Assert.IsFalse(configs.IsEmpty);
        }

        [Test]
        public void NewForType_WhenNeverCalledBefore_ConfigurationIsAdded()
        {
            var configs = new StructureTypeConfigurations();

            configs.NewForType(typeof(Dummy));

            Assert.AreEqual(1, configs.Items.Count());
        }

        [Test]
        public void Generic_NewForType_WhenNeverCalledBefore_ConfigurationIsAdded()
        {
            var configs = new StructureTypeConfigurations();

            configs.NewForType<Dummy>();

            Assert.AreEqual(1, configs.Items.Count());
        }

        [Test]
        public void NewForType_WhenCalledTwice_ThrowsException()
        {
            var configs = new StructureTypeConfigurations();
            configs.NewForType(typeof(Dummy));

            var ex = Assert.Throws<ArgumentException>(() => configs.NewForType(typeof(Dummy)));

            Assert.AreEqual("An item with the same key has already been added.", ex.Message);
        }

        [Test]
        public void Generic_NewForType_WhenCalledTwice_ThrowsException()
        {
            var configs = new StructureTypeConfigurations();
            configs.NewForType<Dummy>();

            var ex = Assert.Throws<ArgumentException>(() => configs.NewForType<Dummy>());

            Assert.AreEqual("An item with the same key has already been added.", ex.Message);
        }

        [Test]
        public void GetConfigurations_WhenRegistreredViaNonGenericVersion_ConfigurationIsReturned()
        {
            var configs = new StructureTypeConfigurations();
            configs.NewForType(typeof(Dummy));

            var config = configs.GetConfiguration(typeof(Dummy));

            Assert.IsNotNull(config);
            Assert.AreEqual(typeof(Dummy), config.Type);
        }

        [Test]
        public void GetConfigurations_WhenRegistreredViaGenericVersion_ConfigurationIsReturned()
        {
            var configs = new StructureTypeConfigurations();
            configs.NewForType<Dummy>();

            var config = configs.GetConfiguration(typeof(Dummy));

            Assert.IsNotNull(config);
            Assert.AreEqual(typeof(Dummy), config.Type);
        }

        [Test]
        public void Generic_GetConfigurations_WhenRegistreredViaNonGenericVersion_ConfigurationIsReturned()
        {
            var configs = new StructureTypeConfigurations();
            configs.NewForType(typeof(Dummy));

            var config = configs.GetConfiguration<Dummy>();

            Assert.IsNotNull(config);
            Assert.AreEqual(typeof(Dummy), config.Type);
        }

        [Test]
        public void Generic_GetConfigurations_WhenRegistreredViaGenericVersion_ConfigurationIsReturned()
        {
            var configs = new StructureTypeConfigurations();
            configs.NewForType<Dummy>();

            var config = configs.GetConfiguration<Dummy>();

            Assert.IsNotNull(config);
            Assert.AreEqual(typeof(Dummy), config.Type);
        }

        [Test]
        public void GetConfigurations_WhenNoConfigurationExists_ReturnsNull()
        {
            var configs = new StructureTypeConfigurations();

            var config = configs.GetConfiguration(typeof(Dummy));

            Assert.IsNull(config);
        }

        [Test]
        public void Generic_GetConfigurations_WhenNoConfigurationExists_ReturnsNull()
        {
            var configs = new StructureTypeConfigurations();

            var config = configs.GetConfiguration<Dummy>();

            Assert.IsNull(config);
        }

        [Test]
        public void AllowNestedStructures_WhenCalled_IncludeNestedStructureMembersBecomesTrue()
        {
            var configs = new StructureTypeConfigurations();

            var config = configs.NewForType(typeof(Dummy)).AllowNestedStructures();

            Assert.IsTrue(config.IncludeNestedStructureMembers);
        }

        //[Test]
        //public void Generic_AllowNestedStructures_WhenCalled_IncludeNestedStructureMembersBecomesTrue()
        //{
        //    var configs = new StructureTypeConfigurations();

        //    var config = configs.NewForType<Dummy>().AllowNestedStructures();

        //    Assert.IsTrue(config.IncludeNestedStructureMembers);
        //}

        private class Dummy { }
    }
}