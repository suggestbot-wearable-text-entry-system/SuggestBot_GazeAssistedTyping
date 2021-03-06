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

public class TouchEventArgs : RoutedEventArgs {
  private HandleRef swigCPtr;

  internal TouchEventArgs(IntPtr cPtr, bool cMemoryOwn) : base(cPtr, cMemoryOwn) {
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(TouchEventArgs obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~TouchEventArgs() {
    Dispose();
  }

  public override void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          if (Noesis.Extend.Initialized) { NoesisGUI_PINVOKE.delete_TouchEventArgs(swigCPtr);}
        }
        swigCPtr = new HandleRef(null, IntPtr.Zero);
      }
      GC.SuppressFinalize(this);
      base.Dispose();
    }
  }

  public int TouchDevice {
    get {
      return GetTouchDeviceHelper();
    }
  }

  public TouchEventArgs(object s, RoutedEvent e, Point touchPoint, uint touchDevice) : this(NoesisGUI_PINVOKE.new_TouchEventArgs(Noesis.Extend.GetInstanceHandle(s), RoutedEvent.getCPtr(e), ref touchPoint, touchDevice), true) {
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
  }

  public Point GetTouchPoint(UIElement relativeTo) {
    IntPtr ret = NoesisGUI_PINVOKE.TouchEventArgs_GetTouchPoint(swigCPtr, UIElement.getCPtr(relativeTo));
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

  private int GetTouchDeviceHelper() {
    int ret = NoesisGUI_PINVOKE.TouchEventArgs_GetTouchDeviceHelper(swigCPtr);
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return ret;
  }

}

}

