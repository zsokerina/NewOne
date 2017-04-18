using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Reflection;

namespace WpfApplication1
{
    class ChangePhotoCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var Image = (WayName)parameter;
            Image.Way = "Resources/Sea.jpg";
            parameter = new WayName(Image.Way,Image.Name);
        }
    }
}
