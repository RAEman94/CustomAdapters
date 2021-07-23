using System;
using FakeRestAPI;

namespace KIAS.CustomAdapters
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            FakeRest.Run();
            Console.ReadLine();
        }
    }
}
