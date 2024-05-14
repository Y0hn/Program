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
}