#include <iostream>
#include <vector>
#include <cmath>
#include <ctime>
#include <random>
using namespace std;

int Random(int min, int max) {
    return rand() % (max - min + 1) + min;
}

int pow_mod(int a, int x, int p) {
    int result = 1;
    a = a % p;
    while (x > 0) {
        if (x % 2 == 1) {
            result = (result * a) % p;
        }
        a = (a * a) % p;
        x /= 2;
    }
    return result;
}

void Eratosphen (vector<int>& ProstCh) {
    for (int i=2; i<=500; i++) {
        ProstCh.push_back(i);
    }
    
    for (int i=0; i <= sqrt(ProstCh.size()); i++) {
        int j = i+1;
        while (j < ProstCh.size()) {
            if (ProstCh[j]%ProstCh[i]==0) {
                for (int k = j; k < ProstCh.size() - 1; k++) {
                    ProstCh[k] = ProstCh[k + 1];
                }
                ProstCh.pop_back();
            }
            else {
                j++;
            }
        }
    }
}

int NOD (int a, int b) {
    if (b == 0) {
        return a;
    }
    return NOD(b , a % b);
}

int Eiler(int p) {
    int result = p;
    for (int i = 2; i * i <= p; i++) {
        if (p % i == 0) {
            while (p % i == 0)
                p /= i;
            result -= result / i;
        }
    }
    if (p > 1)
        result -= result / p;
    return result;
}

int MillerRazlozh(int m, int n1, const vector<int>& ProstCh, vector<int>& ProstMnUnik, vector<int>& ProstMn) {
    for (size_t i = 0; i < ProstCh.size(); i++) {
        int degree = 0;
        if (n1 % ProstCh[i] == 0) {
            while (n1 % ProstCh[i] == 0) {
                n1 /= ProstCh[i];
                degree += 1;
                ProstMn.push_back(ProstCh[i]);
            }
            ProstMnUnik.push_back(ProstCh[i]);
            m *= pow(ProstCh[i], degree);
        }
    }
    return m/2;
}

bool Miller(int n, const vector<int>& ProstMnUnik, int t) {
    if (n == 2) return true;
    if (n < 2 || n % 2 == 0) return false;
    bool MillerF1 = false;
    bool MillerF2 = false;
    for (int j=0; j<t; j++){
        int a = Random(2,n-1);
        if (pow_mod(a,n-1,n) != 1) {
            MillerF1 = false;
        }
        else {
            MillerF1 = true;
        }
        for (size_t i = 0; i < ProstMnUnik.size(); i++) {
            if (pow_mod(a, (n-1)/ProstMnUnik[i], n) == 1) {
                MillerF2 = false;
                break;
            }
            else {
                MillerF2 = true;
            }
        }
        if (MillerF1==true && MillerF2==true) return true;
    }
    return false;
}

void PoklingtonRazlozh(int n, int& F, int& R, const vector<int>& ProstMn) {
    for (size_t i = ProstMn.size() - 1; i + 1 > 0; i--) {
        if (F <= sqrt(n) - 1) {
            F *= ProstMn[i];
        }
        else {
            R *= ProstMn[i];
        }
    }
}

bool Poklington(int n, int F, int R, const vector<int>& ProstMnUnik, int t) {
    if (n == 2 || n==5) return true;
    if (n < 2 || n % 2 == 0) return false;
    bool PoklingF1 = false;
    bool PoklingF2 = false;
    for (int j=0; j < t; j++) {
        int a = Random(2,n-1);
        if (pow_mod(a,n-1,n) != 1) {
            PoklingF1 = false;
        }
        else {
            PoklingF1 = true;
        }
        for (size_t i = 0; i < ProstMnUnik.size(); i++) {
            if (pow_mod(a, (n-1)/ProstMnUnik[i], n) == 1) {
                PoklingF2 = false;
                break;
            }
            else {
                PoklingF2 = true;
            }
        }
        if (PoklingF1==true && PoklingF2==true) return true;
    }
    return false;
}

bool GOST(int t, int q1) {
    int p = 0;

    while (true) {
        int N1 = ceil(pow(2, t - 1) / q1);
        int N2 = ceil(pow(2, t - 1) * 0/ (q1));

        double N = N1 + N2;
        if (static_cast<int>(round(N)) % 2 != 0) {
            N++;
        }

        for (int u = 0; pow(2, t) >= (N + u) * q1 + 1; u += 2) {
            p = (N + u) * q1 + 1;
            if ((pow_mod(2, p - 1, p) == 1) && (pow_mod(2, N + u, p) != 1)) {
                return true;
            }
        }
    }
    return false;
}

bool VerTest(int n, int t, int R, int F) {
    if (NOD(R,F) == 1) {
        double verMiller = (static_cast<double>(Eiler(n-1))/static_cast<double>(n-1)) * t;
        double verPoklington = (static_cast<double>(Eiler(n))/static_cast<double>(n)) * t;
        double result = 1 - verMiller - verPoklington;
        return (result <= 0.1);
    }
    else {
        double verMiller = (static_cast<double>(Eiler(n-1))/static_cast<double>(n-1)) * t;
        double result = 1 - verMiller;
        return (result <= 0.1);
    }
}

void InPut(int n, bool VerTest, int k) {
    if (VerTest && k <= 6) {
        cout << n << " \t\t" << "+" << " \t\t" << k << endl;
    }
    else {
        cout << n << " \t\t" << "-" << " \t\t" << k << endl;
    }
}

int main() {
    srand(time(0));

    vector<int> ProstCh;
    Eratosphen(ProstCh);
    int t = 5;
    int t1 = 4;
    int q1 = 3;
    int k = 0;
    cout << "Число|\tРезультат проверки|\tКоличество отвергнутых чисел" << endl;
    cout << "-------------------------------------------------------" << endl;
    for (int i = 0; i < 10; i++) {
        vector<int> ProstMnUnik;
        vector<int> ProstMn;
        int n = Random(2, 500 - 2);
        int n1 = n - 1;
        int m = 1;
        m = MillerRazlozh(m, n1, ProstCh, ProstMnUnik, ProstMn);
        int F = 1;
        int R = 1;
        PoklingtonRazlozh(n, F, R, ProstMn);

        if (!Miller(n, ProstMnUnik, t) || !Poklington(n, F, R, ProstMnUnik, t)) {
            k++;
            i--;
            continue;
        }
        bool GOSTResult = GOST(t, q1);

        InPut(n, VerTest(n, t, R, F), k);

        if (Miller(n, ProstMnUnik, t) && Poklington(n, F, R, ProstMnUnik, t)) {
            k=0;
        }
    }
}