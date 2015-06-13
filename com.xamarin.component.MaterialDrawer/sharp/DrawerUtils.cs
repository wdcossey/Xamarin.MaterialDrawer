using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Widget;
using com.xamarin.component.MaterialDrawer.Models;
using com.xamarin.component.MaterialDrawer.Models.Interfaces;
using com.xamarin.component.MaterialDrawer.Utils;
using Java.Lang;
using ICheckable = com.xamarin.component.MaterialDrawer.Models.Interfaces.ICheckable;

namespace com.xamarin.component.MaterialDrawer
{

  /// <summary>
  /// Created by wdcossey on 07.06.15.
  /// Original by mikepenz on 23.05.15.
  /// </summary>
  internal class DrawerUtils
  {

    /// <summary>
    /// helper method to handle the onClick of the footer
    /// </summary>
    /// <param name="drawer"></param>
    /// <param name="drawerItem"></param>
    /// <param name="v"></param>
    /// <param name="fireOnClick"></param>
    public static void OnFooterDrawerItemClick(DrawerBuilder drawer, IDrawerItem drawerItem, View v, bool fireOnClick)
    {
      var checkable = !(drawerItem != null && drawerItem is ICheckable && !((ICheckable) drawerItem).IsCheckable());
      if (checkable)
      {
        drawer.ResetStickyFooterSelection();

        if (Android.OS.Build.VERSION.SdkInt >= BuildVersionCodes.Honeycomb)
        {
          v.Activated = true;
        }
        v.Selected = true;

        //remove the selection in the list
        drawer.ListView.SetSelection(-1);
        drawer.ListView.SetItemChecked(drawer.CurrentSelection + drawer.HeaderOffset, false);

        //set currentSelection to -1 because we selected a stickyFooter element
        drawer.CurrentSelection = -1;

        //find the position of the clicked footer item
        if (drawer.StickyFooterView != null && drawer.StickyFooterView is LinearLayout)
        {
          LinearLayout footer = (LinearLayout) drawer.StickyFooterView;
          for (int i = 0; i < footer.ChildCount; i++)
          {
            if (footer.GetChildAt(i) == v)
            {
              drawer.CurrentFooterSelection = i;
              break;
            }
          }
        }
      }


      var consumed = false;
      if (fireOnClick && drawer.mOnDrawerItemClickListener != null)
      {
        consumed = drawer.mOnDrawerItemClickListener.OnItemClick(null, v, -1, -1, drawerItem);
      }

      if (!consumed)
      {
        //close the drawer after click
        drawer.CloseDrawerDelayed();
      }
    }

    /// <summary>
    /// helper method to set the selection in the lsit
    /// </summary>
    /// <param name="drawer"></param>
    /// <param name="position"></param>
    /// <param name="fireOnClick"></param>
    /// <returns></returns>
    public static bool SetListSelection(DrawerBuilder drawer, int position, bool fireOnClick)
    {
      return SetListSelection(drawer, position, fireOnClick, null);
    }

    /// <summary>
    /// helper method to set the selection in the list
    /// </summary>
    /// <param name="drawer"></param>
    /// <param name="position"></param>
    /// <param name="fireOnClick"></param>
    /// <param name="drawerItem"></param>
    /// <returns></returns>
    public static bool SetListSelection(DrawerBuilder drawer, int position, bool fireOnClick, IDrawerItem drawerItem)
    {
      if (position >= -1)
      {
        //predefine selection (should be the first element
        if (drawer.ListView != null && (position + drawer.HeaderOffset) > -1)
        {
          drawer.ResetStickyFooterSelection();
          drawer.ListView.SetSelection(position + drawer.HeaderOffset);
          drawer.ListView.SetItemChecked(position + drawer.HeaderOffset, true);
          drawer.CurrentSelection = position;
          drawer.CurrentFooterSelection = -1;
        }

        if (fireOnClick && drawer.mOnDrawerItemClickListener != null)
        {
          return drawer.mOnDrawerItemClickListener.OnItemClick(null, null, position - drawer.HeaderOffset, -1,
            drawerItem);
        }
      }

      return false;
    }

