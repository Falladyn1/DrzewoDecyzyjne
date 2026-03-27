using System;

namespace DrzewoDecyzyjne
{
    internal class WezelDecyzyjny : Wezel
    {
        private double prog;
        private int cecha;
        private Wezel lewy;
        private Wezel prawy;

        public WezelDecyzyjny(double pr, int c, Wezel l, Wezel p, int[] indeksy)
        {
            this.prog = pr;
            this.cecha = c;
            this.lewy = l;
            this.prawy = p;
            this.Index = indeksy;
        }

        public override void Wypisz(string wciecie, int poziom)
        {
            Console.WriteLine($"{wciecie}[Poziom {poziom}] Czy cecha {cecha} <= {prog}?");
            lewy.Wypisz(wciecie + "  |", poziom + 1);
            prawy.Wypisz(wciecie + "  |", poziom + 1);
        }

        public override string Test(double[] wektor)
        {
            if (wektor[cecha] < prog)
            {
                return lewy.Test(wektor);
            }
            else
            {
                return prawy.Test(wektor);
            }
        }
    }
}