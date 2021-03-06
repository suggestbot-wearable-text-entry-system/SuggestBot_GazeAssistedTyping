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

public class Control : FrameworkElement {
  internal new static Control CreateProxy(IntPtr cPtr, bool cMemoryOwn) {
    return new Control(cPtr, cMemoryOwn);
  }

  internal Control(IntPtr cPtr, bool cMemoryOwn) : base(cPtr, cMemoryOwn) {
  }

  internal static HandleRef getCPtr(Control obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  #region Events
  #region MouseDoubleClick
  public delegate void MouseDoubleClickHandler(object sender, MouseButtonEventArgs e);
  public event MouseDoubleClickHandler MouseDoubleClick {
    add {
      if (!_MouseDoubleClick.ContainsKey(swigCPtr.Handle)) {
        _MouseDoubleClick.Add(swigCPtr.Handle, null);

        NoesisGUI_PINVOKE.BindEvent_Control_MouseDoubleClick(_raiseMouseDoubleClick, swigCPtr.Handle);
        #if UNITY_EDITOR || NOESIS_API
        if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
        #endif
      }

      _MouseDoubleClick[swigCPtr.Handle] += value;
    }
    remove {
      if (_MouseDoubleClick.ContainsKey(swigCPtr.Handle)) {

        _MouseDoubleClick[swigCPtr.Handle] -= value;

        if (_MouseDoubleClick[swigCPtr.Handle] == null) {
          NoesisGUI_PINVOKE.UnbindEvent_Control_MouseDoubleClick(_raiseMouseDoubleClick, swigCPtr.Handle);
          #if UNITY_EDITOR || NOESIS_API
          if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
          #endif

          _MouseDoubleClick.Remove(swigCPtr.Handle);
        }
      }
    }
  }

  internal delegate void RaiseMouseDoubleClickCallback(IntPtr cPtr, IntPtr sender, IntPtr e);
  private static RaiseMouseDoubleClickCallback _raiseMouseDoubleClick = RaiseMouseDoubleClick;

  [MonoPInvokeCallback(typeof(RaiseMouseDoubleClickCallback))]
  private static void RaiseMouseDoubleClick(IntPtr cPtr, IntPtr sender, IntPtr e) {
    try {
      if (!_MouseDoubleClick.ContainsKey(cPtr)) {
        throw new System.InvalidOperationException("Delegate not registered for MouseDoubleClick event");
      }
      if (sender == System.IntPtr.Zero && e == System.IntPtr.Zero) {
        _MouseDoubleClick.Remove(cPtr);
        return;
      }
      if (Noesis.Extend.Initialized) {
        MouseDoubleClickHandler handler = _MouseDoubleClick[cPtr];
        if (handler != null) {
          handler(Noesis.Extend.GetProxy(sender, false), new MouseButtonEventArgs(e, false));
        }
      }
    }
    catch (System.Exception exception) {
      Noesis.Error.SetNativePendingError(exception);
    }
  }

  static System.Collections.Generic.Dictionary<System.IntPtr, MouseDoubleClickHandler> _MouseDoubleClick =
      new System.Collections.Generic.Dictionary<System.IntPtr, MouseDoubleClickHandler>();
  #endregion

  #region PreviewMouseDoubleClick
  public delegate void PreviewMouseDoubleClickHandler(object sender, MouseButtonEventArgs e);
  public event PreviewMouseDoubleClickHandler PreviewMouseDoubleClick {
    add {
      if (!_PreviewMouseDoubleClick.ContainsKey(swigCPtr.Handle)) {
        _PreviewMouseDoubleClick.Add(swigCPtr.Handle, null);

        NoesisGUI_PINVOKE.BindEvent_Control_PreviewMouseDoubleClick(_raisePreviewMouseDoubleClick, swigCPtr.Handle);
        #if UNITY_EDITOR || NOESIS_API
        if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
        #endif
      }

      _PreviewMouseDoubleClick[swigCPtr.Handle] += value;
    }
    remove {
      if (_PreviewMouseDoubleClick.ContainsKey(swigCPtr.Handle)) {

        _PreviewMouseDoubleClick[swigCPtr.Handle] -= value;

        if (_PreviewMouseDoubleClick[swigCPtr.Handle] == null) {
          NoesisGUI_PINVOKE.UnbindEvent_Control_PreviewMouseDoubleClick(_raisePreviewMouseDoubleClick, swigCPtr.Handle);
          #if UNITY_EDITOR || NOESIS_API
          if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
          #endif

          _PreviewMouseDoubleClick.Remove(swigCPtr.Handle);
        }
      }
    }
  }

  internal delegate void RaisePreviewMouseDoubleClickCallback(IntPtr cPtr, IntPtr sender, IntPtr e);
  private static RaisePreviewMouseDoubleClickCallback _raisePreviewMouseDoubleClick = RaisePreviewMouseDoubleClick;

  [MonoPInvokeCallback(typeof(RaisePreviewMouseDoubleClickCallback))]
  private static void RaisePreviewMouseDoubleClick(IntPtr cPtr, IntPtr sender, IntPtr e) {
    try {
      if (!_PreviewMouseDoubleClick.ContainsKey(cPtr)) {
        throw new System.InvalidOperationException("Delegate not registered for PreviewMouseDoubleClick event");
      }
      if (sender == System.IntPtr.Zero && e == System.IntPtr.Zero) {
        _PreviewMouseDoubleClick.Remove(cPtr);
        return;
      }
      if (Noesis.Extend.Initialized) {
        PreviewMouseDoubleClickHandler handler = _PreviewMouseDoubleClick[cPtr];
        if (handler != null) {
          handler(Noesis.Extend.GetProxy(sender, false), new MouseButtonEventArgs(e, false));
        }
      }
    }
    catch (System.Exception exception) {
      Noesis.Error.SetNativePendingError(exception);
    }
  }

  static System.Collections.Generic.Dictionary<System.IntPtr, PreviewMouseDoubleClickHandler> _PreviewMouseDoubleClick =
      new System.Collections.Generic.Dictionary<System.IntPtr, PreviewMouseDoubleClickHandler>();
  #endregion

  #endregion

  public Control() {
  }

  protected override System.IntPtr CreateCPtr(System.Type type, out bool registerExtend) {
    if ((object)type.TypeHandle == typeof(Control).TypeHandle) {
      registerExtend = false;
      return NoesisGUI_PINVOKE.new_Control();
    }
    else {
      return base.CreateExtendCPtr(type, out registerExtend);
    }
  }

  public static DependencyProperty BackgroundProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.Control_BackgroundProperty_get();
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return (DependencyProperty)Noesis.Extend.GetProxy(cPtr, false);
    }
  }

  public static DependencyProperty BorderBrushProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.Control_BorderBrushProperty_get();
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return (DependencyProperty)Noesis.Extend.GetProxy(cPtr, false);
    }
  }

  public static DependencyProperty BorderThicknessProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.Control_BorderThicknessProperty_get();
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return (DependencyProperty)Noesis.Extend.GetProxy(cPtr, false);
    }
  }

  public static DependencyProperty FontFamilyProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.Control_FontFamilyProperty_get();
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return (DependencyProperty)Noesis.Extend.GetProxy(cPtr, false);
    }
  }

  public static DependencyProperty FontSizeProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.Control_FontSizeProperty_get();
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return (DependencyProperty)Noesis.Extend.GetProxy(cPtr, false);
    }
  }

  public static DependencyProperty FontStyleProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.Control_FontStyleProperty_get();
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return (DependencyProperty)Noesis.Extend.GetProxy(cPtr, false);
    }
  }

  public static DependencyProperty FontWeightProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.Control_FontWeightProperty_get();
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return (DependencyProperty)Noesis.Extend.GetProxy(cPtr, false);
    }
  }

  public static DependencyProperty ForegroundProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.Control_ForegroundProperty_get();
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return (DependencyProperty)Noesis.Extend.GetProxy(cPtr, false);
    }
  }

  public static DependencyProperty HorizontalContentAlignmentProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.Control_HorizontalContentAlignmentProperty_get();
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return (DependencyProperty)Noesis.Extend.GetProxy(cPtr, false);
    }
  }

  public static DependencyProperty IsTabStopProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.Control_IsTabStopProperty_get();
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return (DependencyProperty)Noesis.Extend.GetProxy(cPtr, false);
    }
  }

  public static DependencyProperty PaddingProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.Control_PaddingProperty_get();
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return (DependencyProperty)Noesis.Extend.GetProxy(cPtr, false);
    }
  }

  public static DependencyProperty TabIndexProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.Control_TabIndexProperty_get();
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return (DependencyProperty)Noesis.Extend.GetProxy(cPtr, false);
    }
  }

  public static DependencyProperty TemplateProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.Control_TemplateProperty_get();
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return (DependencyProperty)Noesis.Extend.GetProxy(cPtr, false);
    }
  }

  public static DependencyProperty VerticalContentAlignmentProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.Control_VerticalContentAlignmentProperty_get();
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return (DependencyProperty)Noesis.Extend.GetProxy(cPtr, false);
    }
  }

  public Brush Background {
    set {
      NoesisGUI_PINVOKE.Control_Background_set(swigCPtr, Brush.getCPtr(value));
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.Control_Background_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return (Brush)Noesis.Extend.GetProxy(cPtr, false);
    }
  }

  public Brush BorderBrush {
    set {
      NoesisGUI_PINVOKE.Control_BorderBrush_set(swigCPtr, Brush.getCPtr(value));
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.Control_BorderBrush_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return (Brush)Noesis.Extend.GetProxy(cPtr, false);
    }
  }

  public Thickness BorderThickness {
    set {
      NoesisGUI_PINVOKE.Control_BorderThickness_set(swigCPtr, ref value);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    }

    get {
      IntPtr ret = NoesisGUI_PINVOKE.Control_BorderThickness_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      if (ret != IntPtr.Zero) {
        return Marshal.PtrToStructure<Thickness>(ret);
      }
      else {
        return new Thickness();
      }
    }

  }

  public FontFamily FontFamily {
    set {
      NoesisGUI_PINVOKE.Control_FontFamily_set(swigCPtr, FontFamily.getCPtr(value));
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.Control_FontFamily_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return (FontFamily)Noesis.Extend.GetProxy(cPtr, false);
    }
  }

  public float FontSize {
    set {
      NoesisGUI_PINVOKE.Control_FontSize_set(swigCPtr, value);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      float ret = NoesisGUI_PINVOKE.Control_FontSize_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public FontStyle FontStyle {
    set {
      NoesisGUI_PINVOKE.Control_FontStyle_set(swigCPtr, (int)value);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      FontStyle ret = (FontStyle)NoesisGUI_PINVOKE.Control_FontStyle_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public FontWeight FontWeight {
    set {
      NoesisGUI_PINVOKE.Control_FontWeight_set(swigCPtr, (int)value);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      FontWeight ret = (FontWeight)NoesisGUI_PINVOKE.Control_FontWeight_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public Brush Foreground {
    set {
      NoesisGUI_PINVOKE.Control_Foreground_set(swigCPtr, Brush.getCPtr(value));
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.Control_Foreground_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return (Brush)Noesis.Extend.GetProxy(cPtr, false);
    }
  }

  public HorizontalAlignment HorizontalContentAlignment {
    set {
      NoesisGUI_PINVOKE.Control_HorizontalContentAlignment_set(swigCPtr, (int)value);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      HorizontalAlignment ret = (HorizontalAlignment)NoesisGUI_PINVOKE.Control_HorizontalContentAlignment_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public bool IsTabStop {
    set {
      NoesisGUI_PINVOKE.Control_IsTabStop_set(swigCPtr, value);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      bool ret = NoesisGUI_PINVOKE.Control_IsTabStop_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public Thickness Padding {
    set {
      NoesisGUI_PINVOKE.Control_Padding_set(swigCPtr, ref value);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    }

    get {
      IntPtr ret = NoesisGUI_PINVOKE.Control_Padding_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      if (ret != IntPtr.Zero) {
        return Marshal.PtrToStructure<Thickness>(ret);
      }
      else {
        return new Thickness();
      }
    }

  }

  public int TabIndex {
    set {
      NoesisGUI_PINVOKE.Control_TabIndex_set(swigCPtr, value);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      int ret = NoesisGUI_PINVOKE.Control_TabIndex_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public ControlTemplate Template {
    set {
      NoesisGUI_PINVOKE.Control_Template_set(swigCPtr, ControlTemplate.getCPtr(value));
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.Control_Template_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return (ControlTemplate)Noesis.Extend.GetProxy(cPtr, false);
    }
  }

  public VerticalAlignment VerticalContentAlignment {
    set {
      NoesisGUI_PINVOKE.Control_VerticalContentAlignment_set(swigCPtr, (int)value);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      VerticalAlignment ret = (VerticalAlignment)NoesisGUI_PINVOKE.Control_VerticalContentAlignment_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  new internal static IntPtr GetStaticType() {
    IntPtr ret = NoesisGUI_PINVOKE.Control_GetStaticType();
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return ret;
  }


  internal new static IntPtr Extend(string typeName) {
    IntPtr nativeType = NoesisGUI_PINVOKE.Extend_Control(Marshal.StringToHGlobalAnsi(typeName));
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return nativeType;
  }
}

}

