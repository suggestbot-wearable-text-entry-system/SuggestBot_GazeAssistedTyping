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

public class KeyEventArgs : RoutedEventArgs {
  private HandleRef swigCPtr;

  internal KeyEventArgs(IntPtr cPtr, bool cMemoryOwn) : base(cPtr, cMemoryOwn) {
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(KeyEventArgs obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~KeyEventArgs() {
    Dispose();
  }

  public override void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          if (Noesis.Extend.Initialized) { NoesisGUI_PINVOKE.delete_KeyEventArgs(swigCPtr);}
        }
        swigCPtr = new HandleRef(null, IntPtr.Zero);
      }
      GC.SuppressFinalize(this);
      base.Dispose();
    }
  }

  public Noesis.Key Key {
    get {
      return (Noesis.Key)GetKeyHelper();
    }
  }

  public Noesis.KeyStates KeyStates {
    get {
      return (Noesis.KeyStates)GetKeyStatesHelper();
    }
  }

  public bool IsDown {
    get {
      return GetIsDown();
    }
  }

  public bool IsUp {
    get {
      return GetIsUp();
    }
  }

  public bool IsRepeat {
    get {
      return GetIsRepeat();
    }
  }

  public bool IsToggled {
    get {
      return GetIsToggled();
    }
  }

  public KeyEventArgs(object s, RoutedEvent e, Key key, uint keyStates) : this(NoesisGUI_PINVOKE.new_KeyEventArgs(Noesis.Extend.GetInstanceHandle(s), RoutedEvent.getCPtr(e), (int)key, keyStates), true) {
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
  }

  private bool GetIsDown() {
    bool ret = NoesisGUI_PINVOKE.KeyEventArgs_GetIsDown(swigCPtr);
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return ret;
  }

  private bool GetIsRepeat() {
    bool ret = NoesisGUI_PINVOKE.KeyEventArgs_GetIsRepeat(swigCPtr);
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return ret;
  }

  private bool GetIsToggled() {
    bool ret = NoesisGUI_PINVOKE.KeyEventArgs_GetIsToggled(swigCPtr);
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return ret;
  }

  private bool GetIsUp() {
    bool ret = NoesisGUI_PINVOKE.KeyEventArgs_GetIsUp(swigCPtr);
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return ret;
  }

  private Key GetKeyHelper() {
    Key ret = (Key)NoesisGUI_PINVOKE.KeyEventArgs_GetKeyHelper(swigCPtr);
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return ret;
  }

  private uint GetKeyStatesHelper() {
    uint ret = NoesisGUI_PINVOKE.KeyEventArgs_GetKeyStatesHelper(swigCPtr);
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return ret;
  }

}

}

