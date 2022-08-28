//
// Created by Daniel Eni Kandil on 31/07/2022.
//
#include <iostream>
#include <sstream>
#include "Polynomial.h"

using namespace std;
const bool DEBUG = false;
int Polynomial::COUNTER;
Polynomial::totMaxDegList *Polynomial::TOTAL_MAX_DEGREE;

//  -----   constructors & destructor: -----
Polynomial::Polynomial() : _maxDegree(0), _currDegNodePtr(nullptr) {
    ++COUNTER;
    if (DEBUG) cout << "Creating default (" << COUNTER << ") Polynomial..." << endl;
    setPolynomial(true, nullptr,0);
}

Polynomial::Polynomial(int maxDegree) : _maxDegree(maxDegree), _currDegNodePtr(nullptr), _currentDegree(0) {
    ++COUNTER;
    if (DEBUG) cout << "Creating (" << COUNTER << ") Polynomial... with " << maxDegree << " max degree" << endl;
    setPolynomial( true, nullptr,  0);
}

Polynomial::Polynomial(const double *coeff, int maxDegree) : _maxDegree(maxDegree), _currDegNodePtr(nullptr) {
    ++COUNTER;
    if (DEBUG) cout << "Creating (" << COUNTER << ") Polynomial... with " << maxDegree << " max degree and given coeffs" << endl;
    setPolynomial( true, coeff,-1);
}

Polynomial::Polynomial(const Polynomial & poly) : _maxDegree(poly.getDegree(false)), _currDegNodePtr(nullptr) {
    ++COUNTER;
    if (DEBUG) cout << "Creating a copy (" << COUNTER << ") of Polynomial... with " << getDegree(false) << " max degree" << endl;
    setPolynomial(true, poly.getCoeff(), poly.getDegree(true));
}


bool Polynomial::setPolynomial(bool isNew, const double *coeff, int currentDegree) {
    if (DEBUG) cout << "setPolynomial (counter " << COUNTER << ") of Polynomial... with " << getDegree(false) << " max degree" << endl;
    double tempMaxDeg = getDegree(false);
    if (!isNew) {
        delete[](getCoeff());
        this->_maxDegree = tempMaxDeg;
    }
        this->_coeff = new double[tempMaxDeg+1]{0}; // create array with zeros coeffs
    for (int i = 0; coeff != nullptr && i <= getDegree(false); ++i) {
        this->_coeff[i] = coeff[i];
    }
    setCurrentDegree(currentDegree);
    return false;
}

Polynomial::~Polynomial(){
    if (DEBUG) cout << "destructor (size before delete: " << COUNTER << ") Polynomial... with " << _maxDegree << " max deg" << endl;
    --COUNTER;
    delete[](getCoeff());
    totMaxDegList* ptr = getCurrDegNodePtr();
    if (COUNTER > 0){
        detachNode(&ptr, &TOTAL_MAX_DEGREE);
        delete(ptr);
    } else {
        delete(TOTAL_MAX_DEGREE);
        TOTAL_MAX_DEGREE = nullptr;
    }
}

//  -----   getters: -----
double Polynomial::getDegree(bool b) const { // return current with true, max with false
    if (b)
        return _currentDegree;
    else
        return _maxDegree;
}

const double Polynomial::getCoeff(int i) const {
    if (getCoeff() && i >= 0 && i <= _maxDegree)
        return _coeff[i];
    else
        return INT32_MIN;
}

double* Polynomial::getCoeff() const {
    if (_coeff != nullptr)
        return _coeff;
    else
        return nullptr;
}

int Polynomial::getMaxDegree() {
    if (TOTAL_MAX_DEGREE)
        return TOTAL_MAX_DEGREE->_data;
    return 0;
}

Polynomial::totMaxDegList* Polynomial::getCurrDegNodePtr() const{
    return _currDegNodePtr;
}

//  -----   setters: -----

void Polynomial::setCoeff(int i, double d) {
    if (DEBUG) cout << "setCoeff... with _coeff[" << i << "]=" << d << endl;
    if (getCoeff() != nullptr && i >= 0 && i <= _maxDegree) { // index is positive (or 0) and smaller than max-degree
        _coeff[i] = d;
        if (d && i > getDegree(
                true)) // set current degree when given number is NOT 0 AND we set a bigger degree than current one
            setCurrentDegree(i);
        else if (!d && i == getDegree(true)) // given number is zero AND we set current degree's number
            setCurrentDegree(-1); // -1 will go through all the polynom and set the current degree
    } else throw "set _coeff failed: over\\underflow";
}

