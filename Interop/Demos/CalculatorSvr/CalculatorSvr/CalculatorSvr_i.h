

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 8.00.0603 */
/* at Thu Sep 06 16:20:28 2018
 */
/* Compiler settings for CalculatorSvr.idl:
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

#ifndef __CalculatorSvr_i_h__
#define __CalculatorSvr_i_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __ITrigCalculator_FWD_DEFINED__
#define __ITrigCalculator_FWD_DEFINED__
typedef interface ITrigCalculator ITrigCalculator;

#endif 	/* __ITrigCalculator_FWD_DEFINED__ */


#ifndef __ISimpleCalculator_FWD_DEFINED__
#define __ISimpleCalculator_FWD_DEFINED__
typedef interface ISimpleCalculator ISimpleCalculator;

#endif 	/* __ISimpleCalculator_FWD_DEFINED__ */


#ifndef __Calculator_FWD_DEFINED__
#define __Calculator_FWD_DEFINED__

#ifdef __cplusplus
typedef class Calculator Calculator;
#else
typedef struct Calculator Calculator;
#endif /* __cplusplus */

#endif 	/* __Calculator_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"

#ifdef __cplusplus
extern "C"{
#endif 


#ifndef __ITrigCalculator_INTERFACE_DEFINED__
#define __ITrigCalculator_INTERFACE_DEFINED__

/* interface ITrigCalculator */
/* [unique][nonextensible][oleautomation][uuid][object] */ 


EXTERN_C const IID IID_ITrigCalculator;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("AF41A15F-FBD4-4807-A259-6B6089FE76A4")
    ITrigCalculator : public IUnknown
    {
    public:
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE Sin( 
            /* [in] */ double angle,
            /* [retval][out] */ double *result) = 0;
        
        virtual /* [propget] */ HRESULT STDMETHODCALLTYPE get_Degrees( 
            /* [retval][out] */ VARIANT_BOOL *pVal) = 0;
        
        virtual /* [propput] */ HRESULT STDMETHODCALLTYPE put_Degrees( 
            /* [in] */ VARIANT_BOOL newVal) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct ITrigCalculatorVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            ITrigCalculator * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            ITrigCalculator * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            ITrigCalculator * This);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *Sin )( 
            ITrigCalculator * This,
            /* [in] */ double angle,
            /* [retval][out] */ double *result);
        
        /* [propget] */ HRESULT ( STDMETHODCALLTYPE *get_Degrees )( 
            ITrigCalculator * This,
            /* [retval][out] */ VARIANT_BOOL *pVal);
        
        /* [propput] */ HRESULT ( STDMETHODCALLTYPE *put_Degrees )( 
            ITrigCalculator * This,
            /* [in] */ VARIANT_BOOL newVal);
        
        END_INTERFACE
    } ITrigCalculatorVtbl;

    interface ITrigCalculator
    {
        CONST_VTBL struct ITrigCalculatorVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define ITrigCalculator_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define ITrigCalculator_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define ITrigCalculator_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define ITrigCalculator_Sin(This,angle,result)	\
    ( (This)->lpVtbl -> Sin(This,angle,result) ) 

#define ITrigCalculator_get_Degrees(This,pVal)	\
    ( (This)->lpVtbl -> get_Degrees(This,pVal) ) 

#define ITrigCalculator_put_Degrees(This,newVal)	\
    ( (This)->lpVtbl -> put_Degrees(This,newVal) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __ITrigCalculator_INTERFACE_DEFINED__ */


#ifndef __ISimpleCalculator_INTERFACE_DEFINED__
#define __ISimpleCalculator_INTERFACE_DEFINED__

/* interface ISimpleCalculator */
/* [unique][nonextensible][oleautomation][uuid][object] */ 


EXTERN_C const IID IID_ISimpleCalculator;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("AF4978FE-6C37-4D20-84E5-A3FF319DFDE9")
    ISimpleCalculator : public IUnknown
    {
    public:
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE Add( 
            /* [in] */ int a,
            /* [in] */ int b,
            /* [retval][out] */ int *result) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct ISimpleCalculatorVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            ISimpleCalculator * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            ISimpleCalculator * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            ISimpleCalculator * This);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *Add )( 
            ISimpleCalculator * This,
            /* [in] */ int a,
            /* [in] */ int b,
            /* [retval][out] */ int *result);
        
        END_INTERFACE
    } ISimpleCalculatorVtbl;

    interface ISimpleCalculator
    {
        CONST_VTBL struct ISimpleCalculatorVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define ISimpleCalculator_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define ISimpleCalculator_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define ISimpleCalculator_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define ISimpleCalculator_Add(This,a,b,result)	\
    ( (This)->lpVtbl -> Add(This,a,b,result) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __ISimpleCalculator_INTERFACE_DEFINED__ */



#ifndef __CalculatorSvrLib_LIBRARY_DEFINED__
#define __CalculatorSvrLib_LIBRARY_DEFINED__

/* library CalculatorSvrLib */
/* [version][uuid] */ 


EXTERN_C const IID LIBID_CalculatorSvrLib;

EXTERN_C const CLSID CLSID_Calculator;

#ifdef __cplusplus

class DECLSPEC_UUID("9017AC43-C0D1-49CE-8156-A37E0712100B")
Calculator;
#endif
#endif /* __CalculatorSvrLib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


