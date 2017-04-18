using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApplication1
{
    public struct WayName
    {
        public string Way;
        public string Name;
        public WayName(string a,string b)
        {
            this.Way = a;
            this.Name = b;
        }
    }
    class ViewModel : INotifyPropertyChanged
    {
        public ICommand ChangePhoto { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void DoPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }  

        public WayName _wayImage;
        public WayName WayImage
        {
            get { return _wayImage; }
            set
            {
                _wayImage = value;
                DoPropertyChanged("WayImage");
            }
        }

        string _CountBomb;
        public string CountBomb
        {
            get { return _CountBomb; }
            set
            {
                _CountBomb = value;
                DoPropertyChanged("CountBomb");
            }
        }

        public ViewModel()
        {
            Random r = new Random();
            _CountBomb = r.Next(5, 15).ToString();
            _wayImage.Way = "Resources/Picture.jpg";
            _wayImage.Name = "0.0";
            ChangePhoto = new ChangePhotoCommand();
        }
    }
}
