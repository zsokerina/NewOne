using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace WpfApplication2
{
    class ViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void DoPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public ObservableCollection<OneElement> Elements {get;set;}

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
            int max = r.Next(10, 21);
            int count = 0;

            int[,] a=new int [8,8];
            
            for(int i=0; i<max+1;i++)
            {
                a[r.Next(0, 8), r.Next(0, 8)] = 1;
            }
            max = 0;
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    if (a[i, j] == 1) max = max + 1;
                    else a[i, j] = 0;
                }

            _CountBomb = max.ToString();

            Elements = new ObservableCollection<OneElement> { };

            OneElement Element;
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    if (a[i, j] == 1) Element = new OneElement {Image= "Image/Picture.jpg", Bomb = true, CountBombAround = 0 };
                        else {
                        count = 0;
                        count = -1;
                        if ((i == 0) & (j == 0)) count = a[0, 1] + a[1, 0];
                        else if ((i == 0) & (j != 7)) count = a[i + 1, j] + a[i, j - 1] + a[i, j + 1];

                        if ((i == 7) & (j == 0)) count = a[7, 1] + a[6, 0];
                        else if ((j == 0) & (i != 0)) count = a[i + 1, j] + a[i - 1, j] + a[i, j + 1];

                        if ((i == 7) & (j == 7)) count = a[7, 6] + a[6, 7];
                        else if ((i == 7) & (j != 0)) count = a[i - 1, j] + a[i, j - 1] + a[i, j + 1];

                        if ((i == 0) & (j == 7)) count = a[0, 6] + a[1, 7];
                        else if ((j == 7) & (i != 7)) count = a[i + 1, j] + a[i - 1, j] + a[i, j - 1];

                        if (count == -1) count = a[i + 1, j] + a[i - 1, j] + a[i, j - 1] + a[i, j + 1];

                        Element = new OneElement { Image = "Image/Picture.jpg", Bomb = false, CountBombAround = count };
                    }
                    Elements.Add(Element);

                }       
        }
    }
}
