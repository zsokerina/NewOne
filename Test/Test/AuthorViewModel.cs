using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class AuthorViewModel:ViewModelBase
    {
        private string firstName;
        private string secondName;
        private string fatherName;

        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
                RaisePropertyChanged("FirstName");
            }
        }

        public string SecondName
        {
            get
            {
                return secondName;
            }
            set
            {
                secondName = value;
                RaisePropertyChanged("SecondName");
            }
        }

        public string FatherName
        {
            get
            {
                return fatherName;
            }
            set
            {
                fatherName = value;
                RaisePropertyChanged("FatherName");
            }
        }
    }
}
