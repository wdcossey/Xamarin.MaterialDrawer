using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Text;
using Android.Views;
using Android.Widget;
using com.xamarin.AndroidIconics;
using com.xamarin.component.MaterialDrawer.Models.Interfaces;
using com.xamarin.component.MaterialDrawer.Utils;
using com.xamarin.component.MaterialDrawer.Views;
using Java.Lang;
using Java.Util;
using System.Collections.Generic;
using Object = Java.Lang.Object;
using Uri = Android.Net.Uri;

namespace com.xamarin.component.MaterialDrawer.AccountSwitcher
{

  /// <summary>
  /// Created by wdcossey on 06.06.15.
  /// Original by mikepenz on 23.05.15.
  /// </summary>
  public class AccountHeaderBuilder
  {
    // global references to views we need later
    protected View mAccountHeader;
    protected internal ImageView mAccountHeaderBackground;
    protected BezelImageView mCurrentProfileView;
    protected View mAccountHeaderTextSection;
    protected ImageView mAccountSwitcherArrow;
    protected TextView mCurrentProfileName;
    protected TextView mCurrentProfileEmail;
    protected BezelImageView mProfileFirstView;
    protected BezelImageView mProfileSecondView;
    protected BezelImageView mProfileThirdView;

    // global references to the profiles
    //protected IProfile<> mCurrentProfile;
    protected IProfile mCurrentProfile;
    protected IProfile mProfileFirst;
    protected IProfile mProfileSecond;
    protected IProfile mProfileThird;


    // global stuff
    protected internal bool mSelectionListShown = false;
    protected int mAccountHeaderTextSectionBackgroundResource = -1;

    // the activity to use
    protected Activity mActivity;

    /// <summary>
    /// Pass the activity you use the drawer in ;)
    /// </summary>
    /// <param name="activity"></param>
    /// <returns></returns>
    public AccountHeaderBuilder WithActivity(Activity activity)
    {
      mActivity = activity;
      return this;
    }

    // defines if we use the compactStyle
    protected bool mCompactStyle = false;

    /// <summary>
    /// Defines if we should use the compact style for the header.
    /// </summary>
    /// <param name="compactStyle"></param>
    /// <returns></returns>
    public AccountHeaderBuilder WithCompactStyle(bool compactStyle)
    {
      mCompactStyle = compactStyle;
      return this;
    }

    // the typeface used for textViews within the AccountHeader
    protected Typeface mTypeface;

    // the typeface used for name textView only. overrides mTypeface
    protected Typeface mNameTypeface;

    // the typeface used for email textView only. overrides mTypeface
    protected Typeface mEmailTypeface;

    /// <summary>
    /// Define the typeface which will be used for all textViews in the AccountHeader
    /// </summary>
    /// <param name="typeface"></param>
    /// <returns></returns>
    public AccountHeaderBuilder WithTypeface(Typeface typeface)
    {
      mTypeface = typeface;
      return this;
    }

    /// <summary>
    /// Define the typeface which will be used for name textView in the AccountHeader.
    /// Overrides typeface supplied to {@link com.mikepenz.materialdrawer.accountswitcher.AccountHeaderBuilder#withTypeface(android.graphics.Typeface)}
    /// </summary>
    /// <param name="typeface"></param>
    /// <returns></returns>
    /// <see cref="Android.Graphics.Typeface"/>
    public AccountHeaderBuilder WithNameTypeface(Typeface typeface)
    {
      mNameTypeface = typeface;
      return this;
    }

    /// <summary>
    /// Define the typeface which will be used for email textView in the AccountHeader.
    /// Overrides typeface supplied to {@link com.mikepenz.materialdrawer.accountswitcher.AccountHeaderBuilder#withTypeface(android.graphics.Typeface)}
    /// </summary>
    /// <param name="typeface"></param>
    /// <returns></returns>
    /// <see cref="Android.Graphics.Typeface"/>
    public AccountHeaderBuilder WithEmailTypeface(Typeface typeface)
    {
      mEmailTypeface = typeface;
      return this;
    }

    // set the account header height
    protected int mHeightPx = -1;
    protected int mHeightDp = -1;
    protected int mHeightRes = -1;

    /// <summary>
    /// set the height for the header
    /// </summary>
    /// <param name="heightPx"></param>
    /// <returns></returns>
    public AccountHeaderBuilder WithHeightPx(int heightPx)
    {
      mHeightPx = heightPx;
      return this;
    }


    /// <summary>
    /// set the height for the header
    /// </summary>
    /// <param name="heightDp"></param>
    /// <returns></returns>
    public AccountHeaderBuilder WithHeightDp(int heightDp)
    {
      mHeightDp = heightDp;
      return this;
    }

    /// <summary>
    /// set the height for the header by resource
    /// </summary>
    /// <param name="heightRes"></param>
    /// <returns></returns>
    public AccountHeaderBuilder WithHeightRes(int heightRes)
    {
      mHeightRes = heightRes;
      return this;
    }

    //the background color for the slider
    protected Color mTextColor = Color.Transparent;
    protected int mTextColorRes = -1;

    /// <summary>
    /// set the background for the slider as color
    /// </summary>
    /// <param name="textColor"></param>
    /// <returns></returns>
    public AccountHeaderBuilder WithTextColor(Color textColor)
    {
      mTextColor = textColor;
      return this;
    }

    /// <summary>
    /// set the background for the slider as resource
    /// </summary>
    /// <param name="textColorRes"></param>
    /// <returns></returns>
    public AccountHeaderBuilder WithTextColorRes(int textColorRes)
    {
      mTextColorRes = textColorRes;
      return this;
    }

    //the current selected profile is visible in the list
    protected bool mCurrentHiddenInList = false;

