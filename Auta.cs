namespace Auta
{
    public abstract class Auto
    {
        public readonly string name;
        public float motorVolume;
        public int power;
        public float lenght;
        protected Auto()
        {
            Console.Write("Nazov auta: "); name = Console.ReadLine().Trim();
            motorVolume = GetFloatInRange("Objem motora: ", 0, float.MaxValue, false);
            power = GetIntInRange("Vykon motora: ", 0, int.MaxValue, false);
        }
        public Auto(string nazov, float objemMotora, int vykon, float dlzka)
        {
            name = nazov;
            motorVolume = objemMotora;
            power = vykon;
            lenght = dlzka;
        }
        public static string GetParameters(Auto auto)
        {
            string s =  "1 - objem motora\n" +
                        "2 - vykon\n" +
                        "3 - dlzka";
            if (auto is Osobne)
                s += "\n4 - pocet miest na sedenie";
            else if (auto is Nakladne)
                s += "\n" +
                    "4 - naves\n" +
                    "5 - aktualna naloz\n" + 
                    "6 - maximalna naloz";
            return s;
        }
        public override string ToString()
        {
            return  $"Objem: {motorVolume} [l]\n" +
                    $"Vykon: {power} [W]\n" +
                    $"Dlzka: {lenght} [m]";
        }
        public static void Change(ref Auto auto)
        {
            if      (auto is Nakladne)
            {
                Console.WriteLine(Nakladne.GetParameters(auto));
                auto.ChangeParameter(GetIntInRange("Zmenit udaje: ", 1, 6));
            }
            else if (auto is Osobne)
            {
                Console.WriteLine(Osobne.GetParameters(auto));
                auto.ChangeParameter(GetIntInRange("Zmenit udaje: ", 1, 4));
            }
        }
        protected virtual void ChangeParameter(int parameter)
        {
            switch (parameter)
            {
                case 1: motorVolume = GetFloatInRange("Novy objem: ", 0, float.MaxValue); break;
                case 2: power = GetIntInRange("Novy vykon: ", 0, int.MaxValue); break;
                case 3: lenght = GetFloatInRange("Nova dlzka: ", 0, float.MaxValue); break;
                default: break;
            }
        }
        protected static int GetIntInRange(string mess, int min, int max, bool range = true)
        {
            int ret;
            bool suc;
            do 
            {
                Console.Write(mess);
                if (range) Console.Write($" [{min}-{max}] ");
                suc = int.TryParse(Console.ReadLine(), out ret);
            }
            while (!(suc && min <= ret && ret <= max ));
            return ret;
        }
        protected static float GetFloatInRange(string mess, float min, float max, bool range = true)
        {
            float ret;
            bool suc;
            do 
            {
                Console.Write(mess);
                if (range) Console.Write($" [{min}-{max}] ");
                suc = float.TryParse(Console.ReadLine(), out ret);
            }
            while (!(suc && min <= ret && ret <= max ));
            return ret;
        }
        protected static bool GetBoolInRange(string mess, bool range = true)
        {
            int min = 0, max = 1;
            bool ret = false;
            bool suc;
            do 
            {
                Console.Write(mess);
                if (range) Console.Write($" [{min}-{max}] ");
                switch (Console.ReadLine().Trim())
                {
                    case "0": ret = true;  suc = true; break;
                    case "1": ret = false; suc = true; break;
                    default: suc = false; break;
                }
            }
            while (!suc);
            return ret;
        }
        public static string GetAutoList(ref List<Auto> autos)
        {
            string zoznam = "";
            foreach (Auto auto in autos)
            {
                if      (auto is Nakladne)
                    zoznam += "[Nakladne]  ";
                else if (auto is Osobne)
                    zoznam += "[ Osobne ]  ";
                zoznam += auto.name + "\n";
            }
            return zoznam;
        }
        public static Auto GetAutoInList(ref List<Auto> autos)
        {
            Auto auto = null;
            string nazov = "";
            bool suc = false;
            do 
            {

                Console.Write("Nazov auta: ");
                nazov = Console.ReadLine();
                    foreach (Auto a in autos)
                        if (a.name == nazov)
                        {
                            suc = true;
                            auto = a;
                            break;
                        }
            } while (nazov != "" && !suc);
            return auto;
        }
        public static Auto CreateAuto()
        {
            Auto auto = null;
            Console.WriteLine(" 1 - Nakladne\n 2 - Osobne\n");
            switch (GetIntInRange("Volba: ", 1, 2, true))
            {
                case 1: auto = new Nakladne();  break;
                case 2: auto = new Osobne();    break;
            }
            return auto;
        }
    }
    public class Nakladne : Auto
    {
        public bool trial { get; set; }
        public float load;
        public float maxLoad;
        public Nakladne()
        {
            trial = GetBoolInRange("Ma prives ? ", true);
            maxLoad = GetFloatInRange("Maximalna nosnost: ", 0, float.MaxValue, false);
            load = GetFloatInRange("Nosnost: ", 0, maxLoad);
        }
        public Nakladne(string nazov, float objemMotora, int vykon, float dlzka, bool naves, float maxNaloz) : base(nazov, objemMotora, vykon, dlzka)
        {
            trial = naves;
            maxLoad = maxNaloz;
            load = 0;
        }
        public override string ToString()
        {
            return  $"--- Informacie o nakladnom aute ---\n" +
                    $"{base.ToString()}\n" +
                    $"Ma naves: {trial}\n" +
                    $"Nosnost: {load}/{maxLoad} [kg]";
        }
        protected override void ChangeParameter(int parameter)
        {
            switch (parameter)
            {
                case 1: case 2: case 3: base.ChangeParameter(parameter); break;
                case 4: trial = GetBoolInRange("Ma prives? "); break;
                case 5: load = GetFloatInRange("Ma nalozene", 0, maxLoad); break;
                case 6: maxLoad = GetFloatInRange("Nova maximalna nosnost", 0, 1000000); break;
            }
        }
    }
    public class Osobne : Auto
    {
        public int seats;
        public Osobne()
        {
            seats = GetIntInRange("Pocet sedadiel: ", 0, int.MaxValue, false);
        }
        public Osobne(string nazov, float objemMotora, int vykon, float dlzka, int pocetMiest) : base(nazov, objemMotora, vykon, dlzka)
        {
            seats = pocetMiest;
        }
        public override string ToString()
        {
            return  $"--- Informacie o osobnom aute ---\n" +
                    $"{base.ToString()}\n" +
                    $"Miest na sedenie: {seats}";
        }
    }
}