using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using PineCone.Structures.Schemas;
using PineCone.Structures.Schemas.MemberAccessors;

namespace PineCone.Tests.UnitTests.Structures.Schemas
{
    [TestFixture]
    public class StructureSchemaTests : UnitTestBase
    {
        [Test]
        public void Ctor_WhenNameIsWhiteSpaceEmpty_ThrowsArgumentNullException()
        {
            var idAccessorFake = new Mock<IIdAccessor>();

            var ex = Assert.Throws<ArgumentException>(() => new StructureSchema(" ", "FakeHash", idAccessorFake.Object));

            Assert.AreEqual("name", ex.ParamName);
        }

        [Test]
        public void Ctor_WhenHashIsWhiteSpaceEmpty_ThrowsArgumentNullException()
        {
            var idAccessorFake = new Mock<IIdAccessor>();

            var ex = Assert.Throws<ArgumentException>(() => new StructureSchema("FakeName", " ", idAccessorFake.Object));

            Assert.AreEqual("hash", ex.ParamName);
        }

        [Test]
        public void Ctor_WhenIdAccessorIsNull_DoesNotThrowArgumentNullException()
        {
            Assert.DoesNotThrow(() => new StructureSchema("FakeName", "FakeHash", null));
        }

        [Test]
        public void Ctor_WhenUniqueIndexAccessorsInjected_ExistsInBothListOfIndexAccessors()
        {
            var idAccessorFake = new Mock<IIdAccessor>();
            var indexAccessorFake = new Mock<IIndexAccessor>();

            indexAccessorFake.Setup(x => x.Path).Returns("Plain");
            indexAccessorFake.Setup(x => x.IsUnique).Returns(false);
            
            var uniqueIndexAccessorFake = new Mock<IIndexAccessor>();
            uniqueIndexAccessorFake.Setup(x => x.Path).Returns("Unique");
            uniqueIndexAccessorFake.Setup(x => x.IsUnique).Returns(true);

            var schema = new StructureSchema(
                "FakeName",
                "FakeHash",
                idAccessorFake.Object,
                new[] {indexAccessorFake.Object, uniqueIndexAccessorFake.Object});

            Assert.IsTrue(schema.IndexAccessors.Any(iac => iac.Path == indexAccessorFake.Object.Path));
            Assert.IsFalse(schema.UniqueIndexAccessors.Any(iac => iac.Path == indexAccessorFake.Object.Path));
            Assert.IsTrue(schema.IndexAccessors.Any(iac => iac.Path == uniqueIndexAccessorFake.Object.Path));
            Assert.IsTrue(schema.UniqueIndexAccessors.Any(iac => iac.Path == uniqueIndexAccessorFake.Object.Path));
        }
    }
}