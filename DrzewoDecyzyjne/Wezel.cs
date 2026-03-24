using System;

namespace DrzewoDecyzyjne
{
    internal abstract class Wezel
    {
        public int[] Index { get; protected set; }

        public abstract void Wypisz(string wciecie, int poziom);
    }
}