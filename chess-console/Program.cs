using board;

namespace chess_console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Position P = new (3, 4);
            Console.WriteLine("Position: " + P);
            Console.ReadLine();
        }
    }
}