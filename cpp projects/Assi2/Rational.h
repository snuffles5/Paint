//
// Created by Daniel Eni Kandil on 31/07/2022.
//

#ifndef ASSIGNMENT2_RATIONAL_H
#define ASSIGNMENT2_RATIONAL_H


class Rational {
private:
    Polynomial _denom;
    Polynomial _nom;
public:
    Rational();
    Rational(const Polynomial &, const Polynomial &);
    Rational(const Rational &);
    ~Rational();
    //getters
    const Polynomial getDenom() const;
     const Polynomial &getNom() const;
    //functional
    void print();
    string toString() const;

    //overriding operators:
    const Rational& operator=(const Rational&);
    Rational operator+(const Rational&) const;
    Rational operator-(const Rational&) const;
    Rational operator*(const Rational&) const;
    friend Rational operator*(double , const Rational&);
    friend ostream & operator<<(ostream &, const Rational&);
};

#endif //ASSIGNMENT2_RATIONAL_H
