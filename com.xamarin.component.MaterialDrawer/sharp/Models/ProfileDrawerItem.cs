using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Net;
using Android.Views;
using Android.Widget;
using com.xamarin.component.MaterialDrawer.Models.Interfaces;
using com.xamarin.component.MaterialDrawer.Utils;
using Java.Lang;

namespace com.xamarin.component.MaterialDrawer.Models
{
  public class ProfileDrawerItem : Object, IDrawerItem, IProfile<ProfileDrawerItem>, ITagable<ProfileDrawerItem>, IIdentifyable<ProfileDrawerItem>, ITypefaceable<ProfileDrawerItem> 
  {

    private int _identifier = -1;

    private bool _selectable = true;
    private bool _nameShown;

    private Drawable _icon;
    private Bitmap _iconBitmap;
    private Uri _iconUri;

    private string _name;
    private string _email;

    private bool _enabled = true;
    private Object _tag;

    private Color _selectedColor = Color.Transparent;
    private int _selectedColorRes = -1;

    private Color _textColor = Color.Transparent;
    private int _textColorRes = -1;

    private Typeface _typeface;

    private void ResetIcons() {
        _icon = null;
        _iconBitmap = null;
        _iconUri = null;
    }

    public ProfileDrawerItem WithIdentifier(int identifier)
    {
      _identifier = identifier;
      return this;
    }

    public ProfileDrawerItem WithIcon(Drawable icon)
    {
      ResetIcons();
      _icon = icon;
      return this;
    }

    public ProfileDrawerItem WithIcon(Bitmap iconBitmap)
    {
      ResetIcons();
      _iconBitmap = iconBitmap;
      return this;
    }

    public ProfileDrawerItem WithIcon(string url)
    {
      ResetIcons();
      _iconUri = Uri.Parse(url);
      return this;
    }

    public ProfileDrawerItem WithIcon(Uri uri)
    {
      ResetIcons();
      _iconUri = uri;
      return this;
    }

    public ProfileDrawerItem WithName(string name) 
    {
        _name = name;
        return this;
    }

    public ProfileDrawerItem WithEmail(string email)
    {
      _email = email;
      return this;
    }

    public ProfileDrawerItem WithTag(Object @object)
    {
      _tag = @object;
      return this;
    }

    public ProfileDrawerItem SetEnabled(bool enabled)
    {
      _enabled = enabled;
      return this;
    }

    public ProfileDrawerItem WithNameShown(bool nameShown)
    {
      _nameShown = nameShown;
      return this;
    }

    public ProfileDrawerItem WithSelectedColor(Color selectedColor)
    {
      _selectedColor = selectedColor;
      return this;
    }

    public ProfileDrawerItem WithSelectedColorRes(int selectedColorRes)
    {
      _selectedColorRes = selectedColorRes;
      return this;
    }

    public ProfileDrawerItem WithTextColor(Color textColor)
    {
      _textColor = textColor;
      return this;
    }

    public ProfileDrawerItem WithTextColorRes(int textColorRes)
    {
      _textColorRes = textColorRes;
      return this;
    }

    public ProfileDrawerItem WithSelectable(bool selectable)
    {
      _selectable = selectable;
      return this;
    }

    public ProfileDrawerItem WithTypeface(Typeface typeface)
    {
      _typeface = typeface;
      return this;
    }

    public bool IsNameShown()
    {
      return _nameShown;
    }

    public void SetNameShown(bool nameShown) 
    {
        _nameShown = nameShown;
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

    public Typeface GetTypeface() 
    {
        return _typeface;
    }

    public void SetTypeface(Typeface typeface)
    {
      _typeface = typeface;
    }

    public Object GetTag() 
    {
        return _tag;
    }

    public void SetTag(Object tag)
    {
      _tag = tag;
    }

    public Uri GetIconUri()
    {
      return _iconUri;
    }

    public Drawable GetIcon() 
    {
        return _icon;
    }

    public Bitmap GetIconBitmap() 
    {
        return _iconBitmap;
    }

    public void SetIconBitmap(Bitmap iconBitmap)
    {
      ResetIcons();
      _iconBitmap = iconBitmap;
    }

    public void SetIcon(Uri uri) 
    {
        ResetIcons();
        _iconUri = uri;
    }

    public void SetIcon(string url) 
    {
        ResetIcons();
        _iconUri = Uri.Parse(url);
    }

    public void SetIcon(Drawable icon) 
    {
        ResetIcons();
        _icon = icon;
    }

    public bool IsSelectable()
    {
      return _selectable;
    }

    public ProfileDrawerItem SetSelectable(bool selectable)
    {
      _selectable = selectable;
      return this;
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
      return "PROFILE_ITEM";
    }

    public int GetLayoutRes()
    {
      return Resource.Layout.material_drawer_item_profile;
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
        Resource.Attribute.material_drawer_primary_text, Resource.Color.material_drawer_primary_text);

      UIUtils.SetBackground(viewHolder.View, UIUtils.GetDrawerItemBackground(selectedColor));

      if (_nameShown)
      {
        viewHolder.Name.Visibility = ViewStates.Invisible;
        viewHolder.Name.Text = GetName();
      }
      else
      {
        viewHolder.Name.Visibility = ViewStates.Gone;
      }
      //the MaterialDrawer follows the Google Apps. those only show the e-mail
      //within the profile switcher. The problem this causes some confusion for
      //some developers. And if you only set the name, the item would be empty
      //so here's a small fallback which will prevent this issue of empty items ;)
      if (!_nameShown && GetEmail() == null && GetName() != null)
      {
        viewHolder.Email.Text = GetName();
      }
      else
      {
        viewHolder.Email.Text = GetEmail();
      }

      if (GetTypeface() != null)
      {
        viewHolder.Name.Typeface = GetTypeface();
        viewHolder.Email.Typeface = GetTypeface();
      }

      if (_nameShown)
      {
        viewHolder.Name.SetTextColor(color);
      }
      viewHolder.Email.SetTextColor(color);

      viewHolder.ProfileIcon.Visibility = ViewStates.Visible;
      if (GetIconUri() != null)
      {
        viewHolder.ProfileIcon.SetImageDrawable(UIUtils.GetPlaceHolder(ctx));
        viewHolder.ProfileIcon.SetImageURI(_iconUri);
      }
      else if (GetIcon() != null)
      {
        viewHolder.ProfileIcon.SetImageDrawable(GetIcon());
      }
      else if (GetIconBitmap() != null)
      {
        viewHolder.ProfileIcon.SetImageBitmap(GetIconBitmap());
      }
      else
      {
        viewHolder.ProfileIcon.Visibility = ViewStates.Invisible;
      }

      return convertView;
    }

    private class ViewHolder : Object
    {
      private readonly View _view;
      private readonly ImageView _profileIcon;
      private readonly TextView _name;
      private readonly TextView _email;


      public ViewHolder(View view)
      {
        _view = view;
        _profileIcon = (ImageView) view.FindViewById(Resource.Id.profileIcon);
        _name = (TextView) view.FindViewById(Resource.Id.name);
        _email = (TextView) view.FindViewById(Resource.Id.email);
      }

      public View View
      {
        get { return _view; }
      }

      public ImageView ProfileIcon
      {
        get { return _profileIcon; }
      }

      public TextView Name
      {
        get { return _name; }
      }

      public TextView Email
      {
        get { return _email; }
      }
    }
  }

}