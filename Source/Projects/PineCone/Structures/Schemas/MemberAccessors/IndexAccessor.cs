using System.Collections;
using System.Collections.Generic;

namespace PineCone.Structures.Schemas.MemberAccessors
{
    public class IndexAccessor : MemberAccessorBase, IIndexAccessor
    {
        private delegate void OnLastPropertyFound(IStructureProperty lastProperty, object currentNode);

        private readonly List<IStructureProperty> _callstack;

        public bool IsEnumerable
        {
            get { return Property.IsEnumerable; }
        }

        public bool IsElement
        {
            get { return Property.IsElement; }
        }

        public bool IsUnique
        {
            get { return Property.IsUnique; }
        }

        public IndexAccessor(IStructureProperty property)
            : base(property)
        {
            _callstack = GetCallstack(Property);
            _callstack.Reverse();
        }

        private static List<IStructureProperty> GetCallstack(IStructureProperty property)
        {
            if (property.IsRootMember)
                return new List<IStructureProperty> { property };

            var props = new List<IStructureProperty> { property };
            props.AddRange(
                GetCallstack(property.Parent));

            return props;
        }

        public IList<object> GetValues<T>(T item) where T : class
        {
            if (!Property.IsRootMember)
                return EvaluateCallstack(item, startIndex: 0);

            var firstLevelPropValue = Property.GetValue(item);
            if (firstLevelPropValue == null)
                return null;

            return IsEnumerable
                ? CollectionOfValuesToList((ICollection)firstLevelPropValue)
                : new[] { firstLevelPropValue };
        }

        private IList<object> EvaluateCallstack<T>(T startNode, int startIndex)
        {
            object currentNode = startNode;
            for (var c = startIndex; c < _callstack.Count; c++)
            {
                if (currentNode == null)
                    return new object[] { null };

                var currentProperty = _callstack[c];
                var isLastProperty = c == (_callstack.Count - 1);
                if (isLastProperty)
                    return currentNode is ICollection
                        ? ExtractValuesForEnumerableNode((ICollection)currentNode, currentProperty)
                        : ExtractValuesForSimpleNode(currentNode, currentProperty);

                if (!(currentNode is ICollection))
                    currentNode = currentProperty.GetValue(currentNode);
                else
                {
                    var values = new List<object>();
                    foreach (var node in (ICollection)currentNode)
                        values.AddRange(EvaluateCallstack(currentProperty.GetValue(node), startIndex: c + 1));
                    return values;
                }
            }

            return null;
        }

        private static IList<object> ExtractValuesForEnumerableNode(ICollection nodes, IStructureProperty property)
        {
            var values = new List<object>();
            foreach (var node in nodes)
            {
                if (node == null)
                {
                    values.Add(null);
                    continue;
                }

                var nodeValue = property.GetValue(node);

                if (nodeValue == null || !(nodeValue is ICollection))
                    values.Add(nodeValue);
                else
                    values.AddRange(CollectionOfValuesToList((ICollection)nodeValue));
            }

            return values;
        }

        private static IList<object> ExtractValuesForSimpleNode(object node, IStructureProperty property)
        {
            var currentValue = property.GetValue(node);

            if (currentValue == null)
                return null;

            if (!property.IsEnumerable)
                return new[] { currentValue };

            return CollectionOfValuesToList((ICollection)currentValue);
        }

        private static IList<object> CollectionOfValuesToList(ICollection collection)
        {
            var values = new List<object>(collection.Count);

            foreach (var element in collection)
                values.Add(element);

            return values;
        }

        public void SetValue<T>(T item, object value) where T : class
        {
            if (Property.IsRootMember)
            {
                Property.SetValue(item, value);
                return;
            }

            EnumerateToLastProperty(item, startIndex: 0, onLastPropertyFound: (lastProperty, currentNode) => lastProperty.SetValue(currentNode, value));
        }

        private void EnumerateToLastProperty<T>(T startNode, int startIndex, OnLastPropertyFound onLastPropertyFound) where T : class
        {
            object currentNode = startNode;
            for (var c = startIndex; c < _callstack.Count; c++)
            {
                var currentProperty = _callstack[c];
                var isLastPropertyInfo = c == (_callstack.Count - 1);
                if (isLastPropertyInfo)
                {
                    onLastPropertyFound(currentProperty, currentNode);
                    break;
                }

                currentNode = currentProperty.GetValue(currentNode);
            }
        }
    }
}