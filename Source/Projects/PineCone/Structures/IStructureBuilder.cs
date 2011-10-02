using System.Collections.Generic;
using PineCone.Serializers;
using PineCone.Structures.Schemas;

namespace PineCone.Structures
{
    /// <summary>
    /// Builds <see cref="IStructure"/> instances from sent
    /// Items.
    /// </summary>
    public interface IStructureBuilder
    {
        /// <summary>
        /// Optional serializer. If specified,
        /// the <see cref="IStructure.Data"/> member
        /// will be filled with the serialization result.
        /// </summary>
        ISerializer Serializer { get; set; }

        /// <summary>
        /// Factory for creating <see cref="IStructureIndex"/> for each
        /// indexable member of the Items.
        /// </summary>
        IStructureIndexesFactory IndexesFactory { get; }

        /// <summary>
        /// Creates a single <see cref="IStructure"/> for sent <typeparamref name="T"/> item.
        /// The item will be assigned a new Sequential Guid Id as StructureId.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="structureSchema"></param>
        /// <returns></returns>
        IStructure CreateStructure<T>(T item, IStructureSchema structureSchema)
            where T : class;

        /// <summary>
        /// Yields each item as an <see cref="IStructure"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="structureSchema"></param>
        /// <returns></returns>
        IEnumerable<IStructure> CreateStructures<T>(ICollection<T> items, IStructureSchema structureSchema)
            where T : class;

        /// <summary>
        /// Yields batches of <see cref="IStructure"/> for sent <typeparamref name="T"/> item.
        /// All items will be assigned a new Sequential Guid Id as StructureId.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="structureSchema"></param>
        /// <param name="maxBatchSize"></param>
        /// <returns></returns>
        IEnumerable<IStructure[]> CreateStructures<T>(ICollection<T> items, IStructureSchema structureSchema, int maxBatchSize) 
            where T : class;
    }
}