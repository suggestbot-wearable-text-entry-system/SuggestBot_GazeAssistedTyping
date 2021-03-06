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

public class ManipulationDelta : IDisposable {
  private HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal ManipulationDelta(IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(ManipulationDelta obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~ManipulationDelta() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          if (Noesis.Extend.Initialized) { NoesisGUI_PINVOKE.delete_ManipulationDelta(swigCPtr);}
        }
        swigCPtr = new HandleRef(null, IntPtr.Zero);
      }
      GC.SuppressFinalize(this);
    }
  }

  public Noesis.Point Expansion {
    get {
      return GetExpansionHelper();
    }
    set {
      SetExpansionHelper(value);
    }
  }

  public float Rotation {
    get {
      return GetRotationHelper();
    }
    set {
      SetRotationHelper(value);
    }
  }

  public float Scale {
    get {
      return GetScaleHelper();
    }
    set {
      SetScaleHelper(value);
    }
  }

  public Noesis.Point Translation {
    get {
      return GetTranslationHelper();
    }
    set {
      SetTranslationHelper(value);
    }
  }

  private Point GetExpansionHelper() {
    IntPtr ret = NoesisGUI_PINVOKE.ManipulationDelta_GetExpansionHelper(swigCPtr);
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    if (ret != IntPtr.Zero) {
      return Marshal.PtrToStructure<Point>(ret);
    }
    else {
      return new Point();
    }
  }

  private void SetExpansionHelper(Point v) {
    NoesisGUI_PINVOKE.ManipulationDelta_SetExpansionHelper(swigCPtr, ref v);
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
  }

  private float GetRotationHelper() {
    float ret = NoesisGUI_PINVOKE.ManipulationDelta_GetRotationHelper(swigCPtr);
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return ret;
  }

  private void SetRotationHelper(float v) {
    NoesisGUI_PINVOKE.ManipulationDelta_SetRotationHelper(swigCPtr, v);
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
  }

  private float GetScaleHelper() {
    float ret = NoesisGUI_PINVOKE.ManipulationDelta_GetScaleHelper(swigCPtr);
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return ret;
  }

  private void SetScaleHelper(float v) {
    NoesisGUI_PINVOKE.ManipulationDelta_SetScaleHelper(swigCPtr, v);
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
  }

  private Point GetTranslationHelper() {
    IntPtr ret = NoesisGUI_PINVOKE.ManipulationDelta_GetTranslationHelper(swigCPtr);
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    if (ret != IntPtr.Zero) {
      return Marshal.PtrToStructure<Point>(ret);
    }
    else {
      return new Point();
    }
  }

  private void SetTranslationHelper(Point v) {
    NoesisGUI_PINVOKE.ManipulationDelta_SetTranslationHelper(swigCPtr, ref v);
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
  }

  public ManipulationDelta() : this(NoesisGUI_PINVOKE.new_ManipulationDelta(), true) {
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
  }

}

}