    /// <summary>
    /// hide the current selected profile from the list
    /// </summary>
    /// <param name="currentProfileHiddenInList"></param>
    /// <returns></returns>
    public AccountHeaderBuilder WithCurrentProfileHiddenInList(bool currentProfileHiddenInList)
    {
      mCurrentHiddenInList = currentProfileHiddenInList;
      return this;
    }

    //set to hide the first or second line
    protected bool mSelectionFirstLineShown = true;
    protected bool mSelectionSecondLineShown = true;

    /// <summary>
    /// set this to false if you want to hide the first line of the selection box in the header (first line would be the name)
    /// </summary>
    /// <param name="selectionFirstLineShown"></param>
    /// <returns></returns>
    public AccountHeaderBuilder WithSelectionFistLineShown(bool selectionFirstLineShown)
    {
      mSelectionFirstLineShown = selectionFirstLineShown;
      return this;
    }

    /// <summary>
    /// set this to false if you want to hide the second line of the selection box in the header (second line would be the e-mail)
    /// </summary>
    /// <param name="selectionSecondLineShown"></param>
    /// <returns></returns>
    public AccountHeaderBuilder WithSelectionSecondLineShown(bool selectionSecondLineShown)
    {
      mSelectionSecondLineShown = selectionSecondLineShown;
      return this;
    }

    //set one of these to define the text in the first or second line with in the account selector
    protected string mSelectionFirstLine;
    protected string mSelectionSecondLine;

    /// <summary>
    /// set this to define the first line in the selection area if there is no profile
    /// note this will block any values from profiles!
    /// </summary>
    /// <param name="selectionFirstLine"></param>
    /// <returns></returns>
    public AccountHeaderBuilder WithSelectionFirstLine(string selectionFirstLine)
    {
      mSelectionFirstLine = selectionFirstLine;
      return this;
    }

    /// <summary>
    /// set this to define the second line in the selection area if there is no profile
    /// note this will block any values from profiles!
    /// </summary>
    /// <param name="selectionSecondLine"></param>
    /// <returns></returns>
    public AccountHeaderBuilder WithSelectionSecondLine(string selectionSecondLine)
    {
      mSelectionSecondLine = selectionSecondLine;
      return this;
    }

    // set non translucent statusBar mode
    protected bool mTranslucentStatusBar = true;

    /// <summary>
    /// Set or disable this if you use a translucent statusbar
    /// </summary>
    /// <param name="translucentStatusBar"></param>
    /// <returns></returns>
    public AccountHeaderBuilder WithTranslucentStatusBar(bool translucentStatusBar)
    {
      mTranslucentStatusBar = translucentStatusBar;
      return this;
    }

    //the background for the header
    protected Drawable mHeaderBackground = null;
    protected int mHeaderBackgroundRes = -1;

    /// <summary>
    /// set the background for the slider as color
    /// </summary>
    /// <param name="headerBackground"></param>
    /// <returns></returns>
    public AccountHeaderBuilder WithHeaderBackground(Drawable headerBackground)
    {
      mHeaderBackground = headerBackground;
      return this;
    }

    /// <summary>
    /// set the background for the header as resource
    /// </summary>
    /// <param name="headerBackgroundRes"></param>
    /// <returns></returns>
    public AccountHeaderBuilder WithHeaderBackground(int headerBackgroundRes)
    {
      mHeaderBackgroundRes = headerBackgroundRes;
      return this;
    }

    //background scale type
    protected ImageView.ScaleType mHeaderBackgroundScaleType = null;

    /// <summary>
    /// define the ScaleType for the header background
    /// </summary>
    /// <param name="headerBackgroundScaleType"></param>
    /// <returns></returns>
    public AccountHeaderBuilder WithHeaderBackgroundScaleType(ImageView.ScaleType headerBackgroundScaleType)
    {
      mHeaderBackgroundScaleType = headerBackgroundScaleType;
      return this;
    }

    //profile images in the header are shown or not
    protected bool mProfileImagesVisible = true;

    /// <summary>
    /// define if the profile images in the header are shown or not
    /// </summary>
    /// <param name="profileImagesVisible"></param>
    /// <returns></returns>
    public AccountHeaderBuilder WithProfileImagesVisible(bool profileImagesVisible)
    {
      mProfileImagesVisible = profileImagesVisible;
      return this;
    }

    // set the profile images clickable or not
    protected bool mProfileImagesClickable = true;

    /// <summary>
    /// enable or disable the profile images to be clickable
    /// </summary>
    /// <param name="profileImagesClickable"></param>
    /// <returns></returns>
    public AccountHeaderBuilder WithProfileImagesClickable(bool profileImagesClickable)
    {
      mProfileImagesClickable = profileImagesClickable;
      return this;
    }

    // set to use the alternative profile header switching
    protected bool mAlternativeProfileHeaderSwitching = false;

    /// <summary>
    /// enable the alternative profile header switching
    /// </summary>
    /// <param name="alternativeProfileHeaderSwitching"></param>
    /// <returns></returns>
    public AccountHeaderBuilder WithAlternativeProfileHeaderSwitching(bool alternativeProfileHeaderSwitching)
    {
      mAlternativeProfileHeaderSwitching = alternativeProfileHeaderSwitching;
      return this;
    }

    // enable 3 small header previews
    protected bool mThreeSmallProfileImages = false;

    /// <summary>
    /// enable the extended profile icon view with 3 small header images instead of two
    /// </summary>
    /// <param name="threeSmallProfileImages"></param>
    /// <returns></returns>
    public AccountHeaderBuilder WithThreeSmallProfileImages(bool threeSmallProfileImages)
    {
      mThreeSmallProfileImages = threeSmallProfileImages;
      return this;
    }

