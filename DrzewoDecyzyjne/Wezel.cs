using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrzewoDecyzyjne
{
    internal abstract class Wezel
    {
        int[] index;

        public abstract void Wypisz(string wciecie, int poziom);
    }
}
