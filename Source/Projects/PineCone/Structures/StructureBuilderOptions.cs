using PineCone.Serializers;

namespace PineCone.Structures
{
    public class StructureBuilderOptions
    {
        /// <summary>
        /// Is used to generate <see cref="IStructureId"/> that
        /// will be assigned to the structure items, depending
        /// on <see cref="KeepStructureId"/>.
        /// </summary>
        public IStructureIdGenerator IdGenerator { get; set; }

        /// <summary>
        /// If specified, the <see cref="IStructure.Data"/> member
        /// will be filled with the serialization result.
        /// </summary>
        public ISerializer Serializer { get; set; }

        /// <summary>
        /// Determines if the StructureId member
        /// of the item being passed to the <see cref="IStructureBuilder"/>
        /// should be overriden or not.
        /// </summary>
        public bool KeepStructureId { get; set; }

        public StructureBuilderOptions()
        {
            IdGenerator = new GuidStructureIdGenerator();
            Serializer = new EmptySerializer();
            KeepStructureId = false;
        }
    }
}