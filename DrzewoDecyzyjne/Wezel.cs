using System;

namespace DrzewoDecyzyjne
{
    internal abstract class Wezel
    {
        public int[] Index;

        public abstract void Wypisz(string wciecie, int poziom);
    }
}