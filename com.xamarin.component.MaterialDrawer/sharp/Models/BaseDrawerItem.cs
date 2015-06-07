using System;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;
using com.xamarin.component.MaterialDrawer.Models.Interfaces;
using Object = Java.Lang.Object;

namespace com.xamarin.component.MaterialDrawer.Models
{

  //public abstract class BaseDrawerItem : Java.Lang.Object, IDrawerItem
  //{
  //  public abstract int GetIdentifier();
  //  public abstract Object GetTag();
  //  public abstract bool IsEnabled();
  //  public abstract string GetType();
  //  public abstract int GetLayoutRes();
  //  public abstract View ConvertView(LayoutInflater inflater, View convertView, ViewGroup parent);
  //}


  /// <summary>
  /// <para>Created by wdcossey on 06.06.15.</para>
  /// <para>Original created by mikepenz on 03.02.15.</para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public abstract class BaseDrawerItem<T> : Java.Lang.Object, IDrawerItem, INameable<T>, IIconable<T>, ICheckable<T>, ITagable<T>,
    IIdentifyable<T>, ITypefaceable<T>
  {

    private int _identifier = -1;
    private Drawable _icon;
    private int _iconRes = -1;
    private IIcon _iicon;
    private Drawable _selectedIcon;
    private int _selectedIconRes = -1;
    private string _name;
    private int _nameRes = -1;
    private bool _enabled = true;
    private bool _checkable = true;
    private Object _tag;

    private bool _iconTinted = false;

    private Color _selectedColor = Color.Black;
    private int _selectedColorRes = -1;

    private Color _textColor = Color.Black;
    private int _textColorRes = -1;
    private Color _selectedTextColor = Color.Black;
    private int _selectedTextColorRes = -1;
    private Color _disabledTextColor = Color.Black;
    private int _disabledTextColorRes = -1;

    private Color _iconColor = Color.Black;
    private int _iconColorRes = -1;
    private Color _selectedIconColor = Color.Black;
    private int _selectedIconColorRes = -1;
    private Color _disabledIconColor = Color.Black;
    private int _disabledIconColorRes = -1;

    private Typeface _typeface = null;

    public T WithIdentifier(int identifier)
    {
      this._identifier = identifier;
      return (T)this;
    }

    public T WithIcon(Drawable icon)
    {
      this._icon = icon;
      return (T) this;
    }

    public T WithIcon(int iconRes)
    {
      this._iconRes = iconRes;
      return (T)this;
    }

    public T WithIcon(IIcon iicon)
    {
      this._iicon = iicon;
      return (T)this;
    }

    public T WithSelectedIcon(Drawable selectedIcon)
    {
      this._selectedIcon = selectedIcon;
      return (T) this;
    }

    public T WithSelectedIcon(int selectedIconRes)
    {
      this._selectedIconRes = selectedIconRes;
      return (T) this;
    }

    public T WithName(string name)
    {
      this._name = name;
      this._nameRes = -1;
      return (T) this;
    }

    public T WithName(int nameRes)
    {
      this._nameRes = nameRes;
      this._name = null;
      return (T) this;
    }

    public T WithTag(object @object) 
    {
        this._tag = @object;
        return (T) this;
    }

    public T WithCheckable(bool checkable)
    {
      this._checkable = checkable;
      return (T) this;
    }

    public T WithEnabled(bool enabled)
    {
      this._enabled = enabled;
      return (T) this;
    }

    public T SetEnabled(bool enabled)
    {
      this._enabled = enabled;
      return (T) this;
    }

    public T WithSelectedColor(Color selectedColor)
    {
      this._selectedColor = selectedColor;
      return (T) this;
    }

    public T WithSelectedColorRes(int selectedColorRes)
    {
      this._selectedColorRes = selectedColorRes;
      return (T) this;
    }

    public T WithTextColor(Color textColor)
    {
      this._textColor = textColor;
      return (T) this;
    }

    public T WithTextColorRes(int textColorRes)
    {
      this._textColorRes = textColorRes;
      return (T) this;
    }

    public T WithSelectedTextColor(Color selectedTextColor)
    {
      this._selectedTextColor = selectedTextColor;
      return (T) this;
    }

    public T WithSelectedTextColorRes(int selectedColorRes)
    {
      this._selectedTextColorRes = selectedColorRes;
      return (T) this;
    }

    public T WithDisabledTextColor(Color disabledTextColor)
    {
      this._disabledTextColor = disabledTextColor;
      return (T) this;
    }

    public T WithDisabledTextColorRes(int disabledTextColorRes)
    {
      this._disabledTextColorRes = disabledTextColorRes;
      return (T) this;
    }

    public T WithIconColor(Color iconColor)
    {
      this._iconColor = iconColor;
      return (T) this;
    }

    public T WithIconColorRes(int iconColorRes)
    {
      this._iconColorRes = iconColorRes;
      return (T) this;
    }

    public T WithSelectedIconColor(Color selectedIconColor)
    {
      this._selectedIconColor = selectedIconColor;
      return (T) this;
    }

    public T WithSelectedIconColorRes(int selectedColorRes)
    {
      this._selectedIconColorRes = selectedColorRes;
      return (T) this;
    }

    public T WithDisabledIconColor(Color disabledIconColor)
    {
      this._disabledIconColor = disabledIconColor;
      return (T) this;
    }

    public T WithDisabledIconColorRes(int disabledIconColorRes)
    {
      this._disabledIconColorRes = disabledIconColorRes;
      return (T) this;
    }

    /// <summary>
    /// will tint the icon with the default (or set) colors
    /// (default and selected state)
    /// </summary>
    /// <param name="iconTintingEnabled"></param>
    /// <returns></returns>
    public T WithIconTintingEnabled(bool iconTintingEnabled)
    {
      this._iconTinted = iconTintingEnabled;
      return (T) this;
    }

   [Obsolete]
    public T WithIconTinted(bool iconTinted)
    {
      this._iconTinted = iconTinted;
      return (T) this;
    }

    /// <summary>
    /// for backwards compatibility - withIconTinted..
    /// </summary>
    /// <param name="iconTinted"></param>
    /// <returns></returns>
    [Obsolete]
    public T WithTintSelectedIcon(bool iconTinted)
    {
      return WithIconTintingEnabled(iconTinted);
    }

    public T WithTypeface(Typeface typeface)
    {
      this._typeface = typeface;
      return (T) this;
    }

    public Color GetSelectedColor()
    {
      return _selectedColor;
    }

    public void SetSelectedColor(Color selectedColor)
    {
      this._selectedColor = selectedColor;
    }

    public int GetSelectedColorRes()
    {
      return _selectedColorRes;
    }

    public void SetSelectedColorRes(int selectedColorRes)
    {
      this._selectedColorRes = selectedColorRes;
    }

    public Color GetTextColor()
    {
      return _textColor;
    }

    public void SetTextColor(Color textColor)
    {
      this._textColor = textColor;
    }

    public int GetTextColorRes()
    {
      return _textColorRes;
    }

    public void SetTextColorRes(int textColorRes)
    {
      this._textColorRes = textColorRes;
    }

    public Color GetSelectedTextColor()
    {
      return _selectedTextColor;
    }

    public void SetSelectedTextColor(Color selectedTextColor)
    {
      this._selectedTextColor = selectedTextColor;
    }

    public int GetSelectedTextColorRes()
    {
      return _selectedTextColorRes;
    }

    public void SetSelectedTextColorRes(int selectedTextColorRes)
    {
      this._selectedTextColorRes = selectedTextColorRes;
    }

    public Color GetDisabledTextColor()
    {
      return _disabledTextColor;
    }

    public void SetDisabledTextColor(Color disabledTextColor)
    {
      this._disabledTextColor = disabledTextColor;
    }

    public int GetDisabledTextColorRes()
    {
      return _disabledTextColorRes;
    }

    public void SetDisabledTextColorRes(int disabledTextColorRes)
    {
      this._disabledTextColorRes = disabledTextColorRes;
    }

    public bool IsIconTinted()
    {
      return _iconTinted;
    }

    public void SetIconTinted(bool iconTinted)
    {
      this._iconTinted = iconTinted;
    }

    public T WithTag(Object tag)
    {
      _tag = tag;
      return (T)this;
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

    public int GetIconRes()
    {
      return _iconRes;
    }

    public void SetIconRes(int iconRes)
    {
      _iconRes = iconRes;
    }

    public int GetSelectedIconRes()
    {
      return _selectedIconRes;
    }

    public void SetSelectedIconRes(int selectedIconRes)
    {
      _selectedIconRes = selectedIconRes;
    }

    public IIcon GetIIcon()
    {
      return _iicon;
    }

    //private @Override 
    public void SetIIcon(IIcon iicon)
    {
      _iicon = iicon;
    }

    public Drawable GetSelectedIcon()
    {
      return _selectedIcon;
    }

    public void SetSelectedIcon(Drawable selectedIcon)
    {
      _selectedIcon = selectedIcon;
    }

    public string GetName()
    {
      return _name;
    }

    public void SetName(string name)
    {
      _name = name;
      _nameRes = -1;
    }

    public int GetNameRes()
    {
      return _nameRes;
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

    public void SetIdentifier(int identifier)
    {
      this._identifier = identifier;
    }

    //private @Override 
    public bool IsEnabled()
    {
      return _enabled;
    }

    public new abstract string GetType();
    public abstract int GetLayoutRes();
    public abstract View ConvertView(LayoutInflater inflater, View convertView, ViewGroup parent);

    //private @Override 
    public bool IsCheckable()
    {
      return _checkable;
    }

    //private @Override 
    public void SetCheckable(bool checkable)
    {
      _checkable = checkable;
    }

    public int GetDisabledIconColorRes()
    {
      return _disabledIconColorRes;
    }

    public void SetDisabledIconColorRes(int disabledIconColorRes)
    {
      _disabledIconColorRes = disabledIconColorRes;
    }

    public Color GetDisabledIconColor()
    {
      return _disabledIconColor;
    }

    public void SetDisabledIconColor(Color disabledIconColor)
    {
      _disabledIconColor = disabledIconColor;
    }

    public int GetSelectedIconColorRes()
    {
      return _selectedIconColorRes;
    }

    public void SetSelectedIconColorRes(int selectedIconColorRes)
    {
      _selectedIconColorRes = selectedIconColorRes;
    }

    public Color GetSelectedIconColor()
    {
      return _selectedIconColor;
    }

    public void SetSelectedIconColor(Color selectedIconColor)
    {
      _selectedIconColor = selectedIconColor;
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

    public Typeface GetTypeface()
    {
      return _typeface;
    }

    public void SetTypeface(Typeface typeface)
    {
      _typeface = typeface;
    }
  }
}