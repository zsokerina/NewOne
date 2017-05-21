using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            Random r = new Random();
            int max = r.Next(5, 15);
            
            int count = 0;

            int[,] a = new int[8, 8];
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    if (count != max)
                    {
                        a[i, j] = r.Next(0, 2);
                        count++;
                    }
                    else
                    {
                        a[i, j] = 0;
                        count++;
                    }
                }

            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    if (a[i, j] == 1) Console.WriteLine("1");
                    else
                    {
                        Console.WriteLine("{0},  {1}", i, j);
                        count = -1;
                        if ((i == 0) & (j == 0)) count = a[0, 1] + a[1, 0];
                        else if ((i == 0) & (j != 7)) count = a[i + 1, j] + a[i, j - 1] + a[i, j + 1];

                        if ((i == 7) & (j == 0)) count = a[7, 1] + a[6, 0];
                        else if ((j == 0)&(i!=0)) count = a[i + 1, j] + a[i - 1, j] + a[i, j + 1];

                        if ((i == 7) & (j == 7)) count = a[7, 6] + a[6, 7];
                        else if ((i == 7)&(j!=0)) count = a[i - 1, j] + a[i, j - 1] + a[i, j + 1];

                        if ((i == 0) & (j == 7)) count = a[0, 6] + a[1, 7];
                        else if ((j == 7)&(i!=7)) count = a[i + 1, j] + a[i - 1, j] + a[i, j - 1];

                        if (count == -1) count = a[i + 1, j] + a[i - 1, j] + a[i, j - 1] + a[i, j + 1];

                        Console.WriteLine("All right");
                       
                    }

                }
            Console.Read();
        }
    }
}
