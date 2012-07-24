using Moq;
using NUnit.Framework;
using PineCone.Structures.Schemas;

namespace PineCone.Tests.UnitTests.Structures.Schemas
{
    [TestFixture]
    public class StructureTypeFactoryTests
    {
        [Test]
        public void CreateFor_WhenNoExplicitConfigExistsForType_InvokesGetIndexablePropertiesOnReflecter()
        {
            var type = typeof(MyClass);
            var reflecterMock = new Mock<IStructureTypeReflecter>();
            reflecterMock.Setup(m => m.GetIdProperty()).Returns(() =>
            {
                var idProperty = new Mock<IStructureProperty>();

                return idProperty.Object;
            });
            reflecterMock.Setup(m => m.GetIndexableProperties()).Returns(() =>
            {
                var indexProperty = new Mock<IStructureProperty>();

                return new[] { indexProperty.Object };
            });

            var factory = new StructureTypeFactory(t => reflecterMock.Object);
            factory.CreateFor(type);

            reflecterMock.Verify(m => m.GetIndexableProperties());
        }

        [Test]
        public void CreateFor_WhenConfigExcludingMembersExistsForType_InvokesGetIndexablePropertiesExceptOnReflecter()
        {
            var type = typeof(MyClass);
            var reflecterMock = new Mock<IStructureTypeReflecter>();
            reflecterMock.Setup(m => m.GetIdProperty()).Returns(() =>
            {
                var idProperty = new Mock<IStructureProperty>();

                return idProperty.Object;
            });
            reflecterMock.Setup(m => m.GetIndexablePropertiesExcept(new[] { "ExcludeTEMP" })).Returns(() =>
            {
                var indexProperty = new Mock<IStructureProperty>();

                return new[] { indexProperty.Object };
            });

            var factory = new StructureTypeFactory(t => reflecterMock.Object);
            factory.Configurations.Configure(type, cfg => cfg.DoNotIndexThis("ExcludeTEMP"));
            factory.CreateFor(type);

            reflecterMock.Verify(m => m.GetIndexablePropertiesExcept(new[] { "ExcludeTEMP" }));
        }

        [Test]
        public void CreateFor_WhenConfigIncludingMembersExistsForType_InvokesGetSpecificIndexablePropertiesOnReflecter()
        {
            var type = typeof(MyClass);
            var reflecterMock = new Mock<IStructureTypeReflecter>();
            reflecterMock.Setup(m => m.GetIdProperty()).Returns(() =>
            {
                var idProperty = new Mock<IStructureProperty>();

                return idProperty.Object;
            });
            reflecterMock.Setup(m => m.GetSpecificIndexableProperties(new[] { "IncludeTEMP" })).Returns(() =>
            {
                var indexProperty = new Mock<IStructureProperty>();

                return new[] { indexProperty.Object };
            });

            var factory = new StructureTypeFactory(t => reflecterMock.Object);
            factory.Configurations.Configure(type, cfg => cfg.OnlyIndexThis("IncludeTEMP"));
            factory.CreateFor(type);

            reflecterMock.Verify(m => m.GetSpecificIndexableProperties(new[] { "IncludeTEMP" }));
        }

        private class MyClass
        {
        }
    }
}