using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrzewoDecyzyjne
{
    internal class WezelDecyzyjny
    {
        //prog
        //cecha
        //Wezel lewy
        //Wezel prawy

        int prog;
        int cecha;

        Wezel prawy;
        Wezel lewy;

        DrzewoDecyzyjne(int p, int c)
        {
            prog = p;
            cecha = c;
        }
    }
}
