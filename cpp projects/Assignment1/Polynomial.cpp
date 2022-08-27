//
// Created by Daniel Eni Kandil on 31/07/2022.
//
#include <iostream>
#include "Polynomial.h"

using namespace std;
const bool DEBUG = false;
int Polynomial::COUNTER;
Polynomial::totMaxDegList *Polynomial::TOTAL_MAX_DEGREE;

//  -----   constructors & destructor: -----
Polynomial::Polynomial() : _maxDegree(0), _currentDegree(0), _coeff(nullptr), _currDegNodePtr(nullptr) {
    if (DEBUG) cout << "Creating default (" << ++COUNTER << ") Polynomial..." << endl;
    updateDegreeList(nullptr,_currentDegree);
}

Polynomial::Polynomial(int maxDegree) : _maxDegree(maxDegree), _coeff(nullptr), _currDegNodePtr(nullptr), _currentDegree(0) {
    if (DEBUG) cout << "Creating (" << ++COUNTER << ") Polynomial... with " << maxDegree << " max degree" << endl;
    setCurrentDegree(0);
    this->_coeff = new double[maxDegree](); // create array with zeros coeffs
    updateDegreeList(nullptr, _currentDegree);
}

Polynomial::Polynomial(const double *coeff, int maxDegree) : _maxDegree(maxDegree), _currDegNodePtr(nullptr) {
    if (DEBUG) cout << "Creating (" << ++COUNTER << ") Polynomial... with " << maxDegree << " max degree and given coeffs" << endl;
    this->_coeff = new double[maxDegree](); // create array with zeros coeffs
    if (coeff == nullptr) _currentDegree = 0;
    else
        for (int i = 0; i < maxDegree + 1; ++i) {
            this->_coeff[i] = coeff[i];
            if (getCoeff(i) != 0)
                _currentDegree = i;
    }
    updateDegreeList(nullptr, _currentDegree);
}

Polynomial::Polynomial(const Polynomial & poly) : _maxDegree(poly.getDegree(false)), _currDegNodePtr(nullptr) {
    if (DEBUG) cout << "Creating a copy (" << ++COUNTER << ") of Polynomial... with " << getDegree(false) << " max degree" << endl;
    setCurrentDegree(poly.getDegree(true));
    this->_coeff = new double[_maxDegree]();
    if (_coeff == nullptr) _currentDegree = 0;
    else
        for (int i = 0; i < _maxDegree + 1; ++i) {
            this->_coeff[i] = _coeff[i];
            if (getCoeff(i) != 0)
                _currentDegree = i;
        }
    updateDegreeList(nullptr, poly.getDegree(true));
}


Polynomial::~Polynomial(){
    if (DEBUG) cout << "destructor (" << --COUNTER << ") Polynomial... with " << _maxDegree << endl;
    delete[](getCoeff());
    totMaxDegList* ptr = getCurrDegNodePtr();
    if (COUNTER != 0){
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

double Polynomial::getCoeff(int i) const {
    if (getCoeff() && i >= 0 && i < _maxDegree + 1)
        return _coeff[i];
    else
        return -1;
//    throw "get _coeff failed: over\\underflow";
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

void Polynomial::setCurrentDegree(double newDegree) {
    if (DEBUG) cout << "setCurrentDegree... with " << newDegree << " current degree" << endl;
    int initialDegree = getDegree(true);
    if (newDegree >= 0 && newDegree <= _maxDegree)
        this->_currentDegree = newDegree;
    else {
        this->_currentDegree = 0;
        for (int i = 0; getCoeff() && i < _maxDegree; ++i) {
            if (getCoeff(i) != 0)
                this->_currentDegree = i;
        }
    }
    updateDegreeList(&_currDegNodePtr, this->_currentDegree);
}

//  -----   functional methods: -----
void Polynomial::print() const {
    string sign = "";
    if (getDegree(true)) {
        cout << "polynomial = " << getCoeff(0);
        for (int i = 1; i < getDegree(true) + 1; ++i) {
            sign = getCoeff(i) >= 0 ? "+" : "";
            cout << sign << getCoeff(i) << "X^" << i;
        }
    } else
        cout << "polynomial = 0";
    cout << endl;
}

//  -----  handling max degree list methods: -----

void Polynomial::updateDegreeList(totMaxDegList **formerNode, double data) {
    if (DEBUG) cout << "updateDegreeList (" << COUNTER << ") Polynomial... with " << data << " _data" << endl;
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