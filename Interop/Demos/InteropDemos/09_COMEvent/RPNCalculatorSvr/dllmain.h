// dllmain.h : Declaration of module class.

class CRPNCalculatorSvrModule : public ATL::CAtlDllModuleT< CRPNCalculatorSvrModule >
{
public :
	DECLARE_LIBID(LIBID_RPNCalculatorSvrLib)
	DECLARE_REGISTRY_APPID_RESOURCEID(IDR_RPNCALCULATORSVR, "{0F5472F1-D6C3-45B6-8A48-9DD59A5C6826}")
};

extern class CRPNCalculatorSvrModule _AtlModule;
