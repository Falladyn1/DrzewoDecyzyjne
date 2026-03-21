using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrzewoDecyzyjne
{
    internal class WezelLisc : Wezel
    {
        private string etykieta;

        public WezelLisc(string et)
        {
            etykieta = et;
        }

        public override void Wypisz(string wciecie, int poziom)
        {
            Console.WriteLine($"{wciecie}[Poziom {poziom}] Liść: {etykieta}");
        }
    }
}
