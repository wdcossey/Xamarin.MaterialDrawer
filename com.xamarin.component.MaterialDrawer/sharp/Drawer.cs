using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using com.xamarin.AndroidIconics.Typefaces;
using com.xamarin.component.MaterialDrawer.Adapters;
using com.xamarin.component.MaterialDrawer.Models.Interfaces;
using com.xamarin.component.MaterialDrawer.Utils;

namespace com.xamarin.component.MaterialDrawer
{
  /// <summary>
  /// <para>Created by wdcossey on 06.06.15.</para>
  /// <para>Original by mikepenz on 03.02.15.</para>
  /// </summary>
  public class Drawer
  {
    /// <summary>
    /// BUNDLE param to store the selection
    /// </summary>
    protected internal const string BUNDLE_SELECTION = "bundle_selection";
    protected internal const string BUNDLE_FOOTER_SELECTION = "bundle_footer_selection";

    /// <summary>
    /// Per the design guidelines, you should show the drawer on launch until the user manually
    /// expands it. This shared preference tracks this.
    /// </summary>
    protected internal const string PREF_USER_LEARNED_DRAWER = "navigation_drawer_learned";


    private readonly DrawerBuilder _drawerBuilder;
    private FrameLayout _contentView;
    private KeyboardUtil _keyboardUtil;

    /// <summary>
    /// the protected Constructor for the result
    /// </summary>
    /// <param name="drawerBuilder"></param>
    protected internal Drawer(DrawerBuilder drawerBuilder)
    {
      _drawerBuilder = drawerBuilder;
    }

    /// <summary>
    /// Get the DrawerLayout of the current drawer
    /// </summary>
    /// <returns></returns>
    public DrawerLayout GetDrawerLayout()
    {
      return _drawerBuilder._drawerLayout;
    }

    /// <summary>
    /// Open the drawer
    /// </summary>
    public void OpenDrawer()
    {
      if (_drawerBuilder._drawerLayout != null && _drawerBuilder.SliderLayout != null)
      {
        if (_drawerBuilder.DrawerGravity != null)
        {
          _drawerBuilder._drawerLayout.OpenDrawer(_drawerBuilder.DrawerGravity.Value);
        }
        else
        {
          _drawerBuilder._drawerLayout.OpenDrawer(_drawerBuilder.SliderLayout);
        }
      }
    }

    /// <summary>
    /// close the drawer
    /// </summary>
    public void CloseDrawer()
    {
      if (_drawerBuilder._drawerLayout != null)
      {
        if (_drawerBuilder.DrawerGravity != null)
        {
          _drawerBuilder._drawerLayout.CloseDrawer(_drawerBuilder.DrawerGravity.Value);
        }
        else
        {
          _drawerBuilder._drawerLayout.CloseDrawer(_drawerBuilder.SliderLayout);
        }
      }
    }

    /// <summary>
    /// Get the current state of the drawer.
    /// True if the drawer is currently open.
    /// </summary>
    /// <returns></returns>
    public bool IsDrawerOpen()
    {
      if (_drawerBuilder._drawerLayout != null && _drawerBuilder.SliderLayout != null)
      {
        return _drawerBuilder._drawerLayout.IsDrawerOpen(_drawerBuilder.SliderLayout);
      }
      return false;
    }

    /// <summary>
    /// set the insetsFrameLayout to display the content in fullscreen
    /// under the statusBar and navigationBar
    /// </summary>
    /// <param name="fullscreen"></param>
    public void SetFullscreen(bool fullscreen)
    {
      if (_drawerBuilder.DrawerContentRoot != null)
      {
        _drawerBuilder.DrawerContentRoot.SetEnabled(!fullscreen);
      }
    }

    /// <summary>
    /// Set the color for the statusBar
    /// </summary>
    /// <param name="statusBarColor"></param>
    public void SetStatusBarColor(Color statusBarColor)
    {
      if (_drawerBuilder.DrawerContentRoot != null)
      {
        _drawerBuilder.DrawerContentRoot.SetInsetForeground(statusBarColor);
      }
    }