// set new degree for this
// parameter
//  newDegree => new degree to set polynomial
//      if newDegree is -1, it will set the degree automatically.
void Polynomial::setCurrentDegree(double newDegree) {
    if (DEBUG) cout << "setCurrentDegree... with " << newDegree << " current degree" << endl;
//    int initialDegree = getDegree(true);
    int tempDeg = 0;
    if (newDegree >= 0 && newDegree <= _maxDegree)
        tempDeg = newDegree;
    else if (newDegree == -1) {
        for (int i = _maxDegree; getCoeff() != nullptr && i >= 0; --i) {
            if (this->getCoeff(i) != 0) {
                tempDeg = i;
                break;
            }
        }
    } _currentDegree = tempDeg;
    updateDegreeList(&_currDegNodePtr, _currentDegree);
}

//  -----   overloading operators: -----

Polynomial Polynomial::operator+(const Polynomial &right) const {
    if (DEBUG) cout << "Operator +... with this " << this->getDegree(false) << " degree and right "
        << right.getDegree(false) << " degree" << endl;
    if (&right == nullptr)
        throw "operand is null";
        Polynomial temp (right.getDegree(false) > this->getDegree(false)? right.getDegree(false): this->getDegree(false));
        for (int i = 0; i <= temp.getDegree(false); ++i) { // todo: verify it works with Addition of two coefficients that results zero
            if(this->getCoeff(i) != INT32_MIN && right[i] != INT32_MIN)
                temp.setCoeff(i, (this->getCoeff(i) + right[i]));
            else if (this->getCoeff(i) == INT32_MIN)
                temp.setCoeff(i, right[i]);
            else
                temp.setCoeff(i, this->getCoeff(i));
        }
    return temp;
}

Polynomial Polynomial::operator-(const Polynomial & right) const {
    if (DEBUG) cout << "Operator -... with this " << this->getDegree(false) << " degree and right "
                    << right.getDegree(false) << " degree" << endl;
    if (&right == nullptr)
        throw "operand is null";
    Polynomial temp (right.getDegree(false) > this->getDegree(false)? right.getDegree(false): this->getDegree(false));
    for (int i = 0; i <= temp.getDegree(false); ++i) {
        if(this->getCoeff(i) != INT32_MIN && right[i] != INT32_MIN)
            temp.setCoeff(i, (this->getCoeff(i) - right[i]));
        else if (this->getCoeff(i) == INT32_MIN)
            temp.setCoeff(i, right[i]);
        else
            temp.setCoeff(i, this->getCoeff(i));
    }
    return temp;
}

Polynomial Polynomial::operator*(const Polynomial & right) const { // todo!!!
    if (DEBUG) cout << "Operator * ... with this " << this->getDegree(false) << " degree and right "
                    << right.getDegree(false) << " degree" << endl;
    double maxDeg = this->getDegree(false) + right.getDegree(false);
    double *c = new double[maxDeg]{0};
    if (&right == nullptr)
        throw "operand is null";
    for (int i = 0; i <= this -> getDegree(false); ++i) {
        for (int j = 0; j <= right.getDegree(false); ++j) {
            c[i+j] += this->getCoeff(i) * right.getCoeff(j);
        }
    }
    return Polynomial(c,maxDeg);
}

Polynomial operator*(double left , const Polynomial& right){
    if (DEBUG) cout << "Operator +... with this " << left << " value and right "
                    << right.getDegree(false) << " degree" << endl;
    double maxDeg = right.getDegree(false);
    double *c = new double[maxDeg]{0};
    for (int i = 0; i <= maxDeg; ++i) {
        c[i] = left * right[i];
    }
    return Polynomial(c,maxDeg);
}


double Polynomial::operator[](int i) {
    return this->getCoeff(i);
}

const double& Polynomial::operator[](int i) const {
    return this->getCoeff(i);
}

const Polynomial &Polynomial::operator=(const Polynomial & right) {
        if (DEBUG)
        _maxDegree = right.getDegree(false);
        setPolynomial(false,right.getCoeff(),right.getDegree(true));
    return *this;
}

ostream & operator<<(ostream & out, const Polynomial & poly){
    out << poly.toString() << endl;
    return out;
}

//  -----   functional methods: -----
void Polynomial::print() const {
    cout << this->toString() << endl;
}