    /// <summary>
    /// helper method to set the selection of the footer
    /// </summary>
    /// <param name="drawer"></param>
    /// <param name="position"></param>
    /// <param name="fireOnClick"></param>
    public static void SetFooterSelection(DrawerBuilder drawer, int position, bool fireOnClick)
    {
      if (position > -1)
      {
        if (drawer.StickyFooterView != null && drawer.StickyFooterView is LinearLayout)
        {
          var footer = (LinearLayout) drawer.StickyFooterView;

          if (footer.ChildCount > position && position >= 0)
          {
            IDrawerItem drawerItem = (IDrawerItem) footer.GetChildAt(position).Tag;
            OnFooterDrawerItemClick(drawer, drawerItem, footer.GetChildAt(position), fireOnClick);
          }
        }
      }
    }

    /// <summary>
    /// calculates the position of an drawerItem. searching by it's identifier
    /// </summary>
    /// <param name="drawer"></param>
    /// <param name="identifier"></param>
    /// <returns></returns>
    public static int GetPositionFromIdentifier(DrawerBuilder drawer, int identifier)
    {
      if (identifier >= 0)
      {
        if (drawer.DrawerItems != null)
        {
          int position = 0;
          foreach (var i in drawer.DrawerItems)
          {
            if (i.GetIdentifier() == identifier)
            {
              return position;
            }
            position = position + 1;
          }
        }
      }

      return -1;
    }

    /// <summary>
    /// calculates the position of an drawerItem inside the footer. searching by it's identifier
    /// </summary>
    /// <param name="drawer"></param>
    /// <param name="identifier"></param>
    /// <returns></returns>
    public static int GetFooterPositionFromIdentifier(DrawerBuilder drawer, int identifier)
    {
      if (identifier >= 0)
      {
        if (drawer.StickyFooterView != null && drawer.StickyFooterView is LinearLayout)
        {
          var footer = (LinearLayout) drawer.StickyFooterView;

          for (int i = 0; i < footer.ChildCount; i++)
          {
            var o = footer.GetChildAt(i).Tag;
            if (o != null && o is IDrawerItem && ((IDrawerItem) o).GetIdentifier() == identifier)
            {
              return i;
            }
          }
        }
      }

      return -1;
    }

    /// <summary>
    /// helper method to set the TranslucentStatusFlag
    /// </summary>
    /// <param name="activity"></param>
    /// <param name="on"></param>
    public static void SetTranslucentStatusFlag(Activity activity, bool on)
    {
      if (Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat)
      {
        SetFlag(activity, WindowManagerFlags.TranslucentStatus, on);
      }
    }

    /// <summary>
    /// helper method to set the TranslucentNavigationFlag
    /// </summary>
    /// <param name="activity"></param>
    /// <param name="on"></param>
    public static void SetTranslucentNavigationFlag(Activity activity, bool on)
    {
      if (Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat)
      {
        SetFlag(activity, WindowManagerFlags.TranslucentNavigation, on);
      }
    }

    /// <summary>
    /// helper method to activate or deactivate a specific flag
    /// </summary>
    /// <param name="activity"></param>
    /// <param name="bits"></param>
    /// <param name="on"></param>
    public static void SetFlag(Activity activity, WindowManagerFlags bits, bool on)
    {
      Window win = activity.Window;
      var winParams = win.Attributes;
      if (on)
      {
        winParams.Flags |= bits;
      }
      else
      {
        winParams.Flags &= ~bits;
      }
      win.Attributes = winParams;
    }

    /// <summary>
    /// helper method to handle the headerView
    /// </summary>
    /// <param name="drawer"></param>
    public static void HandleHeaderView(DrawerBuilder drawer)
    {
      //use the AccountHeader if set
      if (drawer.AccountHeader != null)
      {
        if (drawer.AccountHeaderSticky)
        {
          drawer.StickyHeaderView = drawer.AccountHeader.GetView();
        }
        else
        {
          drawer.HeaderView = drawer.AccountHeader.GetView();
        }
      }

      //sticky header view
      if (drawer.StickyHeaderView != null)
      {
        //add the sticky footer view and align it to the bottom
        RelativeLayout.LayoutParams layoutParams = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent,
          ViewGroup.LayoutParams.WrapContent);
        layoutParams.AddRule(LayoutRules.AlignParentTop, 1);
        drawer.StickyHeaderView.Id = Resource.Id.sticky_header;
        drawer.SliderLayout.AddView(drawer.StickyHeaderView, 0, layoutParams);

        //now align the listView above the stickyFooterView ;)
        RelativeLayout.LayoutParams layoutParamsListView =
          (RelativeLayout.LayoutParams) drawer.ListView.LayoutParameters;
        layoutParamsListView.AddRule(LayoutRules.Below, Resource.Id.sticky_header);
        drawer.ListView.LayoutParameters = layoutParamsListView;

        //remove the padding of the listView again we have the header on top of it
        drawer.ListView.SetPadding(0, 0, 0, 0);
      }

