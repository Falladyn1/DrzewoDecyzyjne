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

        double prog;
        double cecha;

        Wezel prawy;
        Wezel lewy;

        WezelDecyzyjny(double p, double c)
        {
            prog = p;
            cecha = c;
        }
    }
}
