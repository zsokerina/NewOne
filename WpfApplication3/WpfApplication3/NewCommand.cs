using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApplication3
{
    class NewCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var a = parameter as string;
            using (DataBaseModel ctx = new DataBaseModel())
            {
                Customer NCustomer = new Customer() { Name = a };
                ctx.Customers.Add(NCustomer);
                ctx.SaveChanges();
            }
        }
    }
}
