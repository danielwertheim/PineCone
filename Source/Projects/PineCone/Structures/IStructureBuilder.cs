using System.Collections.Generic;
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
        /// Creates a single <see cref="IStructure"/> for sent <typeparamref name="T"/> item.
        /// The item will be assigned a new Sequential Guid Id as StructureId.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="structureSchema"></param>
        /// <param name="options">optional</param>
        /// <returns></returns>
        IStructure CreateStructure<T>(T item, IStructureSchema structureSchema, StructureBuilderOptions options = null)
            where T : class;

        /// <summary>
        /// Yields each item as an <see cref="IStructure"/>.
        /// All items will be assigned a new Sequential Guid Id as StructureId.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="structureSchema"></param>
        /// <param name="options">optional</param>
        /// <returns></returns>
        IEnumerable<IStructure> CreateStructures<T>(ICollection<T> items, IStructureSchema structureSchema, StructureBuilderOptions options = null)
            where T : class;

        /// <summary>
        /// Yields batches of <see cref="IStructure"/> for sent <typeparamref name="T"/> item.
        /// All items will be assigned a new Sequential Guid Id as StructureId.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="structureSchema"></param>
        /// <param name="options">optional</param>
        /// <param name="maxBatchSize"></param>
        /// <returns></returns>
        IEnumerable<IStructure[]> CreateStructureBatches<T>(ICollection<T> items, IStructureSchema structureSchema, int maxBatchSize, StructureBuilderOptions options = null) 
            where T : class;
    }
}