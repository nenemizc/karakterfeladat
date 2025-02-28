﻿namespace ConsoleApp3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Karakter> karakterek = [];

            Beolvas("karakterek.txt", karakterek);

            (string, int, int) skibidi = LegmagasabbElet(karakterek);
            Console.WriteLine($"Legmagasabb életű karakter neve: {skibidi.Item1}, szintje: {skibidi.Item2}, ereje: {skibidi.Item3}"); Console.WriteLine();

            SzintAvg(karakterek);

            EroRanking(karakterek);

            Console.WriteLine($"{karakterek[0].Nev} erősebb-e, mint 40: {ErosE(karakterek[0],40)}");

            Console.WriteLine($"8-nál nagyobb szintű karakterek:");
            foreach(Karakter k in SzintFilter(karakterek, 8))
            {
                Console.WriteLine($"{k.Nev} - {k.Szint}");
            }
        }

        static void Beolvas(string file, List<Karakter> karakterek)
        {
            StreamReader sr = new(file);
            sr.ReadLine();
            foreach (string line in sr.ReadToEnd().Split("\n"))
            {
                string[] adat = line.Split(';');
                karakterek.Add(new Karakter(adat[0],Convert.ToInt32(adat[1]), Convert.ToInt32(adat[2]), Convert.ToInt32(adat[3])));
            }
        }

        static (string, int, int) LegmagasabbElet(List<Karakter> karakterek) 
        {
            Karakter k = karakterek[0];

            foreach (Karakter ka in karakterek) 
            {
                if (ka.EletEro > k.EletEro) 
                {
                    k = ka;
                }
            }

            return (k.Nev, k.Szint, k.EletEro);
        }

        static void SzintAvg(List<Karakter> karakterek)
        {
            int ossz = 0;
            foreach(Karakter k in karakterek)
            {
                ossz += k.Szint;
            }
            Console.WriteLine($"Összes szint átlaga: {ossz/karakterek.Count}");
        }

        static void EroRanking(List<Karakter> karakterek)
        {
            List<Karakter> sorrend = karakterek.OrderBy(k => k.Ero).ToList();
            sorrend.Reverse();
            Console.WriteLine("\nKarakterek sorrendje erő szerint:");
            for (int i = 0; i < sorrend.Count; i++)
            {
                Console.WriteLine($"{i+1}. {sorrend[i]}\n");
            }
        }

        static bool ErosE(Karakter k,int ero) 
        {
            return (k.Ero > ero);
        }

        static List<Karakter> SzintFilter(List<Karakter> karakterek, int szint)
        {
            List<Karakter> jok = [];
            foreach(Karakter k in karakterek)
            {
                if (k.Szint > szint)
                {
                    jok.Add(k);
                }
            }
            return jok;
        }
    }
}
