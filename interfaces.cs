namespace Program
{
    public interface IProdukt
    {
        public string nazov     { get; set; }
        public string material  { get; set; }
        public double hmotnost  { get; set; }
        public double vyska     { get; set; }
        public double sirka     { get; set; }
    }
    public interface IMaterial
    {
        public string oznacenie     { get; set; }
        public double kvalita       { get; set; }
    }
    public interface IKalkulaica
    {
        public int cislo_uctovanej_polozky  { get; set; }
        public double cena                  { get; set; }
    }
}