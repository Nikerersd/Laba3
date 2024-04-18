using System;
using System.IO;

class Program
{
    static double FirstFragm(double x)
    {
        double k = 1.0 / 3.0;
        double res = k * (-x - 2);
        if (Math.Abs(res) < 0.000001)
        {
            res = 0.0;
        }
        return res;
    }

    static double SecondFragm(double x)
    {
        return Math.Log(Math.Abs(1.0 / Math.Tan(x / 2.0)));
    }

    static double ThirdFragm(double x)
    {
        double k = 1.0 / 3.0;
        double res = k * (x - 2);
        if (Math.Abs(res) < 0.000001)
        {
            res = 0.0;
        }
        return res;
    }

    static void Main()
    {
        double xNach = -5;
        double xKon = 5;
        double dx = 0.1;

        using (StreamWriter outfile = new StreamWriter("1Task_24Var#.txt"))
        {
            if (!outfile.BaseStream.CanWrite)
            {
                Console.WriteLine("При открытии файла произошла ошибка");
                return;
            }

            outfile.WriteLine("{0,10}{1,10}", "X", "Y");
            outfile.WriteLine(new string('-', 20));
            for (double x = xNach; x <= xKon; x += dx)
            {
                if (Math.Abs(x) < 0.000001)
                {
                    x = 0.0;
                }
                if (x >= -5 && x < -2)
                {
                    outfile.WriteLine("{0,10:F2}{1,10:F2}", x, FirstFragm(x));
                }
                if (x >= -2 && x < 2)
                {
                    outfile.WriteLine("{0,10:F2}{1,10:F2}", x, SecondFragm(x));
                }
                if (x >= 2 && x < 5)
                {
                    outfile.WriteLine("{0,10:F2}{1,10:F2}", x, ThirdFragm(x));
                }
            }
        }
    }
}