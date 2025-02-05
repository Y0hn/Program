namespace Budovy
{
    public class Panelak
    {
        Byt[,] byts;
        public Panelak(int[] poctyIzieb, float[] plochyBytov, int pocetPoschodi)
        {
            byts = new Byt[poctyIzieb.Length, pocetPoschodi];
            for (int i = 0; i <  poctyIzieb.Length; i++) 
            {
                for (int j = 0; j < plochyBytov.Length; j++) 
                {
                    byts[j, i] = new Byt(poctyIzieb[j], plochyBytov[j]);
                }
            }
        }
        public string GetByt(int cislo, int poschodie)
        {
            string s;

            if (0 <= cislo && cislo < byts.GetLength(0) && 0 <= poschodie && poschodie < byts.GetLength(1))
                s = $"Byt cislo {cislo+1} je na {poschodie+1}. poschodi\n" +
                        $"Obsahuje {byts[cislo, poschodie].pocetIzieb} izieb\n" +
                        $"Celkova plocha bytu je {byts[cislo, poschodie].plochaBytu} m2\n" +
                        $"Priemerna velkost miestnosti je {byts[cislo, poschodie].PriemernaPlochaIzby} m2";
            else
                s = "Nespravne cislo izby alebo poschodie";

            return s;
        }
        private class Byt
        {
            public int pocetIzieb;
            public float plochaBytu;
            public float PriemernaPlochaIzby
            {
                get
                {
                    return plochaBytu / pocetIzieb;
                }
            }
            public Byt(int pocetIzieb, float plochaBytu)
            {
                this.pocetIzieb = pocetIzieb;
                this.plochaBytu = plochaBytu;
            }
        }
    }
    abstract class Sklad
    {
		public readonly string adresa;
        public bool vPrevadzke { get; set; }
        public readonly int cap;
        protected int pocetTovaru;
        public Sklad(string add, int capacity)
        {
            vPrevadzke = true;
            pocetTovaru = 0;
            cap = capacity;
            adresa = add;
        }
        public virtual bool PrijemTovaru(int tovar)
        {
            if (pocetTovaru + tovar <= cap)
            {
                pocetTovaru += tovar;
                return true;
            }
            else
                return false;
        }
        public virtual bool VydajTovaru(int tovar)
        {
            if (pocetTovaru - tovar >= 0)
            {
                pocetTovaru -= tovar;
                return true;
            }
            else
                return false;
        }
        public override string ToString()
        {
            string s = adresa + "\n";
            if (vPrevadzke)
                s += "je v prevadzke\n";
            else
                s += "mimo prevadzky\n";
            s += $"Tovar: {pocetTovaru}/{cap}";

            return s;
        }
    }
    class Predskladisko : Sklad 
    {
        public Predskladisko(string add, int cap) : base("predsklad_" + add, cap)
        {
        }
        public override bool PrijemTovaru(int tovar)
        {
            if (vPrevadzke)
                return base.PrijemTovaru(tovar);
            else
                return false;
        }
        public override bool VydajTovaru(int tovar)
        {
            if (vPrevadzke)
                return base.VydajTovaru(tovar);
            else
                return false;
        }
    }
    class KoncovySklad : Sklad
    {
        public KoncovySklad(string add, int cap) : base("koncovy_" + add, cap)
        {
        }
        public override bool VydajTovaru(int tovar)
        {
            return false;
        }
    }


    public class TPozicovna
    {
        string nazov;
        bool vPrevadzke;
        List<Auta.TVozidlo> auta;

        public TPozicovna(string n, List<Auta.TVozidlo> a, bool vp)
        {
            nazov = n;
            auta = a;
            vPrevadzke = vp;
        }
    }
}
