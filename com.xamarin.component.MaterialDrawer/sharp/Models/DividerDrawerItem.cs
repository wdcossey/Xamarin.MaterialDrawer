using Android.Views;
using com.xamarin.component.MaterialDrawer.Models.Interfaces;
using com.xamarin.component.MaterialDrawer.Utils;
using Java.Lang;

namespace com.xamarin.component.MaterialDrawer.Models
{
  public class DividerDrawerItem : Object, IDrawerItem
  {
    public DividerDrawerItem()
    {

    }


    public int GetIdentifier()
    {
      return -1;
    }


    public Object GetTag()
    {
      return null;
    }

    public bool IsEnabled()
    {
      return false;
    }

    public new string GetType()
    {
      return "DIVIDER_ITEM";
    }


    public int GetLayoutRes()
    {
      return Resource.Layout.material_drawer_item_divider;
    }


    public View ConvertView(LayoutInflater inflater, View convertView, ViewGroup parent)
    {
      ViewHolder viewHolder;
      if (convertView == null)
      {
        convertView = inflater.Inflate(GetLayoutRes(), parent, false);
        viewHolder = new ViewHolder(convertView);
        convertView.Tag = viewHolder;
      }
      else
      {
        viewHolder = (ViewHolder) convertView.Tag;
      }

      viewHolder.View.Clickable = false;
      viewHolder.View.Enabled = false;
      viewHolder.View.SetMinimumHeight(1);

      //set the color for the divider
      viewHolder.Divider.SetBackgroundColor(UIUtils.GetThemeColorFromAttrOrRes(parent.Context,
        Resource.Attribute.material_drawer_divider, Resource.Color.material_drawer_divider));

      return convertView;
    }

    private class ViewHolder: Object
    {
      private readonly View _view;
      private readonly View _divider;

      public ViewHolder(View view)
      {
        _view = view;
        _divider = view.FindViewById(Resource.Id.divider);
      }

      public View View
      {
        get { return _view; }
      }

      public View Divider
      {
        get { return _divider; }
      }
    }
  }
}