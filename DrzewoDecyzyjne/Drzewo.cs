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

        private Wezel korzen;
        private ZbiorDanych dane;
        private int maxGlebokosc;


        public void UtworzDrzewo(ZbiorDanych daneWejsciowe, int maxGlebokosc)
        {
            this.dane = daneWejsciowe;
            this.maxGlebokosc = maxGlebokosc;

            int[] indeksyZDanych = new int[dane.LiczbaWierszy];
            for (int i = 0; i<dane.LiczbaWierszy; i++)
            {
                indeksyZDanych[i]=i;
            }

            korzen = ZbudujDrzewo(indeksyZDanych, 0);
        }

        public Wezel ZbudujDrzewo(int[] indeksyZDanych, int glebokosc)
        {
            if (indeksyZDanych.Length <= 1 || this.maxGlebokosc < glebokosc)
            {
                if (indeksyZDanych.Length == 0)
                {
                    return new WezelLisc("Brak danych");
                }

                int wiersz = indeksyZDanych[0];
                string znalezionaEtykieta = dane.PobierzEtykiete(wiersz);

                return new WezelLisc(znalezionaEtykieta);
            }

            Random rng = new Random();
            int wylosowanaCecha = rng.Next(0, 4);

            int losowyIndex = rng.Next(0, indeksyZDanych.Length);
            int wierszPrawidlowy = indeksyZDanych[losowyIndex];
            double wylosowanyProg = dane[wierszPrawidlowy, wylosowanaCecha];

            List<int> indeksyPrawe = new List<int>();
            List<int> indeksyLewe = new List<int>();

            foreach (int i in indeksyZDanych)
            {
                if (dane[i, wylosowanaCecha] > wylosowanyProg)
                {
                    indeksyLewe.Add(i);
                }
                else
                {
                    indeksyPrawe.Add(i);
                }
            }

            int[] tabLewe = indeksyLewe.ToArray();
            int[] tabPrawe = indeksyPrawe.ToArray();

            Wezel wezelLewy = ZbudujDrzewo(tabLewe, glebokosc+1);
            Wezel wezelPrawy = ZbudujDrzewo(tabPrawe, glebokosc+1);
            return new WezelDecyzyjny(wylosowanyProg, wylosowanaCecha, wezelLewy, wezelPrawy);
        }


        public void WypiszDrzewo()
        {
            korzen.Wypisz("", 0);
        }
    }
}
