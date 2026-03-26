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
        

        // metoda rekurencyjna

        public string Test(double[] x) // podajemy pojedynczy wektor i sprawdamy gdzie on bedzie i zwracamy etykiete

        {
            string et;



            return;
        }
    }
}