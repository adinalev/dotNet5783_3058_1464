using System;
namespace Targil0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome1464();
            Welcome3058();
            Console.ReadKey();
        }

        static partial void Welcome3058();
        private static void Welcome1464()
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", name);
        }
    }
}