string Polynomial::toString() const {
    std::ostringstream out;
    string sign = "";
    if (this -> getDegree(true) >= 0){
        out << "polynomial = " << (_coeff == nullptr ? 0 : this -> getCoeff(0));
        for (int i = 1; i < this ->getDegree(true) + 1; ++i) {
            sign = getCoeff(i) >= 0 ? "+" : "-";
            out << " " << sign << " " << abs(getCoeff(i)) << "x";
            switch(i) {
                case 1: break;
                case 2:   out << "²";  break;
                case 3:   out << "³";  break;
                case 4:   out << "⁴";  break;
                case 5:   out << "⁵";  break;
                case 6:   out << "⁶";  break;
                case 7:   out << "⁷";  break;
                case 8:   out << "⁸";  break;
                case 9:   out << "⁹";  break;
                case 10:  out << "¹⁰";  break;
                case 11:  out << "¹¹";  break;
                case 12:  out << "¹²";  break;
                case 13:  out << "¹³";  break;
                default:  out << "^" << to_string(i); break;
            }
        }
    }
    return out.str();
}

//  -----  handling max degree list methods: -----

void Polynomial::updateDegreeList(totMaxDegList **formerNode, double data) {
    if (DEBUG) cout << "updateDegreeList (" << COUNTER << ") Polynomial... with " << data << " data" << endl;
    totMaxDegList* ptr = TOTAL_MAX_DEGREE;
    if (formerNode != nullptr && (*formerNode) != nullptr && (*formerNode)->_data == data)
        return;
    if (formerNode == nullptr || (*formerNode) == nullptr) { // new degree need to be created
        if ( TOTAL_MAX_DEGREE == nullptr ||  // list of nodes is empty
            data > TOTAL_MAX_DEGREE->_data ) // OR - current degree is higher than first node
            createNode(data, nullptr); // create node in first place and update TOTAL MAX DEGREE Pointer
        else {
            for (; ptr != nullptr; ptr = ptr->_next) {
                if (ptr->_data >= data && (ptr->_next == nullptr || ptr->_next->_data < data))
                    createNode(data, &ptr);
                return;
            }
        }
    } else { // need to update the degree in the list
        detachNode(formerNode, &TOTAL_MAX_DEGREE);
        (*formerNode)->_data = data;
        if (data >= TOTAL_MAX_DEGREE->_data && TOTAL_MAX_DEGREE != getCurrDegNodePtr()) {
            (*formerNode)->_next = TOTAL_MAX_DEGREE;
            //ptr->_next = nullptr;
            TOTAL_MAX_DEGREE = (*formerNode);
        } else {
            for(; ptr != nullptr; ptr = ptr->_next){
                if (ptr->_data > data && (ptr->_next == nullptr || ptr->_next->_data <= data)) {
                    (*formerNode)->_next  = ptr->_next;
                    ptr->_next = *formerNode;
                    return;
                }
            }
        }
    }
}

void Polynomial::createNode(int data, totMaxDegList **prev) {
    totMaxDegList *newNode = new totMaxDegList;
    totMaxDegList *tempPtr;
    newNode->_data = data;
    if (TOTAL_MAX_DEGREE == nullptr) {// initialize list
        newNode->_next = nullptr;
        TOTAL_MAX_DEGREE = newNode;
    }
    else if (prev == nullptr) { // creating first element
        tempPtr = TOTAL_MAX_DEGREE;
        newNode->_next = tempPtr;
        TOTAL_MAX_DEGREE = newNode;
    } else {
        newNode->_next = (*prev)->_next;
        (*prev)->_next = newNode;
    }
    _currDegNodePtr = newNode;
}

void Polynomial::detachNode(totMaxDegList **formerNode, totMaxDegList **pList1) {
    totMaxDegList *ptr = *pList1;
    if (pList1 == nullptr || formerNode == nullptr || (*pList1)->_next == nullptr) return; // list empty or have 1 node, or former node doesn't exist
    if (*formerNode == ptr) { // first node
        (*pList1) = (*pList1)->_next;
        (*formerNode)->_next = nullptr;
    } else {
        for(; ptr != nullptr; ptr = ptr->_next){
            if(ptr -> _next != nullptr && ptr -> _next == *formerNode){
                ptr->_next = (*formerNode)->_next;
                (*formerNode)->_next = nullptr;
                return;
            }
        }
    }
}