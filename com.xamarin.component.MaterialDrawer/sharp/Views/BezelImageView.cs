using Android.Annotation;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Net;
using Android.OS;
using Android.Support.V4.View;
using Android.Util;
using Android.Views;
using Android.Widget;
using com.xamarin.component.MaterialDrawer.Utils;

namespace com.xamarin.component.MaterialDrawer.Views
{
  /// <summary>
  /// An {@link android.widget.ImageView} that draws its contents inside a mask and draws a border
  /// drawable on top. This is useful for applying a beveled look to image contents, but is also
  /// flexible enough for use with other desired aesthetics.
  /// </summary>
  public class BezelImageView : ImageView
  {
    private readonly Paint _mBlackPaint;
    private readonly Paint _mMaskedPaint;

    private Rect _mBounds;
    private RectF _mBoundsF;

    private readonly Drawable _mMaskDrawable;

    private ColorMatrixColorFilter _mDesaturateColorFilter;

    private readonly int _mSelectorAlpha = 150;
    private int _mSelectorColor;
    private ColorFilter _mSelectorFilter;

    private bool _mCacheValid;
    private Bitmap _mCacheBitmap;
    private int _mCachedWidth;
    private int _mCachedHeight;

    private bool _isPressed;
    private bool _isSelected;

    public BezelImageView(Context context)
      : this(context, null)
    {

    }

    public BezelImageView(Context context, IAttributeSet attrs)
      : this(context, attrs, 0)
    {

    }

    public BezelImageView(Context context, IAttributeSet attrs, int defStyle)
      : base(context, attrs, defStyle)
    {

      // Attribute initialization
      var a = context.ObtainStyledAttributes(attrs, Resource.Styleable.BezelImageView,
        defStyle, 0);

      _mMaskDrawable = a.GetDrawable(Resource.Styleable.BezelImageView_biv_maskDrawable);
      if (_mMaskDrawable != null)
      {
        _mMaskDrawable.SetCallback(this);
      }

      _mSelectorColor = a.GetColor(Resource.Styleable.BezelImageView_biv_selectorOnPress, 0);

      a.Recycle();

      // Other initialization
      _mBlackPaint = new Paint {Color = Color.Black};

      _mMaskedPaint = new Paint();
      _mMaskedPaint.SetXfermode(new PorterDuffXfermode(PorterDuff.Mode.SrcIn));

      // Always want a cache allocated.
      _mCacheBitmap = Bitmap.CreateBitmap(1, 1, Bitmap.Config.Argb8888);

      // Create a desaturate color filter for pressed state.
      var cm = new ColorMatrix();
      cm.SetSaturation(0);
      _mDesaturateColorFilter = new ColorMatrixColorFilter(cm);

      //create a selectorFilter if we already have a color
      if (_mSelectorColor != 0)
      {
        _mSelectorFilter =
          new PorterDuffColorFilter(
            Color.Argb(_mSelectorAlpha, Color.GetRedComponent(_mSelectorColor), Color.GetGreenComponent(_mSelectorColor),
              Color.GetBlueComponent(_mSelectorColor)), PorterDuff.Mode.SrcAtop);
      }
    }

    protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
    {
      if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
      {
        OutlineProvider = new CustomOutline(w, h);
      }
    }


    [TargetApi(Value = 21)]
    private class CustomOutline : ViewOutlineProvider
    {
      private readonly int _width;
      private readonly int _height;

      internal CustomOutline(int width, int height)
      {
        _width = width;
        _height = height;
      }

      public override void GetOutline(View view, Outline outline)
      {
        outline.SetOval(0, 0, _width, _height);
      }
    }

    protected override bool SetFrame(int l, int t, int r, int b)
    {
      var changed = base.SetFrame(l, t, r, b);

      _mBounds = new Rect(0, 0, r - l, b - t);
      _mBoundsF = new RectF(_mBounds);

      if (_mMaskDrawable != null)
      {
        _mMaskDrawable.Bounds = _mBounds;
      }

      if (changed)
      {
        _mCacheValid = false;
      }

      return changed;
    }

