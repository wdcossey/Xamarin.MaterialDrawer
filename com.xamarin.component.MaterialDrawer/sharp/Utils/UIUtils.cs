using System;
using Android.Annotation;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Util;
using Android.Views;

namespace com.xamarin.component.MaterialDrawer.Utils
{
  [SuppressLint(Value = new[] {"InlinedApi"})]
  public static class UIUtils
  {
    public static ColorStateList GetTextColorStateList(int text_color, int selected_text_color)
    {
      return new ColorStateList(
        new[]
        {
          new[]
          {
            Android.Resource.Attribute.StateActivated
          },
          new int[]
          {

          }
        },
        new[]
        {
          selected_text_color,
          text_color
        });
    }

    public static StateListDrawable GetIconStateList(Drawable icon, Drawable selectedIcon)
    {
      var iconStateListDrawable = new StateListDrawable();
      iconStateListDrawable.AddState(new int[] {Android.Resource.Attribute.StateActivated}, selectedIcon);
      iconStateListDrawable.AddState(new int[] {}, icon);
      return iconStateListDrawable;
    }

    public static StateListDrawable GetDrawerItemBackground(Color selected_color)
    {
      var clrActive = new ColorDrawable(selected_color);
      var states = new StateListDrawable();
      states.AddState(new int[] {Android.Resource.Attribute.StateActivated}, clrActive);
      return states;
    }


    /// <summary>
    /// helper to get the system default selectable background inclusive an active state
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="selected_color"></param>
    /// <returns></returns>
    public static StateListDrawable GetSelectableBackground(Context ctx, Color selected_color)
    {
      var states = GetDrawerItemBackground(selected_color);
      states.AddState(new int[] {}, GetCompatDrawable(ctx, GetSelectableBackground(ctx)));
      return states;
    }

    /// <summary>
    /// helper to get the system default selectable background
    /// </summary>
    /// <param name="ctx"></param>
    /// <returns></returns>
    public static int GetSelectableBackground(Context ctx)
    {
      if (Build.VERSION.SdkInt >= BuildVersionCodes.Honeycomb)
      {
        // If we're running on Honeycomb or newer, then we can use the Theme's
        // selectableItemBackground to ensure that the View has a pressed state
        var outValue = new TypedValue();
        ctx.Theme.ResolveAttribute(Resource.Attribute.selectableItemBackground, outValue, true);
        return outValue.ResourceId;
      }
      else
      {
        var outValue = new TypedValue();
        ctx.Theme.ResolveAttribute(Android.Resource.Attribute.ItemBackground, outValue, true);
        return outValue.ResourceId;
      }
    }

    public static int GetThemeColor(Context ctx, int attr)
    {
      var tv = new TypedValue();
      return ctx.Theme.ResolveAttribute(attr, tv, true) ? tv.Data : 0;
    }

    /// <summary>
    /// helper method to get the color by attr (which is defined in the style) or by resource.
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="attr"></param>
    /// <param name="res"></param>
    /// <returns></returns>
    public static Color GetThemeColorFromAttrOrRes(Context ctx, int attr, int res)
    {
      var color = GetThemeColor(ctx, attr);
      if (color == 0)
      {
        return ctx.Resources.GetColor(res);
      }
      return new Color(color);
    }

    /// <summary>
    /// helper method to set the background depending on the android version
    /// </summary>
    /// <param name="v"></param>
    /// <param name="d"></param>
    [SuppressLint(Value = new[] {"NewApi"})]
    public static void SetBackground(View v, Drawable d)
    {
      if (Build.VERSION.SdkInt < BuildVersionCodes.JellyBean)
      {
        v.SetBackgroundDrawable(d);
      }
      else
      {
        v.Background = d;
      }
    }

    /// <summary>
    /// helper method to set the background depending on the android version
    /// </summary>
    /// <param name="v"></param>
    /// <param name="drawableRes"></param>
    public static void SetBackground(View v, int drawableRes)
    {
      SetBackground(v, GetCompatDrawable(v.Context, drawableRes));
    }


    /// <summary>
    /// helper method to get the drawable by its resource. specific to the correct android version
    /// </summary>
    /// <param name="c"></param>
    /// <param name="drawableRes"></param>
    /// <returns></returns>
    public static Drawable GetCompatDrawable(Context c, int drawableRes)
    {
      Drawable d = null;
      try
      {
        d = Build.VERSION.SdkInt < BuildVersionCodes.Lollipop
          ? c.Resources.GetDrawable(drawableRes)
          : c.Resources.GetDrawable(drawableRes, c.Theme);
      }
      catch (Exception ex)
      {
      }
      return d;
    }


    /// <summary>
    /// Returns the screen width in pixels
    /// </summary>
    /// <param name="context">is the context to get the resources</param>
    /// <returns>the screen width in pixels</returns>
    public static int GetScreenWidth(Context context)
    {
      return context.Resources.DisplayMetrics.WidthPixels;
    }

    /// <summary>
    /// Returns the size in pixels of an attribute dimension
    /// </summary>
    /// <param name="context">the context to get the resources from</param>
    /// <param name="attr">is the attribute dimension we want to know the size from</param>
    /// <returns>the size in pixels of an attribute dimension</returns>
    public static int GetThemeAttributeDimensionSize(Context context, int attr)
    {
      TypedArray a = null;
      try
      {
        a = context.Theme.ObtainStyledAttributes(new int[] {attr});
        return a.GetDimensionPixelSize(0, 0);
      }
      finally
      {
        if (a != null)
          a.Recycle();
      }
    }

    /// <summary>
    /// helper to calculate the optimal drawer width
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static int GetOptimalDrawerWidth(Context context)
    {
      var possibleMinDrawerWidth = GetScreenWidth(context) - GetActionBarHeight(context);
      var maxDrawerWidth = context.Resources.GetDimensionPixelSize(Resource.Dimension.material_drawer_width);
      return Math.Min(possibleMinDrawerWidth, maxDrawerWidth);
    }


