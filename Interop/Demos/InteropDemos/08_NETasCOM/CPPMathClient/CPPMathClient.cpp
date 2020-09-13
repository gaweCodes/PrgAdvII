// CPPMathClient.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#import "csmathlibrary.tlb" no_namespace raw_interfaces_only

int _tmain(int argc, _TCHAR* argv []) {
	::CoInitialize(0);

	ICalculator* pCalc;
	HRESULT hr = ::CoCreateInstance(__uuidof(Calculator), nullptr, CLSCTX_ALL, __uuidof(ICalculator), (void**)&pCalc);
	if(SUCCEEDED(hr)) {
		long result;
		pCalc->Multiply(3, 4, &result);
		printf("Result: %d\n", result);
		pCalc->Release();
	}
	::CoUninitialize();

	return 0;
}

