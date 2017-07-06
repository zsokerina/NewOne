using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApplication3
{
    class CustomerViewModel: INotifyPropertyChanged
    {
        public int CustomerId { get; set; }

        public string name;
        public string Name
        { get { return name; }
            set
            {
                name = value;
                DoPropertyChanged("Name");
            }
        }

        public ICommand Command { set; get; }

        public CustomerViewModel()
        {
            Command = new NewCommand();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void DoPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
