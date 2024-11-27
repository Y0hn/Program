using System.Security.Cryptography;
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

namespace Program
{
    /// <summary>
    /// Toto je Subor Obsluznych programov
    /// </summary>
    static class ObsluznyProgram
    {
        static string? path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

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
        private static IEnumerable<int> ProduceEvenNumbers(int upto)
        {
            for (int i = 0; i <= upto; i += 2)
            {
                yield return i;
            }
        }
        public static void MainSklady()
		{
			List<Sklad> s = new List<Sklad>();
			string input;
			while (true)
			{
				Console.Clear();
				Console.Write("Vytvor sklad na adrese: ");
				input = Console.ReadLine();
				if (input == "EXIT" || input == "exit")
					break;

				int c = -1;
				string inp;
				do
				{
					Console.Write("Kapacita skadu: ");
					inp = Console.ReadLine();
				}
				while (!int.TryParse(inp, out c) || c <= 0);


				Console.Write("Typ skladu: \n[ p = predsklad / k = koncovy sklad ]\n");
				string typ = Console.ReadLine();

                if (input != null)
				    if (typ.Contains('p'))
				    	s.Add(new Predskladisko(input, c));
				    else if (typ.Contains('k'))
				    	s.Add(new KoncovySklad(input, c));
			}
			while (true)
			{
				Console.Clear();
				Sklad select = null;
				do
				{
					Console.WriteLine("Sklady: ");
					foreach (Sklad skad in s)
						Console.WriteLine("\t" + skad.adresa);
					Console.Write("Praca so skladom na adrese: ");
					input = Console.ReadLine();
					if (input == "exit")
						break;
                    else if (input == null)
                        continue;
					bool b = false;
					foreach (Sklad skad in s)
						if (skad.adresa.Contains(input))
						{
							if (!b)
							{
								select = skad;
								b = true;
							}
							else
							{
								select = null;
								break;
							}
						}
					if (select == null)
						Console.WriteLine("Sklad nebo urceny!");
				}
				while (select == null);
				if (input == "exit")
					break;
				bool p, v;
				do
				{
					Console.Write("Pridat / Vydat tovar [p/v]: ");
					input = Console.ReadLine();
					p = input.Contains('p');
					v = input.Contains('v');
				}
				while (!(p || v) && !(p && v));
				input = "";

				int t = -1;
				string inp;
				do
				{
					if (p)
						Console.Write("Pridat tovar: ");
					else if (v)
						Console.Write("Vyvies tovar: ");
					inp = Console.ReadLine();
				}
				while (!int.TryParse(inp, out t) || t <= 0);

				if (p)
				{
					if (select.PrijemTovaru(t))
						Console.WriteLine("Uspech");
					else Console.WriteLine("Neuspech");
				}
				else
				{
					if (select.VydajTovaru(t))
						Console.WriteLine("Uspech");
					else Console.WriteLine("Neuspech");
				}
				Console.ReadKey();
			}

			foreach (Sklad sklad in s)
				Console.WriteLine(sklad.ToString() + "\n");
			Console.ReadKey();
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
#pragma warning disable CS8604 // Possible null reference argument.
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
                    _ = ex;
                }
                catch (IndexOutOfRangeException ex)
                {
                    Console.WriteLine("Index mimo range ");
                    _ = ex;
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
#pragma warning restore CS8604 // Possible null reference argument.
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
            Subor2Write("Monika, Jano, Peter, Vanada, Tomas");
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
                        string nazov = Console.ReadLine().Trim();
                        Suciastka r = sucs.Find(s => s.nazov == nazov);
                        if (r != null)
                            sucs.Remove(r);
                        break;
                    default:
                        foreach(var s in sucs)
                            Console.WriteLine(s.nazov);
                        break;

                }
            }
        }
        public static void VodiciAutobusov()
        {
            List<Autobus> autobusy = new();
            List<Vodic> vodici = new();
            string zadanie = "Pridaj [1/p]\nVyhladaj [2/v]\nNastav vodica [3/nv]\n\nUkonci program: [99]\n:";
            string submenu = "Autobus [1/a]\nVodica [2/v]\n\nSpat do menu []\n:";
            bool loop = true;

            while (loop)
            {
                Console.Clear();
                Console.Write(zadanie);
                string vstup = Console.ReadLine().Trim().ToLower();
                Autobus a; Vodic v;
                switch (vstup)
                {
                    case "1":
                    case "p":
                        vstup = AutobuSub(submenu);
                        switch (vstup)
                        {
                            case "1":
                            case "a":
                                autobusy.Add(Autobus.GetAutobus());
                                break;
                            case "2":
                            case "v":
                                vodici.Add(Vodic.GetVodic());
                                break;
                            default:
                                break;
                        }
                        break;
                    case "2":
                    case "v":
                        vstup = AutobuSub(submenu);
                        switch (vstup)
                        {
                            case "1":
                            case "a":
                                Console.Write("SPZ: ");
                                vstup = Console.ReadLine().Trim();
                                a = autobusy.Find(a => a.SPZ == vstup);
                                if (a != null)
                                    Console.WriteLine(a);
                                else
                                    Console.WriteLine("Nenajdene");
                                break;
                            case "2":
                            case "v":
                                Console.Write("Meno: ");
                                vstup = Console.ReadLine().Trim();
                                v = vodici.Find(v => v.meno == vstup);
                                if (v != null)
                                    Console.WriteLine(v.Vypis());
                                else
                                    Console.WriteLine("Nenajdene");
                                break;
                            default:
                                break;
                        }
                        Console.ReadKey();
                        break;
                    case "3":
                    case "nv":
                        Console.Write("Meno vodica: ");
                        vstup = Console.ReadLine().Trim();
                        v = vodici.Find(v => v.meno == vstup);
                        if (v != null && !v.obsadeny)
                        {
                            Console.Write("SPZ: ");
                            vstup = Console.ReadLine().Trim();
                            a = autobusy.Find(a => a.SPZ == vstup);
                            if (a != null)
                            {
                                a.SetVodic(v);
                                v.obsadeny = true;
                            }
                            else
                                Console.WriteLine("Nenajdene");
                            break;
                        }
                        else if (v.obsadeny)
                            Console.WriteLine("Vodic uz ma autobus");
                        else
                            Console.WriteLine("Nenajdene");
                        Console.ReadKey();
                        break;
                    case "99":
                        loop = false;
                        break;
                }
            }
        }
        private static string AutobuSub(string subM)
        {
            Console.Write(subM);
            return Console.ReadLine().Trim().ToLower();
        }
        public static void ManazovanieZamestnanov()
        {
            bool loop = true, error = false, wait = true;
            List<ManazovanyZamestnanec> list = new();

            while (loop)
            {
                Console.Clear();
                Console.Write("Co chces robit?\n\n[1] => vypis\n[2] => pridat\n[3] => zmaz\n[99] => ukonci\n:");
                if (error)
                {
                    Console.Write("\bChyba pri predchazajucom vstupe:");
                    error = false;
                }
                ManazovanyZamestnanec mz;
                switch (Console.ReadLine().Trim())
                {
                    case "1": 
                        Console.WriteLine("\nid\tmeno\tmanazer-id"); 
                        foreach (ManazovanyZamestnanec zam in list)
                            Console.WriteLine(zam);
                        break;
                    case "2":
                        Console.Write("\nMeno: "); string mp = Console.ReadLine();
                        Console.Write("Cislo manazera: ");
                        int.TryParse(Console.ReadLine(), out int i);
                        mz = list.Find(z => z.id == i);
                        if (mz == null || mp == null)
                        {
                            Console.WriteLine("Neexituje");
                            break;
                        }
                        list.Add(new(mp, mz));
                        break;
                    case "3":
                        Console.Write("\nID: "); int.TryParse(Console.ReadLine(), out int id);
                        mz = list.Find(z => z.id == id);
                        if (mz == null)
                            Console.WriteLine("Neexituje");
                        else
                        {
                            list.Remove(mz);
                            foreach(ManazovanyZamestnanec zam in list.FindAll(z => z.manazer == mz))
                                zam.manazer = null;
                        }
                        break;
                    case "99": loop = false; wait = false; break;
                    default: error = true; break;
                }

                if (wait)
                    Console.ReadKey();
                else
                    wait = true;
            }
        }
        public static void Zasobnik()
        {
            Stack<string> zasobnik = new Stack<string>();
            zasobnik.Push("Pondelok");
            zasobnik.Push("Utorok");
            zasobnik.Push("Streda");
            zasobnik.Push("Stvrtok");
            zasobnik.Push("Piatok");

            string poslednyZapis = zasobnik.Peek();
            while (0 < zasobnik.Count)
                Console.WriteLine($"Zasobnik ma {zasobnik.Count} poloziek a najneskorsia bola {zasobnik.Pop()}");
            zasobnik.Clear();
        }
        public static void Fronta()
        {
            Queue<string> fronta = new Queue<string>();
            fronta.Enqueue("Pondelok");
            fronta.Enqueue("Utorok");
            fronta.Enqueue("Streda");
            fronta.Enqueue("Stvrtok");
            fronta.Enqueue("Piatok");

            string prvyZapis = fronta.Peek();
            while (0 < fronta.Count)
                Console.WriteLine($"Zasobnik ma {fronta.Count} poloziek a najneskorsia bola {fronta.Dequeue()}");
            fronta.Clear();
        }
    }
}