    // the onAccountHeaderSelectionListener to set
    private AccountHeader.IOnAccountHeaderSelectionViewClickListener mOnAccountHeaderSelectionViewClickListener;

    /// <summary>
    /// set a onSelection listener for the selection box
    /// </summary>
    /// <param name="onAccountHeaderSelectionViewClickListener"></param>
    /// <returns></returns>
    public AccountHeaderBuilder WithOnAccountHeaderSelectionViewClickListener(
      AccountHeader.IOnAccountHeaderSelectionViewClickListener onAccountHeaderSelectionViewClickListener)
    {
      mOnAccountHeaderSelectionViewClickListener = onAccountHeaderSelectionViewClickListener;
      return this;
    }

    //set the selection list enabled if there is only a single profile
    protected bool mSelectionListEnabledForSingleProfile = true;

    /// <summary>
    /// enable or disable the selection list if there is only a single profile
    /// </summary>
    /// <param name="selectionListEnabledForSingleProfile"></param>
    /// <returns></returns>
    public AccountHeaderBuilder WithSelectionListEnabledForSingleProfile(bool selectionListEnabledForSingleProfile)
    {
      mSelectionListEnabledForSingleProfile = selectionListEnabledForSingleProfile;
      return this;
    }

    //set the selection enabled disabled
    protected bool mSelectionListEnabled = true;

    /// <summary>
    /// enable or disable the selection list
    /// </summary>
    /// <param name="selectionListEnabled"></param>
    /// <returns></returns>
    public AccountHeaderBuilder WithSelectionListEnabled(bool selectionListEnabled)
    {
      mSelectionListEnabled = selectionListEnabled;
      return this;
    }

    // the drawerLayout to use
    protected internal View mAccountHeaderContainer;

    /// <summary>
    /// You can pass a custom view for the drawer lib. note this requires the same structure as the drawer.axml
    /// </summary>
    /// <param name="accountHeader"></param>
    /// <returns></returns>
    public AccountHeaderBuilder WithAccountHeader(View accountHeader)
    {
      mAccountHeaderContainer = accountHeader;
      return this;
    }

    /// <summary>
    /// You can pass a custom layout for the drawer lib. see the drawer.axml in layouts of this lib on GitHub
    /// </summary>
    /// <param name="resLayout"></param>
    /// <returns></returns>
    public AccountHeaderBuilder WithAccountHeader(int resLayout)
    {
      if (mActivity == null)
      {
        throw new RuntimeException("please pass an activity first to use this call");
      }

      if (resLayout != -1)
      {
        mAccountHeaderContainer = mActivity.LayoutInflater.Inflate(resLayout, null, false);
      }
      else
      {
        if (mCompactStyle)
        {
          mAccountHeaderContainer = mActivity.LayoutInflater.Inflate(Resource.Layout.material_drawer_compact_header,
            null, false);
        }
        else
        {
          mAccountHeaderContainer = mActivity.LayoutInflater.Inflate(Resource.Layout.material_drawer_header, null,
            false);
        }
      }

      return this;
    }

    // the profiles to display
    protected internal List<IProfile> mProfiles;

    /// <summary>
    /// set the arrayList of DrawerItems for the drawer
    /// </summary>
    /// <param name="profiles"></param>
    /// <returns></returns>
    public AccountHeaderBuilder WithProfiles(IEnumerable<IProfile> profiles)
    {
      mProfiles = profiles.ToList();
      return this;
    }

    /// <summary>
    /// add single ore more DrawerItems to the Drawer
    /// </summary>
    /// <param name="profiles"></param>
    /// <returns></returns>
    public AccountHeaderBuilder AddProfiles(IProfile[] profiles)
    {
      if (mProfiles == null)
      {
        mProfiles = new List<IProfile>();
      }

      if (profiles != null)
      {
        mProfiles.AddRange(profiles);
      }
      return this;
    }

    // the click listener to be fired on profile or selection click
    protected internal AccountHeader.IOnAccountHeaderListener mOnAccountHeaderListener;

    /// <summary>
    /// add a listener for the accountHeader
    /// </summary>
    /// <param name="onAccountHeaderListener"></param>
    /// <returns></returns>
    public AccountHeaderBuilder WithOnAccountHeaderListener(
      AccountHeader.IOnAccountHeaderListener onAccountHeaderListener)
    {
      this.mOnAccountHeaderListener = onAccountHeaderListener;
      return this;
    }

    // the drawer to set the AccountSwitcher for
    protected internal Drawer mDrawer;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="drawer"></param>
    /// <returns></returns>
    public AccountHeaderBuilder WithDrawer(Drawer drawer)
    {
      this.mDrawer = drawer;
      return this;
    }

    // savedInstance to restore state
    protected Bundle mSavedInstance;

    /// <summary>
    /// create the drawer with the values of a savedInstance
    /// </summary>
    /// <param name="savedInstance"></param>
    /// <returns></returns>
    public AccountHeaderBuilder WithSavedInstance(Bundle savedInstance)
    {
      this.mSavedInstance = savedInstance;
      return this;
    }

    /// <summary>
    /// helper method to set the height for the header!
    /// </summary>
    /// <param name="height"></param>
    private void SetHeaderHeight(int height)
    {
      if (mAccountHeaderContainer != null)
      {
        var @params = mAccountHeaderContainer.LayoutParameters;
        if (@params != null)
        {
          @params.Height = height;
          mAccountHeaderContainer.LayoutParameters = @params;
        }

        var accountHeader = mAccountHeaderContainer.FindViewById(Resource.Id.account_header_drawer);
        if (accountHeader != null)
        {
          @params = accountHeader.LayoutParameters;
          @params.Height = height;
          accountHeader.LayoutParameters = @params;
        }

        var accountHeaderBackground = mAccountHeaderContainer.FindViewById(Resource.Id.account_header_drawer_background);
        if (accountHeaderBackground != null)
        {
          @params = accountHeaderBackground.LayoutParameters;
          @params.Height = height;
          accountHeaderBackground.LayoutParameters = @params;
        }
      }
    }

