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

public class Transform : Animatable {
  internal new static Transform CreateProxy(IntPtr cPtr, bool cMemoryOwn) {
    return new Transform(cPtr, cMemoryOwn);
  }

  internal Transform(IntPtr cPtr, bool cMemoryOwn) : base(cPtr, cMemoryOwn) {
  }

  internal static HandleRef getCPtr(Transform obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  protected Transform() {
  }

  public Transform2 Value {
    get {
      Transform2 value;
      GetTransformValue(out value);
      return value;
    }
  }

  public static Transform Identity {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.Transform_Identity_get();
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return (Transform)Noesis.Extend.GetProxy(cPtr, false);
    }
  }

  private void GetTransformValue(out Transform2 transform) {
    NoesisGUI_PINVOKE.Transform_GetTransformValue(swigCPtr, out transform);
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
  }

  new internal static IntPtr GetStaticType() {
    IntPtr ret = NoesisGUI_PINVOKE.Transform_GetStaticType();
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return ret;
  }

}

}

