using System;

namespace Team_27_Game
{
    class Program
    {
        static void Main(string[] args)
        {
            Server.start(300,8000);
            Console.Title = "Blood lust Server";
            Console.ReadKey();
        }
    }
}