    /// <summary>
    /// helper to calculate the navigationBar height
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static int GetNavigationBarHeight(Context context)
    {
      var resources = context.Resources;
      var id =
        resources.GetIdentifier(
          context.Resources.Configuration.Orientation == Orientation.Portrait
            ? "navigation_bar_height"
            : "navigation_bar_height_landscape", "dimen", "android");
      return id > 0 ? resources.GetDimensionPixelSize(id) : 0;
    }

    /// <summary>
    /// helper to calculate the actionBar height
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static int GetActionBarHeight(Context context)
    {
      var actionBarHeight = GetThemeAttributeDimensionSize(context, Android.Resource.Attribute.ActionBarSize);
      if (actionBarHeight == 0)
      {
        actionBarHeight = context.Resources.GetDimensionPixelSize(Resource.Dimension.abc_action_bar_default_height_material);
      }
      return actionBarHeight;
    }

    ///// <summary>
    ///// helper to calculate the statusBar height
    ///// </summary>
    ///// <param name="context"></param>
    ///// <returns></returns>
    public static int GetStatusBarHeight(Context context)
    {
      return GetStatusBarHeight(context, false);
    }


    /// <summary>
    /// helper to calculate the statusBar height
    /// </summary>
    /// <param name="context"></param>
    /// <param name="force">pass true to get the height even if the device has no translucent statusBar</param>
    /// <returns></returns>
    public static int GetStatusBarHeight(Context context, bool force)
    {
      var result = 0;
      var resourceId = context.Resources.GetIdentifier("status_bar_height", "dimen", "android");
      if (resourceId > 0)
      {
        result = context.Resources.GetDimensionPixelSize(resourceId);
      }

      var dimenResult = context.Resources.GetDimensionPixelSize(Resource.Dimension.tool_bar_top_padding);

      //if our dimension is 0 return 0 because on those devices we don't need the height
      if (dimenResult == 0 && !force)
      {
        return 0;
      }
      else
      {
        //if our dimens is > 0 && the result == 0 use the dimenResult else the result;
        return result == 0 ? dimenResult : result;
      }
    }


    /// <summary>
    /// This method converts dp unit to equivalent pixels, depending on device density.
    /// </summary>
    /// <param name="dp">A value in dp (density independent pixels) unit. Which we need to convert into pixels</param>
    /// <param name="context">Context to get resources and device specific display metrics</param>
    /// <returns>A float value to represent px equivalent to dp depending on device density</returns>
    public static float ConvertDpToPixel(float dp, Context context)
    {
      var resources = context.Resources;
      var metrics = resources.DisplayMetrics;
      var px = dp*(metrics.Density/160f);
      return px;
    }

    /// <summary>
    /// This method converts device specific pixels to density independent pixels.
    /// </summary>
    /// <param name="px">A value in px (pixels) unit. Which we need to convert into db</param>
    /// <param name="context">Context to get resources and device specific display metrics</param>
    /// <returns>A float value to represent dp equivalent to px value</returns>
    public static float ConvertPixelsToDp(float px, Context context)
    {
      var resources = context.Resources;
      var metrics = resources.DisplayMetrics;
      var dp = px/(metrics.Density/160f);
      return dp;
    }

    /// <summary>
    /// helper method to get a person placeHolder drawable
    /// </summary>
    /// <param name="ctx"></param>
    /// <returns></returns>
    public static Drawable GetPlaceHolder(Context ctx)
    {
      var textColor = GetThemeColorFromAttrOrRes(ctx, Resource.Attribute.material_drawer_primary_text,
        Resource.Color.material_drawer_primary_text);
      return
        new IconicsDrawable(ctx, GoogleMaterial.Icon.gmd_person).Color(textColor)
          .backgroundColorRes(Resource.Color.primary)
          .iconOffsetYDp(2)
          .paddingDp(2)
          .sizeDp(56);
    }


    /// <summary>
    /// a small helper method to simplify the color decision and get the correct value
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="color"></param>
    /// <param name="colorRes"></param>
    /// <param name="defStyle"></param>
    /// <param name="defColor"></param>
    /// <returns></returns>
    public static Color DecideColor(Context ctx, Color color, int colorRes, int defStyle, int defColor)
    {
      if (color == 0 && colorRes != -1)
      {
        return ctx.Resources.GetColor(colorRes);
      }

      if (color == 0)
      {
        return GetThemeColorFromAttrOrRes(ctx, defStyle, defColor);
      }
      return color;
    }

    /// <summary>
    /// a small helper method to simplify the icon decision and coloring for the drawerItems
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="icon"></param>
    /// <param name="iicon"></param>
    /// <param name="iconRes"></param>
    /// <param name="iconColor"></param>
    /// <param name="tint"></param>
    /// <returns></returns>
    public static Drawable DecideIcon(Context ctx, Drawable icon, IIcon iicon, int iconRes, Color iconColor, bool tint)
    {
      if (icon == null && iicon != null)
      {
        icon = new IconicsDrawable(ctx, iicon).Color(iconColor).actionBarSize().paddingDp(1);
      }
      else if (icon == null && iconRes > -1)
      {
        icon = GetCompatDrawable(ctx, iconRes);
      }

      //if we got an icon AND we have auto tinting enabled AND it is no IIcon, tint it ;)
      if (icon != null && tint && iicon == null)
      {
        icon = icon.Mutate();
        icon.SetColorFilter(iconColor, PorterDuff.Mode.SrcIn);
        //icon.SetAlpha(Color.GetAlphaComponent(iconColor));
      }

      return icon;
    }
  }
}