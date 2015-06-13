using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using com.xamarin.component.MaterialDrawer.AccountSwitcher;
using com.xamarin.component.MaterialDrawer.Adapters;
using com.xamarin.component.MaterialDrawer.Models.Interfaces;
using com.xamarin.component.MaterialDrawer.Utils;
using com.xamarin.component.MaterialDrawer.Views;
using com.xamarin.AndroidIconics;
using Java.Lang;
using ICheckable = com.xamarin.component.MaterialDrawer.Models.Interfaces.ICheckable;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V4.Widget;

namespace com.xamarin.component.MaterialDrawer
{


  /// <summary>
  /// Created by wdcossey on 07.06.15.
  /// Original by mikepenz on 23.05.15.
  /// </summary>
  public class DrawerBuilder
  {

    // some internal vars
    // variable to check if a builder is only used once
    private bool _used = false;

    protected bool Used
    {
      get { return _used; }
    }

    private int _currentSelection = -1;

    protected internal int CurrentSelection
    {
      get { return _currentSelection; }
      set { _currentSelection = value; }
    }

    private int _currentFooterSelection = -1;

    protected internal int CurrentFooterSelection
    {
      get { return _currentFooterSelection; }
      set { _currentFooterSelection = value; }
    }

    // the activity to use
    protected internal Activity Activity { get; private set; }

    protected internal ViewGroup RootView { get; private set; }

    protected internal ScrimInsetsFrameLayout DrawerContentRoot { get; private set; }

    /// <summary>
    /// default constructor
    /// </summary>
    public DrawerBuilder()
    {
      AccountHeaderSticky = false;
      DrawerGravity = null;
    }

    /// <summary>
    /// constructor With activity instead of
    /// </summary>
    /// <param name="activity"></param>
    public DrawerBuilder(Activity activity)
    {
      AccountHeaderSticky = false;
      DrawerGravity = null;
      RootView = (ViewGroup) activity.FindViewById(Android.Resource.Id.Content);
      Activity = activity;
    }

    /// <summary>
    /// Pass the activity you use the drawer in ;)
    /// This is required if you want to set any values by resource
    /// </summary>
    /// <param name="activity"></param>
    /// <returns></returns>
    public DrawerBuilder WithActivity(Activity activity)
    {
      RootView = (ViewGroup) activity.FindViewById(Android.Resource.Id.Content);
      Activity = activity;
      return this;
    }

    /// <summary>
    /// Pass the rootView of the DrawerBuilder which will be used to inflate the DrawerLayout in
    /// </summary>
    /// <param name="rootView"></param>
    /// <returns></returns>
    public DrawerBuilder WithRootView(ViewGroup rootView)
    {
      RootView = rootView;

      //disable the translucent statusBar we don't need it
      WithTranslucentStatusBar(false);

      return this;
    }

    /// <summary>
    /// Pass the rootView as resource of the DrawerBuilder which will be used to inflate the DrawerLayout in
    /// </summary>
    /// <param name="rootViewRes"></param>
    /// <returns></returns>
    public DrawerBuilder WithRootView(int rootViewRes)
    {
      if (Activity == null)
      {
        throw new RuntimeException("please pass an activity first to use this call");
      }

      return WithRootView((ViewGroup) Activity.FindViewById(rootViewRes));
    }

    // set actionbar Compatibility mode
    protected internal bool TranslucentActionBarCompatibility { get; private set; }

    /// <summary>
    /// Set this to true to use a translucent StatusBar in an activity With a good old
    /// ActionBar. Should be a rare scenario.
    /// </summary>
    /// <param name="translucentActionBarCompatibility"></param>
    /// <returns></returns>
    public DrawerBuilder WithTranslucentActionBarCompatibility(bool translucentActionBarCompatibility)
    {
      TranslucentActionBarCompatibility = translucentActionBarCompatibility;
      return this;
    }


    /// <summary>
    /// Set this to true if you want your drawer to be displayed below the toolbar.
    /// Note this will add a margin above the drawer
    /// </summary>
    /// <param name="displayBelowToolbar"></param>
    /// <returns></returns>
    public DrawerBuilder WithDisplayBelowToolbar(bool displayBelowToolbar)
    {
      TranslucentActionBarCompatibility = displayBelowToolbar;
      return this;
    }

    private bool _translucentStatusBar = true;

    /// <summary>
    /// set non translucent statusBar mode
    /// </summary>
    protected internal bool TranslucentStatusBar
    {
      get { return _translucentStatusBar; }
      private set { _translucentStatusBar = value; }
    }

    /// <summary>
    /// Set to false to disable the use of a translucent statusBar
    /// </summary>
    /// <param name="translucentStatusBar"></param>
    /// <returns></returns>
    public DrawerBuilder WithTranslucentStatusBar(bool translucentStatusBar)
    {
      TranslucentStatusBar = translucentStatusBar;

      //if we disable the translucentStatusBar it should be disabled at all
      if (!translucentStatusBar)
      {
        mTranslucentStatusBarProgrammatically = false;
      }
      return this;
    }

    /// <summary>
    /// set if we want to display the specific Drawer below the statusbar
    /// </summary>
    protected internal bool? DisplayBelowStatusBar { get; private set; }

    /// <summary>
    /// set to true if the current drawer should be displayed below the statusBar
    /// </summary>
    /// <param name="displayBelowStatusBar"></param>
    /// <returns></returns>
    public DrawerBuilder WithDisplayBelowStatusBar(bool displayBelowStatusBar)
    {
      DisplayBelowStatusBar = displayBelowStatusBar;
      return this;
    }


    // set to disable the translucent statusBar Programmatically
    protected bool mTranslucentStatusBarProgrammatically = true;

    /// <summary>
    /// set this to false if you want no translucent statusBar. or
    /// if you want to create this behavior only by theme.</summary>
    /// <param name="translucentStatusBarProgrammatically"></param>
    /// <returns></returns>
    public DrawerBuilder WithTranslucentStatusBarProgrammatically(bool translucentStatusBarProgrammatically)
    {
      mTranslucentStatusBarProgrammatically = translucentStatusBarProgrammatically;
      //if we enable the programmatically translucent statusBar we want also the normal statusBar behavior
      if (translucentStatusBarProgrammatically)
      {
        TranslucentStatusBar = true;
      }
      return this;
    }

    // defines if we want the statusBarShadow to be used in the Drawer
    protected bool? mTranslucentStatusBarShadow = null;

    /// <summary>
    /// set this to true or false if you want activate or deactivate this
    /// set it to null if you want the default behavior (activated for lollipop and up)</summary>
    /// <param name="translucentStatusBarShadow"></param>
    /// <returns></returns>
    public DrawerBuilder WithTranslucentStatusBarShadow(bool translucentStatusBarShadow)
    {
      mTranslucentStatusBarShadow = translucentStatusBarShadow;
      return this;
    }


    // the toolbar of the activity
    protected Toolbar mToolbar;

    /// <summary>
    /// Add the toolbar which is used in combination With this drawer.
    /// NOTE: if you use the drawer in a subActivity you don't need this, if you</summary>
    /// want to display the back arrow.<param name="toolbar"></param>
    /// <returns></returns>
    public DrawerBuilder WithToolbar(Toolbar toolbar)
    {
      mToolbar = toolbar;
      return this;
    }

