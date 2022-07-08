/******************************************************************************

                              Online C++ Compiler.
               Code, Compile, Run and Debug C++ program online.
Write your code in this editor and press "Run" button to compile and execute it.

*******************************************************************************/

#include <iostream>
using namespace std;
#include <iomanip>
typedef struct {
    int h;
    int m;
    int s;
} Time;

void printTime(Time t);
void convertTime(int& h, int& m, int& s);
int inputTime(Time**);
void printTimes(Time* time, int n);

int main()
{
    int h = 55 ,m = 555, s = 128, sizeOfTimes;
    Time* times;
    Time t = {h,m,s};
    printTime(t);
    convertTime(h,m,s);
    t = {h,m,s};
    printTime(t);
    
    sizeOfTimes = inputTime(&times);
    printTimes(times, sizeOfTimes);
    cout << endl;
    return 0;
}

int inputTime(Time** times) // 3 20 80 60 5 68 125 3 7 350
{
    int n;
    cout << "how many values?" << endl;
    cin >> n;
    *times = new Time[n];
    for (int i = 0; i < n; i++) {
        cout << "enter " << i+1 << " hours, mins, seconds" << endl;
        cin >> (*times)[i].h;
        cin >> (*times)[i].m;
        cin >> (*times)[i].s;
        convertTime((*times)[i].h, (*times)[i].m,(*times)[i].s);
    }
    return n;
}

void convertTime(int& h, int& m, int& s)
{
    int sum = h*60*60 + m*60 + s;
    // cout << "sum:" << sum << endl;
    h = sum/3600; 
    // cout << "h:" << h << endl;
    sum -= h*60*60;
    // cout << "sum:" << sum << endl;
    m = sum/60; 
    sum -= m*60;
    s = sum;
}
void printTime(Time t)
{
    cout << setfill('0') << setw(2) << t.h%24 << ":" << setfill('0') << setw(2) << t.m << ":" << setfill('0') << setw(2) << t.s << endl;
}

void printTimes(Time* time, int n){
    for (int i = 0; i < n; i++) {
        cout << setfill('0') << setw(2) << time[i].h % 24 << ":" << setfill('0') << setw(2) << time[i].m << ":"
             << setfill('0') << setw(2) << time[i].s << endl;
    }
}

