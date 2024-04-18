#include <iostream>
#include <fstream>
#include <cmath>
#include <iomanip>
using namespace std;
double firstFragm(double x) {
    double k = 1.0/3.0;
    double res = k*(-x-2);
    if (abs(res) < 0.000001) {
        res = 0.0;
    }
    return res;
}
double secondFragm(double x) {
    return(log(abs(1.0/tan(x/2.0))));
}
double thirdFragm(double x) {
    double k = 1.0/3.0;
    double res = k*(x-2);
    if (abs(res) < 0.000001) {
        res = 0.0;
    }
    return res;
}
int main() {
    double xNach = -5;
    double xKon = 5;
    double dx = 0.1;

    ofstream outfile("1Task_24Var.txt");
    if (!outfile.is_open()) {
        cout << "При открытии файла произошла ошибка" << endl;
        return 1;
    }

    outfile << setw(10) << "X" << setw(10) << "Y" << endl;
    outfile << string (20, '-') << endl;
    for (double x = xNach; x <= xKon ; x += dx) {
        if (abs(x) < 0.000001) {
        x = 0.0;
        }
        if (x >= -5 && x < -2) {
            outfile << setw(10)  << x << setw(10) << firstFragm(x) << endl;
        }
        if (x >= -2 && x < 2) {
            outfile << setw(10) << x << setw(10) << secondFragm(x) << endl;
        }
        if (x >= 2 && x < 5){
            outfile << setw(10) << x << setw(10) << thirdFragm(x) << endl;
        }
    }
}