using DrzewoDecyzyjne;
using System;

ZbiorDanych baza1 = new ZbiorDanych();
baza1.wczytajDane("iris.data");

Drzewo drzewo = new Drzewo();
drzewo.UtworzDrzewo(baza1, 30);
drzewo.WypiszDrzewo();


double[] testowyKwiatek = new double[] { 5.1, 3.5, 1.4, 0.2 };

string przewidywanaEtykieta = drzewo.Test(testowyKwiatek);

Console.WriteLine($"Wynik testu dla wektora [5.1, 3.5, 1.4, 0.2]: {przewidywanaEtykieta}");

