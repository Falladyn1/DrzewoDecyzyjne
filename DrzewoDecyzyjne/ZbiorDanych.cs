using System;
using System.Globalization;
using System.IO;
using System.Collections.Generic;

namespace DrzewoDecyzyjne
{
    internal class ZbiorDanych
    {
        private double[][] wektory;
        private string[] etykiety;

        public int LiczbaWierszy
        {
            get { return wektory.Length; }
        }

        public int LiczbaCech
        {
            get { return wektory.Length > 0 ? wektory[0].Length : 0; }

        public void wczytajDane(string sciezka)
        {
            if (!File.Exists(sciezka)) return;

            string[] linie = File.ReadAllLines(sciezka);
            List<double[]> listaWektorow = new List<double[]>();
            List<string> listaEtykiet = new List<string>();

            foreach (string linia in linie)
            {
                if (string.IsNullOrWhiteSpace(linia)) continue;
                string[] fragmenty = linia.Split(',');

                double[] cechy = new double[fragmenty.Length - 1];
                for (int i = 0; i < cechy.Length; i++)
                {
                    double.TryParse(fragmenty[i], NumberStyles.Any, CultureInfo.InvariantCulture, out cechy[i]);
                }

                listaWektorow.Add(cechy);
                listaEtykiet.Add(fragmenty[fragmenty.Length - 1].Trim());
            }

            wektory = listaWektorow.ToArray();
            etykiety = listaEtykiet.ToArray();
        }

        public double this[int i, int j] { get { return wektory[i][j]; } }
        public string PobierzEtykiete(int i) { return etykiety[i]; }
    }
}