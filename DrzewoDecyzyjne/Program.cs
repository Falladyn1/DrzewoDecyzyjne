using DrzewoDecyzyjne;
using DrzewoDecyzyjne.Drzewo;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

ZbiorDanych baza = new ZbiorDanych();
baza.wczytajDane("iris.data");

int k = 10; // 10-krotna walidacja
CV walidacjaKrzyzowa = new CV(k, baza.LiczbaWierszy);
List<(int[] trening, int[] test)> podzbioryWalidacji = walidacjaKrzyzowa.makeCV();

double sumaDokladnosci = 0;
Console.WriteLine($"Rozpoczynam {k}-krotną walidację\n");

for (int i = 0; i < podzbioryWalidacji.Count; i++)
{
    var (treningoweIdx, testoweIdx) = podzbioryWalidacji[i];

    Drzewo drzewo = new Drzewo(20);
    drzewo.utworzDrzewo(baza, treningoweIdx);

    int poprawne = 0;
    foreach (int idxTestowy in testoweIdx)
    {
        double[] wektorTestowy = baza.pobierzWektor(idxTestowy);
        string prawdziwaEtykieta = baza.pobierzEtykiete(idxTestowy);

        if (prawdziwaEtykieta == drzewo.Test(wektorTestowy))
        {
            poprawne++;
        }
    }

    double dokladnosc = (double)poprawne / testoweIdx.Length * 100;
    sumaDokladnosci += dokladnosc;
    Console.WriteLine($"Fałd {i + 1}: Dokładność = {dokladnosc:F2}%");
}

Console.WriteLine($"\nŚrednia skuteczność algorytmu: {sumaDokladnosci / k:F2}%");
double ObliczEntropie(int[] indeksy, ZbiorDanych dane)
{
    int n = indeksy.Length;
    if (n == 0) return 0.0;

    var licznikiEtykiet = new Dictionary<string, int>();
    foreach (int idx in indeksy)
    {
        string etykieta = dane.pobierzEtykiete(idx);
        if (licznikiEtykiet.ContainsKey(etykieta)) licznikiEtykiet[etykieta]++;
        else licznikiEtykiet[etykieta] = 1;
    }

    // Wzór: E = - suma(p_i * log2(p_i))
    double entropia = 0.0;
    foreach (var para in licznikiEtykiet)
    {
        double p = (double)para.Value / n;
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
    double najmniejszaEntropia = double.MaxValue;

    for (int i = 0; i < dane.LiczbaCech; i++)
    {
        double[] progi = dane.pobierzProgi(i, indeksy);
        foreach (double prog in progi)
        {
        List<int> lewaPodgrupa = new List<int>();
        List<int> prawaPodgrupa = new List<int>();

        foreach (int idx in indeksy)
        {
            if (dane[idx, i] <= prog)
                lewaPodgrupa.Add(idx);
            else
                prawaPodgrupa.Add(idx);
        }

        if (lewaPodgrupa.Count == 0 || prawaPodgrupa.Count == 0) continue;

        double nLewa = lewaPodgrupa.Count;
        double nPrawa = prawaPodgrupa.Count;
        double nRazem = nLewa + nPrawa;

        // Średnia ważona entropii: (nL/n * EL) + (nR/n * ER)
        double aktualnaEntropia = (nLewa / nRazem) * ObliczEntropie(lewaPodgrupa.ToArray(), dane) +
                                  (nPrawa / nRazem) * ObliczEntropie(prawaPodgrupa.ToArray(), dane);

        if (aktualnaEntropia < najmniejszaEntropia)
        {
            najmniejszaEntropia = aktualnaEntropia;
            najlepszaCecha = i;
            najlepszyProg = prog;
        }
    }
}
return (najlepszaCecha, najlepszyProg);
}