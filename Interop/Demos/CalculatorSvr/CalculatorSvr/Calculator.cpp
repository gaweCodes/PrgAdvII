// Calculator.cpp : Implementation of CCalculator

#include "stdafx.h"
#include "Calculator.h"
#include <math.h>

// CCalculator

const double PI = 3.14159265358979;

STDMETHODIMP CCalculator::Add(int a, int b, int* result) {
	*result = a + b;

	return S_OK;
}


STDMETHODIMP CCalculator::Sin(double angle, double* result) {
	if(m_Degrees)
		angle = angle * PI / 180;

	*result = ::sin(angle);

	return S_OK;
}


STDMETHODIMP CCalculator::get_Degrees(VARIANT_BOOL* pVal) {
	*pVal = m_Degrees;

	return S_OK;
}


STDMETHODIMP CCalculator::put_Degrees(VARIANT_BOOL newVal) {
	m_Degrees = newVal;

	return S_OK;
}
