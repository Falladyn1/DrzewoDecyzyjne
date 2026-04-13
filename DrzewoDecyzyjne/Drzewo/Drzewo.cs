using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace DrzewoDecyzyjne.Drzewo
{
    internal class Drzewo
    {
        private Wezel korzen;
        private ZbiorDanych dane;
        private int maxGlebokosc;
        private Random rng = new Random();
        //praca domowa
        //Func<>
        public delegate (int cecha, double prog) StrategiaPodzialu(ZbiorDanych daneWejsiowe, int[] indeksy);

        StrategiaPodzialu FunkcjaPodzialu = null;

        public Drzewo(int glebokosc, StrategiaPodzialu? funkcjaPodzialu = null)
        {
            maxGlebokosc = glebokosc;
            FunkcjaPodzialu = funkcjaPodzialu;
        }

        public void utworzDrzewo(ZbiorDanych daneWejsciowe, int[] indeksyTreningowe)
        {
            //int[] indeksy = new int[dane.LiczbaWierszy];
            //for (int i = 0; i < dane.LiczbaWierszy; i++)
            //{
            //    indeksy[i] = i;
            //}
            this.dane = daneWejsciowe;
            korzen = zbudujDrzewo(indeksyTreningowe, 0);
        }

        private string znajdzNajczestszaEtykiete(int[] indeksy)
        {
            List<string> listaEt = new List<string>();
            List<int> liczniki = new List<int>();

            foreach (int i in indeksy)
            {
                string etykieta = dane.pobierzEtykiete(i);
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

            int maxLicznik = -1;
            int najlepszyIndeks = -1;

            for (int j = 0; j < liczniki.Count; j++)
            {
                if (liczniki[j] > maxLicznik)
                {
                    maxLicznik = liczniki[j];
                    najlepszyIndeks = j;
                }
            }

            return listaEt[najlepszyIndeks];
        }

        public Wezel zbudujDrzewo(int[] indeksy, int glebokosc)
        {
            if (indeksy.Length == 0) return new WezelLisc("Brak danych", indeksy);


            string Etykieta = znajdzNajczestszaEtykiete(indeksy);

            bool czyCzyste = true;
            foreach (int i in indeksy)
            {
                if (dane.pobierzEtykiete(i) != Etykieta)
                {
                    czyCzyste = false;
                    break;
                }
            }

            if (czyCzyste || indeksy.Length <= 1 || glebokosc >= maxGlebokosc)
            {
                return new WezelLisc(Etykieta, indeksy);
            }

            (int cecha, double prog) podzial;

            if (FunkcjaPodzialu == null)
            {
                podzial = najlepszyPodział(indeksy);
            }
            else
            {
                podzial = FunkcjaPodzialu(dane, indeksy);
            }

            int cecha = podzial.cecha;
            double prog = podzial.prog;

            if (cecha == -1)
            {
                return new WezelLisc(Etykieta, indeksy);
            }

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
                zbudujDrzewo(listaLewa.ToArray(), glebokosc + 1),
                zbudujDrzewo(listaPrawa.ToArray(), glebokosc + 1),
                indeksy);
        }

        private double obliczGini(int[] indeksy)
        {
            int n = indeksy.Length;
            if (n == 0) return 0.0;

            List<string> listaEt = new List<string>();
            List<int> liczniki = new List<int>();

            foreach (int i in indeksy)
            {
                string etykieta = dane.pobierzEtykiete(i);
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
                sumaKwadratow += p * p;
            }

            return 1 - sumaKwadratow;
        }

        private (int, double) najlepszyPodział(int[] indeksy)
        {
            int najlepszaCecha = -1; //indeks
            double najlepszyProg = 0;
            double najWynik = double.MaxValue;

            for (int i = 0; i<dane.LiczbaCech; i++)
            {
                double[] progi = dane.pobierzProgi(i,indeksy);
                foreach (double prog in progi)
                {
                    List<int> listaLewa = new List<int>();
                    List<int> listaPrawa = new List<int>();

                    foreach (int indeksWiesza in indeksy)
                    {
                        if (dane[indeksWiesza, i] <= prog)
                        {
                            listaLewa.Add(indeksWiesza);
                        }
                        else
                        {
                            listaPrawa.Add(indeksWiesza);
                        }
                    }
                    // ograiczac uzywanie ToArray bo alokuje pamiec i 
                    // count() liczy cala tablice 
                    double nl = listaLewa.ToArray().Count();
                    double nr = listaPrawa.ToArray().Count();
                    double n = nl + nr;
                    double p = nl*obliczGini(listaLewa.ToArray())/n + nr*obliczGini(listaPrawa.ToArray())/n;

                    if (p<najWynik)
                    {
                        najWynik=p;
                        najlepszaCecha = i;
                        najlepszyProg = prog;
                    }
                }
                
            }


            return (najlepszaCecha, najlepszyProg);
        }
        

        public string Test(double[] x)
        {
            if (korzen != null)
            {
                return korzen.Test(x);
            }
            return "Drzewo nie zostało jeszcze zbudowane!";
        }

        public void wypiszDrzewo()
        {
            if (korzen != null) korzen.Wypisz("", 0);
        }

    }
}