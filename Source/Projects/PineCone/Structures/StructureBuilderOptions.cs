using PineCone.Serializers;

namespace PineCone.Structures
{
    public class StructureBuilderOptions
    {
        /// <summary>
        /// Is used to generate <see cref="IStructureId"/> that
        /// will be assigned to the structure items, depending
        /// on <see cref="AutoGenerateStructureId"/>.
        /// </summary>
        public IStructureIdGenerator StructureIdGenerator { get; set; }

        /// <summary>
        /// If specified, the <see cref="IStructure.Data"/> member
        /// will be filled with the serialization result.
        /// </summary>
        public ISerializer Serializer { get; set; }

        /// <summary>
        /// If true (default) the specified <see cref="StructureIdGenerator"/> generator will be
        /// consumed. If false, you are responsible for assigning values
        /// for the <see cref="IStructureId"/> property of your items you self.
        /// </summary>
        public bool AutoGenerateStructureId { get; set; }

        public StructureBuilderOptions()
        {
            StructureIdGenerator = new GuidStructureIdGenerator();
            Serializer = new EmptySerializer();
            AutoGenerateStructureId = true;
        }
    }
}