    /// <summary>
    /// set non translucent NavigationBar mode
    /// </summary>
    protected internal bool TranslucentNavigationBar { get; private set; }

    /// <summary>
    /// Set to true if you use a translucent NavigationBar
    /// </summary>
    /// <param name="translucentNavigationBar"></param>
    /// <returns></returns>
    public DrawerBuilder WithTranslucentNavigationBar(bool translucentNavigationBar)
    {
      TranslucentNavigationBar = translucentNavigationBar;

      //if we disable the translucentNavigationBar it should be disabled at all
      if (!translucentNavigationBar)
      {
        mTranslucentNavigationBarProgrammatically = false;
      }

      return this;
    }

    // set to disable the translucent statusBar Programmatically
    protected bool mTranslucentNavigationBarProgrammatically = false;

    /// <summary>
    /// set this to true if you want a translucent navigation bar.
    /// </summary>
    /// <param name="translucentNavigationBarProgrammatically"></param>
    /// <returns></returns>
    public DrawerBuilder WithTranslucentNavigationBarProgrammatically(bool translucentNavigationBarProgrammatically)
    {
      mTranslucentNavigationBarProgrammatically = translucentNavigationBarProgrammatically;
      //if we enable the programmatically translucent navigationBar we want also the normal navigationBar behavior
      if (translucentNavigationBarProgrammatically)
      {
        TranslucentNavigationBar = true;
      }
      return this;
    }

    /// <summary>
    /// set non translucent NavigationBar mode
    /// </summary>
    protected internal bool Fullscreen { get; private set; }

    /// <summary>
    /// Set to true if the used theme has a translucent statusBar
    /// and navigationBar and you want to manage the padding on your own.
    /// </summary>
    /// <param name="fullscreen"></param>
    /// <returns></returns>
    public DrawerBuilder WithFullscreen(bool fullscreen)
    {
      Fullscreen = fullscreen;

      if (fullscreen)
      {
        WithTranslucentStatusBar(false);
        WithTranslucentNavigationBar(false);
      }

      return this;
    }

    // a custom view to be used instead of everything else
    protected View mCustomView;

    /// <summary>
    /// Pass a custom view if you need a completely custom drawer
    /// content
    /// </summary>
    /// <param name="customView"></param>
    /// <returns></returns>
    public DrawerBuilder WithCustomView(View customView)
    {
      mCustomView = customView;
      return this;
    }

    /// <summary>
    /// the drawerLayout to use
    /// </summary>
    protected internal DrawerLayout _drawerLayout = null;

    protected internal RelativeLayout SliderLayout { get; private set; }

    /// <summary>
    /// Pass a custom DrawerLayout which will be used.
    /// NOTE: This requires the same structure as the drawer.axml
    /// </summary>
    /// <param name="drawerLayout"></param>
    /// <returns></returns>
    public DrawerBuilder WithDrawerLayout(DrawerLayout drawerLayout)
    {
      _drawerLayout = drawerLayout;
      return this;
    }

    /// <summary>
    /// Pass a custom DrawerLayout Resource which will be used.
    /// NOTE: This requires the same structure as the drawer.axml
    /// </summary>
    /// <param name="resLayout"></param>
    /// <returns></returns>
    public DrawerBuilder WithDrawerLayout(int resLayout)
    {
      if (Activity == null)
      {
        throw new RuntimeException("please pass an activity first to use this call");
      }

      if (resLayout != -1)
      {
        _drawerLayout = Activity.LayoutInflater.Inflate(resLayout, RootView, false).JavaCast<DrawerLayout>();
      }
      else
      {
        _drawerLayout = Activity.LayoutInflater.Inflate(Resource.Layout.material_drawer, RootView, false).JavaCast<DrawerLayout>();
      }

      return this;
    }

    //the statusBar color
    protected Color mStatusBarColor = Color.Transparent;
    protected int mStatusBarColorRes = -1;

    /// <summary>
    /// Set the statusBarColor color for this activity
    /// </summary>
    /// <param name="statusBarColor"></param>
    /// <returns></returns>
    public DrawerBuilder WithStatusBarColor(Color statusBarColor)
    {
      mStatusBarColor = statusBarColor;
      return this;
    }

    /// <summary>
    /// Set the statusBarColor color for this activity from a resource
    /// </summary>
    /// <param name="statusBarColorRes"></param>
    /// <returns></returns>
    public DrawerBuilder WithStatusBarColorRes(int statusBarColorRes)
    {
      mStatusBarColorRes = statusBarColorRes;
      return this;
    }

    //the background color for the slider
    protected Color mSliderBackgroundColor = Color.Transparent;
    protected int mSliderBackgroundColorRes = -1;
    protected Drawable mSliderBackgroundDrawable = null;
    protected int mSliderBackgroundDrawableRes = -1;

    /// <summary>
    /// Set the background color for the Slider.
    /// This is the view containing the list.
    /// </summary>
    /// <param name="sliderBackgroundColor"></param>
    /// <returns></returns>
    public DrawerBuilder WithSliderBackgroundColor(Color sliderBackgroundColor)
    {
      mSliderBackgroundColor = sliderBackgroundColor;
      return this;
    }

    /// <summary>
    /// Set the background color for the Slider from a Resource.
    /// This is the view containing the list.
    /// </summary>
    /// <param name="sliderBackgroundColorRes"></param>
    /// <returns></returns>
    public DrawerBuilder WithSliderBackgroundColorRes(int sliderBackgroundColorRes)
    {
      mSliderBackgroundColorRes = sliderBackgroundColorRes;
      return this;
    }


    /// <summary>
    /// Set the background drawable for the Slider.
    /// This is the view containing the list.
    /// </summary>
    /// <param name="sliderBackgroundDrawable"></param>
    /// <returns></returns>
    public DrawerBuilder WithSliderBackgroundDrawable(Drawable sliderBackgroundDrawable)
    {
      mSliderBackgroundDrawable = sliderBackgroundDrawable;
      return this;
    }


    /// <summary>
    /// Set the background drawable for the Slider from a Resource.
    /// This is the view containing the list.
    /// </summary>
    /// <param name="sliderBackgroundDrawableRes"></param>
    /// <returns></returns>
    public DrawerBuilder WithSliderBackgroundDrawableRes(int sliderBackgroundDrawableRes)
    {
      mSliderBackgroundDrawableRes = sliderBackgroundDrawableRes;
      return this;
    }

    //the width of the drawer
    private int _drawerWidth = -1;

    protected internal int DrawerWidth
    {
      get { return _drawerWidth; }
      private set { _drawerWidth = value; }
    }

    /// <summary>
    /// Set the DrawerBuilder width With a pixel value
    /// </summary>
    /// <param name="drawerWidthPx"></param>
    /// <returns></returns>
    public DrawerBuilder WithDrawerWidthPx(int drawerWidthPx)
    {
      DrawerWidth = drawerWidthPx;
      return this;
    }

