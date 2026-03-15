using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrzewoDecyzyjne
{
    internal class STD
    {
        double[] _x;//srednia
        double[] std;//odchylenie

        // tak był pierwszy pomysł ale przegladam kila razy tą samą tabele co można zredukować 

        //public double[] obliczSrednia(double[][] tab)
        //{
        //    int kolumny = tab[0].Length;
        //    int wiersze = tab.Length;
        //    double[] wynik = new double[kolumny];
        //    for (int i = 0; i < kolumny; i++)
        //    {
        //        double sum = 0;
        //        for (int j = 0; j < wiersze; j++)
        //        {
        //            sum+=tab[j][i];
        //        }
        //        wynik[i] = sum/wiersze;
        //    }
        //    return wynik;   
        //}

        //public double[] obliczOdchylenie(double[][] tab, double[] srednia)
        //{
        //    int kolumny = tab[0].Length;
        //    int wiersze = tab.Length;
        //    double[] wynik = new double[kolumny];
        //    for (int i = 0; i < kolumny; i++)
        //    {
        //        double sumaKwadratow = 0;
        //        for (int j = 0; j < wiersze; j++)
        //        {
        //            double roznica = tab[j][i] - srednia[i];
        //            double kwadrat = roznica * roznica;
        //            sumaKwadratow += kwadrat;
        //        }
        //        wynik[i] = Math.Sqrt(sumaKwadratow/wiersze);
        //    }

        //    return wynik;
        //}

        public void obliczStatystyki(double[][] tab)
        {
            int kolumny = tab[0].Length;
            int wiersze = tab.Length;

            this._x = new double[kolumny];
            this.std = new double[kolumny];

            for (int i = 0; i < kolumny; i++)
            {
                double suma = 0;
                double sumaKwadratow = 0;

                for (int j = 0; j < wiersze; j++)
                {
                    double wartosc = tab[j][i];
                    suma += wartosc;
                    sumaKwadratow += wartosc * wartosc;
                }

                this._x[i] = suma / wiersze;

                double wariancja = (sumaKwadratow / wiersze) - (this._x[i] * this._x[i]);

                this.std[i] = wariancja > 0 ? Math.Sqrt(wariancja) : 0;
            }
        }

        public double[][] standard(double[][] tab)
        {
            int kolumny = tab[0].Length;
            int wiersze = tab.Length;

            obliczStatystyki(tab);
            
            double[][] wynik = new double[wiersze][];
            for (int i = 0; i < wiersze; i++)
            {
                wynik[i] = new double[kolumny];
            } 
            for (int i = 0; i < kolumny; i++)
            {
                double sredniaKol = _x[i];
                double odchylenieKol = std[i];
                for (int j = 0; j < wiersze; j++)
                {
                    if (odchylenieKol != 0)
                        wynik[j][i] = (tab[j][i] - sredniaKol)/odchylenieKol;
                    else
                        wynik[j][i] = 0;
                }
                
            }
            return wynik;
        }
    }
}
