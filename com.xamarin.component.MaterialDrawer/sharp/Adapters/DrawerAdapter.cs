using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Views.Animations;
using com.xamarin.component.MaterialDrawer.Models.Interfaces;
using Java.Lang;

namespace com.xamarin.component.MaterialDrawer.Adapters
{
  public class DrawerAdapter : BaseDrawerAdapter
  {

    private List<IDrawerItem> _drawerItems;

    private readonly bool _animateDrawerItems = false;
    private readonly List<bool> _drawerAnimatedItems;

    private readonly LayoutInflater _inflater;

    private HashSet<string> _typeMapper;

    public DrawerAdapter(Activity activity)
      : this(activity, false)
    {
    }

    public DrawerAdapter(Activity activity, bool animateDrawerItems)
      : this(activity, null, animateDrawerItems)
    {

    }

    public DrawerAdapter(Activity activity, IList<IDrawerItem> drawerItems)
      : this(activity, drawerItems, false)
    {

    }

    public DrawerAdapter(Activity activity, IList<IDrawerItem> drawerItems, bool animateDrawerItems)
    {
      _inflater = (LayoutInflater) activity.GetSystemService(Context.LayoutInflaterService);

      _drawerItems = new List<IDrawerItem>();
      _drawerAnimatedItems = new List<bool>();
      _animateDrawerItems = animateDrawerItems;

      SetDrawerItems(drawerItems);
    }


    public void Add(IDrawerItem[] drawerItems)
    {
      if (drawerItems != null)
      {
        _drawerItems.AddRange(drawerItems);
      }

      if (drawerItems != null)
      {
        for (var i = 0; i < drawerItems.Length; i++)
        {
          _drawerAnimatedItems.Add(false);
        }
      }

      MapTypes();
    }

    public override bool AreAllItemsEnabled()
    {
      return false;
    }


    public override bool IsEnabled(int position)
    {
      return position < Count && _drawerItems[position].IsEnabled();
    }

    public override int Count
    {
      get { return _drawerItems == null ? 0 : _drawerItems.Count; }
    }

    public override Object GetItem(int position)
    {
      return position < Count ? (Object) _drawerItems[position] : null;
    }

    public bool? GetAnimatedItem(int position)
    {
      return position < Count ? _drawerAnimatedItems[position] : (bool?) null;
    }

    public void SetAnimatedItem(int position, bool animated)
    {
      if (position < Count)
      {
        _drawerAnimatedItems[position] = animated;
      }
    }

    public override long GetItemId(int position)
    {
      return position;
    }

    public override IList<IDrawerItem> GetDrawerItems()
    {
      return _drawerItems;
    }

    public override sealed void SetDrawerItems(IList<IDrawerItem> drawerItems)
    {
      _drawerItems = drawerItems.ToList();

      if (drawerItems != null)
      {
        _drawerAnimatedItems.Clear();
        for (var i = 0; i < drawerItems.Count; i++)
        {
          _drawerAnimatedItems.Add(false);
        }
      }

      MapTypes();
    }

    public override HashSet<string> GetTypeMapper()
    {
      return _typeMapper;
    }

    public override void SetTypeMapper(HashSet<string> typeMapper)
    {
      _typeMapper = typeMapper;
    }

    public override void ResetAnimation()
    {
      for (var i = 0; i < _drawerAnimatedItems.Count; i++)
      {
        _drawerAnimatedItems[i] = false;
      }
    }

    public override View GetView(int position, View convertView, ViewGroup parent)
    {
      var item = GetItem(position) as IDrawerItem;

      var view = item.ConvertView(_inflater, convertView, parent);

      if (_animateDrawerItems)
      {
        var animatedItem = GetAnimatedItem(position);
        if (animatedItem == null || animatedItem.Value == false)
        {
          var animationSet = new AnimationSet(false) {Duration = 100};

          var scaleAnimation = new ScaleAnimation(1, 1, 0, 1);
          var alphaAnimation = new AlphaAnimation(0, 1);

          animationSet.AddAnimation(scaleAnimation);
          animationSet.AddAnimation(alphaAnimation);

          view.StartAnimation(animationSet);
          SetAnimatedItem(position, true);
        }
      }

      return view;
    }
  }
}