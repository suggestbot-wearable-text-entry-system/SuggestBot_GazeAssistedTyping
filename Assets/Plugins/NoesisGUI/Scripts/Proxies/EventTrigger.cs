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

public class EventTrigger : TriggerBase {
  internal new static EventTrigger CreateProxy(IntPtr cPtr, bool cMemoryOwn) {
    return new EventTrigger(cPtr, cMemoryOwn);
  }

  internal EventTrigger(IntPtr cPtr, bool cMemoryOwn) : base(cPtr, cMemoryOwn) {
  }

  internal static HandleRef getCPtr(EventTrigger obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  public EventTrigger() {
  }

  protected override System.IntPtr CreateCPtr(System.Type type, out bool registerExtend) {
    registerExtend = false;
    return NoesisGUI_PINVOKE.new_EventTrigger();
  }

  public TriggerActionCollection Actions {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.EventTrigger_Actions_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return (TriggerActionCollection)Noesis.Extend.GetProxy(cPtr, false);
    }
  }

  public RoutedEvent RoutedEvent {
    set {
      NoesisGUI_PINVOKE.EventTrigger_RoutedEvent_set(swigCPtr, RoutedEvent.getCPtr(value));
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.EventTrigger_RoutedEvent_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return (RoutedEvent)Noesis.Extend.GetProxy(cPtr, false);
    }
  }

  public string SourceName {
    set {
      NoesisGUI_PINVOKE.EventTrigger_SourceName_set(swigCPtr, value != null ? value : string.Empty);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    }
    get {
      IntPtr strPtr = NoesisGUI_PINVOKE.EventTrigger_SourceName_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      string str = Noesis.Extend.StringFromNativeUtf8(strPtr);
      return str;
    }
  }

  new internal static IntPtr GetStaticType() {
    IntPtr ret = NoesisGUI_PINVOKE.EventTrigger_GetStaticType();
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return ret;
  }

}

}