    /// <summary>
    /// a helper method to enable the keyboardUtil for a specific activity
    /// or disable it. note this will cause some frame drops because of the
    /// listener. 
    /// </summary>
    /// <param name="activity"></param>
    /// <param name="enable"></param>
    public void KeyboardSupportEnabled(Activity activity, bool enable)
    {
      if (GetContent() != null && GetContent().ChildCount > 0)
      {
        if (_keyboardUtil == null)
        {
          _keyboardUtil = new KeyboardUtil(activity, GetContent().GetChildAt(0));
          _keyboardUtil.Disable();
        }

        if (enable)
        {
          _keyboardUtil.Enable();
        }
        else
        {
          _keyboardUtil.Disable();
        }
      }
    }

    /// <summary>
    /// get the slider layout of the current drawer.
    /// This is the layout containing the ListView
    /// </summary>
    /// <returns></returns>
    public RelativeLayout GetSlider()
    {
      return _drawerBuilder.SliderLayout;
    }

    /// <summary>
    /// get the container frameLayout of the current drawer
    /// </summary>
    /// <returns></returns>
    public FrameLayout GetContent()
    {
      if (_contentView == null && _drawerBuilder._drawerLayout != null)
      {
        _contentView = (FrameLayout)_drawerBuilder._drawerLayout.FindViewById(Resource.Id.content_layout);
      }
      return _contentView;
    }

    /// <summary>
    /// get the listView of the current drawer
    /// </summary>
    /// <returns></returns>
    public ListView GetListView()
    {
      return _drawerBuilder.ListView;
    }

    /// <summary>
    /// get the BaseDrawerAdapter of the current drawer
    /// </summary>
    /// <returns></returns>
    public BaseDrawerAdapter GetAdapter()
    {
      return _drawerBuilder.Adapter;
    }

    /// <summary>
    /// get all drawerItems of the current drawer
    /// </summary>
    /// <returns></returns>
    public IList<IDrawerItem> GetDrawerItems()
    {
      return _drawerBuilder.DrawerItems;
    }

    /// <summary>
    /// get the Header View if set else NULL
    /// </summary>
    /// <returns></returns>
    public View GetHeader()
    {
      return _drawerBuilder.HeaderView;
    }

    /// <summary>
    /// get the StickyHeader View if set else NULL
    /// </summary>
    /// <returns></returns>
    public View GetStickyHeader()
    {
      return _drawerBuilder.StickyHeaderView;
    }

    /// <summary>
    /// method to replace a previous set header
    /// </summary>
    /// <param name="view"></param>
    public void SetHeader(View view)
    {
      if (GetListView() != null)
      {
        BaseDrawerAdapter adapter = GetAdapter();
        GetListView().Adapter = null;
        if (GetHeader() != null)
        {
          GetListView().RemoveHeaderView(GetHeader());
        }
        GetListView().AddHeaderView(view);
        GetListView().Adapter = adapter;
        _drawerBuilder.HeaderView = view;
        _drawerBuilder.HeaderOffset = 1;
      }
    }

    /// <summary>
    /// method to remove the header of the list
    /// </summary>
    public void RemoveHeader()
    {
      if (GetListView() != null && GetHeader() != null)
      {
        GetListView().RemoveHeaderView(GetHeader());
        _drawerBuilder.HeaderView = null;
        _drawerBuilder.HeaderOffset = 0;
      }
    }

    /// <summary>
    /// get the Footer View if set else NULL
    /// </summary>
    /// <returns></returns>
    public View GetFooter()
    {
      return _drawerBuilder.FooterView;
    }

    /// <summary>
    /// get the StickyFooter View if set else NULL
    /// </summary>
    /// <returns></returns>
    public View GetStickyFooter()
    {
      return _drawerBuilder.StickyFooterView;
    }