    /// <summary>
    /// Set the DrawerBuilder width With a dp value
    /// </summary>
    /// <param name="drawerWidthDp"></param>
    /// <returns></returns>
    public DrawerBuilder WithDrawerWidthDp(int drawerWidthDp)
    {
      if (Activity == null)
      {
        throw new RuntimeException("please pass an activity first to use this call");
      }

      DrawerWidth = Activity.ConvertDpToPx(drawerWidthDp);
      return this;
    }

    /// <summary>
    /// Set the DrawerBuilder width With a dimension resource
    /// </summary>
    /// <param name="drawerWidthRes"></param>
    /// <returns></returns>
    public DrawerBuilder WithDrawerWidthRes(int drawerWidthRes)
    {
      if (Activity == null)
      {
        throw new RuntimeException("please pass an activity first to use this call");
      }

      DrawerWidth = Activity.Resources.GetDimensionPixelSize(drawerWidthRes);
      return this;
    }

    /// <summary>
    /// the gravity of the drawer
    /// </summary>
    protected internal int? DrawerGravity { get; private set; }

    /// <summary>
    /// Set the gravity for the drawer. START, LEFT | RIGHT, END
    /// </summary>
    /// <param name="gravity"></param>
    /// <returns></returns>
    public DrawerBuilder WithDrawerGravity(int gravity)
    {
      DrawerGravity = gravity;
      return this;
    }

    /// <summary>
    /// the account selection header to use
    /// </summary>
    protected internal AccountHeader AccountHeader { get; private set; }

    protected internal bool AccountHeaderSticky { get; private set; }

    /// <summary>
    /// Add a AccountSwitcherHeader which will be used in this drawer instance.
    /// NOTE: This will overwrite any set headerView.
    /// </summary>
    /// <param name="accountHeader"></param>
    /// <returns></returns>
    public DrawerBuilder WithAccountHeader(AccountHeader accountHeader)
    {
      return WithAccountHeader(accountHeader, false);
    }

    /// <summary>
    /// Add a AccountSwitcherHeader which will be used in this drawer instance. Pass true if it should be sticky
    /// NOTE: This will overwrite any set headerView or stickyHeaderView (depends on the bool).
    /// </summary>
    /// <param name="accountHeader"></param>
    /// <param name="accountHeaderSticky"></param>
    /// <returns></returns>
    public DrawerBuilder WithAccountHeader(AccountHeader accountHeader, bool accountHeaderSticky)
    {
      AccountHeader = accountHeader;
      AccountHeaderSticky = accountHeaderSticky;

      //set the header offset
      if (!accountHeaderSticky)
      {
        HeaderOffset = 1;
      }
      return this;
    }

    // enable/disable the actionBarDrawerToggle animation
    protected bool mAnimateActionBarDrawerToggle = false;

    /// <summary>
    /// Set this to true if you want the ActionBarDrawerToggle to be animated.
    /// NOTE: This will only work if the built in ActionBarDrawerToggle is used.
    /// Enable it by setting WithActionBarDrawerToggle to true
    /// </summary>
    /// <param name="actionBarDrawerToggleAnimated"></param>
    /// <returns></returns>
    public DrawerBuilder WithActionBarDrawerToggleAnimated(bool actionBarDrawerToggleAnimated)
    {
      mAnimateActionBarDrawerToggle = actionBarDrawerToggleAnimated;
      return this;
    }


    // enable the drawer toggle / if WithActionBarDrawerToggle we will autoGenerate it
    protected bool mActionBarDrawerToggleEnabled = true;

    /// <summary>
    /// Set this to false if you don't need the included ActionBarDrawerToggle
    /// </summary>
    /// <param name="actionBarDrawerToggleEnabled"></param>
    /// <returns></returns>
    public DrawerBuilder WithActionBarDrawerToggle(bool actionBarDrawerToggleEnabled)
    {
      mActionBarDrawerToggleEnabled = actionBarDrawerToggleEnabled;
      return this;
    }

    /// <summary>
    /// drawer toggle
    /// </summary>
    protected internal ActionBarDrawerToggle ActionBarDrawerToggle { get; private set; }

    /// <summary>
    /// Add a custom ActionBarDrawerToggle which will be used in combination With this drawer.
    /// </summary>
    /// <param name="actionBarDrawerToggle"></param>
    /// <returns></returns>
    public DrawerBuilder WithActionBarDrawerToggle(ActionBarDrawerToggle actionBarDrawerToggle)
    {
      mActionBarDrawerToggleEnabled = true;
      ActionBarDrawerToggle = actionBarDrawerToggle;
      return this;
    }

    /// <summary>
    /// header view
    /// </summary>
    protected internal View HeaderView { get; internal set; }
  
    protected internal int HeaderOffset { get; internal set; }

    private bool _headerDivider = true;

    protected internal bool HeaderDivider
    {
      get { return _headerDivider; }
      private set { _headerDivider = value; }
    }

    protected internal bool HeaderClickable { get; private set; }

    /// <summary>
    /// Add a header to the DrawerBuilder ListView. This can be any view
    /// </summary>
    /// <param name="headerView"></param>
    /// <returns></returns>
    public DrawerBuilder WithHeader(View headerView)
    {
      HeaderView = headerView;
      //set the header offset
      HeaderOffset = 1;
      return this;
    }

    /// <summary>
    /// Add a header to the DrawerBuilder ListView defined by a resource.
    /// </summary>
    /// <param name="headerViewRes"></param>
    /// <returns></returns>
    public DrawerBuilder WithHeader(int headerViewRes)
    {
      if (Activity == null)
      {
        throw new RuntimeException("please pass an activity first to use this call");
      }

      if (headerViewRes != -1)
      {
        //i know there should be a root, bit i got none here
        HeaderView = Activity.LayoutInflater.Inflate(headerViewRes, null, false);
        //set the headerOffset :D
        HeaderOffset = 1;
      }

      return this;
    }

    /// <summary>
    /// Set this to true if you want the header to be clickable
    /// </summary>
    /// <param name="headerClickable"></param>
    /// <returns></returns>
    public DrawerBuilder WithHeaderClickable(bool headerClickable)
    {
      HeaderClickable = headerClickable;
      return this;
    }

    /// <summary>
    /// Set this to false if you don't need the divider below the header
    /// </summary>
    /// <param name="headerDivider"></param>
    /// <returns></returns>
    public DrawerBuilder WithHeaderDivider(bool headerDivider)
    {
      HeaderDivider = headerDivider;
      return this;
    }

    /// <summary>
    /// sticky view
    /// </summary>
    protected internal View StickyHeaderView { get; internal set; }

    /// <summary>
    /// Add a sticky header below the DrawerBuilder ListView. This can be any view
    /// </summary>
    /// <param name="stickyHeader"></param>
    /// <returns></returns>
    public DrawerBuilder WithStickyHeader(View stickyHeader)
    {
      StickyHeaderView = stickyHeader;
      return this;
    }

    /// <summary>
    /// Add a sticky header below the DrawerBuilder ListView defined by a resource.
    /// </summary>
    /// <param name="stickyHeaderRes"></param>
    /// <returns></returns>
    public DrawerBuilder WithStickyHeader(int stickyHeaderRes)
    {
      if (Activity == null)
      {
        throw new RuntimeException("please pass an activity first to use this call");
      }

      if (stickyHeaderRes != -1)
      {
        //i know there should be a root, bit i got none here
        StickyHeaderView = Activity.LayoutInflater.Inflate(stickyHeaderRes, null, false);
      }

      return this;
    }

