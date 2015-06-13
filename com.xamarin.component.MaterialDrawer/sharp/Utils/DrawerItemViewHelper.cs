using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.Views;
using Android.Widget;
using com.xamarin.component.MaterialDrawer.Models.Interfaces;
using Java.Lang;
using Java.Util;

namespace com.xamarin.component.MaterialDrawer.Utils
{
  public class DrawerItemViewHelper
  {

    private readonly Context _context;

    public DrawerItemViewHelper(Context context) 
    {
        _context = context;
    }

    private List<IDrawerItem> _drawerItems = new List<IDrawerItem>();

    public DrawerItemViewHelper WithDrawerItems(IEnumerable<IDrawerItem> drawerItems)
    {
      _drawerItems = drawerItems.ToList();
      //_drawerItems.AddRange(drawerItems);
      return this;
    }

    public DrawerItemViewHelper WithDrawerItems(IDrawerItem[] drawerItems)
    {
      _drawerItems.AddRange(drawerItems);
      return this;
    }

    private bool _divider = true;

    public DrawerItemViewHelper WithDivider(bool divider) 
    {
        this._divider = divider;
        return this;
    }

    private OnDrawerItemClickListener _onDrawerItemClickListener = null;

    public DrawerItemViewHelper WithOnDrawerItemClickListener(OnDrawerItemClickListener onDrawerItemClickListener)
    {
      _onDrawerItemClickListener = onDrawerItemClickListener;
      return this;
    }

    public View Build()
    {
      //create the container view
      var linearLayout = new LinearLayout(_context)
      {
        LayoutParameters = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent,
          ViewGroup.LayoutParams.WrapContent),
        Orientation = Orientation.Vertical
      };

      //create the divider
      if (_divider)
      {
        var divider = new LinearLayout(_context)
        {
          LayoutParameters =
            new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent),
          Orientation = Orientation.Vertical
        };
        divider.SetMinimumHeight((int) UIUtils.ConvertDpToPixel(1, _context));
        divider.SetBackgroundColor(UIUtils.GetThemeColorFromAttrOrRes(_context,
          Resource.Attribute.material_drawer_divider, Resource.Color.material_drawer_divider));
        linearLayout.AddView(divider);
      }

      //get the inflater
      var layoutInflater = LayoutInflater.From(_context);

      //add all drawer items
      foreach (var drawerItem in _drawerItems)
      {
        var view = drawerItem.ConvertView(layoutInflater, null, linearLayout);
        view.Tag = (Object)drawerItem;

        if (drawerItem.IsEnabled())
        {
          view.SetBackgroundResource(UIUtils.GetSelectableBackground(_context));
          //todo: wdcossey
          view.Click += (sender, args) =>
          {
            if (_onDrawerItemClickListener != null)
            {
              _onDrawerItemClickListener.OnItemClick((View)sender, (IDrawerItem)((View)sender).Tag);
            }
          };
          //view.setOnClickListener(new View.OnClickListener() {
          //    @Override
          //    public void onClick(View v) {
          //        if (mOnDrawerItemClickListener != null) {
          //            mOnDrawerItemClickListener.onItemClick(v, (IDrawerItem) v.getTag());
          //        }
          //    }
          //});
        }

        linearLayout.AddView(view);
      }

      return linearLayout;
    }


    public interface OnDrawerItemClickListener 
{
      void OnItemClick(View view, IDrawerItem drawerItem);
    }
}
}