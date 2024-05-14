using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Linq;
using System;
using Budovy;
using Ucty;
using Osoby;

namespace Program
{
    class ObsluznyProgram
    {
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
        public static void Banka(string[] args)
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
    }
}
