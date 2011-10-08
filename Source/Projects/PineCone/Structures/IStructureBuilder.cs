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
        /// Factory used for constructing <see cref="IStructureIndex"/>.
        /// </summary>
        IStructureIndexesFactory IndexesFactory { get; set; }

        /// <summary>
        /// Default structure Id generator that will be used if
        /// you dont call <see cref="CreateStructure{T}"/> or
        /// <see cref="CreateStructures{T}"/> or
        /// <see cref="CreateStructureBatches{T}"/> with a
        /// specific <see cref="IStructureIdGenerator"/>.
        /// </summary>
        IStructureIdGenerator IdGenerator { get; set; }

        /// <summary>
        /// Optional serializer. If specified,
        /// the <see cref="IStructure.Data"/> member
        /// will be filled with the serialization result.
        /// </summary>
        ISerializer Serializer { get; set; }

        /// <summary>
        /// Creates a single <see cref="IStructure"/> for sent <typeparamref name="T"/> item.
        /// The item will be assigned a new Sequential Guid Id as StructureId.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="structureSchema"></param>
        /// <param name="structureIdGenerator">Optional</param>
        /// <returns></returns>
        IStructure CreateStructure<T>(T item, IStructureSchema structureSchema, IStructureIdGenerator structureIdGenerator = null)
            where T : class;

        /// <summary>
        /// Yields each item as an <see cref="IStructure"/>.
        /// All items will be assigned a new Sequential Guid Id as StructureId.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="structureSchema"></param>
        /// <param name="structureIdGenerator">Optional</param>
        /// <returns></returns>
        IEnumerable<IStructure> CreateStructures<T>(ICollection<T> items, IStructureSchema structureSchema, IStructureIdGenerator structureIdGenerator = null)
            where T : class;

        /// <summary>
        /// Yields batches of <see cref="IStructure"/> for sent <typeparamref name="T"/> item.
        /// All items will be assigned a new Sequential Guid Id as StructureId.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="structureSchema"></param>
        /// <param name="structureIdGenerator">Optional</param>
        /// <param name="maxBatchSize"></param>
        /// <returns></returns>
        IEnumerable<IStructure[]> CreateStructureBatches<T>(ICollection<T> items, IStructureSchema structureSchema, int maxBatchSize, IStructureIdGenerator structureIdGenerator = null) 
            where T : class;
    }
}