    // footer view
    protected internal View FooterView { get; internal set; }

    private bool _footerDivider = true;

    protected internal bool FooterDivider
    {
      get { return _footerDivider; }
      private set { _footerDivider = value; }
    }

    protected internal bool FooterClickable { get; private set; }

    /// <summary>
    /// Add a footer to the DrawerBuilder ListView. This can be any view
    /// </summary>
    /// <param name="footerView"></param>
    /// <returns></returns>
    public DrawerBuilder WithFooter(View footerView)
    {
      FooterView = footerView;
      return this;
    }

    /// <summary>
    /// Add a footer to the DrawerBuilder ListView defined by a resource.
    /// </summary>
    /// <param name="footerViewRes"></param>
    /// <returns></returns>
    public DrawerBuilder WithFooter(int footerViewRes)
    {
      if (Activity == null)
      {
        throw new RuntimeException("please pass an activity first to use this call");
      }

      if (footerViewRes != -1)
      {
        //i know there should be a root, bit i got none here
        FooterView = Activity.LayoutInflater.Inflate(footerViewRes, null, false);
      }

      return this;
    }

    /// <summary>
    /// Set this to true if you want the footer to be clickable
    /// </summary>
    /// <param name="footerClickable"></param>
    /// <returns></returns>
    public DrawerBuilder WithFooterClickable(bool footerClickable)
    {
      FooterClickable = footerClickable;
      return this;
    }

    /// <summary>
    /// Set this to false if you don't need the divider above the footer
    /// </summary>
    /// <param name="footerDivider"></param>
    /// <returns></returns>
    public DrawerBuilder WithFooterDivider(bool footerDivider)
    {
      FooterDivider = footerDivider;
      return this;
    }

    // sticky view
    protected internal ViewGroup StickyFooterView { get; internal set; }
    protected internal bool? StickyFooterDivider { get; internal set; }

    /// <summary>
    /// Add a sticky footer below the DrawerBuilder ListView. This can be any view
    /// </summary>
    /// <param name="stickyFooter"></param>
    /// <returns></returns>
    public DrawerBuilder WithStickyFooter(ViewGroup stickyFooter)
    {
      StickyFooterView = stickyFooter;
      return this;
    }

    /// <summary>
    /// Add a sticky footer below the DrawerBuilder ListView defined by a resource.
    /// </summary>
    /// <param name="stickyFooterRes"></param>
    /// <returns></returns>
    public DrawerBuilder WithStickyFooter(int stickyFooterRes)
    {
      if (Activity == null)
      {
        throw new RuntimeException("please pass an activity first to use this call");
      }

      if (stickyFooterRes != -1)
      {
        //i know there should be a root, bit i got none here
        StickyFooterView = (ViewGroup) Activity.LayoutInflater.Inflate(stickyFooterRes, null, false);
      }

      return this;
    }

    /// <summary>
    /// Set this to false if you don't need the divider above the sticky footer
    /// </summary>
    /// <param name="stickyFooterDivider"></param>
    /// <returns></returns>
    public DrawerBuilder WithStickyFooterDivider(bool stickyFooterDivider)
    {
      StickyFooterDivider = stickyFooterDivider;
      return this;
    }

    // fire onClick after build
    protected bool mFireInitialOnClick = false;

    /// <summary>
    /// Set this to true if you love to get an initial onClick event after the build method is called
    /// </summary>
    /// <param name="fireOnInitialOnClick"></param>
    /// <returns></returns>
    public DrawerBuilder WithFireOnInitialOnClick(bool fireOnInitialOnClick)
    {
      mFireInitialOnClick = fireOnInitialOnClick;
      return this;
    }

    // item to select
    protected int mSelectedItem = 0;

    /// <summary>
    /// Set this to the index of the item, you would love to select upon start
    /// </summary>
    /// <param name="selectedItem"></param>
    /// <returns></returns>
    public DrawerBuilder WithSelectedItem(int selectedItem)
    {
      mSelectedItem = selectedItem;
      return this;
    }

    /// <summary>
    /// ListView to use Within the drawer :D
    /// </summary>
    protected internal ListView ListView { get; private set; }

    /// <summary>
    /// Define a custom ListView which will be used in the drawer
    /// NOTE: this is not recommended
    /// </summary>
    /// <param name="listView"></param>
    /// <returns></returns>
    public DrawerBuilder WithListView(ListView listView)
    {
      ListView = listView;
      return this;
    }

    /// <summary>
    /// Adapter to use for the list
    /// </summary>
    protected internal BaseDrawerAdapter Adapter { get; private set; }

    /// <summary>
    /// Define a custom Adapter which will be used in the drawer
    /// NOTE: this is not recommended
    /// </summary>
    /// <param name="adapter"></param>
    /// <returns></returns>
    public DrawerBuilder WithAdapter(BaseDrawerAdapter adapter)
    {
      Adapter = adapter;
      return this;
    }

    /// <summary>
    /// animate the drawerItems
    /// </summary>
    private bool AnimateDrawerItems { get; set; }

    /// <summary>
    /// define if the items should be animated on their first view / and when switching the drawer
    /// </summary>
    /// <param name="animateDrawerItems"></param>
    /// <returns></returns>
    public DrawerBuilder WithAnimateDrawerItems(bool animateDrawerItems)
    {
      AnimateDrawerItems = animateDrawerItems;
      return this;
    }

    // list in drawer
    private List<IDrawerItem> _drawerItems = new List<IDrawerItem>();

    protected internal List<IDrawerItem> DrawerItems
    {
      get { return _drawerItems; }
      internal set { _drawerItems = value; }
    }

    /// <summary>
    /// Set the initial List of IDrawerItems for the Drawer
    /// </summary>
    /// <param name="drawerItems"></param>
    /// <returns></returns>
    public DrawerBuilder WithDrawerItems(IEnumerable<IDrawerItem> drawerItems)
    {
      DrawerItems = drawerItems.ToList();
      return this;
    }

    /// <summary>
    /// Add a initial DrawerItem or a DrawerItem Array  for the Drawer
    /// </summary>
    /// <param name="?"></param>
    /// <returns></returns>
    public DrawerBuilder AddDrawerItems(params IDrawerItem[] drawerItems)
    {
      if (DrawerItems == null)
      {
        DrawerItems = new List<IDrawerItem>();
      }

      if (drawerItems != null)
      {
        DrawerItems.AddRange(drawerItems);
      }

      return this;
    }

    // always visible list in drawer
    protected internal List<IDrawerItem> mStickyDrawerItems = new List<IDrawerItem>();

    /// <summary>
    /// Set the initial List of IDrawerItems for the StickyDrawerFooter
    /// </summary>
    /// <param name="stickyDrawerItems"></param>
    /// <returns></returns>
    public DrawerBuilder WithStickyDrawerItems(IEnumerable<IDrawerItem> stickyDrawerItems)
    {
      mStickyDrawerItems = stickyDrawerItems.ToList();
      return this;
    }

