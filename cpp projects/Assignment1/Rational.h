//
// Created by Daniel Eni Kandil on 31/07/2022.
//

#ifndef ASSIGNMENT1_RATIONAL_H
#define ASSIGNMENT1_RATIONAL_H


class Rational {
private:
    Polynomial _denom;
    Polynomial _nom;
public:
    Rational();
    Rational(Polynomial, Polynomial);
//    Rational(const Rational &); // todo: implement copy constructor
    ~Rational(); //todo: implement
    const Polynomial &getDenom();
    const Polynomial &getNom();
    void print();
};

#endif //ASSIGNMENT1_RATIONAL_H
