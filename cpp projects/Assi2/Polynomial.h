//
// Created by Daniel Eni Kandil on 31/07/2022.
//

class Polynomial {

public:
    struct totMaxDegList {
        int _data;
        totMaxDegList  * _next;
    };
    static totMaxDegList* TOTAL_MAX_DEGREE;
    Polynomial();
    Polynomial(int);
    Polynomial(const double*, int);
    Polynomial(const Polynomial &);
    bool setPolynomial(bool,const double*, int);
    ~Polynomial();

    //getters
    double getDegree(bool b) const; // return current with true, max with false
    const double getCoeff(int i) const;
    double* getCoeff() const;
    totMaxDegList* getCurrDegNodePtr() const;
    static int getMaxDegree();
    string toString() const;

    //overriding operators:
    const Polynomial& operator=(const Polynomial&);
    Polynomial operator+(const Polynomial&) const;
    Polynomial operator-(const Polynomial&) const;
    Polynomial operator*(const Polynomial&) const;
    friend Polynomial operator*(double , const Polynomial&);
    friend ostream & operator<<(ostream &, const Polynomial&);
    const double& operator[](int i) const;
    double operator[](int i);

    void print() const;
    void setCoeff(int, double);
    void createNode(int, totMaxDegList**);

private:
    double _maxDegree;
    double _currentDegree;
    double* _coeff;
    totMaxDegList* _currDegNodePtr;
    static int COUNTER;
    void setCurrentDegree(double);
    void updateDegreeList(totMaxDegList **, double);
    void detachNode(totMaxDegList **, totMaxDegList **);
};