    /// <summary>
    /// get the ActionBarDrawerToggle
    /// </summary>
    /// <returns></returns>
    public ActionBarDrawerToggle GetActionBarDrawerToggle()
    {
      return _drawerBuilder.ActionBarDrawerToggle;
    }

    /// <summary>
    /// calculates the position of an drawerItem. searching by it's identifier
    /// </summary>
    /// <param name="drawerItem"></param>
    /// <returns></returns>
    public int GetPositionFromIdentifier(IDrawerItem drawerItem)
    {
      return GetPositionFromIdentifier(drawerItem.GetIdentifier());
    }

    /// <summary>
    /// calculates the position of an drawerItem. searching by it's identifier
    /// </summary>
    /// <param name="identifier"></param>
    /// <returns></returns>
    public int GetPositionFromIdentifier(int identifier)
    {
      return DrawerUtils.GetPositionFromIdentifier(_drawerBuilder, identifier);
    }

    /// <summary>
    /// calculates the position of an drawerItem. searching by it's identifier
    /// </summary>
    /// <param name="drawerItem"></param>
    /// <returns></returns>
    public int GetFooterPositionFromIdentifier(IDrawerItem drawerItem)
    {
      return GetFooterPositionFromIdentifier(drawerItem.GetIdentifier());
    }

    /// <summary>
    /// calculates the position of an drawerItem inside the footer. searching by it's identfier
    /// </summary>
    /// <param name="identifier"></param>
    /// <returns></returns>
    public int GetFooterPositionFromIdentifier(int identifier)
    {
      return DrawerUtils.GetFooterPositionFromIdentifier(_drawerBuilder, identifier);
    }

    /// <summary>
    /// get the current selection
    /// </summary>
    /// <returns></returns>
    public int GetCurrentSelection()
    {
      return _drawerBuilder.CurrentSelection;
    }

    /// <summary>
    /// get the current footer selection
    /// </summary>
    /// <returns></returns>
    public int GetCurrentFooterSelection()
    {
      return _drawerBuilder.CurrentFooterSelection;
    }

    /// <summary>
    /// set the current selection in the drawer
    /// NOTE: This will trigger onDrawerItemSelected without a view!
    /// </summary>
    /// <param name="identifier"></param>
    /// <returns></returns>
    public bool SetSelectionByIdentifier(int identifier)
    {
      return SetSelection(GetPositionFromIdentifier(identifier), true);
    }

    /// <summary>
    /// set the current selection in the drawer
    /// NOTE: This will trigger onDrawerItemSelected without a view if you pass fireOnClick = true;
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="fireOnClick"></param>
    /// <returns></returns>
    public bool SetSelectionByIdentifier(int identifier, bool fireOnClick)
    {
      return SetSelection(GetPositionFromIdentifier(identifier), fireOnClick);
    }

    /// <summary>
    /// set the current selection in the footer of the drawer
    /// NOTE: This will trigger onDrawerItemSelected without a view if you pass fireOnClick = true;
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="fireOnClick"></param>
    public void SetFooterSelectionByIdentifier(int identifier, bool fireOnClick)
    {
      SetFooterSelection(GetPositionFromIdentifier(identifier), fireOnClick);
    }

    /// <summary>
    /// set the current selection in the drawer
    /// NOTE: This will trigger onDrawerItemSelected without a view!
    /// </summary>
    /// <param name="drawerItem"></param>
    /// <returns></returns>
    public bool SetSelection(IDrawerItem drawerItem)
    {
      return SetSelection(GetPositionFromIdentifier(drawerItem), true);
    }

    /// <summary>
    /// set the current selection in the drawer
    /// NOTE: This will trigger onDrawerItemSelected without a view if you pass fireOnClick = true;
    /// </summary>
    /// <param name="drawerItem"></param>
    /// <param name="fireOnClick"></param>
    /// <returns></returns>
    public bool SetSelection(IDrawerItem drawerItem, bool fireOnClick)
    {
      return SetSelection(GetPositionFromIdentifier(drawerItem), fireOnClick);
    }

