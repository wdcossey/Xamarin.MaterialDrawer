using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Net;
using Android.Views;
using Android.Widget;
using com.xamarin.AndroidIconics;
using com.xamarin.AndroidIconics.Typefaces;
using com.xamarin.component.MaterialDrawer.Models.Interfaces;
using com.xamarin.component.MaterialDrawer.Utils;
using Java.Lang;

namespace com.xamarin.component.MaterialDrawer.Models
{
  public class ProfileSettingDrawerItem : IDrawerItem, IProfile<ProfileSettingDrawerItem>,
    ITagable<ProfileSettingDrawerItem>, IIdentifyable<ProfileSettingDrawerItem>, ITypefaceable<ProfileSettingDrawerItem>
  {

    private int _identifier = -1;

    private bool _selectable = false;

    private Drawable _icon;
    private Bitmap _iconBitmap;
    private IIcon _iicon;
    private Uri _iconUri;

    private string _name;
    private string _email;

    private bool _enabled = true;
    private Object _tag;

    private bool _iconTinted = false;

    private Color _selectedColor = Color.Transparent;
    private int _selectedColorRes = -1;

    private Color _textColor = Color.Transparent;
    private int _textColorRes = -1;

    private Color _iconColor = Color.Transparent;
    private int _iconColorRes = -1;

    private Typeface _typeface = null;

    public ProfileSettingDrawerItem WithIdentifier(int identifier)
    {
      _identifier = identifier;
      return this;
    }

    public ProfileSettingDrawerItem WithIcon(Drawable icon)
    {
      _icon = icon;
      return this;
    }

    public ProfileSettingDrawerItem WithIcon(Bitmap icon)
    {
      _iconBitmap = icon;
      return this;
    }

    public ProfileSettingDrawerItem WithIcon(IIcon iicon)
    {
      _iicon = iicon;
      return this;
    }

    public ProfileSettingDrawerItem WithIcon(string url)
    {
      _iconUri = Uri.Parse(url);
      return this;
    }

    public ProfileSettingDrawerItem WithIcon(Uri uri)
    {
      _iconUri = uri;
      return this;
    }

    public ProfileSettingDrawerItem WithName(string name)
    {
      _name = name;
      return this;
    }

    public ProfileSettingDrawerItem WithDescription(string description)
    {
      _email = description;
      return this;
    }

    //NOTE we reuse the IProfile here to allow custom items within the AccountSwitcher. There is an alias method withDescription for this
    public ProfileSettingDrawerItem WithEmail(string email)
    {
      _email = email;
      return this;
    }

    public ProfileSettingDrawerItem WithTag(Object @object)
    {
      _tag = @object;
      return this;
    }

    public ProfileSettingDrawerItem SetEnabled(bool enabled)
    {
      _enabled = enabled;
      return this;
    }

    public ProfileSettingDrawerItem WithEnabled(bool enabled)
    {
      _enabled = enabled;
      return this;
    }

    public ProfileSettingDrawerItem WithSelectedColor(Color selectedColor)
    {
      _selectedColor = selectedColor;
      return this;
    }

    public ProfileSettingDrawerItem WithSelectedColorRes(int selectedColorRes)
    {
      _selectedColorRes = selectedColorRes;
      return this;
    }

    public ProfileSettingDrawerItem WithTextColor(Color textColor)
    {
      _textColor = textColor;
      return this;
    }

    public ProfileSettingDrawerItem WithTextColorRes(int textColorRes)
    {
      _textColorRes = textColorRes;
      return this;
    }

    public ProfileSettingDrawerItem WithIconColor(Color iconColor)
    {
      _iconColor = iconColor;
      return this;
    }

    public ProfileSettingDrawerItem WithIconColorRes(int iconColorRes)
    {
      _iconColorRes = iconColorRes;
      return this;
    }

    public ProfileSettingDrawerItem WithSelectable(bool selectable)
    {
      _selectable = selectable;
      return this;
    }

    public ProfileSettingDrawerItem WithTypeface(Typeface typeface)
    {
      _typeface = typeface;
      return this;
    }

    public ProfileSettingDrawerItem WithIconTinted(bool iconTinted)
    {
      _iconTinted = iconTinted;
      return this;
    }

    public Bitmap GetIconBitmap()
    {
      return _iconBitmap;
    }

    public void SetIconBitmap(Bitmap iconBitmap)
    {
      _iconBitmap = iconBitmap;
    }

    public Color GetSelectedColor()
    {
      return _selectedColor;
    }

    public void SetSelectedColor(Color selectedColor)
    {
      _selectedColor = selectedColor;
    }

    public int GetSelectedColorRes()
    {
      return _selectedColorRes;
    }

    public void SetSelectedColorRes(int selectedColorRes)
    {
      _selectedColorRes = selectedColorRes;
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

    public int GetIconColorRes()
    {
      return _iconColorRes;
    }

    public void SetIconColorRes(int iconColorRes)
    {
      _iconColorRes = iconColorRes;
    }

    public Color GetIconColor()
    {
      return _iconColor;
    }

    public void SetIconColor(Color iconColor)
    {
      _iconColor = iconColor;
    }


    public Object GetTag()
    {
      return _tag;
    }

    public void SetTag(Object tag)
    {
      _tag = tag;
    }

    public Drawable GetIcon()
    {
      return _icon;
    }

    public void SetIcon(Drawable icon)
    {
      _icon = icon;
    }

    public IIcon GetIIcon()
    {
      return _iicon;
    }

    public void SetIIcon(IIcon iicon)
    {
      _iicon = iicon;
    }

    public void SetIcon(Uri uri)
    {
      _iconUri = uri;
    }

    public void SetIcon(string url)
    {
      _iconUri = Uri.Parse(url);
    }

    public Uri GetIconUri()
    {
      return _iconUri;
    }

    public bool IsSelectable()
    {
      return _selectable;
    }

    public ProfileSettingDrawerItem SetSelectable(bool selectable)
    {
      _selectable = selectable;
      return this;
    }

    public bool IsIconTinted()
    {
      return _iconTinted;
    }

    public void SetIconTinted(bool iconTinted)
    {
      _iconTinted = iconTinted;
    }

    public Typeface GetTypeface()
    {
      return _typeface;
    }

    public void SetTypeface(Typeface typeface)
    {
      _typeface = typeface;
    }

    public string GetName()
    {
      return _name;
    }

    public void SetName(string name)
    {
      _name = name;
    }

    public string GetEmail()
    {
      return _email;
    }

    public void SetEmail(string email)
    {
      _email = email;
    }

    public string GetDescription()
    {
      return _email;
    }

    public void SetDescription(string description)
    {
      _email = _email;
    }

    public int GetIdentifier()
    {
      return _identifier;
    }

    public void SetIdentifier(int identifier)
    {
      _identifier = identifier;
    }

    public bool IsEnabled()
    {
      return _enabled;
    }

    public new string GetType()
    {
      return "PROFILE_SETTING_ITEM";
    }

    public int GetLayoutRes()
    {
      return Resource.Layout.material_drawer_item_profile_setting;
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

      //get the correct color for the background
      var selectedColor = UIUtils.DecideColor(ctx, GetSelectedColor(), GetSelectedColorRes(),
        Resource.Attribute.material_drawer_selected, Resource.Color.material_drawer_selected);
      //get the correct color for the text
      var color = UIUtils.DecideColor(ctx, GetTextColor(), GetTextColorRes(),
        Resource.Attribute.material_drawer_primary_text,
        Resource.Color.material_drawer_primary_text);
      var iconColor = UIUtils.DecideColor(ctx, GetIconColor(), GetIconColorRes(),
        Resource.Attribute.material_drawer_primary_icon,
        Resource.Color.material_drawer_primary_icon);

      UIUtils.SetBackground(viewHolder.View, UIUtils.GetDrawerItemBackground(selectedColor));

      viewHolder.Name.Text = GetName();
      viewHolder.Name.SetTextColor(color);

      if (GetTypeface() != null)
      {
        viewHolder.Name.Typeface = GetTypeface();
      }

      //get the correct icon
      if (GetIcon() != null)
      {
        if (_icon != null && IsIconTinted())
        {
          _icon.SetColorFilter(iconColor, PorterDuff.Mode.SrcIn);
        }
        viewHolder.Icon.SetImageDrawable(_icon);
        viewHolder.Icon.Visibility = ViewStates.Visible;
      }
      else if (GetIconBitmap() != null)
      {
        viewHolder.Icon.SetImageBitmap(_iconBitmap);
        viewHolder.Icon.Visibility = ViewStates.Visible;
      }
      else if (GetIIcon() != null)
      {
        viewHolder.Icon.SetImageDrawable(
          new IconicsDrawable(ctx, GetIIcon()).Color(iconColor).ActionBarSize().PaddingDp(2));
        viewHolder.Icon.Visibility = ViewStates.Visible;
      }
      else
      {
        viewHolder.Icon.Visibility = ViewStates.Gone;
      }

      return convertView;
    }

    private class ViewHolder : Object
    {
      private readonly View _view;
      private readonly ImageView _icon;
      private readonly TextView _name;

      public ViewHolder(View view)
      {
        _view = view;
        _icon = (ImageView) view.FindViewById(Resource.Id.icon);
        _name = (TextView) view.FindViewById(Resource.Id.name);
      }

      public View View
      {
        get { return _view; }
      }

      public ImageView Icon
      {
        get { return _icon; }
      }

      public TextView Name
      {
        get { return _name; }
      }
    }
  }
}