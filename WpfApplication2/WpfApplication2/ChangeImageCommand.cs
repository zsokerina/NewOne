using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApplication2
{
    class ChangeImageCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var Picture= (OneElement)parameter;
            if (Picture.Bomb == true)
            {
                Picture.Image = "Image/Bomb.jpg";
                Picture.Bomb = false;
            }
            else if (Picture.CountBombAround == 1) Picture.Image = "Image/1.jpg";
            else if (Picture.CountBombAround == 2) Picture.Image = "Image/2.jpg";
            else if (Picture.CountBombAround == 3) Picture.Image = "Image/3.jpg";
            else if (Picture.CountBombAround == 4) Picture.Image = "Image/4.jpg";
            else Picture.Image = "Image/Background.jpg";
        }
    }
}
