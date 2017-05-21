using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApplication2
{
    class OneElement : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void DoPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        public Boolean Bomb { get; set; }
        public int CountBombAround { get; set; }
        string _Image;
        public string Image
        {
            get { return _Image; }
            set
            {
                _Image = value;
                DoPropertyChanged("Image");
            }
        }

        public ICommand ChangeImage { get; set; }
        public OneElement()
        {
            ChangeImage = new ChangeImageCommand();

        }
    }
}
