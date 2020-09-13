// RPNCalculator.cpp : Implementation of CRPNCalculator

#include "stdafx.h"
#include "RPNCalculator.h"


// CRPNCalculator



STDMETHODIMP CRPNCalculator::Push(double value) {
	_stack.push(value);

	return S_OK;
}


STDMETHODIMP CRPNCalculator::Pop(double* value) {
	if(_stack.empty())
		return E_UNEXPECTED;

	*value = _stack.top();
	_stack.pop();

	return S_OK;
}


STDMETHODIMP CRPNCalculator::Add() {
	if(_stack.size() < 2) {
		Fire_StackEmpty();
	}
	else {
		double x, y;
		Pop(&x);
		Pop(&y);
		Push(x + y);
	}
	return S_OK;
}


STDMETHODIMP CRPNCalculator::Mul() {
	if(_stack.size() < 2) {
		Fire_StackEmpty();
	}
	else {
		double x, y;
		Pop(&x);
		Pop(&y);
		Push(x * y);
	}

	return S_OK;
}
