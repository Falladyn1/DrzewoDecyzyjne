using System;

namespace DrzewoDecyzyjne
{
    internal class WezelLisc : Wezel
    {
        private string etykieta;

        public WezelLisc(string et, int[] indeksy)
        {
            this.etykieta = et;
            this.Index = indeksy;
        }

        public override void Wypisz(string wciecie, int poziom)
        {
            Console.WriteLine($"{wciecie}[Poziom {poziom}] LISC: {etykieta} (Liczba wierszy: {Index.Length})");
        }
        

        public override string Test(double[] wektor)
        {
            return this.etykieta;
        }
    }
}