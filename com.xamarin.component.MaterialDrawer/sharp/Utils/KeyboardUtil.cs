using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Views.InputMethods;

namespace com.xamarin.component.MaterialDrawer.Utils
{
  public class KeyboardUtil/*: Java.Lang.Object, ViewTreeObserver.IOnGlobalLayoutListener*/
  {
    private readonly View _decorView;
    private readonly View _contentView;
    private float _initialDpDiff = -1;

    public KeyboardUtil(Activity act, View contentView)
    {
      _decorView = act.Window.DecorView;
      _contentView = contentView;

      //only required on newer android versions. it was working on API level 19
      if (Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat)
      {
        _decorView.ViewTreeObserver.GlobalLayout += OnGlobalLayoutListener;
        //decorView.ViewTreeObserver.AddOnGlobalLayoutListener(this);
      }
    }

    public void Enable()
    {
      if (Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat)
      {
        _decorView.ViewTreeObserver.GlobalLayout += OnGlobalLayoutListener;
        //decorView.ViewTreeObserver.AddOnGlobalLayoutListener(this);
      }
    }

    public void Disable()
    {
      if (Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat)
      {
        _decorView.ViewTreeObserver.GlobalLayout -= OnGlobalLayoutListener;
        //decorView.ViewTreeObserver.RemoveOnGlobalLayoutListener(this);
      }
    }


    /// <summary>
    /// a small helper to allow showing the editText focus
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="eventArgs"></param>
    private void OnGlobalLayoutListener(object sender, EventArgs eventArgs)
    {
      var r = new Rect();
      //r will be populated with the coordinates of your view that area still visible.
      _decorView.GetWindowVisibleDisplayFrame(r);

      //get the height diff as dp
      var heightDiffDp = UIUtils.ConvertPixelsToDp(_decorView.RootView.Height - (r.Bottom - r.Top), _decorView.Context);

      //set the initialDpDiff at the beginning. (on my phone this was 73dp)
      if (_initialDpDiff.Equals(-1))
      {
        _initialDpDiff = heightDiffDp;
      }

      //if it could be a keyboard add the padding to the view
      if (heightDiffDp - _initialDpDiff > 100)
      {
        // if more than 100 pixels, its probably a keyboard...
        //check if the padding is 0 (if yes set the padding for the keyboard)
        if (_contentView.PaddingBottom == 0)
        {
          //set the padding of the contentView for the keyboard
          _contentView.SetPadding(0, 0, 0,
            (int)UIUtils.ConvertDpToPixel((heightDiffDp - _initialDpDiff), _decorView.Context));
        }
      }
      else
      {
        //check if the padding is != 0 (if yes reset the padding)
        if (_contentView.PaddingBottom != 0)
        {
          //reset the padding of the contentView
          _contentView.SetPadding(0, 0, 0, 0);
        }
      }
    }

    /// <summary>
    /// Helper to hide the keyboard
    /// </summary>
    /// <param name="act"></param>
    public static void HideKeyboard(Activity act)
    {
      if (act == null || act.CurrentFocus == null) 
        return;

      var inputMethodManager = (InputMethodManager) act.GetSystemService(Context.InputMethodService);
      inputMethodManager.HideSoftInputFromWindow(act.CurrentFocus.WindowToken, 0);
    }

    //todo: wdcossey
    //public void OnGlobalLayout()
    //{
    //  var r = new Rect();
    //  //r will be populated with the coordinates of your view that area still visible.
    //  decorView.GetWindowVisibleDisplayFrame(r);

    //  //get the height diff as dp
    //  var heightDiffDp = UIUtils.ConvertPixelsToDp(decorView.RootView.Height - (r.Bottom - r.Top), decorView.Context);

    //  //set the initialDpDiff at the beginning. (on my phone this was 73dp)
    //  if (initialDpDiff.Equals(-1))
    //  {
    //    initialDpDiff = heightDiffDp;
    //  }

    //  //if it could be a keyboard add the padding to the view
    //  if (heightDiffDp - initialDpDiff > 100)
    //  {
    //    // if more than 100 pixels, its probably a keyboard...
    //    //check if the padding is 0 (if yes set the padding for the keyboard)
    //    if (contentView.PaddingBottom == 0)
    //    {
    //      //set the padding of the contentView for the keyboard
    //      contentView.SetPadding(0, 0, 0,
    //        (int) UIUtils.ConvertDpToPixel((heightDiffDp - initialDpDiff), decorView.Context));
    //    }
    //  }
    //  else
    //  {
    //    //check if the padding is != 0 (if yes reset the padding)
    //    if (contentView.PaddingBottom != 0)
    //    {
    //      //reset the padding of the contentView
    //      contentView.SetPadding(0, 0, 0, 0);
    //    }
    //  }
    //}
  }
}