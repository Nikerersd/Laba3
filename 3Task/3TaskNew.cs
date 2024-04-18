using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Cofe(double Tk, double Tsr, double r, int t, List<double> Cofe)
    {
        for (int i = 0; i <= t; i++)
        {
            Cofe.Add(Tk);
            Tk -= r * (Tk - Tsr);
        }
    }

    static double AproxA(List<double> Cofe)
    {
        double Ys = 0, Xs = 0, XYs = 0, X2s = 0;
        int n = Cofe.Count;

        for (int i = 0; i < n; i++)
        {
            Ys += Cofe[i];
            Xs += i;

            XYs += Cofe[i] * i;
            X2s += i * i;
        }
        return (n * XYs - (Xs * Ys)) / (n * X2s - Xs * Xs);
    }

    static double AproxB(List<double> Cofe)
    {
        double Ys = 0, Xs = 0;
        double a = AproxA(Cofe);
        int n = Cofe.Count;

        for (int i = 0; i < n; i++)
        {
            Ys += Cofe[i];
            Xs += i;
        }
        return (Ys - a * Xs) / n;
    }

    static double Korrel(List<double> Cofe)
    {
        int n = Cofe.Count;
        double sumT = Cofe.Sum();
        double SrT = sumT / n;
        double Srt = (n - 1) * n / 2.0;
        double XYs = 0.0;
        double Xs2 = 0.0;
        double Ys2 = 0.0;

        for (int i = 0; i < n; i++)
        {
            double vremX = i - Srt;
            double vremY = Cofe[i] - SrT;

            XYs += vremX * vremY;
            Xs2 += vremX * vremX;
            Ys2 += vremY * vremY;
        }
        double r = XYs / Math.Sqrt(Xs2 * Ys2);
        return r;
    }

    static void Main()
    {
        double Tk, Tsr, r;
        int t;
        List<double> CofeList = new List<double>();
        Console.WriteLine("Введите температуру кофе и среды, коэффициент остывания и время остывания в минутах через пробел");
        var inputs = Console.ReadLine().Split();
        Tk = double.Parse(inputs[0]);
        Tsr = double.Parse(inputs[1]);
        r = double.Parse(inputs[2]);
        t = int.Parse(inputs[3]);
        Cofe(Tk, Tsr, r, t, CofeList);
        double a = AproxA(CofeList);
        double b = AproxB(CofeList);
        double korr = Korrel(CofeList);
        t = 0;
        Console.WriteLine("{0,19} {1,25}", "Время", "Температура");
        foreach (double T in CofeList)
        {
            Console.WriteLine("{0,10} {1,10}", t, T);
            t++;
        }
        Console.WriteLine("Линия аппроксимации: T = {0} * t + {1}", a, b);
        Console.WriteLine("Коэффициент корреляции: {0}", korr);
    }
}