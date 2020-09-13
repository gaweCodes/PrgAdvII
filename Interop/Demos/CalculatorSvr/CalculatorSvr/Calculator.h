// Calculator.h : Declaration of the CCalculator

#pragma once
#include "resource.h"       // main symbols



#include "CalculatorSvr_i.h"

using namespace ATL;


// CCalculator

class ATL_NO_VTABLE CCalculator :
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CCalculator, &CLSID_Calculator>,
	public ISimpleCalculator,
	public ITrigCalculator {
public:
	CCalculator() : m_Degrees(VARIANT_FALSE) {
		ATLTRACE(_T("Calculator created %p\n"), this);
	}

	DECLARE_REGISTRY_RESOURCEID(IDR_CALCULATOR)


	BEGIN_COM_MAP(CCalculator)
		COM_INTERFACE_ENTRY(ISimpleCalculator)
		COM_INTERFACE_ENTRY(ITrigCalculator)
	END_COM_MAP()



	DECLARE_PROTECT_FINAL_CONSTRUCT()

	HRESULT FinalConstruct() {
		return S_OK;
	}

	void FinalRelease() {
	}

	~CCalculator() {
		ATLTRACE(_T("Calculator destroyed %p\n"), this);
	}

private:
	VARIANT_BOOL m_Degrees;

public:
	STDMETHOD(Add)(int a, int b, int* result);
	STDMETHOD(Sin)(double angle, double* result);
	STDMETHOD(get_Degrees)(VARIANT_BOOL* pVal);
	STDMETHOD(put_Degrees)(VARIANT_BOOL newVal);
};

OBJECT_ENTRY_AUTO(__uuidof(Calculator), CCalculator)