    /// <summary>
    /// set the current selection in the drawer
    /// NOTE: This will trigger onDrawerItemSelected without a view!
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public bool SetSelection(int position)
    {
      return SetSelection(position, true);
    }

    /// <summary>
    /// set the current selection in the drawer
    /// NOTE: This will trigger onDrawerItemSelected without a view if you pass fireOnClick = true;
    /// </summary>
    /// <param name="position"></param>
    /// <param name="fireOnClick"></param>
    /// <returns>true if the event was consumed</returns>
    public bool SetSelection(int position, bool fireOnClick)
    {
      if (_drawerBuilder.ListView != null)
      {
        return DrawerUtils.SetListSelection(_drawerBuilder, position, fireOnClick,
          _drawerBuilder.GetDrawerItem(position, false));
      }
      return false;
    }

    /// <summary>
    /// set the current selection in the footer of the drawer
    /// NOTE: This will trigger onDrawerItemSelected without a view!
    /// </summary>
    /// <param name="position"></param>
    public void SetFooterSelection(int position)
    {
      SetFooterSelection(position, true);
    }

    /// <summary>
    /// set the current selection in the footer of the drawer
    /// NOTE: This will trigger onDrawerItemSelected without a view if you pass fireOnClick = true;
    /// </summary>
    /// <param name="position"></param>
    /// <param name="fireOnClick"></param>
    public void SetFooterSelection(int position, bool fireOnClick)
    {
      DrawerUtils.SetFooterSelection(_drawerBuilder, position, fireOnClick);
    }

    /// <summary>
    /// update a specific drawer item :D
    /// automatically identified by its id
    /// </summary>
    /// <param name="drawerItem"></param>
    public void UpdateItem(IDrawerItem drawerItem)
    {
      UpdateItem(drawerItem, GetPositionFromIdentifier(drawerItem));
    }

    /// <summary>
    /// Update a drawerItem at a specific position
    /// </summary>
    /// <param name="drawerItem"></param>
    /// <param name="position"></param>
    public void UpdateItem(IDrawerItem drawerItem, int position)
    {
      if (_drawerBuilder.CheckDrawerItem(position, false))
      {
        _drawerBuilder.DrawerItems[position] = drawerItem;
        _drawerBuilder.Adapter.DataUpdated();
      }
    }

    /// <summary>
    /// Add a drawerItem at the end
    /// </summary>
    /// <param name="drawerItem"></param>
    public void AddItem(IDrawerItem drawerItem)
    {
      if (_drawerBuilder.DrawerItems != null)
      {
        _drawerBuilder.DrawerItems.Add(drawerItem);
        _drawerBuilder.Adapter.DataUpdated();
      }
    }

    /// <summary>
    /// Add a drawerItem at a specific position
    /// </summary>
    /// <param name="drawerItem"></param>
    /// <param name="position"></param>
    public void AddItem(IDrawerItem drawerItem, int position)
    {
      if (_drawerBuilder.DrawerItems != null)
      {
        _drawerBuilder.DrawerItems.Insert(position, drawerItem);
        _drawerBuilder.Adapter.DataUpdated();
      }
    }

    /// <summary>
    /// Set a drawerItem at a specific position
    /// </summary>
    /// <param name="drawerItem"></param>
    /// <param name="position"></param>
    public void SetItem(IDrawerItem drawerItem, int position)
    {
      if (_drawerBuilder.DrawerItems != null)
      {
        _drawerBuilder.DrawerItems[position] = drawerItem;
        _drawerBuilder.Adapter.DataUpdated();
      }
    }

    /// <summary>
    /// Remove a drawerItem at a specific position
    /// </summary>
    /// <param name="position"></param>
    public void RemoveItem(int position)
    {
      if (_drawerBuilder.CheckDrawerItem(position, false))
      {
        _drawerBuilder.DrawerItems.RemoveAt(position);
        _drawerBuilder.Adapter.DataUpdated();
      }
    }

