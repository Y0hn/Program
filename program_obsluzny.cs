using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Linq;
using System.IO;
using System;
using Budovy;
using Osoby;
using Ucty;
using Auta;
using System.Reflection;

namespace Program
{
    class ObsluznyProgram
    {        
        const string path = @"C:/Users/janik/Documents/GitHub/Program/";
        public static void Auta()
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
                Console.WriteLine(chooses + "\n");
                
                switch (GetIntInRange("Volba ", 1, 5))
                {
                    case 1: automobils.Add(Auto.CreateAuto()); break;
                    case 2: automobils.Remove(Auto.GetAutoInList(ref automobils)); break;
                    case 3: Console.WriteLine(Auto.GetAutoList(ref automobils)); break;
                    case 4: Console.WriteLine(Auto.GetAutoInList(ref automobils)); break; // info
                    case 5: doing = false; break;
                }
            }
            Console.ReadKey();
            Console.Clear();
        }
        public static void Zamestnanec()
        {
            IZamestnanec[] emploies = new IZamestnanec[2];
            Zamestnanec z = new Zamestnanec("Meno", "skola", 1234);
            ZamestnanecKontakt k = new ZamestnanecKontakt("aloks", 4321, "Onem", 1979);

            emploies[0] = z;
            emploies[1] = k;

            Console.WriteLine("Z: " + emploies[0].celeMeno);
            try
            { ((IKmenovyZamestnanec)emploies[0]).Bonus(1); Console.WriteLine(emploies[0].plat); }
            catch
            { Console.WriteLine("Unable to bonus"); }
            try
            { Console.WriteLine("MesBon: " + ((IKontaktZamestnanec)emploies[0]).MesacnyBonus()); }
            catch
            { Console.WriteLine("Unable to mesacny bonus"); }

            Console.WriteLine();

            Console.WriteLine("K: " + emploies[1].celeMeno);
            try
            { ((IKmenovyZamestnanec)emploies[1]).Bonus(1); Console.WriteLine(emploies[0].plat); }
            catch
            { Console.WriteLine("Unable to bonus"); }
            try
            { Console.WriteLine("MesBon: " + ((IKontaktZamestnanec)emploies[1]).MesacnyBonus()); }
            catch
            { Console.WriteLine("Unable to mesacny bonus"); }

            Console.ReadKey();
        }
        public static void Banka()
        {
            int suma = 0;
            int[] pole = { 500, 200, 100, 50, 20, 10, 5 };

            do
                Console.Write("Zadaj sumu v Eurach [kladne cele cislo]: ");
            while (!int.TryParse(Console.ReadLine(), out suma) || suma < 1);

            /*
            int hodnota = 0;
            do
                Console.Write("Zadaj hodnotu bankovky: ");
            while (!int.TryParse(Console.ReadLine(), out hodnota) || hodnota < 1);          
            Console.WriteLine("Bankovka hodnoty [{0}] sa nachadza v sume [{1}] {2}x", hodnota, suma, suma / hodnota);
            
            foreach (int i in pole) 
                Console.Write("\nBankovka hodnoty [{0}]\t sa nachadza v sume [{1}]\t {2}x", i, suma, suma / i);
            
            for (int i = 0; i < pole.Length; i++)
                Console.Write("\nBankovka hodnoty [{0}]\t sa nachadza v sume [{1}]\t {2}x", pole[i], suma, suma / pole[i]);
            */
            Console.Write(Bank(suma, pole));

            Console.ReadKey();
        }
        private static string Bank(int suma, int[] pole)
        {
            string ret = "";
            for (int i = 0, nova_suma = suma; nova_suma >= pole.Last(); i++)
            {
                if (nova_suma / pole[i] > 0)
                {
                    ret += "\nBankovka hodnoty [" + pole[i] + "]\t sa nachadza v sume [" + suma + "]\t " + nova_suma / pole[i] + "x";
                    nova_suma %= pole[i];
                }
            }
            return ret;
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
        public static void Vynimky()
        {
            int[] pole = new int[10];
            for (int i = 0; i < 10; i++)
                pole[i] = 10 - i;

            for (; ; )
            {
                try
                {
                    int i = int.Parse(Console.ReadLine());
                    Console.WriteLine($"Pole[{i}] = {pole[i]}");
                }
                catch (FormatException ex)
                { 
                    Console.WriteLine("Nespravny format ");
                }
                catch (IndexOutOfRangeException ex)
                {
                    Console.WriteLine("Index mimo range ");
                }
                finally
                {
                    Console.WriteLine("Try again\n");
                }
            }
        }
        public static void Subory()
        {
            int a, b;
            DateTime time;
            for (;;)
            {
                try
                {
                    Console.Write("Zadaj cislo: ");
                    a = int.Parse(Console.ReadLine());
                    b = 10/a;
                }
                catch (DivideByZeroException ex)
                {
                    Console.WriteLine("Chyba");
                    time = DateTime.Now;
                    DebugLog($"[{time}] {ex.Message}");
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Chyba");
                    time = DateTime.Now;
                    DebugLog($"[{time}] {ex.Message}");
                }
                finally
                {
                    Console.WriteLine("Try again\n");
                }
            }
        }
        private static void DebugLog(string message)
        {
            string filename = "subor.txt";
            if (File.Exists(path + filename))
            {
                using (StreamWriter sw = new StreamWriter(path + filename, true))
                {
                    sw.WriteLine(message);
                    sw.Flush();
                }
            }
            else
            {
                Console.Write("Unable to log: ");
                Console.WriteLine(message);
                Console.WriteLine("Log file does not exist! ");
            }            
        }
    }
}
