using System;

namespace DrzewoDecyzyjne.Drzewo
{
    internal abstract class Wezel
    {
        public int[] Index;

        public abstract void Wypisz(string wciecie, int poziom);

        public abstract string Test(double[] wektor);
    }
}