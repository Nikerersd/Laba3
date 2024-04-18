#include <iostream>
#include <math.h>
#include <vector>
#include <iomanip>

using namespace std;

void cofe(double Tk, double Tsr, double r, int t, vector<double>& Cofe) {
    for (int i = 0; i<=t; i++) {
        Cofe.push_back(Tk);
        Tk -= r * (Tk - Tsr);
    }
}

double aproxA(const vector<double>& Cofe) {

    double Ys = 0, Xs = 0, XYs = 0, X2s = 0;
    int n = Cofe.size();

    for (int i = 0; i < n; i++) {
        Ys += Cofe[i];
        Xs += i;     

        XYs += Cofe[i] * i;
        X2s += i*i;
    }
    return (n * XYs - (Xs * Ys)) / (n * X2s - Xs * Xs);
}

double aproxB(const vector<double>& Cofe) {
    double Ys = 0, Xs = 0;
    double a = aproxA(Cofe);
    int n = Cofe.size();

    for (int i = 0; i < n; i++) {
        Ys += Cofe[i];
        Xs += i;
    }
    return (Ys - a * Xs) / n;
}

double korrel(const vector<double>& Cofe) {
    int n = Cofe.size();
    double sumT = 0.0;
    for (double T : Cofe) {
        sumT += T;                              
    }
    double SrT = sumT/n;
    double Srt = (n-1) * n / 2;
    double XYs = 0.0;
    double Xs2 = 0.0;
    double Ys2 = 0.0;

    for (int i = 0; i < n; i++) {
        double vremX = i - Srt;
        double vremY = Cofe[i] - SrT;

        XYs += vremX * vremY;
        Xs2 += vremX * vremX;
        Ys2 += vremY * vremY;
    }
    double r = XYs / sqrt(Xs2 * Ys2);
    return r;
}

int main() {
    double Tk, Tsr, r;
    int t;
    vector<double> Cofe;
    cout << "Введите температуру кофе и среды, коэффициент остывания и время остывания в минутах через пробел" << endl;
    cin >> Tk >> Tsr >> r >> t;
    cofe (Tk, Tsr, r, t, Cofe);
    double a = aproxA(Cofe);
    double b = aproxB(Cofe);
    double korr = korrel(Cofe);
    t = 0;
    cout << setw(19) << "Время" << setw(25) << "Температура" << endl;
    for (double T : Cofe) {
        cout << setw(10) << t << setw(10) << T << endl;
        t++;
    }
    cout << "Линия аппроксимации: T = " << a << " * t + " << b << endl;
    cout << "Коэффициент корреляции: " << korr << endl;
}