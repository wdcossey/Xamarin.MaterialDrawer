using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using com.xamarin.component.MaterialDrawer.Models.Interfaces;
using com.xamarin.component.MaterialDrawer.Utils;
using Java.Lang;

namespace com.xamarin.component.MaterialDrawer.Models
{
  public class SectionDrawerItem : IDrawerItem, INameable<SectionDrawerItem>, ITagable<SectionDrawerItem>,
    ITypefaceable<SectionDrawerItem>
  {
    private int _identifier = -1;

    private string _name;
    private int _nameRes = -1;
    private bool _divider = true;
    private Object _tag;

    private Color _textColor = Color.Black;
    private int _textColorRes = -1;

    private Typeface _typeface;

    public SectionDrawerItem WithIdentifier(int identifier)
    {
      _identifier = identifier;
      return this;
    }

    public SectionDrawerItem WithName(string name)
    {
      _name = name;
      _nameRes = -1;
      return this;
    }

    public SectionDrawerItem WithName(int nameRes)
    {
      _nameRes = nameRes;
      _name = null;
      return this;
    }

    public SectionDrawerItem WithTag(Object @object)
    {
      _tag = @object;
      return this;
    }

    public SectionDrawerItem SetDivider(bool divider)
    {
      _divider = divider;
      return this;
    }

    public SectionDrawerItem WithTextColor(Color textColor)
    {
      _textColor = textColor;
      return this;
    }

    public SectionDrawerItem WithTextColorRes(int textColorRes)
    {
      _textColorRes = textColorRes;
      return this;
    }

    public SectionDrawerItem WithTypeface(Typeface typeface)
    {
      _typeface = typeface;
      return this;
    }

    public Object GetTag()
    {
      return _tag;
    }

    public void SetTag(Object tag)
    {
      _tag = tag;
    }

    public bool HasDivider()
    {
      return _divider;
    }

    public string GetName()
    {
      return _name;
    }

    public int GetNameRes()
    {
      return _nameRes;
    }

    public void SetName(string name)
    {
      _name = name;
      _nameRes = -1;
    }


    public void SetNameRes(int nameRes)
    {
      _nameRes = nameRes;
      _name = null;
    }

    public int GetIdentifier()
    {
      return _identifier;
    }

    public bool IsEnabled()
    {
      return false;
    }

    public new string GetType()
    {
      return "SECTION_ITEM";
    }

    public int GetLayoutRes()
    {
      return Resource.Layout.material_drawer_item_section;
    }

    public Color GetTextColor()
    {
      return _textColor;
    }

    public void SetTextColor(Color textColor)
    {
      _textColor = textColor;
    }

    public int GetTextColorRes()
    {
      return _textColorRes;
    }

    public void SetTextColorRes(int textColorRes)
    {
      _textColorRes = textColorRes;
    }

    public Typeface GetTypeface()
    {
      return _typeface;
    }

    public void SetTypeface(Typeface typeface)
    {
      _typeface = typeface;
    }

    public View ConvertView(LayoutInflater inflater, View convertView, ViewGroup parent)
    {
      Context ctx = parent.Context;

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

      _textColor = UIUtils.DecideColor(ctx, GetTextColor(), GetTextColorRes(),
        Resource.Attribute.material_drawer_secondary_text, Resource.Color.material_drawer_secondary_text);
      viewHolder.Name.SetTextColor(_textColor);

      if (GetNameRes() != -1)
      {
        viewHolder.Name.SetText(GetNameRes());
      }
      else
      {
        viewHolder.Name.Text = GetName();
      }

      if (HasDivider())
      {
        viewHolder.Divider.Visibility = ViewStates.Visible;
      }
      else
      {
        viewHolder.Divider.Visibility = ViewStates.Gone;
      }
      //set the color for the divider
      viewHolder.Divider.SetBackgroundColor(UIUtils.GetThemeColorFromAttrOrRes(parent.Context,
        Resource.Attribute.material_drawer_divider, Resource.Color.material_drawer_divider));

      return convertView;
    }

    private class ViewHolder : Object
    {
      private readonly View _view;
      private readonly View _divider;
      private readonly TextView _name;

      public ViewHolder(View view)
      {
        _view = view;
        _divider = view.FindViewById(Resource.Id.divider);
        _name = (TextView) view.FindViewById(Resource.Id.name);
      }

      public View View
      {
        get { return _view; }
      }

      public View Divider
      {
        get { return _divider; }
      }

      public TextView Name
      {
        get { return _name; }
      }
    }
  }
}