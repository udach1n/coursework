using System;

namespace HW1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Task1");

            Console.WriteLine("Write int variable");
            int a = Convert.ToInt32(Console.ReadLine());

            int perimetr = 4 * a;
            int area = a * a;
            Console.WriteLine("Perimetr of square {0}, area of square - {1}", perimetr, area);


            Console.WriteLine("Task2");

            string name;
            int age;
            Console.WriteLine("What is your name?");
            name = Console.ReadLine();
            Console.WriteLine($"How old are you,{name}");
            age = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"My name is {name}, I`m {age} years old");


            Console.WriteLine("Task3");

            Console.WriteLine("Write radius of a circle");
            double r = Convert.ToDouble(Console.ReadLine());
            double l = 2 * Math.PI * r;
            double s = Math.PI * r * r;
            double v = 4.0 / 3.0 * Math.PI * r * r * r;
            Console.WriteLine("length of a circle = {0:F3},area = {1:F3}, volume = {2:F3}",l,s,v);
        
        }


    }
}
