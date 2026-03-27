using System;

namespace DrzewoDecyzyjne.Drzewo
{
    internal class WezelDecyzyjny : Wezel
    {
        private double prog;
        private int cecha;
        private Wezel lewy;
        private Wezel prawy;

        public WezelDecyzyjny(double pr, int c, Wezel l, Wezel p, int[] indeksy)
        {
            prog = pr;
            cecha = c;
            lewy = l;
            prawy = p;
            Index = indeksy;
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