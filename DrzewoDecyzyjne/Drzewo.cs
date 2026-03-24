using System;
using System.Collections.Generic;
using System.Linq;

namespace DrzewoDecyzyjne
{
    internal class Drzewo
    {
        private Wezel korzen;
        private ZbiorDanych dane;
        private int maxGlebokosc;
        private Random rng = new Random();

        public void UtworzDrzewo(ZbiorDanych daneWejsciowe, int maxGlebokosc)
        {
            this.dane = daneWejsciowe;
            this.maxGlebokosc = maxGlebokosc;

            int[] indeksy = new int[dane.LiczbaWierszy];
            for (int i = 0; i < dane.LiczbaWierszy; i++)
            {
                indeksy[i] = i;
            }

            korzen = ZbudujDrzewo(indeksy, 0);
        }

        public Wezel ZbudujDrzewo(int[] indeksy, int glebokosc)
        {
            if (indeksy.Length == 0) return new WezelLisc("Brak danych");

            int losowyIndeks = indeksy[rng.Next(indeksy.Length)];
            string Etykieta = dane.PobierzEtykiete(losowyIndeks);

            bool czyCzyste = true;
            foreach (int i in indeksy)
            {
                if (dane.PobierzEtykiete(i) != Etykieta)
                {
                    czyCzyste = false;
                    break;
                }
            }

            if (czyCzyste || indeksy.Length <= 1 || glebokosc >= maxGlebokosc)
            {
                return new WezelLisc(Etykieta);
            }

            int cecha = rng.Next(0, 4);
            double prog = dane[indeksy[rng.Next(indeksy.Length)], cecha];

            List<int> listaLewa = new List<int>();
            List<int> listaPrawa = new List<int>();

            foreach (int i in indeksy)
            {
                if (dane[i, cecha] <= prog)
                    listaLewa.Add(i);
                else
                    listaPrawa.Add(i);
            }

            if (listaLewa.Count == 0 || listaPrawa.Count == 0)
            {
                return new WezelLisc(Etykieta);
            }

            return new WezelDecyzyjny(prog, cecha,
                ZbudujDrzewo(listaLewa.ToArray(), glebokosc + 1),
                ZbudujDrzewo(listaPrawa.ToArray(), glebokosc + 1));
        }

        public void WypiszDrzewo()
        {
            if (korzen != null) korzen.Wypisz("", 0);
        }
    }
}