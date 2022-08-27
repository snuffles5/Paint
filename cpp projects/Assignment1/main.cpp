#include <iostream>
//#include "Polynomial.h"
#include "Polynomial.cpp" // todo 1 - understand why including .cpp is needed
#include "Rational.h"
#include "Rational.cpp" // todo 1 - (same as above) understand why including .cpp is needed
double getDegree(const Polynomial& p, bool what)
{
    return p.getDegree(what);
}
double getCoeff(const Polynomial& p, int index)
{
    return p.getCoeff(index);
}
void testPolynomial()
{
    cout << "----- start testPolynomial ---------" << endl;
    cout << "test0 :" << Polynomial::getMaxDegree() << endl;
    Polynomial p1;
    cout << "test1: ";
    p1.print();
    Polynomial p2(6);
    p2.setCoeff(2, 0.4);
    cout << "test2: ";
    p2.print();
    cout << "degree=" << p2.getDegree(false) << endl;
    double c[] = { 0.1, 0.2, 0 ,11.5, 1.3, 0, 0 };
    Polynomial p3(c, 5);
    int val[2];
    val[0] = getDegree(p3, true);
    val[1] = getDegree(p3, false);
    cout << "test3: ";
    p3.print();

    p2.setCoeff(2, 0.0);
    cout << "test4: ";
    p2.print();
    cout << "test6 :_maxDegree = " << Polynomial::getMaxDegree() << endl;
    cout << "______________" << endl;
}
void testRational() {
    cout << "----- start testRational ---------" << endl;
    cout << "test7" << endl;
    Rational r1;
    r1.print();
    double c[] = { 0,2,2,3,4,5 };
    Polynomial p1(c, 5);
    c[0] = 1.2;
    Polynomial p2(c, 3);
    Rational r2(p1, p2);
    cout << "test8" << endl;
    r2.print();
    cout << "test9" << endl;
    cout << "_nom = ";
    r2.getNom().print();
    double val[5];
    val[0]= getCoeff(r2.getNom(),1);
    val[1] = getCoeff(r2.getNom(), 5);
    cout << "_denom = ";
    r2.getDenom().print();
    val[2] = getCoeff(r2.getDenom(),0);
    val[3] = getCoeff(r2.getDenom(), 15);
    val[4] = getCoeff(r2.getDenom(), -1);
    cout << "test10 :_maxDegree = " << Polynomial::getMaxDegree() << endl;
    cout << "__________________________" << endl;
}
int main()
{
    testPolynomial();
    testRational();
}
