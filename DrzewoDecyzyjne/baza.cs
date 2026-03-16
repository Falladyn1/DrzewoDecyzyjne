using System;
using System.Globalization;
using System.IO;

namespace DrzewoDecyzyjne
{
    internal class Baza
    {
        public double[][] wektory;
        public string[] etykiety;

        public void wczytajDane(string sciezka)
        {
            if (!File.Exists(sciezka))
            {
                Console.WriteLine($"Błąd: Nie znaleziono pliku {sciezka}");
                return;
            }

            int lWierszy = 0;

            using (StreamReader sr = new StreamReader(sciezka))
            {
                string linia;
                while ((linia = sr.ReadLine()) != null)
                {
                    if (!string.IsNullOrWhiteSpace(linia))
                    {
                        lWierszy++;
                    }
                }
            }

            if (lWierszy == 0) return;

            wektory = new double[lWierszy][];
            etykiety = new string[lWierszy];

            using (StreamReader sr = new StreamReader(sciezka))
            {
                string linia;
                int i = 0;
                while ((linia = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(linia)) continue;

                    string[] fragmenty = linia.Split(',');
                    int liczbaCech = fragmenty.Length - 1;

                    wektory[i] = new double[liczbaCech];

                    for (int j = 0; j < liczbaCech; j++)
                    {
                        double.TryParse(fragmenty[j], NumberStyles.Any, CultureInfo.InvariantCulture, out wektory[i][j]);
                    }

                    etykiety[i] = fragmenty[liczbaCech].Trim();
                    i++;
                }
            }
        }

        public void drukujDane()
        {
            if (wektory == null) return;

            for (int i = 0; i < wektory.Length; i++)
            {
                for (int j = 0; j < wektory[i].Length; j++)
                {
                    Console.Write($"{wektory[i][j],7:0.0} ");
                }
                Console.WriteLine($"  {etykiety[i]}");
            }
        }
    }
}