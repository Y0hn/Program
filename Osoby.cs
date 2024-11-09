namespace Osoby
{
    public class Osoba
    {
        protected static readonly string def = "not set";
        private static string rodneCislo = "010101/0000";
        protected string RC;
        protected string meno;
        protected string priezvisko;
        protected byte vek;

        static Osoba()
        {
            string[] s = rodneCislo.Split('/');
            byte b = byte.Parse(s[1]);
            b++;
            s[1] = b.ToString();
            rodneCislo = s[0] + "/" + s[1].PadLeft(4, '0');
        }
        public Osoba()
        {
            RC = rodneCislo;
            meno = def;
            priezvisko = def;
            vek = 0;
        }
        public Osoba(string name, bool male, byte age)
        {
            RC = rodneCislo;
            string[] s = name.Split(' ');
            meno = s[0];
            priezvisko = s[1];
            if (!male)
            {
                string[] t = RC.Split('/');
                t[0] = (int.Parse(t[0]) + 5000).ToString().PadLeft(6, '0');
                RC = t[0] + "/" + t[1];
            }        
            vek = age;
        }
        protected string Pohlavie()
        {
            string[] t = RC.Split('/');
            byte b = byte.Parse(t[0].Substring(2,2));
            if (b > 5)
                return "zena";
            else
                return "muz";
        }
        public override string ToString()
        {
            string s = "_____";
            s = s + "/" + meno + " | " + priezvisko + "\\" + s;
            s += "\nRodne cislo: " + RC;
            s += "\nPohlavie: " + Pohlavie();
            s += "\nVek: " + vek;
            return s;
        }
    }
    class Student : Osoba
    {
        public string studijnyOdbor { get; }
        public byte rocnik { get; }
        public string temaZP { get; }
        /*public string student
        {
            get
               meno + ' ' + priezvisko; 
            set
                string[] s = value.Split(' ');
                if (s.Lenght > 1)
                    priezvisko = s[1];
                meno = s[0];
        }*/

        public Student(string name, bool male, byte age, byte rocnik, string studOdbr = "", string temaZP = "") : base(name, male, age)
        {
            this.rocnik = rocnik;

            if (studOdbr != "")
                studijnyOdbor = studOdbr;
            else
                studijnyOdbor = def;
            if (temaZP != "")
                this.temaZP = temaZP;
            else
                this.temaZP = def;
        }
        public override string ToString()
        {
            string s = base.ToString();
            s += "\nRocnik: " + rocnik;
            s += "\nOdbor: " + studijnyOdbor;
            s += "\nTema zaverecnej prace: " + temaZP;
            return s;
        }
    }

    public interface IKmenovyZamestnanec
    {
        string zariadenie { get; }
        void Bonus(int navysenie);
    }
    public interface IZamestnanec
    {
        string celeMeno { get; }
        int plat { get; set; }
    }
    public interface IKontaktZamestnanec
    {
        int rokZahajenia { get; }
        int MesacnyBonus();
    }
    public class Zamestnanec : IKmenovyZamestnanec, IZamestnanec
    {
        public Zamestnanec(string meno, string zar, int plat)
        {
            celeMeno = meno;
            zariadenie = zar;
            this.plat = plat;
        }
        public string zariadenie { get; set; }
        public string celeMeno { get; set; }
        public int plat { get; set; }
        public void Bonus(int nav)
        {
            plat += nav;
        }
    }
    public class ZamestnanecKontakt : IZamestnanec, IKontaktZamestnanec
    {
        public ZamestnanecKontakt(string zariadenie, int plat, string celeMeno, int rokZahajenia)
        {
            this.zariadenie = zariadenie;
            this.plat = plat;
            this.celeMeno = celeMeno;
            this.rokZahajenia = rokZahajenia;
        }
        public string zariadenie { get; private set; }
        public int plat { get; set; }
        public string celeMeno { get; set; }
        public int rokZahajenia { get; set; }
        public int MesacnyBonus()
        {
            return plat + rokZahajenia;
        }
    }
    public class Vodic
    {
        public string meno { get; }
        private int rokNarodenia;
        private bool bezNehody;
        public bool obsadeny;

        public Vodic(string name, int rN, bool bN = true, bool ob = false)
        {
            meno = name;
            rokNarodenia = rN;
            bezNehody = bN;
            obsadeny = ob;
        }
        public static Vodic GetVodic(bool simple = true)
        {
            Vodic v = null;
            Console.Write("Meno: ");
            string name = Console.ReadLine().Trim();
            Console.Write("Rok narodenia: ");
            int rN = int.Parse(Console.ReadLine().Trim());

            if (simple)
            {
                Console.WriteLine("Nebural");
                Console.WriteLine("Nema prideleny autobus");
                v = new Vodic (name, rN);
            }

            return v;            
        }
        public string Vypis()
        {
            return 
            $"===< {meno} >===\n" +
            $"rok narodenia: {rokNarodenia}\n" +
            $"nehodobval: {!bezNehody}\n" +
            $"ma prideleny autobus: {obsadeny}";
        }
        public override string ToString()
        {
            return meno;
        }
    }
    public class ManazovanyZamestnanec
    {
        public int id { get; }
        public string meno;
        public string priezvisko;
        public ManazovanyZamestnanec manazer;

        private static int count;
        static ManazovanyZamestnanec()
        {
            count = 1;
        }
        public ManazovanyZamestnanec(string name, ManazovanyZamestnanec mz = null)
        {
            this.id = count;
            string[] n = name.Split(' ');
            this.meno = n[0];
            if (n.Length > 1)
                this.priezvisko = n[1];
            else
                this.priezvisko = "";
            this.manazer = mz;
            count++;
        }
        public override string ToString()
        {
            string m;
            if (manazer == null)
                m = "null";
            else
                m = manazer.id.ToString();
            return $"{id}\t{meno} {priezvisko}\t{m}";
        }
    }
}