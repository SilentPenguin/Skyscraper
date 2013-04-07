using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscraper.Utilities
{
    public class TypeList<T> : IList<T> where T : class
    {
        private List<Type> types;
        private Dictionary<int, T> instances;
        
        public TypeList (){
            this.types = new List<Type>();
            this.instances = new Dictionary<int, T>();
        }
        public TypeList (List<Type> list)
        {
            this.types = list;
            this.instances = new Dictionary<int, T>();
        }

        public int IndexOf(T item)
        {
            return this.types.IndexOf(item.GetType());
        }

        public void Insert(int index, T item)
        {
            this.types.Insert(index, item.GetType());
            this.instances.Add(index, item);
        }

        public void RemoveAt(int index)
        {
            this.types.RemoveAt(index);
            this.instances.Remove(index);
        }

        public T this[int index]
        {
            get
            {
                if (this.instances.ContainsKey(index)){
                    return this.instances[index];
                }
                else
                {
                    lock (this.instances)
                    {
                        Type type = this.types[index];
                        T instance = Activator.CreateInstance(type) as T;

                        if (instance != null)
                        {
                            this.instances[index] = instance;
                        }
                        return instance;
                    }
                }
            }
            set
            {
                this.types[index] = value.GetType();
            }
        }

        public void Add(T item)
        {
            this.types.Add(item.GetType());
        }

        public void Clear()
        {
            this.types.Clear();
            this.instances.Clear();
        }

        public bool Contains(T item)
        {
            return this.types.Contains(item.GetType());
        }

        public bool Contains(Type item)
        {
            return this.types.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { return this.types.Count(); }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            this.instances.Remove(this.types.IndexOf(item.GetType()));
            return this.types.Remove(item.GetType());
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator<T>(this);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
