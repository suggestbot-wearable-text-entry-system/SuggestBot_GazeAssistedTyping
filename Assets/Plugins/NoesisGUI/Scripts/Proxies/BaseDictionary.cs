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

public class BaseDictionary : BaseComponent {
  internal new static BaseDictionary CreateProxy(IntPtr cPtr, bool cMemoryOwn) {
    return new BaseDictionary(cPtr, cMemoryOwn);
  }

  internal BaseDictionary(IntPtr cPtr, bool cMemoryOwn) : base(cPtr, cMemoryOwn) {
  }

  internal static HandleRef getCPtr(BaseDictionary obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  protected BaseDictionary() {
  }

}

}

