//
// Created by Daniel Eni Kandil on 31/07/2022.
//

#include "Rational.h"

//  -----   constructors & destructor: -----

Rational::Rational(): _nom(Polynomial()), _denom(Polynomial()){ if (DEBUG) cout << "Creating default Rational..." << endl;}

Rational::Rational(const Polynomial & nom, const Polynomial & denom): _nom(nom), _denom(denom) {
    if (DEBUG) cout << "Creating Rational... with " << nom.getDegree(false) << "Nominator degree and " << denom.getDegree(false) <<
        "Denominator degree " << endl;
}

Rational::Rational(const Rational & r): _nom(r._nom), _denom(r._denom){if (DEBUG) cout << "Copy constructor Rational..." << endl; }

Rational::~Rational() { if (DEBUG) cout << "Destructor Rational..." << endl; }

//  -----   getters: -----

const Polynomial Rational::getDenom() const {
    return _denom;
}

const Polynomial &Rational::getNom() const{
    return _nom;
}

//  -----   functional methods: -----

void Rational::print() {
    cout << toString() << endl;
}

string Rational::toString() const {
    std::ostringstream out;
    out << this->getNom().toString() << endl << "--------------------------" << endl;
    if (getDenom().getDegree(true) != 0)
        out << getDenom().toString();
    else
        out << "polynomial = 1" << endl;
    return out.str();
}

//overriding operators:

const Rational &Rational::operator=(const Rational & right) {
    if (DEBUG) cout << "operator=..." << endl;
    this -> _denom = right.getDenom();
    this -> _nom = right.getNom();
    return *this;
}

Rational Rational::operator+(const Rational & right) const {
    if (DEBUG) cout << "operator+..." << endl;
    Polynomial nom((this->getNom() * right.getDenom()) + right.getNom() * this->getDenom());
    Polynomial denom(this->getDenom() * right.getDenom());
    return Rational(nom,denom);
}

Rational Rational::operator-(const Rational & right ) const {
    if (DEBUG) cout << "operator-..." << endl;
    Polynomial nom((this->getNom() * right.getDenom()) - right.getNom() * this->getDenom());
    Polynomial denom(this->getDenom() * right.getDenom());
    return Rational(nom,denom);
}

Rational Rational::operator*(const Rational & right) const {
    if (DEBUG) cout << "operator*..." << endl;
    Polynomial nom(this->getNom() *  right.getNom() );
    Polynomial denom(this->getDenom() * right.getDenom());
    return Rational(nom,denom);
}

Rational operator*(double left, const Rational & right) {
    if (DEBUG) cout << "operator*  ..." << endl;
    Polynomial nom(left *  right.getNom() );
    return Rational(nom, right.getDenom());
}

ostream &operator<<(ostream &out, const Rational &rational) {
    if (DEBUG) cout << "operator<<..." << endl;
    out << rational.toString();
    return out;
}






