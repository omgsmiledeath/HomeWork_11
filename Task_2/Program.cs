using System;

namespace Task_2
{
    class Program
    {
        static void Main(string[] args)
        {
            B b = new B();

            b.M();
            (b as I1).M();
            (b as I2).M();
            Console.ReadKey();
        }
    }
}
