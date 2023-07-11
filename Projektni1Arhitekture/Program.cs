using System;

namespace Projektni1Arhitekture
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ParserLinija pl = new ParserLinija();
            int opcija = 0;
            int izbor = 3;
            while (opcija != 1 && opcija != 2)
            {
                Console.WriteLine("Odaberite opciju:");
                Console.WriteLine("Pisanje skripte: 1");
                Console.WriteLine("Citanje iz skripte: 2");
                Console.Write("-> "); int.TryParse(Console.ReadLine(), out opcija);//int tmp = Console.Read();//int.TryParse(tmp, out opcija);//opcija = tmp;
                Console.Clear();
            }
            if (opcija == 1)//ako je pisanje skripte izabrano
            {
                string filename = pl.Write();
                while (izbor != 0 && izbor != 1)
                {
                    Console.WriteLine("Da li zelite Single Step Debugging? (0 = NE ili 1 = DA)");
                    Console.Write("-> "); int.TryParse(Console.ReadLine(), out izbor);
                }
                try
                {
                    if (izbor == 0)//ako je bez single step poslije pisanja
                    {
                        pl.ReadBezSingleStep(filename);
                    }
                    if (izbor == 1)
                    {
                        pl.ReadSaSingleStep(filename);
                    }
                }
                catch (Exception e) { Console.WriteLine(e.Message); }
            }
            if (opcija == 2) //ako je izabrano citanje iz file
            {
                Console.WriteLine("Unesite naziv skripte koju zelite da otvorite (bez ekstenzije):");
                string filename = Console.ReadLine(); filename = filename + ".txt";
                while (izbor != 0 && izbor != 1)
                {
                    Console.WriteLine("Da li zelite Single Step Debugging? (0 = NE ili 1 = DA)");
                    Console.Write("-> "); int.TryParse(Console.ReadLine(), out izbor);
                    Console.Clear();
                }

                if (izbor == 0)
                {
                    pl.ReadBezSingleStep(filename);
                }
                if (izbor == 1)
                {
                    pl.ReadSaSingleStep(filename);
                }
            }

            Console.ReadLine();
        }
    }
}