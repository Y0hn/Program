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
using Produkty;
using System.Reflection;
using System.Security.Cryptography;

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
        private static Dictionary<KniznyProdukt, int> sklad = new();
        public static void Knihy()
        {
            var xml = """
            <element attr="content">
                <body>
                </body>
            </element>
            """;
            /*
            foreach (int i in  ProduceEvenNumbers(6))
                Console.WriteLine(i);
            */
            Console.WriteLine(xml);
            Console.ReadKey();
        }
        static IEnumerable<int> ProduceEvenNumbers(int upto)
        {
            for (int i = 0; i <= upto; i += 2)
            {
                yield return i;
            }
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
                    DebugLog(ex.Message);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Chyba");
                    DebugLog(ex.Message);
                }
                finally
                {
                    Console.WriteLine("Try again\n");
                }
            }
        }
        private static void DebugLog(string message)
        {
            var time = DateTime.Now;
            string filename = "debug.log";
            if (File.Exists(path + filename))
            {
                using (StreamWriter sw = new StreamWriter(path + filename, true))
                {
                    sw.WriteLine($"[{time}] " + message);
                    sw.Flush();
                }
            }
            else
            {
                Console.Write("Unable to log: ");
                Console.WriteLine($"[{time}] " + message);
                Console.WriteLine("Log file does not exist! ");
            }            
        }
        public static void ReadDebug(string filename = "debug.log")
        {
            using (var sr = new StreamReader(path + filename))
            {
                for (int i = 0; !sr.EndOfStream; i++)
                    Console.WriteLine($"[{(i + 1).ToString().PadLeft(6,'0')}] line: {sr.ReadLine()}");
                sr.Close();
            }
        }
        public static void Subory2 ()
        {
            ObsluznyProgram.Subor2Write("Monika, Jano, Peter, Vanada, Tomas");
            try
            {

                string text = "";
                using (var sr = new StreamReader(path + "subor2.txt"))
                {
                    string[] temp = sr.ReadToEnd().Split('\n');
                    for (int i = 0; i < temp.Length; i++)
                    {
                        string[] temp2 = temp[i].Split(',');
                        for (int j = 0; j < temp2.Length; j++)
                            text += temp2[j].Trim() + "\n";
                    }
                    sr.Close();
                }
                using (var sw = new StreamWriter(path + "subor1.txt"))
                {
                    sw.Write(text);
                    sw.Close();
                }
            }
            catch (FileNotFoundException ex)
            {
                DebugLog("File not found: " + ex.FileName);
            }
        }
        private static void Subor2Write(string w)
        {
            using (var sw = new StreamWriter(path + "subor2.txt"))
            {
                sw.WriteLine(w);
                sw.Close();
            }
        }
        public static void Suciastky()
        {
            Suciastka suc = new Suciastka();
            Console.WriteLine(suc);
        }
        public static void SuciastkyInput()
        {
            List<Suciastka> sucs = new();
            while (true)
            {
                Console.WriteLine("1 - Pridat\n2 - Vyhladat\n3 - Odobrat\nnic - vypis listu");
                switch (Console.ReadLine().Trim())
                {
                    case "1":
                        sucs.Add(Suciastka.GetInput());
                        break;
                    case "2":
                        Console.Write("Nazov suciastky: ");
                        Console.WriteLine(sucs.Find(s => s.nazov == Console.ReadLine().Trim()));
                        break;
                    case "3":
                        Console.Write("Nazov suciastky: ");
                        sucs.Remove(sucs.Find(s => s.nazov == Console.ReadLine().Trim()));
                        break;
                    default:
                        foreach(var s in sucs)
                            Console.WriteLine(s.nazov);
                        break;

                }
            }
        }
    }
}