    /// <summary>
    /// Removes all items from drawer
    /// </summary>
    public void RemoveAllItems()
    {
      _drawerBuilder.DrawerItems.Clear();
      _drawerBuilder.Adapter.DataUpdated();
    }

    /// <summary>
    /// add new Items to the current DrawerItem List
    /// </summary>
    /// <param name="drawerItems"></param>
    public void AddItems(IDrawerItem[] drawerItems)
    {
      if (_drawerBuilder.DrawerItems != null)
      {
        _drawerBuilder.DrawerItems.AddRange(drawerItems);
        _drawerBuilder.Adapter.DataUpdated();
      }
    }

    /// <summary>
    /// Replace the current DrawerItems with a new ArrayList of items
    /// </summary>
    /// <param name="drawerItems"></param>
    public void SetItems(IList<IDrawerItem> drawerItems)
    {
      SetItems(drawerItems, false);
    }

    /// <summary>
    /// replace the current DrawerItems with the new ArrayList.
    /// </summary>
    /// <param name="drawerItems"></param>
    /// <param name="switchedItems"></param>
    private void SetItems(IList<IDrawerItem> drawerItems, bool switchedItems)
    {
      _drawerBuilder.DrawerItems = drawerItems.ToList();

      //if we are currently at a switched list set the new reference
      if (_originalDrawerItems != null && !switchedItems)
      {
        _originalDrawerItems = drawerItems;
      }
      else
      {
        _drawerBuilder.Adapter.SetDrawerItems(_drawerBuilder.DrawerItems);
      }

      _drawerBuilder.Adapter.DataUpdated();
    }

    /// <summary>
    /// Update the name of a drawer item if its an instance of nameable
    /// </summary>
    /// <param name="nameRes"></param>
    /// <param name="position"></param>
    public void UpdateName(int nameRes, int position)
    {
      if (_drawerBuilder.CheckDrawerItem(position, false))
      {
        IDrawerItem drawerItem = _drawerBuilder.DrawerItems[position];

        if (drawerItem is INameable)
        {
          ((INameable) drawerItem).SetNameRes(nameRes);
          ((INameable) drawerItem).SetName(null);
        }

        _drawerBuilder.DrawerItems[position] = drawerItem;
        _drawerBuilder.Adapter.NotifyDataSetChanged();
      }
    }

    /// <summary>
    /// Update the name of a drawer item if its an instance of nameable
    /// </summary>
    /// <param name="name"></param>
    /// <param name="position"></param>
    public void UpdateName(string name, int position)
    {
      if (_drawerBuilder.CheckDrawerItem(position, false))
      {
        IDrawerItem drawerItem = _drawerBuilder.DrawerItems[position];

        if (drawerItem is INameable)
        {
          ((INameable) drawerItem).SetName(name);
          ((INameable) drawerItem).SetNameRes(-1);
        }

        _drawerBuilder.DrawerItems[position] = drawerItem;
        _drawerBuilder.Adapter.NotifyDataSetChanged();
      }
    }

    /// <summary>
    /// Update the badge of a drawer item if its an instance of badgeable
    /// </summary>
    /// <param name="badge"></param>
    /// <param name="position"></param>
    public void UpdateBadge(string badge, int position)
    {
      if (_drawerBuilder.CheckDrawerItem(position, false))
      {
        IDrawerItem drawerItem = _drawerBuilder.DrawerItems[position];

        if (drawerItem is IBadgeable)
        {
          ((IBadgeable) drawerItem).SetBadge(badge);
        }

        _drawerBuilder.DrawerItems[position] = drawerItem;
        _drawerBuilder.Adapter.NotifyDataSetChanged();
      }
    }

    /// <summary>
    /// Update the icon of a drawer item if its an instance of iconable
    /// </summary>
    /// <param name="icon"></param>
    /// <param name="position"></param>
    public void UpdateIcon(Drawable icon, int position)
    {
      if (_drawerBuilder.CheckDrawerItem(position, false))
      {
        IDrawerItem drawerItem = _drawerBuilder.DrawerItems[position];

        if (drawerItem is IIconable)
        {
          ((IIconable) drawerItem).SetIcon(icon);
        }

        _drawerBuilder.DrawerItems[position] = drawerItem;
        _drawerBuilder.Adapter.NotifyDataSetChanged();
      }
    }

