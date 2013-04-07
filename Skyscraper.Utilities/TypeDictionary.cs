using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Skyscraper.Utilities
{
    public class TypeDictionary<TKey, TValue> : IDictionary <TKey, TValue> where TValue : class
    {
        private Dictionary<TKey, Type> types;
        private Dictionary<TKey, TValue> instances;

        public TypeDictionary()
        {
            this.types = new Dictionary<TKey, Type> { };
            this.instances = new Dictionary<TKey, TValue> { };
        }

        public TypeDictionary(Dictionary<TKey, Type> types)
        {
            this.types = types;
            this.instances = new Dictionary<TKey, TValue> { };
        }

        public ICollection<TKey> Keys
        {
            get { return this.types.Keys; }
        }

        public ICollection<TValue> Values
        {
            get {
                ICollection<TValue> result = new Collection<TValue>();
                foreach (TKey type in types.Keys)
                {
                    result.Add(this[type]);
                }
                return result;
            }
        }

        public int Count
        {
            get { return this.types.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public TValue InitialiseType(TKey key)
        {
            return this[key];
        }

        public TValue this[TKey key]
        {
            get
            {
                TValue result;
                this.TryGetValue(key, out result);

                return result;
            }
            set
            {
                lock (this)
                {
                    this.types[key] = value.GetType();
                    this.instances[key] = value;
                }
            }
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            this.Add(item.Key, item.Value);
        }
        public void Add(TKey key, TValue value)
        {
            lock (this)
            {
                this.types.Add(key, value.GetType());
                this.instances.Add(key, value);
            }
        }

        public bool ContainsKey(TKey key)
        {
            return this.types.ContainsKey(key);
        }

        public bool Remove(TKey key)
        {
            lock (this)
            {
                this.instances.Remove(key);

                return this.types.Remove(key);
            }
        }
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            lock (this)
            {
                this.instances.Remove(item.Key);

                return this.types.Remove(item.Key);
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (this.instances.ContainsKey(key))
            {
                return this.instances.TryGetValue(key, out value);
            }
            else
            {
                lock (this.instances)
                {
                    Type type;
                    Boolean success = this.types.TryGetValue(key, out type);
                    value = Activator.CreateInstance(type) as TValue;
                    success &= value != null;

                    if (success)
                    {
                        this.instances.Add(key, value);
                    }

                    return success;
                }
            }
        }

        public void Clear()
        {
            lock (this)
            {
                this.types.Clear();
                this.instances.Clear();
            }
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return this.instances.Contains(item) || this.types.Contains(new KeyValuePair<TKey, Type>(item.Key, item.Value.GetType()));
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            if (this.types.Count() != this.instances.Count())
            {
                foreach (TKey type in this.types.Keys)
                {
                    this.InitialiseType(type);
                }
            }

            return new Enumerator<KeyValuePair<TKey, TValue>>(this.instances.ToList());
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
