using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrzewoDecyzyjne
{
    internal class WezelDecyzyjny : Wezel
    {
        //prog
        //cecha
        //Wezel lewy
        //Wezel prawy

        double prog;
        int cecha;

        Wezel prawy;
        Wezel lewy;

        public WezelDecyzyjny(double pr, int c, Wezel l, Wezel p)
        {
            prog = pr;
            cecha = c;
            lewy = l;
            prawy = p;
        }

        public override void Wypisz(string wciecie, int poziom)
        {
            Console.WriteLine($"{wciecie}[Poziom {poziom}] Czy cecha {cecha} > {prog}?");

            lewy.Wypisz(wciecie + "   ", poziom + 1);
            prawy.Wypisz(wciecie + "   ", poziom + 1);
        }
    }
}