    /// <summary>
    /// a small helper to handle the selectionView
    /// </summary>
    /// <param name="profile"></param>
    /// <param name="on"></param>
    private void HandleSelectionView(IProfile profile, bool on)
    {
      if (on)
      {
        if (Android.OS.Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
        {
          ((FrameLayout) mAccountHeaderContainer).Foreground = UIUtils.GetCompatDrawable(
            mAccountHeaderContainer.Context, mAccountHeaderTextSectionBackgroundResource);
          //todo: wdcossey
          mAccountHeaderContainer.Click += AccountHeaderContainerOnClick;
          //mAccountHeaderContainer.SetOnClickListener(onSelectionClickListener);
          mAccountHeaderContainer.SetTag(Resource.Id.profile_header, (Object) profile);
        }
        else
        {
          mAccountHeaderTextSection.SetBackgroundResource(mAccountHeaderTextSectionBackgroundResource);
          //todo: wdcossey
          mAccountHeaderTextSection.Click += AccountHeaderContainerOnClick;
          //mAccountHeaderTextSection.SetOnClickListener(onSelectionClickListener);
          mAccountHeaderTextSection.SetTag(Resource.Id.profile_header, (Object) profile);
        }
      }
      else
      {
        if (Android.OS.Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
        {
          ((FrameLayout) mAccountHeaderContainer).Foreground = null;
          mAccountHeaderContainer.SetOnClickListener(null);
        }
        else
        {
          UIUtils.SetBackground(mAccountHeaderTextSection, null);
          mAccountHeaderTextSection.SetOnClickListener(null);
        }
      }
    }

    private void AccountHeaderContainerOnClick(object sender, EventArgs eventArgs)
    {
      bool consumed = false;
      if (mOnAccountHeaderSelectionViewClickListener != null)
      {
        consumed = mOnAccountHeaderSelectionViewClickListener.OnClick((View)sender, (IProfile)((View)sender).GetTag(Resource.Id.profile_header));
      }

      if (mAccountSwitcherArrow.Visibility == ViewStates.Visible && !consumed)
      {
        ToggleSelectionList(((View)sender).Context);
      }
    }

    /// <summary>
    /// method to build the header view
    /// </summary>
    /// <returns></returns>
    public AccountHeader Build()
    {
      // if the user has not set a accountHeader use the default one :D
      if (mAccountHeaderContainer == null)
      {
        WithAccountHeader(-1);
      }

      // get the header view within the container
      mAccountHeader = mAccountHeaderContainer.FindViewById(Resource.Id.account_header_drawer);

      // handle the height for the header
      var height = -1;

      if (mHeightPx != -1)
      {
        height = mHeightPx;
      }
      else if (mHeightDp != -1)
      {
        height = mActivity.ConvertDpToPx(mHeightDp);
      }
      else if (mHeightRes != -1)
      {
        height = mActivity.Resources.GetDimensionPixelSize(mHeightRes);
      }
      else
      {
        if (mCompactStyle)
        {
          height =
            mActivity.Resources.GetDimensionPixelSize(Resource.Dimension.material_drawer_account_header_height_compact);
        }
        else
        {
          //calculate the header height by getting the optimal drawer width and calculating it * 9 / 16
          height = (int) (UIUtils.GetOptimalDrawerWidth(mActivity)*AccountHeader.NAVIGATION_DRAWER_ACCOUNT_ASPECT_RATIO);

          //if we are lower than api 19 (>= 19 we have a translucentStatusBar) the height should be a bit lower
          //probably even if we are non translucent on > 19 devices?
          if (Android.OS.Build.VERSION.SdkInt < BuildVersionCodes.Kitkat)
          {
            int tempHeight = height - UIUtils.GetStatusBarHeight(mActivity, true);
            if (UIUtils.ConvertPixelsToDp(tempHeight, mActivity) > 140)
            {
              height = tempHeight;
            }
          }
        }
      }

      // handle everything if we don't have a translucent status bar
      if (mTranslucentStatusBar)
      {
        mAccountHeader.SetPadding(0, UIUtils.GetStatusBarHeight(mActivity), 0, 0);
        //in fact it makes no difference if we have a translucent statusBar or not. we want 9/16 just if we are not compact
        if (mCompactStyle)
        {
          height = height + UIUtils.GetStatusBarHeight(mActivity);
        }
      }

      //set the height for the header
      SetHeaderHeight(height);

      // get the background view
      mAccountHeaderBackground =
        (ImageView) mAccountHeaderContainer.FindViewById(Resource.Id.account_header_drawer_background);
      // set the background
      if (mHeaderBackground != null)
      {
        mAccountHeaderBackground.SetImageDrawable(mHeaderBackground);
      }
      else if (mHeaderBackgroundRes != -1)
      {
        mAccountHeaderBackground.SetImageResource(mHeaderBackgroundRes);
      }

      if (mHeaderBackgroundScaleType != null)
      {
        mAccountHeaderBackground.SetScaleType(mHeaderBackgroundScaleType);
      }

      // get the text color to use for the text section
      if (mTextColor == 0 && mTextColorRes != -1)
      {
        mTextColor = mActivity.Resources.GetColor(mTextColorRes);
      }
      else if (mTextColor == 0)
      {
        mTextColor = UIUtils.GetThemeColorFromAttrOrRes(mActivity,
          Resource.Attribute.material_drawer_header_selection_text, Resource.Color.material_drawer_header_selection_text);
      }

      // set the background for the section
      if (mCompactStyle)
      {
        mAccountHeaderTextSection = mAccountHeader;
      }
      else
      {
        mAccountHeaderTextSection = mAccountHeaderContainer.FindViewById(Resource.Id.account_header_drawer_text_section);
      }

      mAccountHeaderTextSectionBackgroundResource = UIUtils.GetSelectableBackground(mActivity);
      HandleSelectionView(mCurrentProfile, true);

      // set the arrow :D
      mAccountSwitcherArrow =
        (ImageView) mAccountHeaderContainer.FindViewById(Resource.Id.account_header_drawer_text_switcher);
      mAccountSwitcherArrow.SetImageDrawable(
        new IconicsDrawable<GoogleMaterial>(mActivity, GoogleMaterial.Icon.gmd_arrow_drop_down).SizeDp(24)
          .PaddingDp(6)
          .Color(mTextColor));

      //get the fields for the name
      mCurrentProfileView = (BezelImageView) mAccountHeader.FindViewById(Resource.Id.account_header_drawer_current);
      mCurrentProfileName = (TextView) mAccountHeader.FindViewById(Resource.Id.account_header_drawer_name);
      mCurrentProfileEmail = (TextView) mAccountHeader.FindViewById(Resource.Id.account_header_drawer_email);

      //set the typeface for the AccountHeader
      if (mNameTypeface != null)
      {
        mCurrentProfileName.Typeface = mNameTypeface;
      }
      else if (mTypeface != null)
      {
        mCurrentProfileName.Typeface = mTypeface;
      }

      if (mEmailTypeface != null)
      {
        mCurrentProfileEmail.Typeface = mEmailTypeface;
      }
      else if (mTypeface != null)
      {
        mCurrentProfileEmail.Typeface = mTypeface;
      }

      mCurrentProfileName.SetTextColor(mTextColor);
      mCurrentProfileEmail.SetTextColor(mTextColor);

      mProfileFirstView = (BezelImageView) mAccountHeader.FindViewById(Resource.Id.account_header_drawer_small_first);
      mProfileSecondView = (BezelImageView) mAccountHeader.FindViewById(Resource.Id.account_header_drawer_small_second);
      mProfileThirdView = (BezelImageView) mAccountHeader.FindViewById(Resource.Id.account_header_drawer_small_third);

      //calculate the profiles to set
      CalculateProfiles();

      //process and build the profiles
      BuildProfiles();

      // try to restore all saved values again
      if (mSavedInstance != null)
      {
        int selection = mSavedInstance.GetInt(AccountHeader.BUNDLE_SELECTION_HEADER, -1);
        if (selection != -1)
        {
          //predefine selection (should be the first element
          if (mProfiles != null && (selection) > -1 && selection < mProfiles.Count)
          {
            SwitchProfiles(mProfiles[selection]);
          }
        }
      }

      //everything created. now set the header
      if (mDrawer != null)
      {
        mDrawer.SetHeader(mAccountHeaderContainer);
      }

      //forget the reference to the activity
      mActivity = null;

      return new AccountHeader(this);
    }

    /// <summary>
    /// helper method to calculate the order of the profiles
    /// </summary>
    protected internal void CalculateProfiles()
    {
      if (mProfiles == null)
      {
        mProfiles = new List<IProfile>();
      }

      if (mCurrentProfile == null)
      {
        int setCount = 0;
        for (int i = 0; i < mProfiles.Count; i++)
        {
          if (mProfiles.Count > i && mProfiles[i].IsSelectable())
          {
            if (setCount == 0 && (mCurrentProfile == null))
            {
              mCurrentProfile = mProfiles[i];
            }
            else if (setCount == 1 && (mProfileFirst == null))
            {
              mProfileFirst = mProfiles[i];
            }
            else if (setCount == 2 && (mProfileSecond == null))
            {
              mProfileSecond = mProfiles[i];
            }
            else if (setCount == 3 && (mProfileThird == null))
            {
              mProfileThird = mProfiles[i];
            }
            setCount++;
          }
        }

        return;
      }

      IProfile[] previousActiveProfiles = new IProfile[]
      {
        mCurrentProfile,
        mProfileFirst,
        mProfileSecond,
        mProfileThird
      };

      IProfile[] newActiveProfiles = new IProfile[4];
      Stack<IProfile> unusedProfiles = new Stack<IProfile>();

      // try to keep existing active profiles in the same positions
      for (int i = 0; i < mProfiles.Count; i++)
      {
        IProfile p = mProfiles[i];
        if (p.IsSelectable())
        {
          bool used = false;
          for (int j = 0; j < 4; j++)
          {
            if (previousActiveProfiles[j] == p)
            {
              newActiveProfiles[j] = p;
              used = true;
              break;
            }
          }
          if (!used)
          {
            unusedProfiles.Push(p);
          }
        }
      }

      Stack<IProfile> activeProfiles = new Stack<IProfile>();
      // try to fill the gaps with new available profiles
      for (int i = 0; i < 4; i++)
      {
        if (newActiveProfiles[i] != null)
        {
          activeProfiles.Push(newActiveProfiles[i]);
        }
        else if (unusedProfiles.Any())
        {
          activeProfiles.Push(unusedProfiles.Pop());
        }
      }

      Stack<IProfile> reversedActiveProfiles = new Stack<IProfile>();
      while (activeProfiles.Any())
      {
        reversedActiveProfiles.Push(activeProfiles.Pop());
      }

      // reassign active profiles
      if (!reversedActiveProfiles.Any())
      {
        mCurrentProfile = null;
      }
      else
      {
        mCurrentProfile = reversedActiveProfiles.Pop();
      }
      if (!reversedActiveProfiles.Any())
      {
        mProfileFirst = null;
      }
      else
      {
        mProfileFirst = reversedActiveProfiles.Pop();
      }
      if (!reversedActiveProfiles.Any())
      {
        mProfileSecond = null;
      }
      else
      {
        mProfileSecond = reversedActiveProfiles.Pop();
      }
      if (!reversedActiveProfiles.Any())
      {
        mProfileThird = null;
      }
      else
      {
        mProfileThird = reversedActiveProfiles.Pop();
      }
    }

    /// <summary>
    /// helper method to switch the profiles
    /// </summary>
    /// <param name="newSelection"></param>
    /// <returns>true if the new selection was the current profile</returns>
    protected internal bool SwitchProfiles(IProfile newSelection)
    {
      if (newSelection == null)
      {
        return false;
      }
      if (mCurrentProfile == newSelection)
      {
        return true;
      }

      if (mAlternativeProfileHeaderSwitching)
      {
        int prevSelection = -1;
        if (mProfileFirst == newSelection)
        {
          prevSelection = 1;
        }
        else if (mProfileSecond == newSelection)
        {
          prevSelection = 2;
        }
        else if (mProfileThird == newSelection)
        {
          prevSelection = 3;
        }

        IProfile tmp = mCurrentProfile;
        mCurrentProfile = newSelection;

        if (prevSelection == 1)
        {
          mProfileFirst = tmp;
        }
        else if (prevSelection == 2)
        {
          mProfileSecond = tmp;
        }
        else if (prevSelection == 3)
        {
          mProfileThird = tmp;
        }
      }
      else
      {
        if (mProfiles != null)
        {
          IList<IProfile> previousActiveProfiles = new List<IProfile>
          {
            mCurrentProfile,
            mProfileFirst,
            mProfileSecond,
            mProfileThird
          };

          if (previousActiveProfiles.Contains(newSelection))
          {
            var position = -1;

            for (var i = 0; i < 4; i++)
            {
              if (previousActiveProfiles[i] == newSelection)
              {
                position = i;
                break;
              }
            }

            if (position != -1)
            {
              previousActiveProfiles.RemoveAt(position);
              previousActiveProfiles.Insert(0, newSelection);

              mCurrentProfile = previousActiveProfiles[0];
              mProfileFirst = previousActiveProfiles[1];
              mProfileSecond = previousActiveProfiles[2];
              mProfileThird = previousActiveProfiles[3];
            }
          }
          else
          {
            mProfileThird = mProfileSecond;
            mProfileSecond = mProfileFirst;
            mProfileFirst = mCurrentProfile;
            mCurrentProfile = newSelection;
          }
        }
      }

      BuildProfiles();

      return false;
    }

    /// <summary>
    /// helper method to build the views for the ui
    /// </summary>
    protected internal void BuildProfiles()
    {
      mCurrentProfileView.Visibility = ViewStates.Invisible;
      mAccountHeaderTextSection.Visibility = ViewStates.Invisible;
      mAccountSwitcherArrow.Visibility = ViewStates.Invisible;
      mProfileFirstView.Visibility = ViewStates.Invisible;
      mProfileFirstView.SetOnClickListener(null);
      mProfileSecondView.Visibility = ViewStates.Invisible;
      mProfileSecondView.SetOnClickListener(null);
      mProfileThirdView.Visibility = ViewStates.Invisible;
      mProfileThirdView.SetOnClickListener(null);

      HandleSelectionView(mCurrentProfile, true);

      if (mCurrentProfile != null)
      {
        if (mProfileImagesVisible)
        {
          SetImageOrPlaceholder(mCurrentProfileView, mCurrentProfile.GetIcon(), mCurrentProfile.GetIconBitmap(),
            mCurrentProfile.GetIconUri());
          if (mProfileImagesClickable)
          {
            //todo: wdcossey
            mCurrentProfileView.Click += ProfileOnClick;
            //mCurrentProfileView.SetOnClickListener(onProfileClickListener);
            mCurrentProfileView.DisableTouchFeedback(false);
          }
          else
          {
            mCurrentProfileView.DisableTouchFeedback(true);
          }
          mCurrentProfileView.Visibility = ViewStates.Visible;
        }
        else if (mCompactStyle)
        {
          mCurrentProfileView.Visibility = ViewStates.Gone;
        }

        mAccountHeaderTextSection.Visibility = ViewStates.Visible;
        HandleSelectionView(mCurrentProfile, true);
        mAccountSwitcherArrow.Visibility = ViewStates.Visible;
        mCurrentProfileView.SetTag(Resource.Id.profile_header, (Object) mCurrentProfile);
        mCurrentProfileName.Text = mCurrentProfile.GetName();
        mCurrentProfileEmail.Text = mCurrentProfile.GetEmail();

        if (mProfileFirst != null && mProfileImagesVisible)
        {
          SetImageOrPlaceholder(mProfileFirstView, mProfileFirst.GetIcon(), mProfileFirst.GetIconBitmap(),
            mProfileFirst.GetIconUri());
          mProfileFirstView.SetTag(Resource.Id.profile_header, (Object) mProfileFirst);
          if (mProfileImagesClickable)
          {
            //todo: wdcossey
            mProfileFirstView.Click += ProfileOnClick;
            //mProfileFirstView.SetOnClickListener(onProfileClickListener);
            mProfileFirstView.DisableTouchFeedback(false);
          }
          else
          {
            mProfileFirstView.DisableTouchFeedback(true);
          }
          mProfileFirstView.Visibility = ViewStates.Visible;
        }
        if (mProfileSecond != null && mProfileImagesVisible)
        {
          SetImageOrPlaceholder(mProfileSecondView, mProfileSecond.GetIcon(), mProfileSecond.GetIconBitmap(),
            mProfileSecond.GetIconUri());
          mProfileSecondView.SetTag(Resource.Id.profile_header, (Object) mProfileSecond);
          if (mProfileImagesClickable)
          {
            //todo: wdcossey
            mProfileSecondView.Click += ProfileOnClick;
            //mProfileSecondView.SetOnClickListener(onProfileClickListener);
            mProfileSecondView.DisableTouchFeedback(false);
          }
          else
          {
            mProfileSecondView.DisableTouchFeedback(true);
          }
          mProfileSecondView.Visibility = ViewStates.Visible;
          AlignParentLayoutParam(mProfileFirstView, 0);
        }
        else
        {
          AlignParentLayoutParam(mProfileFirstView, 1);
        }
        if (mProfileThird != null && mThreeSmallProfileImages && mProfileImagesVisible)
        {
          SetImageOrPlaceholder(mProfileThirdView, mProfileThird.GetIcon(), mProfileThird.GetIconBitmap(),
            mProfileThird.GetIconUri());
          mProfileThirdView.SetTag(Resource.Id.profile_header, (Object) mProfileThird);
          if (mProfileImagesClickable)
          {
            //todo: wdcossey
            mProfileThirdView.Click += ProfileOnClick;
            //mProfileThirdView.SetOnClickListener(onProfileClickListener);
            mProfileThirdView.DisableTouchFeedback(false);
          }
          else
          {
            mProfileThirdView.DisableTouchFeedback(true);
          }
          mProfileThirdView.Visibility = ViewStates.Visible;
          AlignParentLayoutParam(mProfileSecondView, 0);
        }
        else
        {
          AlignParentLayoutParam(mProfileSecondView, 1);
        }
      }
      else if (mProfiles != null && mProfiles.Count > 0)
      {
        IProfile profile = mProfiles[0];
        mAccountHeaderTextSection.SetTag(Resource.Id.profile_header, (Object) profile);
        mAccountHeaderTextSection.Visibility = ViewStates.Visible;
        HandleSelectionView(mCurrentProfile, true);
        mAccountSwitcherArrow.Visibility = ViewStates.Visible;
        mCurrentProfileName.Text = profile.GetName();
        mCurrentProfileEmail.Text = profile.GetEmail();
      }

      if (!mSelectionFirstLineShown)
      {
        mCurrentProfileName.Visibility = ViewStates.Gone;
      }
      if (!TextUtils.IsEmpty(mSelectionFirstLine))
      {
        mCurrentProfileName.Text = mSelectionFirstLine;
        mAccountHeaderTextSection.Visibility = ViewStates.Visible;
      }
      if (!mSelectionSecondLineShown)
      {
        mCurrentProfileEmail.Visibility = ViewStates.Gone;
      }
      if (!TextUtils.IsEmpty(mSelectionSecondLine))
      {
        mCurrentProfileEmail.Text = mSelectionSecondLine;
        mAccountHeaderTextSection.Visibility = ViewStates.Visible;
      }

      //if we disabled the list
      if (!mSelectionListEnabled)
      {
        mAccountSwitcherArrow.Visibility = ViewStates.Invisible;
        HandleSelectionView(null, false);
      }
      if (!mSelectionListEnabledForSingleProfile && mProfileFirst == null && (mProfiles == null || mProfiles.Count == 1))
      {
        mAccountSwitcherArrow.Visibility = ViewStates.Invisible;
        HandleSelectionView(null, false);
      }

      //if we disabled the list but still have set a custom listener
      if (mOnAccountHeaderSelectionViewClickListener != null)
      {
        HandleSelectionView(mCurrentProfile, true);
      }
    }

    private void ProfileOnClick(object sender, EventArgs eventArgs)
    {
      OnProfileClick((View)sender, true);
    }

    /// <summary>
    /// small helper method to change the align parent lp for the view
    /// </summary>
    /// <param name="view"></param>
    /// <param name="add"></param>
    private void AlignParentLayoutParam(View view, int add)
    {
      RelativeLayout.LayoutParams lp = (RelativeLayout.LayoutParams) view.LayoutParameters;
      lp.AddRule(LayoutRules.AlignParentRight, add);
      if (Android.OS.Build.VERSION.SdkInt >= BuildVersionCodes.JellyBeanMr1)
      {
        lp.AddRule(LayoutRules.AlignParentEnd, add);
      }
      view.LayoutParameters = lp;
    }

    /// <summary>
    /// small helper method to set an profile image or a placeholder
    /// </summary>
    /// <param name="iv"></param>
    /// <param name="d"></param>
    /// <param name="b"></param>
    /// <param name="uri"></param>
    private void SetImageOrPlaceholder(ImageView iv, Drawable d, Bitmap b, Uri uri)
    {
      if (uri != null)
      {
        iv.SetImageDrawable(UIUtils.GetPlaceHolder(iv.Context));
        iv.SetImageURI(uri);
      }
      else if (d == null && b == null)
      {
        iv.SetImageDrawable(UIUtils.GetPlaceHolder(iv.Context));
      }
      else if (b == null)
      {
        iv.SetImageDrawable(d);
      }
      else
      {
        iv.SetImageBitmap(b);
      }
    }

    //TODO: wdcossey
    ///**
    // * onProfileClickListener to notify onClick on a profile image
    // */
    //private View.IOnClickListener onCurrentProfileClickListener = new View.OnClickListener() {
    //    @Override
    //    public void onClick(final View v) {
    //        onProfileClick(v, true);
    //    }
    //};

    //TODO: wdcossey
    ///**
    // * onProfileClickListener to notify onClick on a profile image
    // */
    //private View.OnClickListener onProfileClickListener = new View.OnClickListener() {
    //    @Override
    //    public void onClick(final View v) {
    //        onProfileClick(v, false);
    //    }
    //};

    protected void OnProfileClick(View v, bool current)
    {
      IProfile profile = (IProfile) v.GetTag(Resource.Id.profile_header);
      SwitchProfiles(profile);

      bool consumed = false;
      if (mOnAccountHeaderListener != null)
      {
        consumed = mOnAccountHeaderListener.OnProfileChanged(v, profile, current);
      }

      if (!consumed)
      {
        //reset the drawer content
        ResetDrawerContent(v.Context);

        new Handler().PostDelayed(delegate
        {
          if (mDrawer != null)
          {
            mDrawer.CloseDrawer();
          }
        }, 200);
      }
    }

    /// <summary>
    /// get the current selection
    /// </summary>
    /// <returns></returns>
    protected internal int GetCurrentSelection()
    {
      if (mCurrentProfile != null && mProfiles != null)
      {
        int i = 0;
        foreach (var profile in mProfiles)
        {
          if (profile == mCurrentProfile)
          {
            return i;
          }
          i++;
        }
      }
      return -1;
    }

    //TODO: wdcossey
    ///**
    // * onSelectionClickListener to notify the onClick on the checkbox
    // */
    //private View.IOnClickListener onSelectionClickListener = new View.OnClickListener() {
    //    @Override
    //    public void onClick(View v) {
    //        boolean consumed = false;
    //        if (mOnAccountHeaderSelectionViewClickListener != null) {
    //            consumed = mOnAccountHeaderSelectionViewClickListener.onClick(v, (IProfile) v.getTag(R.id.profile_header));
    //        }

    //        if (mAccountSwitcherArrow.getVisibility() == View.VISIBLE && !consumed) {
    //            toggleSelectionList(v.getContext());
    //        }
    //    }
    //};

    /// <summary>
    /// helper method to toggle the collection
    /// </summary>
    /// <param name="ctx"></param>
    protected internal void ToggleSelectionList(Context ctx)
    {
      if (mDrawer != null)
      {
        //if we already show the list. reset everything instead
        if (mDrawer.SwitchedDrawerContent())
        {
          ResetDrawerContent(ctx);
          mSelectionListShown = false;
        }
        else
        {
          //build and set the drawer selection list
          BuildDrawerSelectionList();

          // update the arrow image within the drawer
          mAccountSwitcherArrow.SetImageDrawable(
            new IconicsDrawable<GoogleMaterial>(ctx, GoogleMaterial.Icon.gmd_arrow_drop_up).SizeDp(24).PaddingDp(6).Color(mTextColor));
          mSelectionListShown = true;
        }
      }
    }

    /// <summary>
    /// helper method to build and set the drawer selection list
    /// </summary>
    protected void BuildDrawerSelectionList()
    {
      var selectedPosition = -1;
      var position = 0;
      var profileDrawerItems = new List<IDrawerItem>();
      if (mProfiles != null)
      {
        foreach (var profile in mProfiles)
        {
          if (profile == mCurrentProfile)
          {
            if (mCurrentHiddenInList)
            {
              continue;
            }
            else
            {
              selectedPosition = position;
            }
          }
          if (profile is IDrawerItem)
          {
            profileDrawerItems.Add((IDrawerItem) profile);
          }
          position = position + 1;
        }
      }
      //todo: wdcossey
      //mDrawer.SwitchDrawerContent(onDrawerItemClickListener, profileDrawerItems, selectedPosition);
    }

    //todo: wdcossey
    ///**
    // * onDrawerItemClickListener to catch the selection for the new profile!
    // */
    //private Drawer.OnDrawerItemClickListener onDrawerItemClickListener = new Drawer.OnDrawerItemClickListener() {
    //    @Override
    //    public boolean onItemClick(AdapterView<?> parent, final View view, int position, long id, final IDrawerItem drawerItem) {
    //        final boolean isCurrentSelectedProfile;
    //        if (drawerItem != null && drawerItem instanceof IProfile && ((IProfile) drawerItem).isSelectable()) {
    //            isCurrentSelectedProfile = switchProfiles((IProfile) drawerItem);
    //        } else {
    //            isCurrentSelectedProfile = false;
    //        }
    //        mDrawer.setOnDrawerItemClickListener(null);
    //        //wrap the onSelection call and the reset stuff within a handler to prevent lag
    //        new Handler().postDelayed(new Runnable() {
    //            @Override
    //            public void run() {
    //                if (mDrawer != null && view != null && view.getContext() != null) {
    //                    resetDrawerContent(view.getContext());
    //                }
    //                if (drawerItem != null && drawerItem instanceof IProfile) {
    //                    if (mOnAccountHeaderListener != null) {
    //                        mOnAccountHeaderListener.onProfileChanged(view, (IProfile) drawerItem, isCurrentSelectedProfile);
    //                    }
    //                }

    //            }
    //        }, 350);

    //        return false;
    //    }
    //};

    /// <summary>
    /// helper method to reset the drawer content
    /// </summary>
    /// <param name="ctx"></param>
    private void ResetDrawerContent(Context ctx)
    {
      mDrawer.ResetDrawerContent();
      mAccountSwitcherArrow.SetImageDrawable(
        new IconicsDrawable<GoogleMaterial>(ctx, GoogleMaterial.Icon.gmd_arrow_drop_down).SizeDp(24).PaddingDp(6).Color(mTextColor));
    }

    /// <summary>
    /// small helper class to update the header and the list
    /// </summary>
    protected internal void UpdateHeaderAndList()
    {
      //recalculate the profiles
      CalculateProfiles();
      //update the profiles in the header
      BuildProfiles();
      //if we currently show the list add the new item directly to it
      if (mSelectionListShown)
      {
        BuildDrawerSelectionList();
      }
    }
  }
}