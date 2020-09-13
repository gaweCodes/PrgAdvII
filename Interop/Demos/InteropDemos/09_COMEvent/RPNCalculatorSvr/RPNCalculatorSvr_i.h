

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 8.00.0603 */
/* at Sun Sep 09 17:53:55 2018
 */
/* Compiler settings for RPNCalculatorSvr.idl:
    Oicf, W1, Zp8, env=Win32 (32b run), target_arch=X86 8.00.0603 
    protocol : dce , ms_ext, c_ext, robust
    error checks: allocation ref bounds_check enum stub_data 
    VC __declspec() decoration level: 
         __declspec(uuid()), __declspec(selectany), __declspec(novtable)
         DECLSPEC_UUID(), MIDL_INTERFACE()
*/
/* @@MIDL_FILE_HEADING(  ) */

#pragma warning( disable: 4049 )  /* more than 64k source lines */


/* verify that the <rpcndr.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCNDR_H_VERSION__
#define __REQUIRED_RPCNDR_H_VERSION__ 475
#endif

#include "rpc.h"
#include "rpcndr.h"

#ifndef __RPCNDR_H_VERSION__
#error this stub requires an updated version of <rpcndr.h>
#endif // __RPCNDR_H_VERSION__

#ifndef COM_NO_WINDOWS_H
#include "windows.h"
#include "ole2.h"
#endif /*COM_NO_WINDOWS_H*/

#ifndef __RPNCalculatorSvr_i_h__
#define __RPNCalculatorSvr_i_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __IRPNCalculator_FWD_DEFINED__
#define __IRPNCalculator_FWD_DEFINED__
typedef interface IRPNCalculator IRPNCalculator;

#endif 	/* __IRPNCalculator_FWD_DEFINED__ */


#ifndef ___IRPNCalculatorEvents_FWD_DEFINED__
#define ___IRPNCalculatorEvents_FWD_DEFINED__
typedef interface _IRPNCalculatorEvents _IRPNCalculatorEvents;

#endif 	/* ___IRPNCalculatorEvents_FWD_DEFINED__ */


#ifndef __RPNCalculator_FWD_DEFINED__
#define __RPNCalculator_FWD_DEFINED__

#ifdef __cplusplus
typedef class RPNCalculator RPNCalculator;
#else
typedef struct RPNCalculator RPNCalculator;
#endif /* __cplusplus */

#endif 	/* __RPNCalculator_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"

#ifdef __cplusplus
extern "C"{
#endif 


#ifndef __IRPNCalculator_INTERFACE_DEFINED__
#define __IRPNCalculator_INTERFACE_DEFINED__

/* interface IRPNCalculator */
/* [unique][nonextensible][oleautomation][uuid][object] */ 


EXTERN_C const IID IID_IRPNCalculator;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("AE7CC251-1F28-49D3-A5E9-12035F491874")
    IRPNCalculator : public IUnknown
    {
    public:
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE Push( 
            /* [in] */ double value) = 0;
        
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE Pop( 
            /* [retval][out] */ double *value) = 0;
        
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE Add( void) = 0;
        
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE Mul( void) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct IRPNCalculatorVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IRPNCalculator * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IRPNCalculator * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IRPNCalculator * This);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *Push )( 
            IRPNCalculator * This,
            /* [in] */ double value);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *Pop )( 
            IRPNCalculator * This,
            /* [retval][out] */ double *value);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *Add )( 
            IRPNCalculator * This);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *Mul )( 
            IRPNCalculator * This);
        
        END_INTERFACE
    } IRPNCalculatorVtbl;

    interface IRPNCalculator
    {
        CONST_VTBL struct IRPNCalculatorVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IRPNCalculator_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IRPNCalculator_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IRPNCalculator_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IRPNCalculator_Push(This,value)	\
    ( (This)->lpVtbl -> Push(This,value) ) 

#define IRPNCalculator_Pop(This,value)	\
    ( (This)->lpVtbl -> Pop(This,value) ) 

#define IRPNCalculator_Add(This)	\
    ( (This)->lpVtbl -> Add(This) ) 

#define IRPNCalculator_Mul(This)	\
    ( (This)->lpVtbl -> Mul(This) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IRPNCalculator_INTERFACE_DEFINED__ */



#ifndef __RPNCalculatorSvrLib_LIBRARY_DEFINED__
#define __RPNCalculatorSvrLib_LIBRARY_DEFINED__

/* library RPNCalculatorSvrLib */
/* [version][uuid] */ 


EXTERN_C const IID LIBID_RPNCalculatorSvrLib;

#ifndef ___IRPNCalculatorEvents_DISPINTERFACE_DEFINED__
#define ___IRPNCalculatorEvents_DISPINTERFACE_DEFINED__

/* dispinterface _IRPNCalculatorEvents */
/* [uuid] */ 


EXTERN_C const IID DIID__IRPNCalculatorEvents;

#if defined(__cplusplus) && !defined(CINTERFACE)

    MIDL_INTERFACE("040E0579-4276-471C-A4F1-DFF83C941D77")
    _IRPNCalculatorEvents : public IDispatch
    {
    };
    
#else 	/* C style interface */

    typedef struct _IRPNCalculatorEventsVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            _IRPNCalculatorEvents * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            _IRPNCalculatorEvents * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            _IRPNCalculatorEvents * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            _IRPNCalculatorEvents * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            _IRPNCalculatorEvents * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            _IRPNCalculatorEvents * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            _IRPNCalculatorEvents * This,
            /* [annotation][in] */ 
            _In_  DISPID dispIdMember,
            /* [annotation][in] */ 
            _In_  REFIID riid,
            /* [annotation][in] */ 
            _In_  LCID lcid,
            /* [annotation][in] */ 
            _In_  WORD wFlags,
            /* [annotation][out][in] */ 
            _In_  DISPPARAMS *pDispParams,
            /* [annotation][out] */ 
            _Out_opt_  VARIANT *pVarResult,
            /* [annotation][out] */ 
            _Out_opt_  EXCEPINFO *pExcepInfo,
            /* [annotation][out] */ 
            _Out_opt_  UINT *puArgErr);
        
        END_INTERFACE
    } _IRPNCalculatorEventsVtbl;

    interface _IRPNCalculatorEvents
    {
        CONST_VTBL struct _IRPNCalculatorEventsVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define _IRPNCalculatorEvents_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define _IRPNCalculatorEvents_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define _IRPNCalculatorEvents_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define _IRPNCalculatorEvents_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define _IRPNCalculatorEvents_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define _IRPNCalculatorEvents_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define _IRPNCalculatorEvents_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */


#endif 	/* ___IRPNCalculatorEvents_DISPINTERFACE_DEFINED__ */


EXTERN_C const CLSID CLSID_RPNCalculator;

#ifdef __cplusplus

class DECLSPEC_UUID("FB62202B-8F01-4844-88D9-15DDC368A17F")
RPNCalculator;
#endif
#endif /* __RPNCalculatorSvrLib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


