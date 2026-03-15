using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DrzewoDecyzyjne
{
    internal class baza
    {
        public double[][] wektory;
        string[] etykiety;

        public void wczytajDane(string sciezka)
        {
            if (File.Exists(sciezka))
            {
                int lWierszy = 0;
                using (StreamReader sr = new StreamReader(sciezka))
                {


                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (!string.IsNullOrWhiteSpace(line))
                            lWierszy++;
                    }
                    if (lWierszy == 0) return;

                    wektory = new double[lWierszy][];
                    etykiety = new string[lWierszy];
                }

                using (StreamReader sr = new StreamReader(sciezka))
                {
                    string line;
                    int i = 0;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (string.IsNullOrWhiteSpace(line)) continue;

                        string[] fragmenty = line.Split(',');
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

