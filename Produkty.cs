using Program;

namespace Produkty
{
    public abstract class KniznyProdukt : IEquatable<KniznyProdukt>
    {
        public required abstract string nazov { get; set; }
        public required abstract string vydavatel { get; set; }
        public required abstract int rokVydania { get; set; }

        public abstract bool PridajDoSkladu(int pKusov);
        public abstract bool OdoberZOSkladu(int pKusov);

        public virtual bool Equals(KniznyProdukt? other)
        {
            // implement inokedy
            return false;
        }
        public KniznyProdukt(string n, string v, int r)
        {
            nazov = n;
            vydavatel = v;
            rokVydania = r;
        }
    }
    public class Kniha : KniznyProdukt
    {
        public int ISBN { get; }
        public int pocetStran { get; set; }
        public Kniha(string n, string v, int r, int i, int p) : base(n, v, r)
        {
            pocetStran = i;
            pocetStran = p;
        }

        public required override string nazov { get; set; }
        public required override string vydavatel { get; set; }
        public required override int rokVydania { get; set; }

        public override bool OdoberZOSkladu(int pKusov)
        { return true; }
        public override bool PridajDoSkladu(int pKusov)
        { return true; }
    }

    public class Suciastka : IProdukt, IMaterial, IKalkulaica
    {
        public string nazov     { get; set; }
        public string material  { get; set; }
        public double hmotnost  { get; set; }
        public double vyska     { get; set; }
        public double sirka     { get; set; }

        public string oznacenie     { get; set; }
        public double kvalita       { get; set; }

        public int cislo_uctovanej_polozky  { get; set; }
        public double cena                  { get; set; }

        public Suciastka()
        {
            nazov = "Skrutka";
            material = "Zelezo";
            hmotnost = 0.05; //kg
            vyska = 0.05;
            sirka = 0.002;

            oznacenie = "Fe";
            kvalita = 0.9;

            cislo_uctovanej_polozky = 1234;
            cena = 0.2;
        }
        public Suciastka(string nazov, string material, double hmotnost, double x, double y,
                        string oznacenie, double kvalita, int cup, double cena)
        {
            this.nazov = nazov;
            this.material = material;
            this.hmotnost = hmotnost; //kg
            this.sirka = x;
            this.vyska = y;

            this.oznacenie = oznacenie;
            this.kvalita = kvalita;
            
            this.cislo_uctovanej_polozky = cup;
            this.cena = cena;
        }
        public override string ToString()
        {
            string r = $"---< {nazov} >---";
            r += $"\nmaterial: [{oznacenie}] => " + material;
            r += $"\nkvalita materialu: {kvalita}";
            r += "\nhmotnost: " + hmotnost;
            r += $"\nrozmery: [{sirka},{vyska}]";
            r += $"\ncena: {cena} $";

            return r;
        }
        public static Suciastka GetInput()
        {
            Console.Write("Nazov suciastky: ");
            string s = Console.ReadLine().Trim();
            Console.Write("Material (nazov,znacka): ");
            string[] t = Console.ReadLine().Split(',');
            for (int i = 0; i < t.Length; i++)
                t[i] = t[i].Trim();
            Console.Write("Kvalita materialu (0-1): ");
            double q = double.Parse(Console.ReadLine());
            Console.Write("Hmotnost suciasky (kg): ");
            double m = double.Parse(Console.ReadLine());
            Console.Write("Rozmery [x,y]: ");
            string[] xys = Console.ReadLine().Split(',');
            double[] xy = new double[xys.Length];
            for (int i = 0; i < xys.Length; i++)
                xy[i] = double.Parse(xys[i]);
            Console.Write("Cislo polozky: ");
            int cp = int.Parse(Console.ReadLine());
            Console.Write("Cena: ");
            double cena = double.Parse(Console.ReadLine());

            return new Suciastka(s, t[0], m, xy[0], xy[1], t[1], q, cp, cena);
        }
    }
}