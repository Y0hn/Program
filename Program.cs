using Auta;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            string chooses = " 1 => Vloz auto\n 2 => Odstran auto\n 3 => Zoznam aut\n 4 => Info o aute \n 5 => koniec";
            List<Auto> automobils = new();
            bool doing = true;
            Console.Clear();

            // Osobne(string nazov, float objemMotora, int vykon, float dlzka, int pocetMiest)
            automobils.Add(new Osobne("Mercedes", 20f, 500, 5.3f, 4));
            // Nakladne(string nazov, float objemMotora, int vykon, float dlzka, bool naves, float maxNaloz)
            automobils.Add(new Nakladne("Iveco", 30f, 2000, 10.7f, true, 2500f));
            while (doing)
            {
                Console.WriteLine("Evidencia aut, zadaj volbu:\n");
                Console.WriteLine(chooses);
                
                switch (GetIntInRange("Volba: ", 1, 5))
                {
                    case 1: break;
                    case 2: automobils.Remove(Auto.GetAutoInList(ref automobils)); break;
                    case 5: doing = false; break;
                }
            }
            Console.ReadKey();
            Console.Clear();
        }
        private static int GetIntInRange(string mess, int min, int max)
        {
            int ret;
            bool suc;
            do 
            {
                Console.Write(mess + $"[{min}-{max}] ");
                suc = int.TryParse(Console.ReadLine(), out ret);
            }
            while (!(suc && min <= ret && ret <= max ));
            return ret;
        }
    }
}