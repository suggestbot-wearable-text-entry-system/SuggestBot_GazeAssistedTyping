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

public class ManipulationStartedEventArgs : RoutedEventArgs {
  private HandleRef swigCPtr;

  internal ManipulationStartedEventArgs(IntPtr cPtr, bool cMemoryOwn) : base(cPtr, cMemoryOwn) {
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(ManipulationStartedEventArgs obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~ManipulationStartedEventArgs() {
    Dispose();
  }

  public override void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          if (Noesis.Extend.Initialized) { NoesisGUI_PINVOKE.delete_ManipulationStartedEventArgs(swigCPtr);}
        }
        swigCPtr = new HandleRef(null, IntPtr.Zero);
      }
      GC.SuppressFinalize(this);
      base.Dispose();
    }
  }

  public Noesis.UIElement ManipulationContainer {
    get {
      return GetManipulationContainerHelper();
    }
  }

  public Noesis.Point ManipulationOrigin {
    get {
      return GetManipulationOriginHelper();
    }
  }

  public ManipulationStartedEventArgs(object s, RoutedEvent e, Visual manipulationContainer, Point manipulationOrigin) : this(NoesisGUI_PINVOKE.new_ManipulationStartedEventArgs(Noesis.Extend.GetInstanceHandle(s), RoutedEvent.getCPtr(e), Visual.getCPtr(manipulationContainer), ref manipulationOrigin), true) {
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
  }

  public bool Cancel() {
    bool ret = NoesisGUI_PINVOKE.ManipulationStartedEventArgs_Cancel(swigCPtr);
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return ret;
  }

  public void Complete() {
    NoesisGUI_PINVOKE.ManipulationStartedEventArgs_Complete(swigCPtr);
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
  }

  private UIElement GetManipulationContainerHelper() {
    IntPtr cPtr = NoesisGUI_PINVOKE.ManipulationStartedEventArgs_GetManipulationContainerHelper(swigCPtr);
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return (UIElement)Noesis.Extend.GetProxy(cPtr, false);
  }

  private Point GetManipulationOriginHelper() {
    IntPtr ret = NoesisGUI_PINVOKE.ManipulationStartedEventArgs_GetManipulationOriginHelper(swigCPtr);
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

}

}

