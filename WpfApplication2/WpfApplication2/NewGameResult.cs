using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication2
{
    class NewGameResult
    {
        public static void GiveNewResult(string result,DateTime date)
        {
            GameResultsEntities ctx = new GameResultsEntities();
            Results _result = new Results() { Date = date, Result = result };
            ctx.Results.Add(_result);
            ctx.SaveChanges();
        }
    }
}
