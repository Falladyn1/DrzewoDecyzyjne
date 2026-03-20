using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrzewoDecyzyjne
{
    internal class Drzewo
    {
        //korzen
        //dane
        //glebokosc
        //utworzDrzewo(){}
        //Wezel ZbudujDrzewo(index)

        Wezel korzen;
        ZbiorDanych dane;
        int glebokosc;

        Drzewo(ZbiorDanych daneWejsciowe, int maxGlebokosc)
        {
            this.dane = daneWejsciowe;
            this.glebokosc = maxGlebokosc;
        }
        void UtworzDrzewo()
        {
            int[] indeksyP = 

            korzen = ZbudujDrzewo(indeksyP)        }

        //Wezel ZbudujDrzewo(int index)
        //{

        //}
    }
}
