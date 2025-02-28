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
    }
}
