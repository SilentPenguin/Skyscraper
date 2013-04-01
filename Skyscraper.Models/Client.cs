using Skyscraper.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscraper.Models
{
    public class Client : NotifyPropertyChangedBase, IClient
    {
        public ObservableCollection<INetwork> networks;
        public ObservableCollection<INetwork> Networks
        {
            get
            {
                return this.networks;
            }
            set
            {
                this.SetProperty(ref this.networks, value);
            }
        }

        private ObservableCollection<ILogEntry> log;
        public ObservableCollection<ILogEntry> Log
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

        public Client()
        {
            this.Networks = new ObservableCollection<INetwork>();
            this.Log = new ObservableCollection<ILogEntry>();
        }
    }
}