    /// <summary>
    /// Update the icon of a drawer item from an iconRes
    /// </summary>
    /// <param name="iconRes"></param>
    /// <param name="position"></param>
    public void UpdateIcon(int iconRes, int position)
    {
      if (_drawerBuilder.RootView != null && _drawerBuilder.CheckDrawerItem(position, false))
      {
        IDrawerItem drawerItem = _drawerBuilder.DrawerItems[position];

        if (drawerItem is IIconable)
        {
          ((IIconable) drawerItem).SetIcon(UIUtils.GetCompatDrawable(_drawerBuilder.RootView.Context, iconRes));
        }

        _drawerBuilder.DrawerItems[position] = drawerItem;
        _drawerBuilder.Adapter.NotifyDataSetChanged();
      }
    }

    /// <summary>
    /// Update the icon of a drawer item if its an instance of iconable
    /// </summary>
    /// <param name="icon"></param>
    /// <param name="position"></param>
    public void UpdateIcon(IIcon icon, int position)
    {
      if (_drawerBuilder.CheckDrawerItem(position, false))
      {
        IDrawerItem drawerItem = _drawerBuilder.DrawerItems[position];

        if (drawerItem is IIconable)
        {
          ((IIconable) drawerItem).SetIIcon(icon);
        }

        _drawerBuilder.DrawerItems[position] = drawerItem;
        _drawerBuilder.Adapter.NotifyDataSetChanged();
      }
    }

    /// <summary>
    /// update a specific footerDrawerItem :D
    /// automatically identified by it's id
    /// </summary>
    /// <param name="drawerItem"></param>
    public void UpdateFooterItem(IDrawerItem drawerItem)
    {
      UpdateFooterItem(drawerItem, GetFooterPositionFromIdentifier(drawerItem));
    }

    /// <summary>
    /// update a footerDrawerItem at a specific position
    /// </summary>
    /// <param name="drawerItem"></param>
    /// <param name="position"></param>
    public void UpdateFooterItem(IDrawerItem drawerItem, int position)
    {
      if (_drawerBuilder.mStickyDrawerItems != null && _drawerBuilder.mStickyDrawerItems.Count > position)
      {
        _drawerBuilder.mStickyDrawerItems[position] = drawerItem;
      }

      DrawerUtils.RebuildFooterView(_drawerBuilder);
    }


    /// <summary>
    /// Add a footerDrawerItem at the end
    /// </summary>
    /// <param name="drawerItem"></param>
    public void AddFooterItem(IDrawerItem drawerItem)
    {
      if (_drawerBuilder.mStickyDrawerItems == null)
      {
        _drawerBuilder.mStickyDrawerItems = new List<IDrawerItem>();
      }
      _drawerBuilder.mStickyDrawerItems.Add(drawerItem);

      DrawerUtils.RebuildFooterView(_drawerBuilder);
    }

    /// <summary>
    /// Add a footerDrawerItem at a specific position
    /// </summary>
    /// <param name="drawerItem"></param>
    /// <param name="position"></param>
    public void AddFooterItem(IDrawerItem drawerItem, int position)
    {
      if (_drawerBuilder.mStickyDrawerItems == null)
      {
        _drawerBuilder.mStickyDrawerItems = new List<IDrawerItem>();
      }
      _drawerBuilder.mStickyDrawerItems.Insert(position, drawerItem);

      DrawerUtils.RebuildFooterView(_drawerBuilder);
    }

