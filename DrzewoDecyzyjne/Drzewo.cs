using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
            if (indeksy.Length == 0) return new WezelLisc("Brak danych", indeksy);

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
                return new WezelLisc(Etykieta, indeksy);
            }

            int cecha = rng.Next(0, dane.LiczbaCech);
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
                return new WezelLisc(Etykieta, indeksy);
            }

            return new WezelDecyzyjny(prog, cecha,
                ZbudujDrzewo(listaLewa.ToArray(), glebokosc + 1),
                ZbudujDrzewo(listaPrawa.ToArray(), glebokosc + 1),
                indeksy);
        }

        public void WypiszDrzewo()
        {
            if (korzen != null) korzen.Wypisz("", 0);
        }

        public string Test(double[] x)
        {
            if (korzen != null)
            {
                return korzen.Test(x);
            }
            return "Drzewo nie zostało jeszcze zbudowane!";
        }

        private double obliczGini(int[] indeksy)
        {
            int n = indeksy.Length;
            if (n == 0) return 0.0;

            List<string> listaEt = new List<string>();
            List<int> liczniki = new List<int>();

            foreach (int i in indeksy)
            {
                string etykieta = dane.PobierzEtykiete(i);
                int pozycja = listaEt.IndexOf(etykieta);
                
                if (pozycja == -1)
                {
                    listaEt.Add(etykieta);
                    liczniki.Add(1);
                }
                else
                {
                    liczniki[pozycja]++;
                }
            }

            double sumaKwadratow = 0.0;

            foreach (int licznik in liczniki)
            {
                double p = (double)licznik/n;
                sumaKwadratow = p * p;
            }

            return 1 - sumaKwadratow;
        }
    }
}