      // set the header (do this before the setAdapter because some devices will crash else
      if (drawer.HeaderView != null)
      {
        if (drawer.ListView == null)
        {
          throw new RuntimeException("can't use a headerView without a listView");
        }

        if (drawer.HeaderDivider)
        {
          LinearLayout headerContainer =
            (LinearLayout)
              drawer.Activity.LayoutInflater.Inflate(Resource.Layout.material_drawer_item_header, drawer.ListView,
                false);
          headerContainer.AddView(drawer.HeaderView, 0);
          //set the color for the divider
          headerContainer.FindViewById(Resource.Id.divider)
            .SetBackgroundColor(UIUtils.GetThemeColorFromAttrOrRes(drawer.Activity,
              Resource.Attribute.material_drawer_divider, Resource.Color.material_drawer_divider));
          //add the headerContainer to the list
          drawer.ListView.AddHeaderView(headerContainer, null, drawer.HeaderClickable);
          //link the view including the container to the headerView field
          drawer.HeaderView = headerContainer;
        }
        else
        {
          drawer.ListView.AddHeaderView(drawer.HeaderView, null, drawer.HeaderClickable);
        }
        //set the padding on the top to 0
        drawer.ListView.SetPadding(drawer.ListView.PaddingLeft, 0, drawer.ListView.PaddingRight,
          drawer.ListView.PaddingBottom);
      }
    }

    /// <summary>
    /// small helper to rebuild the FooterView
    /// </summary>
    /// <param name="drawer"></param>
    public static void RebuildFooterView(DrawerBuilder drawer)
    {
      if (drawer.SliderLayout != null)
      {
        if (drawer.StickyFooterView != null && drawer.StickyFooterView is ViewGroup)
        {
          ((LinearLayout) drawer.StickyFooterView).RemoveAllViews();
        }

        //todo: wdcossey
        //handle the footer
  //      DrawerUtils.FillStickyDrawerItemFooter(
  //        drawer, 
  //        drawer.StickyFooterView, 
  //        new View.OnClickListener()
  //      {
  //        @Override
  //      public void onClick(View v) {
  //IDrawerItem drawerItem = (IDrawerItem) v.getTag();
  //DrawerUtils.onFooterDrawerItemClick(drawer,
  //        drawerItem,
  //        v,
  //        true);
  //      }
  //    }
  //    )
  //      ;

        SetFooterSelection(drawer, drawer.CurrentFooterSelection, false);
      }
    }

    /**
     * helper method to handle the footerView
     *
     * @param drawer
     */

    public static void HandleFooterView(DrawerBuilder drawer, View.IOnClickListener onClickListener)
    {
      //use the StickyDrawerItems if set
      if (drawer.mStickyDrawerItems != null && drawer.mStickyDrawerItems.Count > 0)
      {
        drawer.StickyFooterView = BuildStickyDrawerItemFooter(drawer, onClickListener);
      }

      //sticky footer view
      if (drawer.StickyFooterView != null)
      {
        //add the sticky footer view and align it to the bottom
        RelativeLayout.LayoutParams layoutParams = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent,
          ViewGroup.LayoutParams.WrapContent);
        layoutParams.AddRule(LayoutRules.AlignParentBottom, 1);
        drawer.StickyFooterView.Id = Resource.Id.sticky_footer;
        drawer.SliderLayout.AddView(drawer.StickyFooterView, layoutParams);

        if ((drawer.TranslucentNavigationBar || drawer.Fullscreen) &&
            Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat)
        {
          drawer.StickyFooterView.SetPadding(0, 0, 0, UIUtils.GetNavigationBarHeight(drawer.Activity));
        }

        //now align the listView above the stickyFooterView ;)
        RelativeLayout.LayoutParams layoutParamsListView =
          (RelativeLayout.LayoutParams) drawer.ListView.LayoutParameters;
        layoutParamsListView.AddRule(LayoutRules.Above, Resource.Id.sticky_footer);
        drawer.ListView.LayoutParameters = layoutParamsListView;

        //remove the padding of the listView again we have the footer below it
        drawer.ListView.SetPadding(drawer.ListView.PaddingLeft, drawer.ListView.PaddingTop,
          drawer.ListView.PaddingRight,
          drawer.Activity.Resources.GetDimensionPixelSize(Resource.Dimension.material_drawer_padding));
      }

