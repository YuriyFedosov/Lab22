using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1
{
    class Program
    {

        /*1.    Сформировать массив случайных целых чисел (размер  задается пользователем). 
         * Вычислить сумму чисел массива и максимальное число в массиве.  
         * Реализовать  решение  задачи  с  использованием  механизма  задач продолжения.*/

        static int[] arrayValues;
        static void Main(string[] args)
        {
            Console.Write("Введите размерность массива случайных чисел: ");
            int n = Convert.ToInt32(Console.ReadLine());
            Random random = new Random();
            arrayValues = new int[n];
            for (int i = 0; i < arrayValues.Length; i++)
            {
                arrayValues[i] = random.Next(0, 1000);
            }
            Console.WriteLine("Массив случайных чисел: ");
            for (int i = 0; i < arrayValues.Length; i++)
            {
                Console.Write($"{arrayValues[i]} ");
            }
            Console.WriteLine("\n");

            Func<int> func = new Func<int>(Summa);
            Task<int> task1 = new Task<int>(func);
            Func<Task, int> funcTask = new Func<Task, int>(MaxValue);
            Task<int> task2 = task1.ContinueWith(funcTask);
            task1.Start();

            Console.Write("Сумма результатов: ");
            Console.WriteLine(task1.Result);
            Console.Write("Максимальное значение: ");
            Console.WriteLine(task2.Result);
            Console.ReadKey();
        }

        
        static int Summa()
        {
            int v = 0;
        
            for (int i = 0; i < arrayValues.Length; i++)
            {
                v += arrayValues[i];
            }
            return v;
        }

        static int MaxValue(Task t)
        {
            int maxNum = 0;
            for (int i = 0; i < arrayValues.Length; i++)
            {
                if (maxNum < arrayValues[i])
                    maxNum = arrayValues[i];
            }
            return maxNum;
        }


    }
}
