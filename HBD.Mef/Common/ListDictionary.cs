using System;
using System.Collections;
using System.Collections.Generic;

namespace HBD.Mef.Common
{
    public sealed class ListDictionary<TKey, TValue> : IDictionary<TKey, IList<TValue>>,
        ICollection<KeyValuePair<TKey, IList<TValue>>>, IEnumerable<KeyValuePair<TKey, IList<TValue>>>, IEnumerable
    {
        private readonly Dictionary<TKey, IList<TValue>> innerValues = new Dictionary<TKey, IList<TValue>>();

        /// <summary>
        ///     Gets a shallow copy of all values in all lists.
        /// </summary>
        /// <value>List of values.</value>
        public IList<TValue> Values
        {
            get
            {
                var objList = new List<TValue>();
                foreach (IEnumerable<TValue> collection in innerValues.Values)
                    objList.AddRange(collection);
                return objList;
            }
        }

        /// <summary>
        ///     Gets the list of keys in the dictionary.
        /// </summary>
        /// <value>Collection of keys.</value>
        public ICollection<TKey> Keys
        {
            get { return innerValues.Keys; }
        }

        /// <summary>
        ///     Gets or sets the list associated with the given key. The access always succeeds,
        ///     eventually returning an empty list.
        /// </summary>
        /// <param name="key">The key of the list to access.</param>
        /// <returns>The list associated with the key.</returns>
        public IList<TValue> this[TKey key]
        {
            get
            {
                if (!innerValues.ContainsKey(key))
                    innerValues.Add(key, new List<TValue>());
                return innerValues[key];
            }
            set { innerValues[key] = value; }
        }

        /// <summary>
        ///     Gets the number of lists in the dictionary.
        /// </summary>
        /// <value>Value indicating the values count.</value>
        public int Count
        {
            get { return innerValues.Count; }
        }

        ICollection<IList<TValue>> IDictionary<TKey, IList<TValue>>.Values
        {
            get { return innerValues.Values; }
        }

        bool ICollection<KeyValuePair<TKey, IList<TValue>>>.IsReadOnly
        {
            get { return ((ICollection<KeyValuePair<TKey, IList<TValue>>>) innerValues).IsReadOnly; }
        }

        /// <summary>
        ///     Removes all entries in the dictionary.
        /// </summary>
        public void Clear()
        {
            innerValues.Clear();
        }

        /// <summary>
        ///     Determines whether the dictionary contains the given key.
        /// </summary>
        /// <param name="key">The key to locate.</param>
        /// <returns>true if the dictionary contains the given key; otherwise, false.</returns>
        public bool ContainsKey(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            return innerValues.ContainsKey(key);
        }

        /// <summary>
        ///     Removes a list by key.
        /// </summary>
        /// <param name="key">The key of the list to remove.</param>
        /// <returns><see langword="true" /> if the element was removed.</returns>
        public bool Remove(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            return innerValues.Remove(key);
        }

        void IDictionary<TKey, IList<TValue>>.Add(TKey key, IList<TValue> value)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (value == null)
                throw new ArgumentNullException("value");
            innerValues.Add(key, value);
        }

        bool IDictionary<TKey, IList<TValue>>.TryGetValue(TKey key, out IList<TValue> value)
        {
            value = this[key];
            return true;
        }

        void ICollection<KeyValuePair<TKey, IList<TValue>>>.Add(KeyValuePair<TKey, IList<TValue>> item)
        {
            ((ICollection<KeyValuePair<TKey, IList<TValue>>>) innerValues).Add(item);
        }

        bool ICollection<KeyValuePair<TKey, IList<TValue>>>.Contains(KeyValuePair<TKey, IList<TValue>> item)
        {
            return ((ICollection<KeyValuePair<TKey, IList<TValue>>>) innerValues).Contains(item);
        }

