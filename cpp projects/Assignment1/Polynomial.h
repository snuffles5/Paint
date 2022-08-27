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
    Polynomial(const Polynomial &); // todo: implement copy constructor
    ~Polynomial();
    double getDegree(bool b) const;
    double getCoeff(int i) const;
    double* getCoeff() const;
    totMaxDegList* getCurrDegNodePtr() const;
    static int getMaxDegree();
    void print() const;
    void setCoeff(int, double);
//    static int getTotalMaxDegree();
//    void deleteNextNode(totMaxDegList *);
    void createNode(int, totMaxDegList**);
//    Polynomial &operator=(const	Polynomial	&);

private:
    const double _maxDegree;
    double _currentDegree;
    double* _coeff;
    totMaxDegList* _currDegNodePtr;
    static int COUNTER;
    void setCurrentDegree(double);
    void updateDegreeList(totMaxDegList **, double);
    void detachNode(totMaxDegList **, totMaxDegList **);
};
