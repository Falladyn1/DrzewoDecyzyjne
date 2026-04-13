using DrzewoDecyzyjne;
using DrzewoDecyzyjne.Drzewo;
using System;
using System.Collections.Generic;
using System.Linq;


ZbiorDanych baza = new ZbiorDanych();
baza.wczytajDane("iris.data");

int k = 10; // 10-krotna walidacja
CV cv = new CV(k, baza.LiczbaWierszy);
List<(int[] train, int[] test)> folds = cv.makeCV();

double sumaDokladnosci = 0;
Console.WriteLine($"Rozpoczynam {k}-krotną walidację\n");

for (int i = 0; i < folds.Count; i++)
{
    var (train, test) = folds[i];

    Drzewo drzewo = new Drzewo(20);
    drzewo.utworzDrzewo(baza, train);

    int poprawne = 0;
    foreach (int testIdx in test)
    {
        double[] wektorTestowy = baza.pobierzWektor(testIdx);
        string prawdziwaEtykieta = baza.pobierzEtykiete(testIdx);
        if (prawdziwaEtykieta == drzewo.Test(wektorTestowy))
        {
            poprawne++;
        }
    }

    double dokladnosc = (double)poprawne / test.Length * 100;
    sumaDokladnosci += dokladnosc;
    Console.WriteLine($"Fold {i + 1}: Dokładność = {dokladnosc:F2}%");
}

Console.WriteLine($"\nŚrednia skuteczność algorytmu: {sumaDokladnosci / k:F2}%");



double obliczEntropie(int[] indeksy, ZbiorDanych dane)
{
    int n = indeksy.Length;
    if (n == 0) return 0.0;

    var liczniki = new Dictionary<string, int>();
    foreach (int idx in indeksy)
    {
        string et = dane.pobierzEtykiete(idx);
        if (liczniki.ContainsKey(et)) liczniki[et]++;
        else liczniki[et] = 1;
    }

    // E = - suma(pi * log2(pi))
    double entropia = 0.0;
    foreach (var kvp in liczniki)
    {
        double p = (double)kvp.Value / n;
        if (p > 0)
        {
            entropia -= p * Math.Log2(p);
        }
    }
    return entropia;
}

(int cecha, double prog) PodzialEntropia(ZbiorDanych dane, int[] indeksy)
{
    int najlepszaCecha = -1;
    double najlepszyProg = 0;
    double najWynik = double.MaxValue;

    for (int i = 0; i < dane.LiczbaCech; i++)
    {
        double[] progi = dane.pobierzProgi(i, indeksy);
        foreach (double prog in progi)
        {
            List<int> lewa = new List<int>();
            List<int> prawa = new List<int>();

            foreach (int idx in indeksy)
            {
                if (dane[idx, i] <= prog) lewa.Add(idx);
                else prawa.Add(idx);
            }

            if (lewa.Count == 0 || prawa.Count == 0) continue;

            double nl = lewa.Count;
            double nr = prawa.Count;
            double n = nl + nr;

            //(nl/n * El) + (nr/n * Er)
            double wynik = (nl / n) * obliczEntropie(lewa.ToArray(), dane) +
                           (nr / n) * obliczEntropie(prawa.ToArray(), dane);

            if (wynik < najWynik)
            {
                najWynik = wynik;
                najlepszaCecha = i;
                najlepszyProg = prog;
            }
        }
    }
    return (najlepszaCecha, najlepszyProg);
}