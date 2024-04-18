
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static Random random = new Random();

    static int Random(int min, int max)
    {
        return random.Next(min, max + 1);
    }

    static int PowMod(int a, int x, int p)
    {
        int result = 1;
        a %= p;
        while (x > 0)
        {
            if (x % 2 == 1)
            {
                result = (result * a) % p;
            }
            a = (a * a) % p;
            x /= 2;
        }
        return result;
    }

    static void Eratosphen(List<int> ProstCh)
    {
        for (int i = 2; i <= 500; i++)
        {
            ProstCh.Add(i);
        }

        for (int i = 0; i <= Math.Sqrt(ProstCh.Count); i++)
        {
            int j = i + 1;
            while (j < ProstCh.Count)
            {
                if (ProstCh[j] % ProstCh[i] == 0)
                {
                    for (int k = j; k < ProstCh.Count - 1; k++)
                    {
                        ProstCh[k] = ProstCh[k + 1];
                    }
                    ProstCh.RemoveAt(ProstCh.Count - 1);
                }
                else
                {
                    j++;
                }
            }
        }
    }

    static int NOD(int a, int b)
    {
        if (b == 0)
        {
            return a;
        }
        return NOD(b, a % b);
    }

    static int Eiler(int p)
    {
        int result = p;
        for (int i = 2; i * i <= p; i++)
        {
            if (p % i == 0)
            {
                while (p % i == 0)
                    p /= i;
                result -= result / i;
            }
        }
        if (p > 1)
            result -= result / p;
        return result;
    }

    static int MillerRazlozh(int m, int n1, List<int> ProstCh, List<int> ProstMnUnik, List<int> ProstMn)
    {
        foreach (int i in ProstCh)
        {
            int degree = 0;
            if (n1 % i == 0)
            {
                while (n1 % i == 0)
                {
                    n1 /= i;
                    degree += 1;
                    ProstMn.Add(i);
                }
                ProstMnUnik.Add(i);
                m *= (int)Math.Pow(i, degree);
            }
        }
        return m / 2;
    }

    static bool Miller(int n, List<int> ProstMnUnik, int t)
    {
        if (n == 2) return true;
        if (n < 2 || n % 2 == 0) return false;
        bool MillerF1 = false;
        bool MillerF2 = false;
        for (int j = 0; j < t; j++)
        {
            int a = Random(2, n - 1);
            if (PowMod(a, n - 1, n) != 1)
            {
                MillerF1 = false;
            }
            else
            {
                MillerF1 = true;
            }
            foreach (int i in ProstMnUnik)
            {
                if (PowMod(a, (n - 1) / i, n) == 1)
                {
                    MillerF2 = false;
                    break;
                }
                else
                {
                    MillerF2 = true;
                }
            }
            if (MillerF1 == true && MillerF2 == true) return true;
        }
        return false;
    }

    static void PoklingtonRazlozh(int n, ref int F, ref int R, List<int> ProstMn)
    {
        for (int i = ProstMn.Count - 1; i >= 0; i--)
        {
            if (F <= Math.Sqrt(n) - 1)
            {
                F *= ProstMn[i];
            }
            else
            {
                R *= ProstMn[i];
            }
        }
    }

    static bool Poklington(int n, int F, int R, List<int> ProstMnUnik, int t)
    {
        if (n == 2 || n == 5) return true;
        if (n < 2 || n % 2 == 0) return false;
        bool PoklingF1 = false;
        bool PoklingF2 = false;
        for (int j = 0; j < t; j++)
        {
            int a = Random(2, n - 1);
            if (PowMod(a, n - 1, n) != 1)
            {
                PoklingF1 = false;
            }
            else
            {
                PoklingF1 = true;
            }
            foreach (int i in ProstMnUnik)
            {
                if (PowMod(a, (n - 1) / i, n) == 1)
                {
                    PoklingF2 = false;
                    break;
                }
                else
                {
                    PoklingF2 = true;
                }
            }
            if (PoklingF1 == true && PoklingF2 == true) return true;
        }
        return false;
    }

    static bool GOST(int t, int q1)
    {
        int p = 0;

        while (true)
        {
            int N1 = (int)Math.Ceiling(Math.Pow(2, t - 1) / q1);
            int N2 = (int)Math.Ceiling(Math.Pow(2, t - 1) * 0 / (q1));

            double N = N1 + N2;
            if (Math.Round(N) % 2 != 0)
            {
                N++;
            }

            for (int u = 0; Math.Pow(2, t) >= (N + u) * q1 + 1; u += 2)
            {
                p = (int)((N + u) * q1 + 1);
                if ((PowMod(2, p - 1, p) == 1) && (PowMod(2, (int)N + u, p) != 1))
                {
                    return true;
                }
            }
        }
    }

    static bool VerTest(int n, int t, int R, int F)
    {
        if (NOD(R, F) == 1)
        {
            double verMiller = (Eiler(n - 1) * 1.0 / (n - 1)) * t;
            double verPoklington = (Eiler(n) * 1.0 / n) * t;
            double result = 1 - verMiller - verPoklington;
            return (result <= 0.1);
        }
        else
        {
            double verMiller = (Eiler(n - 1) * 1.0 / (n - 1)) * t;
            double result = 1 - verMiller;
            return (result <= 0.1);
        }
    }

    static void InPut(int n, bool VerTest, int k)
    {
        if (VerTest && k <= 6)
        {
            Console.WriteLine($"{n} \t\t+ \t\t{k}");
        }
        else
        {
            Console.WriteLine($"{n} \t\t- \t\t{k}");
        }
    }

    static void Main(string[] args)
    {
        List<int> ProstCh = new List<int>();
        Eratosphen(ProstCh);
        int t = 5;
        int t1 = 4;
        int q1 = 3;
        int k = 0;
        Console.WriteLine("Число|\tРезультат проверки|\tКоличество отвергнутых чисел");
        Console.WriteLine("-------------------------------------------------------");
        for (int i = 0; i < 10; i++)
        {
            List<int> ProstMnUnik = new List<int>();
            List<int> ProstMn = new List<int>();
            int n = Random(2, 500 - 2);
            int n1 = n - 1;
            int m = 1;
            m = MillerRazlozh(m, n1, ProstCh, ProstMnUnik, ProstMn);
            int F = 1;
            int R = 1;
            PoklingtonRazlozh(n, ref F, ref R, ProstMn);

            if (!Miller(n, ProstMnUnik, t) || !Poklington(n, F, R, ProstMnUnik, t))
            {
                k++;
                i--;
                continue;
            }
            bool GOSTResult = GOST(t, q1);

            InPut(n, VerTest(n, t, R, F), k);

            if (Miller(n, ProstMnUnik, t) && Poklington(n, F, R, ProstMnUnik, t))
            {
                k = 0;
            }
        }
    }
}