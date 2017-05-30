using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApplication2
{
    class SaveResultCommand:ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var AllViewModel = parameter as ViewModel;
            int CountBomb = AllViewModel.Elements.Count(x => x.Bomb == true);
            string NewResult;
            DateTime NewDate=DateTime.Now;
            if (AllViewModel.MaxBomb == CountBomb) NewResult = "win";
            else NewResult = "lost";
            NewGameResult.GiveNewResult(NewResult, NewDate);
        }
    }
}
