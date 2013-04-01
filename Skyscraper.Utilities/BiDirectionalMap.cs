using System;
using System.Collections.Generic;
using System.Linq;

namespace Skyscraper.Utilities
{
    public class BiDirectionalMap <TKey, TValue> : IDictionary<TKey, TValue>
    {
        private IDictionary<TKey, TValue> values = new Dictionary<TKey, TValue>();
        private IDictionary <TValue, TKey> keys = new Dictionary<TValue, TKey>();

        public int Count
        {
            get { return Math.Min(this.values.Count(), this.keys.Count()); }
        }

        public bool IsReadOnly
        {
            get { return this.values.IsReadOnly || this.keys.IsReadOnly; }
        }

        public ICollection<TKey> Keys
        {
            get { return this.keys.Values; }
        }

        public ICollection<TValue> Values
        {
            get { return this.values.Values; }
        }

        public TValue this[TKey key]
        {
            get
            {
                return this.values[key];
            }
            set
            {
                this.values[key] = value;
                this.keys[value] = key;
            }
        }

        public TKey this[TValue _value]
        {
            get
            {
                return this.keys[_value];
            }
            set
            {
                this.values[value] = _value;
                this.keys[_value] = value;
            }
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            this.Add(item.Key, item.Value);
        }
        public void Add(KeyValuePair<TValue, TKey> item)
        {
            this.Add(item.Key, item.Value);
        }
        public void Add(TValue value, TKey key)
        {
            Add(key, value);
        }
        public void Add(TKey key, TValue value)
        {
            this.values.Add(key, value);
            this.keys.Add(value, key);
        }

        public bool ContainsKey(TKey key)
        {
            return this.values.ContainsKey(key);
        }

        public bool ContainsKey(TValue value)
        {
            return this.keys.ContainsKey(value);
        }

        public bool Remove(TKey key)
        {
            TValue value = this.values[key];

            return this.Remove(key, value);
        }

        public bool Remove(TValue value)
        {
            TKey key = this.keys[value];

            return this.Remove(key, value);
        }

        private bool Remove(TKey key, TValue value)
        {
            return this.values.Remove(key) && this.keys.Remove(value);
        }


        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return this.Remove(item.Key, item.Value);
        }

        public void Clear()
        {
            this.keys.Clear();
            this.values.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return this.values.Contains(item);
        }

        public bool Contains(KeyValuePair<TValue, TKey> item)
        {
            return this.keys.Contains(item);
        }


        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return this.values.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.values.GetEnumerator();
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return this.values.TryGetValue(key, out value);
        }

        public bool TryGetValue(TValue key, out TKey value)
        {
            return this.keys.TryGetValue(key, out value);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            //implementation doesn't garentee that both dictionaries are in the same order
            throw new NotImplementedException();
        }
    }
}