    /// <summary>
    /// Set a footerDrawerItem at a specific position
    /// </summary>
    /// <param name="drawerItem"></param>
    /// <param name="position"></param>
    public void SetFooterItem(IDrawerItem drawerItem, int position)
    {
      if (_drawerBuilder.mStickyDrawerItems != null && _drawerBuilder.mStickyDrawerItems.Count > position)
      {
        _drawerBuilder.mStickyDrawerItems[position] = drawerItem;
      }

      DrawerUtils.RebuildFooterView(_drawerBuilder);
    }


    /// <summary>
    /// Remove a footerDrawerItem at a specific position
    /// </summary>
    /// <param name="position"></param>
    public void RemoveFooterItem(int position)
    {
      if (_drawerBuilder.mStickyDrawerItems != null && _drawerBuilder.mStickyDrawerItems.Count > position)
      {
        _drawerBuilder.mStickyDrawerItems.RemoveAt(position);
      }

      DrawerUtils.RebuildFooterView(_drawerBuilder);
    }

    /// <summary>
    /// Removes all footerItems from drawer
    /// </summary>
    public void RemoveAllFooterItems()
    {
      if (_drawerBuilder.mStickyDrawerItems != null)
      {
        _drawerBuilder.mStickyDrawerItems.Clear();
      }
      if (_drawerBuilder.StickyFooterView != null)
      {
        _drawerBuilder.StickyFooterView.Visibility = ViewStates.Gone;
      }
    }

    /// <summary>
    /// setter for the OnDrawerItemClickListener
    /// </summary>
    /// <param name="onDrawerItemClickListener"></param>
    public void SetOnDrawerItemClickListener(IOnDrawerItemClickListener onDrawerItemClickListener)
    {
      _drawerBuilder.mOnDrawerItemClickListener = onDrawerItemClickListener;
    }

    /// <summary>
    /// method to get the OnDrawerItemClickListener
    /// </summary>
    /// <returns></returns>
    public IOnDrawerItemClickListener GetOnDrawerItemClickListener()
    {
      return _drawerBuilder.mOnDrawerItemClickListener;
    }

    /// <summary>
    /// setter for the OnDrawerItemLongClickListener
    /// </summary>
    /// <param name="onDrawerItemLongClickListener"></param>
    public void SetOnDrawerItemLongClickListener(IOnDrawerItemLongClickListener onDrawerItemLongClickListener)
    {
      _drawerBuilder.mOnDrawerItemLongClickListener = onDrawerItemLongClickListener;
    }

    /// <summary>
    /// method to get the OnDrawerItemLongClickListener
    /// </summary>
    /// <returns></returns>
    public IOnDrawerItemLongClickListener GetOnDrawerItemLongClickListener()
    {
      return _drawerBuilder.mOnDrawerItemLongClickListener;
    }

    //variables to store and remember the original list of the drawer
    private IOnDrawerItemClickListener _originalOnDrawerItemClickListener;
    private IList<IDrawerItem> _originalDrawerItems;
    private int _originalDrawerSelection = -1;

    public bool SwitchedDrawerContent()
    {
      return
        !(_originalOnDrawerItemClickListener == null && _originalDrawerItems == null && _originalDrawerSelection == -1);
    }

    /// <summary>
    /// method to switch the drawer content to new elements
    /// </summary>
    /// <param name="onDrawerItemClickListener"></param>
    /// <param name="drawerItems"></param>
    /// <param name="drawerSelection"></param>
    public void SwitchDrawerContent(IOnDrawerItemClickListener onDrawerItemClickListener,
      IEnumerable<IDrawerItem> drawerItems, int drawerSelection)
    {
      //just allow a single switched drawer
      if (!SwitchedDrawerContent())
      {
        //save out previous values
        _originalOnDrawerItemClickListener = GetOnDrawerItemClickListener();
        _originalDrawerItems = GetDrawerItems();
        _originalDrawerSelection = GetCurrentSelection();

        //set the new items
        SetOnDrawerItemClickListener(onDrawerItemClickListener);
        SetItems(drawerItems.ToList(), true);
        SetSelection(drawerSelection, false);

        _drawerBuilder.Adapter.ResetAnimation();

        if (GetStickyFooter() != null)
        {
          GetStickyFooter().Visibility = ViewStates.Gone;
        }
      }
    }

