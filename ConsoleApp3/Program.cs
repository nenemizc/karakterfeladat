namespace ConsoleApp3
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

            CsV(karakterek);

            Top3(karakterek);

            Rangsor(karakterek);

            Harc(karakterek[3], karakterek[4]);
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

        static void CsV(List<Karakter> karakterek)
        {
            string szoveg = "Név;Szint;Életerő;Erő\n";
            foreach(Karakter k in karakterek)
            {
                szoveg += k.Nev + ";" + k.Szint + ";" + k.EletEro + k.Ero +"\n";
            }

            File.WriteAllText("karakterek.csv", szoveg);
        }

        static void Top3(List<Karakter> karakterek)
        {
            List<int[]> osszEro = [];

            foreach (Karakter k in karakterek)
            {
                osszEro.Add([k.Szint +  k.Ero, karakterek.IndexOf(k)]);
            }

            List<int[]> sorrend = osszEro.OrderBy(a => a[0]).ToList();
            sorrend.Reverse();
            Console.WriteLine("Top 3 legerősebb karakter:\n");
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"Név: {karakterek[sorrend[i][1]].Nev}, szintje: {karakterek[sorrend[i][1]].Szint}, ereje: {karakterek[sorrend[i][1]].Ero}, szint+erő: {sorrend[i][0]}");
            }
        }

        static void Rangsor(List<Karakter> karakterek)
        {
            List<int[]> osszEro = [];

            foreach (Karakter k in karakterek)
            {
                osszEro.Add([k.EletEro + k.Ero, karakterek.IndexOf(k)]);
            }
            List<int[]> sorrend = osszEro.OrderBy(a => a[0]).ToList();
            sorrend.Reverse();
            Console.WriteLine("Ranglista:\n");
            for (int i = 0;i < sorrend.Count; i++)
            {
                Console.WriteLine($"Név: {karakterek[sorrend[i][1]].Nev}, életereje: {karakterek[sorrend[i][1]].EletEro}, ereje: {karakterek[sorrend[i][1]].Ero}, szint+erő: {sorrend[i][0]}");
            }
        }

        static void Harc(Karakter k1, Karakter k2) 
        {
            Console.WriteLine($"{k1.Nev} és {k2.Nev} elkezdek harcolni");
            if (k1.Szint + k1.Ero > k2.Szint + k2.Ero)
            {
                Console.WriteLine($"{k1.Nev} győzött.");
            }
            else if (k2.Szint + k2.Ero > k1.Szint + k1.Ero)
            {
                Console.WriteLine($"{k2.Nev} győzött.");
            }
            else 
            {
                Console.WriteLine("Döntetlen");
            }
        }
    }
}
