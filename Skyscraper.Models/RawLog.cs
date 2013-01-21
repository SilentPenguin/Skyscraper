using System.Collections.ObjectModel;
using Skyscraper.Utilities;

namespace Skyscraper.Models
{
    public class RawLog : NotifyPropertyChangedBase, IRawLog
    {
        private ObservableCollection<RawMessage> log;
        public ObservableCollection<RawMessage> Log
        {
            get
            {
                return this.log;
            }
            set
            {
                this.SetProperty(ref this.log, value);
            }
        }

        public RawLog()
        {
            this.log = new ObservableCollection<RawMessage>();
        }
    }
}