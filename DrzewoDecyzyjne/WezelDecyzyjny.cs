using System;

namespace DrzewoDecyzyjne
{
    internal class WezelDecyzyjny : Wezel
    {
        double prog;
        int cecha;
        Wezel lewy;
        Wezel prawy;

        public WezelDecyzyjny(double pr, int c, Wezel l, Wezel p)
        {
            this.prog = pr;
            this.cecha = c;
            this.lewy = l;
            this.prawy = p;
        }

        public override void Wypisz(string wciecie, int poziom)
        {
            Console.WriteLine($"{wciecie}[Poziom {poziom}] Czy cecha {cecha} <= {prog}?");
            lewy.Wypisz(wciecie + "  |", poziom + 1);
            prawy.Wypisz(wciecie + "  |", poziom + 1);
        }
    }
}