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

public class DoubleAnimationUsingKeyFrames : AnimationTimeline {
  internal new static DoubleAnimationUsingKeyFrames CreateProxy(IntPtr cPtr, bool cMemoryOwn) {
    return new DoubleAnimationUsingKeyFrames(cPtr, cMemoryOwn);
  }

  internal DoubleAnimationUsingKeyFrames(IntPtr cPtr, bool cMemoryOwn) : base(cPtr, cMemoryOwn) {
  }

  internal static HandleRef getCPtr(DoubleAnimationUsingKeyFrames obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  public DoubleAnimationUsingKeyFrames() {
  }

  protected override System.IntPtr CreateCPtr(System.Type type, out bool registerExtend) {
    registerExtend = false;
    return NoesisGUI_PINVOKE.new_DoubleAnimationUsingKeyFrames();
  }

  public DoubleKeyFrameCollection KeyFrames {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.DoubleAnimationUsingKeyFrames_KeyFrames_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return (DoubleKeyFrameCollection)Noesis.Extend.GetProxy(cPtr, false);
    }
  }

  new internal static IntPtr GetStaticType() {
    IntPtr ret = NoesisGUI_PINVOKE.DoubleAnimationUsingKeyFrames_GetStaticType();
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return ret;
  }

}

}

