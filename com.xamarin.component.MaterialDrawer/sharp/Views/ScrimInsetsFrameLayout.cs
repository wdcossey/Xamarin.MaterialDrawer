/*
 * Copyright 2014 Google Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Util;
using Android.Widget;

namespace com.xamarin.component.MaterialDrawer.Views
{

  /// <summary>
  /// A layout that draws something in the insets passed to {@link #fitSystemWindows(Rect)}, i.e. the area above UI chrome
  /// (status and navigation bars, overlay action bars).
  /// </summary>
  public class ScrimInsetsFrameLayout : FrameLayout
  {
    private Drawable _insetForeground;

    private Rect _insets;
    private Rect _tempRect = new Rect();
    private IOnInsetsCallback _onInsetsCallback;

    private bool _enabled = true;

    private ScrimInsetsFrameLayout(IntPtr javaReference, JniHandleOwnership transfer)
      : base(javaReference, transfer)
    {
    }

    public ScrimInsetsFrameLayout(Context context)
      : base(context)
    {
      Init(context, null, 0);
    }

    public ScrimInsetsFrameLayout(Context context, IAttributeSet attrs)
      : base(context, attrs)
    {
      Init(context, attrs, 0);
    }

    public ScrimInsetsFrameLayout(Context context, IAttributeSet attrs, int defStyle)
      : base(context, attrs, defStyle)
    {
      Init(context, attrs, defStyle);
    }

    private void Init(Context context, IAttributeSet attrs, int defStyle)
    {
      var a = context.ObtainStyledAttributes(attrs, Resource.Styleable.ScrimInsetsView, defStyle, 0);

      if (a == null)
        return;

      _insetForeground = a.GetDrawable(Resource.Styleable.ScrimInsetsView_siv_insetForeground);
      a.Recycle();

      SetWillNotDraw(true);
    }

    [Obsolete("deprecated", false)]
    protected override bool FitSystemWindows(Rect insets)
    {
      _insets = new Rect(insets);
      SetWillNotDraw(_insetForeground == null);
      ViewCompat.PostInvalidateOnAnimation(this);
      if (_onInsetsCallback != null)
      {
        _onInsetsCallback.OnInsetsChanged(insets);
      }
      return true; // consume insets
    }

    public override void Draw(Canvas canvas)
    {
      base.Draw(canvas);

      var width = Width;
      var height = Height;
      if (_insets != null && _insetForeground != null)
      {
        int sc = canvas.Save();
        canvas.Translate(ScrollX, ScrollY);

        // Top
        _tempRect.Set(0, 0, width, _insets.Top);
        _insetForeground.Bounds = _tempRect;
        _insetForeground.Draw(canvas);

        // Bottom
        _tempRect.Set(0, height - _insets.Bottom, width, height);
        _insetForeground.Bounds = _tempRect;
        _insetForeground.Draw(canvas);

        // Left
        _tempRect.Set(0, _insets.Top, _insets.Left, height - _insets.Bottom);
        _insetForeground.Bounds = _tempRect;
        _insetForeground.Draw(canvas);

        // Right
        _tempRect.Set(width - _insets.Right, _insets.Top, width, height - _insets.Bottom);
        _insetForeground.Bounds = _tempRect;
        _insetForeground.Draw(canvas);

        canvas.RestoreToCount(sc);
      }
    }

    protected override void OnAttachedToWindow()
    {
      base.OnAttachedToWindow();
      if (_insetForeground != null)
      {
        _insetForeground.SetCallback(this);
      }
    }

    protected override void OnDetachedFromWindow()
    {
      base.OnDetachedFromWindow();
      if (_insetForeground != null)
      {
        _insetForeground.SetCallback(null);
      }
    }

    public bool IsEnabled()
    {
      return _enabled;
    }

    public void SetEnabled(bool enabled)
    {
      _enabled = enabled;
      //setWillNotDraw(false);
      Invalidate();
    }


    public Drawable GetInsetForeground()
    {
      return _insetForeground;
    }

    public void SetInsetForeground(Drawable mInsetForeground)
    {
      _insetForeground = mInsetForeground;
    }

    public void SetInsetForeground(Color mInsetForegroundColor)
    {
      _insetForeground = new ColorDrawable(mInsetForegroundColor);
    }

    /// <summary>
    /// Allows the calling container to specify a callback for custom processing when insets change (i.e. when
    /// {@link #fitSystemWindows(Rect)} is called. This is useful for setting padding on UI elements based on
    /// UI chrome insets (e.g. a Google Map or a ListView). When using with ListView or GridView, remember to set
    /// clipToPadding to false.
    /// </summary>
    /// <param name="onInsetsCallback"></param>
    public void SetOnInsetsCallback(IOnInsetsCallback onInsetsCallback)
    {
      _onInsetsCallback = onInsetsCallback;
    }

    public interface IOnInsetsCallback
    {
      void OnInsetsChanged(Rect insets);
    }
  }
}