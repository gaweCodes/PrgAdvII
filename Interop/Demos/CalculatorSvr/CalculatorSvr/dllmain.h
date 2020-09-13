// dllmain.h : Declaration of module class.

class CCalculatorSvrModule : public ATL::CAtlDllModuleT< CCalculatorSvrModule >
{
public :
	DECLARE_LIBID(LIBID_CalculatorSvrLib)
	DECLARE_REGISTRY_APPID_RESOURCEID(IDR_CALCULATORSVR, "{F7734521-811D-4EB2-8115-B1D476BDE302}")
};

extern class CCalculatorSvrModule _AtlModule;
