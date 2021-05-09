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

public class RelativeSource : BaseComponent {
  internal new static RelativeSource CreateProxy(IntPtr cPtr, bool cMemoryOwn) {
    return new RelativeSource(cPtr, cMemoryOwn);
  }

  internal RelativeSource(IntPtr cPtr, bool cMemoryOwn) : base(cPtr, cMemoryOwn) {
  }

  internal static HandleRef getCPtr(RelativeSource obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  public RelativeSource() {
  }

  protected override System.IntPtr CreateCPtr(System.Type type, out bool registerExtend) {
    registerExtend = false;
    return NoesisGUI_PINVOKE.new_RelativeSource__SWIG_0();
  }

  public RelativeSource(RelativeSourceMode mode) : this(NoesisGUI_PINVOKE.new_RelativeSource__SWIG_1((int)mode), true) {
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
  }

  public RelativeSource(RelativeSourceMode mode, System.Type type, int level) : this(NoesisGUI_PINVOKE.new_RelativeSource__SWIG_2((int)mode, new HandleRef(type, (type != null ? Noesis.Extend.GetNativeType(type) : IntPtr.Zero)), level), true) {
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
  }

  public RelativeSource(RelativeSource other) : this(NoesisGUI_PINVOKE.new_RelativeSource__SWIG_3(RelativeSource.getCPtr(other)), true) {
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
  }

  public static RelativeSource Self {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.RelativeSource_Self_get();
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return (RelativeSource)Noesis.Extend.GetProxy(cPtr, false);
    }
  }

  public static RelativeSource TemplatedParent {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.RelativeSource_TemplatedParent_get();
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return (RelativeSource)Noesis.Extend.GetProxy(cPtr, false);
    }
  }

  public RelativeSourceMode Mode {
    set {
      NoesisGUI_PINVOKE.RelativeSource_Mode_set(swigCPtr, (int)value);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      RelativeSourceMode ret = (RelativeSourceMode)NoesisGUI_PINVOKE.RelativeSource_Mode_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public System.Type AncestorType {
    set {
      NoesisGUI_PINVOKE.RelativeSource_AncestorType_set(swigCPtr, new HandleRef(value, (value != null ? Noesis.Extend.GetNativeType(value) : IntPtr.Zero)));
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    }
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.RelativeSource_AncestorType_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      if (cPtr != IntPtr.Zero) {
        Noesis.Extend.NativeTypeInfo info = Noesis.Extend.GetNativeTypeInfo(cPtr);
        return info.Type;
      }
      return null;
    }
  }

  public int AncestorLevel {
    set {
      NoesisGUI_PINVOKE.RelativeSource_AncestorLevel_set(swigCPtr, value);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      int ret = NoesisGUI_PINVOKE.RelativeSource_AncestorLevel_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  new internal static IntPtr GetStaticType() {
    IntPtr ret = NoesisGUI_PINVOKE.RelativeSource_GetStaticType();
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return ret;
  }

}

}