    protected override void OnDraw(Canvas canvas)
    {
      if (_mBounds == null)
      {
        return;
      }

      var width = _mBounds.Width();
      var height = _mBounds.Height();

      if (width == 0 || height == 0)
      {
        return;
      }

      if (!_mCacheValid || width != _mCachedWidth || height != _mCachedHeight || _isSelected != _isPressed)
      {
        // Need to redraw the cache
        if (width == _mCachedWidth && height == _mCachedHeight)
        {
          // Have a correct-sized bitmap cache already allocated. Just erase it.
          _mCacheBitmap.EraseColor(0);
        }
        else
        {
          // Allocate a new bitmap with the correct dimensions.
          _mCacheBitmap.Recycle();
          //noinspection AndroidLintDrawAllocation
          _mCacheBitmap = Bitmap.CreateBitmap(width, height, Bitmap.Config.Argb8888);
          _mCachedWidth = width;
          _mCachedHeight = height;
        }

        var cacheCanvas = new Canvas(_mCacheBitmap);
        if (_mMaskDrawable != null)
        {
          int sc = cacheCanvas.Save();
          _mMaskDrawable.Draw(cacheCanvas);
          if (_isSelected)
          {
            _mMaskedPaint.SetColorFilter(_mSelectorFilter ?? _mDesaturateColorFilter);
          }
          else
          {
            _mMaskedPaint.SetColorFilter(null);
          }
          cacheCanvas.SaveLayer(_mBoundsF, _mMaskedPaint, SaveFlags.HasAlphaLayer | SaveFlags.FullColorLayer);
          base.OnDraw(cacheCanvas);
          cacheCanvas.RestoreToCount(sc);
        }
        else if (_isSelected)
        {
          var sc = cacheCanvas.Save();
          cacheCanvas.DrawRect(0, 0, _mCachedWidth, _mCachedHeight, _mBlackPaint);
          _mMaskedPaint.SetColorFilter(_mSelectorFilter ?? _mDesaturateColorFilter);

          cacheCanvas.SaveLayer(_mBoundsF, _mMaskedPaint, SaveFlags.HasAlphaLayer | SaveFlags.FullColorLayer);
          base.OnDraw(cacheCanvas);
          cacheCanvas.RestoreToCount(sc);
        }
        else
        {
          base.OnDraw(cacheCanvas);
        }
      }

      // Draw from cache
      canvas.DrawBitmap(_mCacheBitmap, _mBounds.Left, _mBounds.Top, null);

      //remember the previous press state
      _isPressed = Pressed;
    }


    public override bool DispatchTouchEvent(MotionEvent e)
    {
      // Check for clickable state and do nothing if disabled
      if (!Clickable)
      {
        _isSelected = false;
        return OnTouchEvent(e);
      }

      // Set selected state based on Motion Event
      switch (e.Action)
      {
        case MotionEventActions.Down:
          _isSelected = true;
          break;
        case MotionEventActions.Up:
        case MotionEventActions.Scroll:
        case MotionEventActions.Outside:
        case MotionEventActions.Cancel:
          _isSelected = false;
          break;
      }

      // Redraw image and return super type
      Invalidate();
      return base.DispatchTouchEvent(e);
    }

    protected override void DrawableStateChanged()
    {
      base.DrawableStateChanged();
      if (_mMaskDrawable != null && _mMaskDrawable.IsStateful)
      {
        _mMaskDrawable.SetState(GetDrawableState());
      }
      if (DuplicateParentStateEnabled)
      {
        ViewCompat.PostInvalidateOnAnimation(this);
      }
    }

    public override void InvalidateDrawable(Drawable drawable)
    {
      if (drawable == _mMaskDrawable)
      {
        Invalidate();
      }
      else
      {
        base.InvalidateDrawable(drawable);
      }
    }

    protected override bool VerifyDrawable(Drawable who)
    {
      return who == _mMaskDrawable || base.VerifyDrawable(who);
    }


    /// <summary>
    /// Sets the color of the selector to be draw over the
    /// CircularImageView. Be sure to provide some opacity.
    /// </summary>
    /// <param name="selectorColor">The color (including alpha) to set for the selector overlay.</param>
    public void SetSelectorColor(int selectorColor)
    {
      _mSelectorColor = selectorColor;
      _mSelectorFilter =
        new PorterDuffColorFilter(
          Color.Argb(_mSelectorAlpha, Color.GetRedComponent(_mSelectorColor), Color.GetGreenComponent(_mSelectorColor),
            Color.GetBlueComponent(_mSelectorColor)), PorterDuff.Mode.SrcAtop);
      Invalidate();
    }

    public override void SetImageDrawable(Drawable drawable)
    {
      DrawerImageLoader.Instance.CancelImage(this);
      base.SetImageDrawable(drawable);
    }

    public override void SetImageResource(int resId)
    {
      DrawerImageLoader.Instance.CancelImage(this);
      base.SetImageResource(resId);
    }

    public override void SetImageBitmap(Bitmap bm)
    {
      DrawerImageLoader.Instance.CancelImage(this);
      base.SetImageBitmap(bm);
    }

    public override void SetImageURI(Uri uri)
    {
      if (uri.Scheme.Equals("http") || uri.Scheme.Equals("https"))
      {
        DrawerImageLoader.Instance.SetImage(this, uri);
      }
      else
      {
        base.SetImageURI(uri);
      }
    }

    private ColorMatrixColorFilter _mTempDesaturateColorFilter;
    private ColorFilter _mTempSelectorFilter;

    public void DisableTouchFeedback(bool disable)
    {
      if (disable)
      {
        _mTempDesaturateColorFilter = _mDesaturateColorFilter;
        _mTempSelectorFilter = _mSelectorFilter;
        _mSelectorFilter = null;
        _mDesaturateColorFilter = null;
      }
      else
      {
        if (_mTempDesaturateColorFilter != null)
        {
          _mDesaturateColorFilter = _mTempDesaturateColorFilter;
        }
        if (_mTempSelectorFilter != null)
        {
          _mSelectorFilter = _mTempSelectorFilter;
        }
      }
    }
  }
}