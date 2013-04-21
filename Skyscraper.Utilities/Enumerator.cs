using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscraper.Utilities
{
    public class Enumerator<T> : IEnumerator<T>
    {
        public IList<T> List;
        public int CurrentIndex;

        public Enumerator(IList<T> List)
        {
            this.List = List;
            this.Reset();
        }

        public T Current
        {
            get
            {
                try
                {
                    return this.List[CurrentIndex];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        object System.Collections.IEnumerator.Current
        {
            get { return this.Current; }
        }

        public void Dispose()
        {

        }

        public bool MoveNext()
        {
            return (++this.CurrentIndex < this.List.Count());
        }

        public void Reset()
        {
            this.CurrentIndex = -1;
        }
    }
}
