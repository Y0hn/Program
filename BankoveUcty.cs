using System.Collections.Generic;
using System.Text;
using System.Linq;
using System;

namespace Ucty
{
    abstract class BankovyUcet
    {
        public double stavUctu;
        public double minimalnyZostatok;
        public readonly string cisloUctu;
        protected static int cislovac = 12345;

        public BankovyUcet ()
        {
            cisloUctu = "SK" + cislovac.ToString().PadLeft(12, '0');
            cislovac++;
        }

        public abstract void Vlozit(double suma);
        public abstract bool Uhradit(double suma);
        public override string ToString()
        {
            return $"___/{cisloUctu}\\___\nStav: {stavUctu}\nMinimalny: {minimalnyZostatok}";
        }
    }
    class BeznyUcet : BankovyUcet
    {
        public string majitelUctu;
        public BeznyUcet(string majitel)
        {
            majitelUctu = majitel;
            minimalnyZostatok = 0;
            stavUctu = 0;
        }
        public override void Vlozit(double suma)
        {
            stavUctu += suma;
        }
        public override bool Uhradit(double suma)
        {
            if (minimalnyZostatok > stavUctu - suma)
                stavUctu -= suma;
            else
                return false;
            return true;
        }
        public override string ToString()
        {
            return base.ToString() + $"\nMajitel Uctu: {majitelUctu}";
        }
    }
}
