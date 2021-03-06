﻿using System;
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
        public int MaxBomb { get; set; }

        public ObservableCollection<OneElement> Elements {get;set;}

        public ICommand SaveResult { get; set; }

        public ViewModel()
        {
            SaveResult = new SaveResultCommand();
            Random r = new Random();
            MaxBomb = r.Next(10, 21);
            int count = 0;

            int[,] a=new int [8,8];
            
            for(int i=0; i<MaxBomb+1;i++)
            {
                a[r.Next(0, 8), r.Next(0, 8)] = 1;
            }
            MaxBomb = 0;
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    if (a[i, j] == 1) MaxBomb = MaxBomb + 1;
                    else a[i, j] = 0;
                }

            Elements = new ObservableCollection<OneElement> { };

            OneElement Element;
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    if (a[i, j] == 1) Element = new OneElement ( true,  0, "Image/Picture.jpg");
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

                        Element = new OneElement(false, count, "Image/Picture.jpg");
                    }
                    Elements.Add(Element);

                }       
        }
    }
}
