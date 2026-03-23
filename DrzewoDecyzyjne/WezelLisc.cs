using System;

namespace DrzewoDecyzyjne
{
    internal class WezelLisc : Wezel
    {
        private string etykieta;
        public WezelLisc(string et) => etykieta = et;

        public override void Wypisz(string wciecie, int poziom)
        {
            Console.WriteLine($"{wciecie}[Poziom {poziom}] LISC: {etykieta}");
        }
    }
}