    /// <summary>
    /// Add a initial DrawerItem or a DrawerItem Array for the StickyDrawerFooter
    /// </summary>
    /// <param name="stickyDrawerItems"></param>
    /// <returns></returns>
    public DrawerBuilder AddStickyDrawerItems(params IDrawerItem[] stickyDrawerItems)
    {
      if (mStickyDrawerItems == null)
      {
        mStickyDrawerItems = new List<IDrawerItem>();
      }

      if (stickyDrawerItems != null)
      {
        mStickyDrawerItems.AddRange(stickyDrawerItems);
      }
      return this;
    }

    // close drawer on click
    protected bool mCloseOnClick = true;

    /// <summary>
    /// Set this to false if the drawer should stay opened after an item was clicked
    /// </summary>
    /// <param name="closeOnClick"></param>
    /// <returns></returns>
    public DrawerBuilder WithCloseOnClick(bool closeOnClick)
    {
      mCloseOnClick = closeOnClick;
      return this;
    }

    // delay drawer close to prevent lag
    protected int mDelayOnDrawerClose = 150;

    /// <summary>
    /// Define the delay for the drawer close operation after a click.
    /// This is a small trick to improve the speed (and remove lag) if you open a new activity after a DrawerItem
    /// was selected.
    /// NOTE: Disable this by passing -1
    /// </summary>
    /// <param name="delayOnDrawerClose"></param>
    /// <returns></returns>
    public DrawerBuilder WithDelayOnDrawerClose(int delayOnDrawerClose)
    {
      mDelayOnDrawerClose = delayOnDrawerClose;
      return this;
    }

    // onDrawerListener
    protected Drawer.IOnDrawerListener mOnDrawerListener;

    /// <summary>
    /// Define a OnDrawerListener for this Drawer
    /// </summary>
    /// <param name="onDrawerListener"></param>
    /// <returns></returns>
    public DrawerBuilder WithOnDrawerListener(Drawer.IOnDrawerListener onDrawerListener)
    {
      mOnDrawerListener = onDrawerListener;
      return this;
    }

    // onDrawerItemClickListeners
    protected internal Drawer.IOnDrawerItemClickListener mOnDrawerItemClickListener;

    /// <summary>
    /// Define a OnDrawerItemClickListener for this Drawer
    /// </summary>
    /// <param name="onDrawerItemClickListener"></param>
    /// <returns></returns>
    public DrawerBuilder WithOnDrawerItemClickListener(Drawer.IOnDrawerItemClickListener onDrawerItemClickListener)
    {
      mOnDrawerItemClickListener = onDrawerItemClickListener;
      return this;
    }

    // onDrawerItemClickListeners
    protected internal Drawer.IOnDrawerItemLongClickListener mOnDrawerItemLongClickListener;

    /// <summary>
    /// Define a OnDrawerItemLongClickListener for this Drawer
    /// </summary>
    /// <param name="onDrawerItemLongClickListener"></param>
    /// <returns></returns>
    public DrawerBuilder WithOnDrawerItemLongClickListener(
      Drawer.IOnDrawerItemLongClickListener onDrawerItemLongClickListener)
    {
      mOnDrawerItemLongClickListener = onDrawerItemLongClickListener;
      return this;
    }

    // onDrawerItemClickListeners
    protected Drawer.IOnDrawerItemSelectedListener mOnDrawerItemSelectedListener;

    /// <summary>
    /// Define a OnDrawerItemSelectedListener for this Drawer
    /// </summary>
    /// <param name="onDrawerItemSelectedListener"></param>
    /// <returns></returns>
    public DrawerBuilder WithOnDrawerItemSelectedListener(
      Drawer.IOnDrawerItemSelectedListener onDrawerItemSelectedListener)
    {
      mOnDrawerItemSelectedListener = onDrawerItemSelectedListener;
      return this;
    }

    // onDrawerListener
    protected Drawer.IOnDrawerNavigationListener mOnDrawerNavigationListener;

    /// <summary>
    /// Define a OnDrawerNavigationListener for this Drawer
    /// </summary>
    /// <param name="onDrawerNavigationListener"></param>
    /// <returns></returns>
    public DrawerBuilder WithOnDrawerNavigationListener(Drawer.IOnDrawerNavigationListener onDrawerNavigationListener)
    {
      mOnDrawerNavigationListener = onDrawerNavigationListener;
      return this;
    }

    //show the drawer on the first launch to show the user its there
    protected bool mShowDrawerOnFirstLaunch = false;

    /// <summary>
    /// define if the DrawerBuilder is shown on the first launch
    /// </summary>
    /// <param name="showDrawerOnFirstLaunch"></param>
    /// <returns></returns>
    public DrawerBuilder WithShowDrawerOnFirstLaunch(bool showDrawerOnFirstLaunch)
    {
      mShowDrawerOnFirstLaunch = showDrawerOnFirstLaunch;
      return this;
    }

    // savedInstance to restore state
    protected Bundle mSavedInstance;

    /// <summary>
    /// Set the Bundle (savedInstance) which is passed by the activity.
    /// No need to null-check everything is handled automatically
    /// </summary>
    /// <param name="savedInstance"></param>
    /// <returns></returns>
    public DrawerBuilder WithSavedInstance(Bundle savedInstance)
    {
      mSavedInstance = savedInstance;
      return this;
    }

    /// <summary>
    /// helper method to handle when the drawer should be shown on the first launch
    /// </summary>
    private void handleShowOnFirstLaunch()
    {
      //check if it should be shown on first launch (and we have a drawerLayout)
      if (Activity != null && _drawerLayout != null && mShowDrawerOnFirstLaunch)
      {
        var preferences = PreferenceManager.GetDefaultSharedPreferences(Activity);
        //if it was not shown yet
        if (!preferences.GetBoolean(Drawer.PREF_USER_LEARNED_DRAWER, false))
        {
          //open the drawer
          if (DrawerGravity != null)
          {
            _drawerLayout.OpenDrawer(DrawerGravity.Value);
          }
          else if (SliderLayout != null)
          {
            _drawerLayout.OpenDrawer(SliderLayout);
          }

          //save that it showed up once ;)
          var editor = preferences.Edit();
          editor.PutBoolean(Drawer.PREF_USER_LEARNED_DRAWER, true);
          editor.Apply();
        }
      }
    }

