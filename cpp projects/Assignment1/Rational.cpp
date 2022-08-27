//
// Created by Daniel Eni Kandil on 31/07/2022.
//

#include "Rational.h"

//  -----   constructors & destructor: -----

Rational::Rational(): _nom(Polynomial()), _denom(Polynomial()){
    if (DEBUG) cout << "Creating default Rational..." << endl;
}

Rational::Rational(Polynomial nom, Polynomial denom): _nom(nom), _denom(denom) {
    if (DEBUG) cout << "Creating Rational... with " << nom.getDegree(false) << "Nominator degree and " << denom.getDegree(false) <<
        "Denominator degree " << endl;
}

Rational::~Rational() {
    if (DEBUG) cout << "Destructor Rational..." << endl;

}

//  -----   getters: -----

const Polynomial &Rational::getDenom() {
    return _denom;
}

const Polynomial &Rational::getNom() {
    return _nom;
}

//  -----   functional methods: -----

void Rational::print() {
    getNom().print();
    cout << "--------------------------" << endl;
    if (getDenom().getDegree(true) != 0)
        getDenom().print();
    else
        cout << "polynomial = 1" << endl;
}




