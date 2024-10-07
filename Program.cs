namespace Program
{
    /// <summary>
    /// Toto je main executing program
    /// </summary>
    class Program
    {
        private static Dictionary<KniznyProdukt, int> sklad = new();
        public static void Main()
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
    }

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
}