    /// <summary>
    /// Build and add the DrawerBuilder to your activity
    /// </summary>
    /// <returns></returns>
    public Drawer Build()
    {
      if (_used)
      {
        throw new RuntimeException("you must not reuse a DrawerBuilder builder");
      }
      if (Activity == null)
      {
        throw new RuntimeException("please pass an activity");
      }

      //set that this builder was used. now you have to create a new one
      _used = true;

      // if the user has not set a drawerLayout use the default one :D
      if (_drawerLayout == null)
      {
        WithDrawerLayout(-1);
      }

      //check if the activity was initialized correctly
      if (RootView == null || RootView.ChildCount == 0)
      {
        throw new RuntimeException(
          "You have to set your layout for this activity With setContentView() first. Or you build the drawer on your own With .buildView()");
      }

      //get the content view
      View contentView = RootView.GetChildAt(0);
      bool alreadyInflated = contentView is DrawerLayout;

      //get the drawer root
      DrawerContentRoot = _drawerLayout.GetChildAt(0) as ScrimInsetsFrameLayout;

      //do some magic specific to the statusBar
      if (!alreadyInflated && TranslucentStatusBar)
      {
        if (Android.OS.Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat &&
            Android.OS.Build.VERSION.SdkInt < BuildVersionCodes.Lollipop)
        {
          DrawerUtils.SetTranslucentStatusFlag(Activity, true);
        }

        if (Android.OS.Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat)
        {
          if (mTranslucentStatusBarProgrammatically)
          {
            //mActivity.getWindow()
            //  .getDecorView()
            //  .setSystemUiVisibility(View.SYSTEM_UI_FLAG_LAYOUT_STABLE | View.SYSTEM_UI_FLAG_LAYOUT_FULLSCREEN);
            Activity.Window.DecorView.DispatchSystemUiVisibilityChanged(SystemUiFlags.LayoutStable |
                                                                         SystemUiFlags.LayoutFullscreen);

          }
        }

        if (Android.OS.Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
        {
          DrawerUtils.SetTranslucentStatusFlag(Activity, false);
          if (mTranslucentStatusBarProgrammatically)
          {
            Activity.Window.SetStatusBarColor(Color.Transparent);
          }
        }

        DrawerContentRoot.SetPadding(0, UIUtils.GetStatusBarHeight(Activity), 0, 0);

        // define the statusBarColor
        if (mStatusBarColor == 0 && mStatusBarColorRes != -1)
        {
          mStatusBarColor = Activity.Resources.GetColor(mStatusBarColorRes);
        }
        else if (mStatusBarColor == 0)
        {
          mStatusBarColor = UIUtils.GetThemeColorFromAttrOrRes(Activity, Resource.Attribute.colorPrimaryDark,
            Resource.Color.material_drawer_primary_dark);
        }
        DrawerContentRoot.SetInsetForeground(mStatusBarColor);
      }

      //do some magic specific to the navigationBar
      if (!alreadyInflated && TranslucentNavigationBar)
      {
        if (Android.OS.Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat &&
            Android.OS.Build.VERSION.SdkInt < BuildVersionCodes.Lollipop)
        {
          DrawerUtils.SetTranslucentNavigationFlag(Activity, true);
        }
        if (Android.OS.Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat)
        {
          if (mTranslucentNavigationBarProgrammatically)
          {
            //mActivity.getWindow()
            //  .getDecorView()
            //  .setSystemUiVisibility(View.SYSTEM_UI_FLAG_LAYOUT_STABLE | View.SYSTEM_UI_FLAG_LAYOUT_FULLSCREEN);

            Activity.Window.DecorView.DispatchSystemUiVisibilityChanged(SystemUiFlags.LayoutStable |
                                                                         SystemUiFlags.LayoutFullscreen);

            DrawerUtils.SetTranslucentNavigationFlag(Activity, true);
          }
        }
        if (Android.OS.Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
        {
          if (mTranslucentNavigationBarProgrammatically)
          {
            Activity.Window.SetNavigationBarColor(Color.Transparent);
          }
        }
      }

      //if we are fullscreen disable the ScrimInsetsLayout
      if (Fullscreen && Android.OS.Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat)
      {
        DrawerContentRoot.SetEnabled(false);
      }

      //only add the new layout if it wasn't done before
      if (!alreadyInflated)
      {
        // remove the contentView
        RootView.RemoveView(contentView);
      }
      else
      {
        //if it was already inflated we have to clean up again
        RootView.RemoveAllViews();
      }

      //create the layoutParams to use for the contentView
      var layoutParamsContentView = new FrameLayout.LayoutParams(
        ViewGroup.LayoutParams.MatchParent,
        ViewGroup.LayoutParams.MatchParent
        );

      //if we have a translucent navigation bar set the bottom margin
      if (TranslucentNavigationBar && Android.OS.Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat)
      {
        layoutParamsContentView.BottomMargin = UIUtils.GetNavigationBarHeight(Activity);
      }

      //add the contentView to the drawer content frameLayout
      DrawerContentRoot.AddView(contentView, layoutParamsContentView);

      //add the drawerLayout to the root
      RootView.AddView(_drawerLayout, new ViewGroup.LayoutParams(
        ViewGroup.LayoutParams.MatchParent,
        ViewGroup.LayoutParams.MatchParent
        ));

      //set the navigationOnClickListener
      //TODO: wdcossey
      //View.OnClickListener toolbarNavigationListener = new View.OnClickListener() {
      //    @Override
      //    public void onClick(View v) {
      //        bool handled = false;

      //        if (mOnDrawerNavigationListener != null && (mActionBarDrawerToggle != null && !mActionBarDrawerToggle.isDrawerIndicatorEnabled())) {
      //            handled = mOnDrawerNavigationListener.onNavigationClickListener(v);
      //        }
      //        if (!handled) {
      //            if (mDrawerLayout.isDrawerOpen(mSliderLayout)) {
      //                mDrawerLayout.closeDrawer(mSliderLayout);
      //            } else {
      //                if (mDrawerGravity != null) {
      //                    mDrawerLayout.openDrawer(mDrawerGravity);
      //                } else {
      //                    mDrawerLayout.openDrawer(mSliderLayout);
      //                }
      //            }
      //        }
      //    }
      //};

      // create the ActionBarDrawerToggle if not set and enabled and if we have a toolbar
      if (mActionBarDrawerToggleEnabled && ActionBarDrawerToggle == null && mToolbar != null)
      {
        ActionBarDrawerToggle = new ActionBarDrawerToggle(Activity, _drawerLayout, mToolbar,
          Resource.String.drawer_open, Resource.String.drawer_close);
        //TODO: wdcossey
        //@Override
        //public void onDrawerOpened(View drawerView) {
        //    if (mOnDrawerListener != null) {
        //        mOnDrawerListener.onDrawerOpened(drawerView);
        //    }
        //    super.onDrawerOpened(drawerView);
        //}

        //TODO: wdcossey
        //@Override
        //public void onDrawerClosed(View drawerView) {
        //    if (mOnDrawerListener != null) {
        //        mOnDrawerListener.onDrawerClosed(drawerView);
        //    }
        //    super.onDrawerClosed(drawerView);
        //}

        //TODO: wdcossey
        //@Override
        //public void onDrawerSlide(View drawerView, float slideOffset) {
        //    if (mOnDrawerListener != null) {
        //        mOnDrawerListener.onDrawerSlide(drawerView, slideOffset);
        //    }

        //    if (!mAnimateActionBarDrawerToggle) {
        //        super.onDrawerSlide(drawerView, 0);
        //    } else {
        //        super.onDrawerSlide(drawerView, slideOffset);
        //    }
        //}

        ActionBarDrawerToggle.SyncState();
      }

      //if we got a toolbar set a toolbarNavigationListener
      //we also have to do this after setting the ActionBarDrawerToggle as this will overwrite this
      if (mToolbar != null)
      {
        //todo: wdcossey
        //mToolbar.SetNavigationOnClickListener(toolbarNavigationListener);
      }

      //handle the ActionBarDrawerToggle
      if (ActionBarDrawerToggle != null)
      {
        //todo: wdcossey
        //ActionBarDrawerToggle.setToolbarNavigationClickListener(toolbarNavigationListener);
        _drawerLayout.SetDrawerListener(ActionBarDrawerToggle);
      }
      else
      {

        _drawerLayout.DrawerSlide += delegate(object sender, DrawerLayout.DrawerSlideEventArgs args)
        {
          if (mOnDrawerListener != null)
          {
            mOnDrawerListener.OnDrawerSlide(args.DrawerView, args.SlideOffset);
          }
        };

        _drawerLayout.DrawerOpened += delegate(object sender, DrawerLayout.DrawerOpenedEventArgs args)
        {
          if (mOnDrawerListener != null)
          {
            mOnDrawerListener.OnDrawerOpened(args.DrawerView);
          }
        };

        _drawerLayout.DrawerClosed += delegate(object sender, DrawerLayout.DrawerClosedEventArgs args)
        {
          if (mOnDrawerListener != null)
          {
            mOnDrawerListener.OnDrawerClosed(args.DrawerView);
          }
        };


        _drawerLayout.DrawerStateChanged += delegate(object sender, DrawerLayout.DrawerStateChangedEventArgs args)
        {

        };
      }

      //build the view which will be set to the drawer
      Drawer result = BuildView();

      // add the slider to the drawer
      _drawerLayout.AddView(SliderLayout, 1);

      return result;
    }

    /// <summary>
    /// build the drawers content only. This will still return a Result object, but only With the content set. No inflating of a DrawerLayout.
    /// </summary>
    /// <returns>Result object With only the content set</returns>
    public Drawer BuildView()
    {
      // get the slider view
      SliderLayout =
        (RelativeLayout)Activity.LayoutInflater.Inflate(Resource.Layout.material_drawer_slider, _drawerLayout, false);
      SliderLayout.SetBackgroundColor(UIUtils.GetThemeColorFromAttrOrRes(Activity,
        Resource.Attribute.material_drawer_background, Resource.Color.material_drawer_background));
      // get the layout params
      var @params = SliderLayout.LayoutParameters.JavaCast<DrawerLayout.LayoutParams>();
      if (@params != null)
      {
        // if we've set a custom gravity set it
        if (DrawerGravity != null)
        {
          @params.Gravity = DrawerGravity.Value;
        }
        // if this is a drawer from the right, change the margins :D
        @params = DrawerUtils.ProcessDrawerLayoutParams(this, @params);
        // set the new layout params
        SliderLayout.LayoutParameters = @params;
      }

      // set the background
      if (mSliderBackgroundColor != Color.Transparent)
      {
        SliderLayout.SetBackgroundColor(mSliderBackgroundColor);
      }
      else if (mSliderBackgroundColorRes != -1)
      {
        SliderLayout.SetBackgroundColor(Activity.Resources.GetColor(mSliderBackgroundColorRes));
      }
      else if (mSliderBackgroundDrawable != null)
      {
        UIUtils.SetBackground(SliderLayout, mSliderBackgroundDrawable);
      }
      else if (mSliderBackgroundDrawableRes != -1)
      {
        UIUtils.SetBackground(SliderLayout, mSliderBackgroundColorRes);
      }

      //create the content
      CreateContent();

      //create the result object
      Drawer result = new Drawer(this);
      //set the drawer for the accountHeader if set
      if (AccountHeader != null)
      {
        AccountHeader.SetDrawer(result);
      }

      //handle if the drawer should be shown on first launch
      handleShowOnFirstLaunch();

      //forget the reference to the activity
      Activity = null;

      return result;
    }

    /// <summary>
    /// Call this method to append a new DrawerBuilder to a existing Drawer.
    /// </summary>
    /// <param name="result">the Drawer.Result of an existing Drawer</param>
    /// <returns></returns>
    public Drawer Append(Drawer result)
    {
      if (_used)
      {
        throw new RuntimeException("you must not reuse a DrawerBuilder builder");
      }
      if (DrawerGravity == null)
      {
        throw new RuntimeException("please set the gravity for the drawer");
      }

      //set that this builder was used. now you have to create a new one
      _used = true;

      //get the drawer layout from the previous drawer
      _drawerLayout = result.GetDrawerLayout();

      // get the slider view
      SliderLayout =
        (RelativeLayout)Activity.LayoutInflater.Inflate(Resource.Layout.material_drawer_slider, _drawerLayout, false);
      SliderLayout.SetBackgroundColor(UIUtils.GetThemeColorFromAttrOrRes(Activity,
        Resource.Attribute.material_drawer_background, Resource.Color.material_drawer_background));
      // get the layout params
      DrawerLayout.LayoutParams @params = (DrawerLayout.LayoutParams) SliderLayout.LayoutParameters;

      // set the gravity of this drawerGravity
      @params.Gravity = DrawerGravity.Value;

      // if this is a drawer from the right, change the margins :D
      @params = DrawerUtils.ProcessDrawerLayoutParams(this, @params);
      // set the new params
      SliderLayout.LayoutParameters = @params;
      // add the slider to the drawer
      _drawerLayout.AddView(SliderLayout, 1);

      //create the content
      CreateContent();

      //forget the reference to the activity
      Activity = null;

      return new Drawer(this);
    }

    /// <summary>
    /// the helper method to create the content for the drawer
    /// </summary>
    private void CreateContent()
    {
      //if we have a customView use this
      if (mCustomView != null)
      {
        LinearLayout.LayoutParams contentParams = new LinearLayout.LayoutParams(
          ViewGroup.LayoutParams.MatchParent,
          ViewGroup.LayoutParams.MatchParent
          );
        contentParams.Weight = 1f;
        SliderLayout.AddView(mCustomView, contentParams);
        return;
      }

      // if we have an adapter (either by defining a custom one or the included one add a list :D
      if (ListView == null)
      {
        ListView = new ListView(Activity);
        ListView.ChoiceMode = ChoiceMode.Single;
        ListView.Divider = null;
        //some style improvements on older devices
        ListView.SetFadingEdgeLength(0);
        ListView.CacheColorHint = Color.Transparent;
        //set the drawing cache background to the same color as the slider to improve performance
        ListView.DrawingCacheBackgroundColor = UIUtils.GetThemeColorFromAttrOrRes(Activity,
          Resource.Attribute.material_drawer_background, Resource.Color.material_drawer_background);
        //only draw the selector on top if we are on a newer api than 21 because this makes only sense for ripples
        if (Android.OS.Build.VERSION.SdkInt > BuildVersionCodes.Lollipop)
        {
          ListView.SetDrawSelectorOnTop(true);
        }
        ListView.SetClipToPadding(false);

        int paddingTop = 0;
        if ((TranslucentStatusBar && !TranslucentActionBarCompatibility) || Fullscreen)
        {
          paddingTop = UIUtils.GetStatusBarHeight(Activity);
        }
        int paddingBottom = 0;
        if ((TranslucentNavigationBar || Fullscreen) && Android.OS.Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat)
        {
          paddingBottom = UIUtils.GetNavigationBarHeight(Activity);
        }

        ListView.SetPadding(0, paddingTop, 0, paddingBottom);
      }

      LinearLayout.LayoutParams @params = new LinearLayout.LayoutParams(
        ViewGroup.LayoutParams.MatchParent,
        ViewGroup.LayoutParams.MatchParent
        );
      @params.Weight = 1f;
      SliderLayout.AddView(ListView, @params);

      //some extra stuff to beautify the whole thing ;)
      if ((TranslucentStatusBar && !TranslucentActionBarCompatibility) ||
          (mTranslucentStatusBarShadow != null && mTranslucentStatusBarShadow.Value))
      {
        if (mTranslucentStatusBarShadow == null)
        {
          //if we use the default behavior show it only if we are above API Level 20
          if (Android.OS.Build.VERSION.SdkInt > BuildVersionCodes.KitkatWatch)
          {
            //bring shadow bar to front again
            SliderLayout.GetChildAt(0).BringToFront();
          }
          else
          {
            //disable the shadow if  we are on a lower sdk
            SliderLayout.GetChildAt(0).Visibility = ViewStates.Gone;
          }
        }
        else
        {
          //bring shadow bar to front again
          SliderLayout.GetChildAt(0).BringToFront();
        }
      }
      else
      {
        //disable the shadow if we don't use a translucent activity
        SliderLayout.GetChildAt(0).Visibility = ViewStates.Gone;
      }

      // initialize list if there is an adapter or set items
      if (DrawerItems != null && Adapter == null)
      {
        Adapter = new DrawerAdapter(Activity, DrawerItems, AnimateDrawerItems);
      }

      //handle the header
      DrawerUtils.HandleHeaderView(this);

      //todo: wdcossey
      //handle the footer
  //    DrawerUtils.HandleFooterView(this, new View.OnClickListener()
  //    {
  //      @Override
  //    public void onClick(View v) {
  //IDrawerItem drawerItem = (IDrawerItem) v.getTag();
  //DrawerUtils.onFooterDrawerItemClick(DrawerBuilder.this,
  //      drawerItem,
  //      v,
  //      true);
  //    }
  //  }
  //  )
  //    ;

      //after adding the header do the setAdapter and set the selection
      if (Adapter != null)
      {
        //set the adapter on the listView
        ListView.Adapter = Adapter;

        //predefine selection (should be the first element
        DrawerUtils.SetListSelection(this, mSelectedItem, false);
      }

      // add the onDrawerItemClickListener if set
      ListView.ItemClick += delegate(object sender, AdapterView.ItemClickEventArgs args)
      {
        var i = GetDrawerItem(args.Position, true);

        if (i != null && i is ICheckable && !((ICheckable) i).IsCheckable())
        {
          ListView.SetSelection(_currentSelection + HeaderOffset);
          ListView.SetItemChecked(_currentSelection + HeaderOffset, true);
        }
        else
        {
          ResetStickyFooterSelection();
          _currentSelection = args.Position - HeaderOffset;
          _currentFooterSelection = -1;
        }

        var consumed = false;
        if (mOnDrawerItemClickListener != null)
        {
          consumed = mOnDrawerItemClickListener.OnItemClick(args.Parent, args.View, args.Position - HeaderOffset,
            args.Id,
            i);
        }

        if (!consumed)
        {
          //close the drawer after click
          CloseDrawerDelayed();
        }
      };

      // add the onDrawerItemLongClickListener if set
      ListView.ItemLongClick += delegate(object sender, AdapterView.ItemLongClickEventArgs args)
      {
        if (mOnDrawerItemLongClickListener != null)
        {
          args.Handled = mOnDrawerItemLongClickListener.OnItemLongClick(args.Parent, args.View,
            args.Position - HeaderOffset,
            args.Id, GetDrawerItem(args.Position, true));
        }
        args.Handled = false;
      };


      // add the onDrawerItemSelectedListener if set
      ListView.ItemSelected += delegate(object sender, AdapterView.ItemSelectedEventArgs args)
      {
        if (mOnDrawerItemSelectedListener != null)
        {
          mOnDrawerItemSelectedListener.OnItemSelected(args.Parent, args.View, args.Position - HeaderOffset, args.Id,
            GetDrawerItem(args.Position, true));
        }
        _currentSelection = args.Position - HeaderOffset;
      };


      ListView.NothingSelected += delegate(object sender, AdapterView.NothingSelectedEventArgs args)
      {
        if (mOnDrawerItemSelectedListener != null)
        {
          mOnDrawerItemSelectedListener.OnNothingSelected(args.Parent);
        }
      };


      if (ListView != null)
      {
        ListView.SmoothScrollToPosition(0);
      }

      // try to restore all saved values again
      if (mSavedInstance != null)
      {
        int selection = mSavedInstance.GetInt(Drawer.BUNDLE_SELECTION, -1);
        DrawerUtils.SetListSelection(this, selection, false);
        int footerSelection = mSavedInstance.GetInt(Drawer.BUNDLE_FOOTER_SELECTION, -1);
        DrawerUtils.SetFooterSelection(this, footerSelection, false);
      }

      // call initial onClick event to allow the dev to init the first view
      if (mFireInitialOnClick && mOnDrawerItemClickListener != null)
      {
        mOnDrawerItemClickListener.OnItemClick(null, null, _currentSelection, _currentSelection,
          GetDrawerItem(_currentSelection, false));
      }
    }

    /**
     * helper method to close the drawer delayed
     */

    protected internal void CloseDrawerDelayed()
    {
      if (mCloseOnClick && _drawerLayout != null)
      {
        if (mDelayOnDrawerClose > -1)
        {
          new Handler().PostDelayed(() => { _drawerLayout.CloseDrawers(); }, mDelayOnDrawerClose);
        }
        else
        {
          _drawerLayout.CloseDrawers();
        }
      }
    }

    /**
     * get the drawerItem at a specific position
     *
     * @param position
     * @return
     */

    protected internal IDrawerItem GetDrawerItem(int position, bool includeOffset)
    {
      if (includeOffset)
      {
        if (DrawerItems != null && DrawerItems.Count > (position - HeaderOffset) && (position - HeaderOffset) > -1)
        {
          return DrawerItems[position - HeaderOffset];
        }
      }
      else
      {
        if (DrawerItems != null && DrawerItems.Count > position && position > -1)
        {
          return DrawerItems[position];
        }
      }
      return null;
    }

    /**
     * check if the item is Within the bounds of the list
     *
     * @param position
     * @param includeOffset
     * @return
     */

    protected internal bool CheckDrawerItem(int position, bool includeOffset)
    {
      if (includeOffset)
      {
        if (_drawerItems != null && _drawerItems.Count > (position - HeaderOffset) && (position - HeaderOffset) > -1)
        {
          return true;
        }
      }
      else
      {
        if (_drawerItems != null && _drawerItems.Count > position && position > -1)
        {
          return true;
        }
      }
      return false;
    }

    /**
     * simple helper method to reset the selection of the sticky footer
     */

    protected internal void ResetStickyFooterSelection()
    {
      if (StickyFooterView is LinearLayout)
      {
        for (int i = 0; i < ((LinearLayout) StickyFooterView).ChildCount; i++)
        {
          if (Android.OS.Build.VERSION.SdkInt >= BuildVersionCodes.Honeycomb)
          {
            ((LinearLayout) StickyFooterView).GetChildAt(i).Activated = false;
          }
          ((LinearLayout) StickyFooterView).GetChildAt(i).Selected = false;
        }
      }
    }
  }
}