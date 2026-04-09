using DrzewoDecyzyjne;
using DrzewoDecyzyjne.Drzewo;
using System;
using System.Collections.Generic;

ZbiorDanych baza = new ZbiorDanych();
baza.wczytajDane("iris.data");

int k = 5; // 5-krotna walidacja
CV cv = new CV(k, baza.LiczbaWierszy);

List<(int[] train, int[] test)> folds = cv.makeCV();

double sumaDokladnosci = 0;

Console.WriteLine($"Rozpoczynam {k}-krotną walidację...\n");

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

        string przewidywana = drzewo.Test(wektorTestowy);

        if (prawdziwaEtykieta == przewidywana)
        {
            poprawne++;
        }
    }

    double dokladnosc = (double)poprawne / test.Length * 100;
    sumaDokladnosci += dokladnosc;
    Console.WriteLine($"Fold {i + 1}: Dokładność = {dokladnosc:F2}% (Odgadnięto {poprawne}/{test.Length})");
}

double sredniaDokladnosc = sumaDokladnosci / k;
Console.WriteLine($"\nSrednia skuteczność Twojego algorytmu: {sredniaDokladnosc:F2}%");