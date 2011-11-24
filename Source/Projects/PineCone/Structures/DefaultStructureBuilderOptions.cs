using EnsureThat;
using PineCone.Serializers;

namespace PineCone.Structures
{
    public class DefaultStructureBuilderOptions : IStructureBuilderOptions
    {
        private IStructureIndexesFactory _indexesFactory;
        private IStructureIdStrategy _structureIdStrategy;
        private IStructureSerializer _structureSerializer;

        public IStructureIndexesFactory IndexesFactory
        {
            get { return _indexesFactory; }
            set
            {
                Ensure.That(value, "IndexesFactory").IsNotNull();

                _indexesFactory = value;
            }
        }

        public IStructureIdStrategy StructureIdStrategy
        {
            get { return _structureIdStrategy; }
            set
            {
                Ensure.That(value, "StructureIdStrategy").IsNotNull();

                _structureIdStrategy = value;
            }
        }


        public IStructureSerializer StructureSerializer
        {
            get { return _structureSerializer; }
            set
            {
                Ensure.That(value, "StructureSerializer").IsNotNull();

                _structureSerializer = value;
            }
        }

        public DefaultStructureBuilderOptions()
        {
            IndexesFactory = new StructureIndexesFactory();
            StructureIdStrategy = new AlwaysNewGuidStructureIdStrategy();
            StructureSerializer = new EmptyStructureSerializer();
        }
    }
}