/* ----------------------------------------------------------------------------
 * This file was automatically generated by SWIG (http://www.swig.org).
 * Version 2.0.4
 *
 * Do not make changes to this file unless you know what you are doing--modify
 * the SWIG interface file instead.
 * ----------------------------------------------------------------------------- */


using System;
using System.Runtime.InteropServices;

namespace Noesis
{

public class ElasticEase : EasingFunctionBase {
  internal new static ElasticEase CreateProxy(IntPtr cPtr, bool cMemoryOwn) {
    return new ElasticEase(cPtr, cMemoryOwn);
  }

  internal ElasticEase(IntPtr cPtr, bool cMemoryOwn) : base(cPtr, cMemoryOwn) {
  }

  internal static HandleRef getCPtr(ElasticEase obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  public ElasticEase() {
  }

  protected override System.IntPtr CreateCPtr(System.Type type, out bool registerExtend) {
    registerExtend = false;
    return NoesisGUI_PINVOKE.new_ElasticEase();
  }

  public static DependencyProperty OscillationsProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.ElasticEase_OscillationsProperty_get();
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return (DependencyProperty)Noesis.Extend.GetProxy(cPtr, false);
    }
  }

  public static DependencyProperty SpringinessProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.ElasticEase_SpringinessProperty_get();
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return (DependencyProperty)Noesis.Extend.GetProxy(cPtr, false);
    }
  }

  public int Oscillations {
    set {
      NoesisGUI_PINVOKE.ElasticEase_Oscillations_set(swigCPtr, value);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      int ret = NoesisGUI_PINVOKE.ElasticEase_Oscillations_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public float Springiness {
    set {
      NoesisGUI_PINVOKE.ElasticEase_Springiness_set(swigCPtr, value);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      float ret = NoesisGUI_PINVOKE.ElasticEase_Springiness_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  new internal static IntPtr GetStaticType() {
    IntPtr ret = NoesisGUI_PINVOKE.ElasticEase_GetStaticType();
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return ret;
  }

}

}