        void ICollection<KeyValuePair<TKey, IList<TValue>>>.CopyTo(KeyValuePair<TKey, IList<TValue>>[] array,
            int arrayIndex)
        {
            ((ICollection<KeyValuePair<TKey, IList<TValue>>>) innerValues).CopyTo(array, arrayIndex);
        }

        bool ICollection<KeyValuePair<TKey, IList<TValue>>>.Remove(KeyValuePair<TKey, IList<TValue>> item)
        {
            return ((ICollection<KeyValuePair<TKey, IList<TValue>>>) innerValues).Remove(item);
        }

        IEnumerator<KeyValuePair<TKey, IList<TValue>>> IEnumerable<KeyValuePair<TKey, IList<TValue>>>.GetEnumerator()
        {
            return innerValues.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return innerValues.GetEnumerator();
        }

        /// <summary>
        ///     If a list does not already exist, it will be created automatically.
        /// </summary>
        /// <param name="key">The key of the list that will hold the value.</param>
        public void Add(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            CreateNewList(key);
        }

        /// <summary>
        ///     Adds a value to a list with the given key. If a list does not already exist, it will be
        ///     created automatically.
        /// </summary>
        /// <param name="key">The key of the list that will hold the value.</param>
        /// <param name="value">The value to add to the list under the given key.</param>
        public void Add(TKey key, TValue value)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (value == null)
                throw new ArgumentNullException("value");
            if (innerValues.ContainsKey(key))
                innerValues[key].Add(value);
            else
                CreateNewList(key).Add(value);
        }

        private List<TValue> CreateNewList(TKey key)
        {
            var objList = new List<TValue>();
            innerValues.Add(key, objList);
            return objList;
        }

        /// <summary>
        ///     Determines whether the dictionary contains the specified value.
        /// </summary>
        /// <param name="value">The value to locate.</param>
        /// <returns>true if the dictionary contains the value in any list; otherwise, false.</returns>
        public bool ContainsValue(TValue value)
        {
            foreach (var innerValue in innerValues)
                if (innerValue.Value.Contains(value))
                    return true;
            return false;
        }

        /// <summary>
        ///     Retrieves the all the elements from the list which have a key that matches the condition
        ///     defined by the specified predicate.
        /// </summary>
        /// <param name="keyFilter">
        ///     The filter with the condition to use to filter lists by their key.
        /// </param>
        /// <returns>
        ///     The elements that have a key that matches the condition defined by the specified predicate.
        /// </returns>
        public IEnumerable<TValue> FindAllValuesByKey(Predicate<TKey> keyFilter)
        {
            foreach (var keyValuePair in this)
                if (keyFilter(keyValuePair.Key))
                    foreach (var obj in keyValuePair.Value)
                        yield return obj;
        }

        /// <summary>
        ///     Retrieves all the elements that match the condition defined by the specified predicate.
        /// </summary>
        /// <param name="valueFilter">The filter with the condition to use to filter values.</param>
        /// <returns>The elements that match the condition defined by the specified predicate.</returns>
        public IEnumerable<TValue> FindAllValues(Predicate<TValue> valueFilter)
        {
            foreach (var keyValuePair in this)
                foreach (var obj in keyValuePair.Value)
                    if (valueFilter(obj))
                        yield return obj;
        }

        /// <summary>
        ///     Removes a value from the list with the given key.
        /// </summary>
        /// <param name="key">The key of the list where the value exists.</param>
        /// <param name="value">The value to remove.</param>
        public void Remove(TKey key, TValue value)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (value == null)
                throw new ArgumentNullException("value");
            if (!innerValues.ContainsKey(key))
                return;
            ((List<TValue>) innerValues[key]).RemoveAll(item => value.Equals(item));
        }

        /// <summary>
        ///     Removes a value from all lists where it may be found.
        /// </summary>
        /// <param name="value">The value to remove.</param>
        public void Remove(TValue value)
        {
            foreach (var innerValue in innerValues)
                Remove(innerValue.Key, value);
        }
    }
}