      // set the footer (do this before the setAdapter because some devices will crash else
      if (drawer.FooterView != null)
      {
        if (drawer.ListView == null)
        {
          throw new RuntimeException("can't use a footerView without a listView");
        }

        if (drawer.FooterDivider)
        {
          LinearLayout footerContainer =
            (LinearLayout)
              drawer.Activity.LayoutInflater.Inflate(Resource.Layout.material_drawer_item_footer, drawer.ListView,
                false);
          footerContainer.AddView(drawer.FooterView, 1);
          //set the color for the divider
          footerContainer.FindViewById(Resource.Id.divider)
            .SetBackgroundColor(UIUtils.GetThemeColorFromAttrOrRes(drawer.Activity,
              Resource.Attribute.material_drawer_divider, Resource.Color.material_drawer_divider));
          //add the footerContainer to the list
          drawer.ListView.AddFooterView(footerContainer, null, drawer.FooterClickable);
          //link the view including the container to the footerView field
          drawer.FooterView = footerContainer;
        }
        else
        {
          drawer.ListView.AddFooterView(drawer.FooterView, null, drawer.FooterClickable);
        }
      }
    }


    /**
     * build the sticky footer item view
     *
     * @return
     */

    public static ViewGroup BuildStickyDrawerItemFooter(DrawerBuilder drawer, View.IOnClickListener onClickListener)
    {
      //create the container view
      LinearLayout linearLayout = new LinearLayout(drawer.Activity);
      linearLayout.LayoutParameters = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent,
        ViewGroup.LayoutParams.WrapContent);
      linearLayout.Orientation = Orientation.Vertical;
      //set the background color to the drawer background color (if it has alpha the shadow won't be visible)
      linearLayout.SetBackgroundColor(UIUtils.GetThemeColorFromAttrOrRes(drawer.Activity,
        Resource.Attribute.material_drawer_background, Resource.Color.material_drawer_background));

      if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
      {
        //set the elevation shadow
        linearLayout.Elevation = UIUtils.ConvertDpToPixel(4f, drawer.Activity);
      }
      else
      {
        //if we use the default values and we are on a older sdk version we want the divider
        if (drawer.StickyFooterDivider == null)
        {
          drawer.StickyFooterDivider = true;
        }
      }

      //create the divider
      if (drawer.StickyFooterDivider != null && drawer.StickyFooterDivider.Value)
      {
        LinearLayout divider = new LinearLayout(drawer.Activity);
        LinearLayout.LayoutParams dividerParams = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent,
          ViewGroup.LayoutParams.WrapContent);
        //remove bottomMargin --> See inbox it also has no margin here
        //dividerParams.bottomMargin = mActivity.getResources().getDimensionPixelSize(R.dimen.material_drawer_padding);
        divider.SetMinimumHeight((int) UIUtils.ConvertDpToPixel(1, drawer.Activity));
        divider.Orientation = Orientation.Vertical;
        divider.SetBackgroundColor(UIUtils.GetThemeColorFromAttrOrRes(drawer.Activity,
          Resource.Attribute.material_drawer_divider, Resource.Color.material_drawer_divider));
        linearLayout.AddView(divider, dividerParams);
      }

      FillStickyDrawerItemFooter(drawer, linearLayout, onClickListener);

      return linearLayout;
    }

    /**
     * helper method to fill the sticky footer with it's elements
     *
     * @param drawer
     * @param container
     * @param onClickListener
     */

    public static void FillStickyDrawerItemFooter(DrawerBuilder drawer, ViewGroup container,
      View.IOnClickListener onClickListener)
    {
      //get the inflater
      LayoutInflater layoutInflater = LayoutInflater.From(container.Context);
      int padding =
        container.Context.Resources.GetDimensionPixelSize(Resource.Dimension.material_drawer_vertical_padding);

      //add all drawer items
      foreach (var drawerItem in drawer.mStickyDrawerItems)
      {
        //get the selected_color
        var selected_color = UIUtils.GetThemeColorFromAttrOrRes(container.Context,
          Resource.Attribute.material_drawer_selected, Resource.Color.material_drawer_selected);
        if (drawerItem is PrimaryDrawerItem)
        {
          if (selected_color == 0 && ((PrimaryDrawerItem) drawerItem).GetSelectedColorRes() != -1)
          {
            selected_color = container.Context.Resources.GetColor(((PrimaryDrawerItem) drawerItem).GetSelectedColorRes());
          }
          else if (((PrimaryDrawerItem) drawerItem).GetSelectedColor() != 0)
          {
            selected_color = ((PrimaryDrawerItem) drawerItem).GetSelectedColor();
          }
        }
        else if (drawerItem is SecondaryDrawerItem)
        {
          if (selected_color == 0 && ((SecondaryDrawerItem) drawerItem).GetSelectedColorRes() != -1)
          {
            selected_color =
              container.Context.Resources.GetColor(((SecondaryDrawerItem) drawerItem).GetSelectedColorRes());
          }
          else if (((SecondaryDrawerItem) drawerItem).GetSelectedColor() != 0)
          {
            selected_color = ((SecondaryDrawerItem) drawerItem).GetSelectedColor();
          }
        }

        View view = drawerItem.ConvertView(layoutInflater, null, container);
        view.Tag = (Object) drawerItem;

        if (drawerItem.IsEnabled())
        {
          UIUtils.SetBackground(view, UIUtils.GetSelectableBackground(container.Context, selected_color));
          view.SetOnClickListener(onClickListener);
        }

        //don't ask my why but it forgets the padding from the original layout
        view.SetPadding(padding, 0, padding, 0);
        container.AddView(view);
      }

      foreach (var drawerItem in drawer.mStickyDrawerItems)
      {

      }

      //and really. don't ask about this. it won't set the padding if i don't set the padding for the container
      container.SetPadding(0, 0, 0, 0);
    }


    /**
     * helper to extend the layoutParams of the drawer
     *
     * @param params
     * @return
     */

    public static DrawerLayout.LayoutParams ProcessDrawerLayoutParams(DrawerBuilder drawer,
      DrawerLayout.LayoutParams @params)
    {
      if (@params != null)
      {
        if (drawer.DrawerGravity != null &&
            (drawer.DrawerGravity == (int) GravityFlags.Right || drawer.DrawerGravity == (int) GravityFlags.End))
        {
          @params.RightMargin = 0;
          if (Android.OS.Build.VERSION.SdkInt >= BuildVersionCodes.JellyBeanMr1)
          {
            @params.MarginEnd = 0;
          }

          @params.LeftMargin =
            drawer.Activity.Resources.GetDimensionPixelSize(Resource.Dimension.material_drawer_margin);
          if (Android.OS.Build.VERSION.SdkInt >= BuildVersionCodes.JellyBeanMr1)
          {
            @params.MarginEnd =
              drawer.Activity.Resources.GetDimensionPixelSize(Resource.Dimension.material_drawer_margin);
          }
        }

        if (drawer.TranslucentActionBarCompatibility)
        {
          int topMargin = UIUtils.GetActionBarHeight(drawer.Activity);
          if (drawer.TranslucentStatusBar)
          {
            topMargin = topMargin + UIUtils.GetStatusBarHeight(drawer.Activity);
          }
          @params.TopMargin = topMargin;
        }
        else if (drawer.DisplayBelowStatusBar != null && drawer.DisplayBelowStatusBar.Value)
        {
          @params.TopMargin = UIUtils.GetStatusBarHeight(drawer.Activity, true);
        }

        if (drawer.DrawerWidth > -1)
        {
          @params.Width = drawer.DrawerWidth;
        }
        else
        {
          @params.Width = UIUtils.GetOptimalDrawerWidth(drawer.Activity);
        }
      }

      return @params;
    }
  }

}