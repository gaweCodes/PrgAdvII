// SampleNativeLib.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"

int* g_pData;

extern "C" __declspec(dllexport) int WINAPI DoCalc() {
	int sum = 0;
	for(int i = 0; i < 10; i++)
		sum += g_pData[i];
	return sum;
}

extern "C" __declspec(dllexport) int WINAPI SetData(int* data) {
	g_pData = data;
	return DoCalc();
}