    /// <summary>
    /// helper method to reset to the original drawerContent
    /// </summary>
    public void ResetDrawerContent()
    {
      if (SwitchedDrawerContent())
      {
        //set the new items
        SetOnDrawerItemClickListener(_originalOnDrawerItemClickListener);
        SetItems(_originalDrawerItems, true);
        SetSelection(_originalDrawerSelection, false);
        //remove the references
        _originalOnDrawerItemClickListener = null;
        _originalDrawerItems = null;
        _originalDrawerSelection = -1;

        _drawerBuilder.Adapter.ResetAnimation();

        if (GetStickyFooter() != null)
        {
          GetStickyFooter().Visibility = ViewStates.Visible;
        }
      }
    }

    /// <summary>
    /// add the values to the bundle for saveInstanceState
    /// </summary>
    /// <param name="savedInstanceState"></param>
    /// <returns></returns>
    public Bundle SaveInstanceState(Bundle savedInstanceState)
    {
      if (savedInstanceState != null)
      {
        savedInstanceState.PutInt(BUNDLE_SELECTION, _drawerBuilder.CurrentSelection);
        savedInstanceState.PutInt(BUNDLE_FOOTER_SELECTION, _drawerBuilder.CurrentFooterSelection);
      }
      return savedInstanceState;
    }


    public interface IOnDrawerNavigationListener
    {
      /// <summary>
      /// 
      /// </summary>
      /// <param name="clickedView"></param>
      /// <returns>true if the event was consumed</returns>
      bool OnNavigationClickListener(View clickedView);
    }

    public interface IOnDrawerItemClickListener
    {
      /// <summary>
      /// 
      /// </summary>
      /// <param name="parent"></param>
      /// <param name="view"></param>
      /// <param name="position"></param>
      /// <param name="id"></param>
      /// <param name="drawerItem"></param>
      /// <returns>true if the event was consumed</returns>
      bool OnItemClick(AdapterView parent, View view, int position, long id, IDrawerItem drawerItem);
    }

    //public class DrawerItemClickEventArgs : EventArgs
    //{
    //  public AdapterView Parent;
    //  public View View;
    //  public int Position;
    //  public long Id;
    //  public IDrawerItem DrawerItem;
    //}

    //public static EventHandler<DrawerItemClickEventArgs> DrawerItemClickEvent;

    public interface IOnDrawerItemLongClickListener
    {
      /// <summary>
      /// 
      /// </summary>
      /// <param name="parent"></param>
      /// <param name="view"></param>
      /// <param name="position"></param>
      /// <param name="id"></param>
      /// <param name="drawerItem"></param>
      /// <returns>true if the event was consumed</returns>
      bool OnItemLongClick(AdapterView parent, View view, int position, long id, IDrawerItem drawerItem);
    }

    public interface IOnDrawerListener
    {
      /// <summary>
      /// 
      /// </summary>
      /// <param name="drawerView"></param>
      void OnDrawerOpened(View drawerView);

      /// <summary>
      /// 
      /// </summary>
      /// <param name="drawerView"></param>
      void OnDrawerClosed(View drawerView);

      /// <summary>
      /// 
      /// </summary>
      /// <param name="drawerView"></param>
      /// <param name="slideOffset"></param>
      void OnDrawerSlide(View drawerView, float slideOffset);
    }

    public interface IOnDrawerItemSelectedListener
    {

      /// <summary>
      /// 
      /// </summary>
      /// <param name="parent"></param>
      /// <param name="view"></param>
      /// <param name="position"></param>
      /// <param name="id"></param>
      /// <param name="drawerItem"></param>
      void OnItemSelected(AdapterView parent, View view, int position, long id, IDrawerItem drawerItem);

      /// <summary>
      /// 
      /// </summary>
      /// <param name="parent"></param>
      void OnNothingSelected(AdapterView parent);
    }
  }
}