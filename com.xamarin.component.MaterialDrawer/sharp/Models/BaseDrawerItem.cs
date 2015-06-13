using System;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;
using com.xamarin.AndroidIconics.Typefaces;
using com.xamarin.component.MaterialDrawer.Models.Interfaces;
using Object = Java.Lang.Object;

namespace com.xamarin.component.MaterialDrawer.Models
{

  public abstract class BaseDrawerItem : Object, IDrawerItem, INameable, IIconable, ICheckable, ITagable,
    IIdentifyable, ITypefaceable
  {

    private int _identifier = -1;
    private int _iconRes = -1;
    private int _selectedIconRes = -1;
    private int _nameRes = -1;
    private bool _enabled = true;
    private bool _checkable = true;

    private Color _selectedColor = Color.Transparent;
    private int _selectedColorRes = -1;

    private Color _textColor = Color.Transparent;
    private int _textColorRes = -1;
    private Color _selectedTextColor = Color.Transparent;
    private int _selectedTextColorRes = -1;
    private Color _disabledTextColor = Color.Transparent;
    private int _disabledTextColorRes = -1;

    private Color _iconColor = Color.Transparent;
    private int _iconColorRes = -1;
    private Color _selectedIconColor = Color.Transparent;
    private int _selectedIconColorRes = -1;
    private Color _disabledIconColor = Color.Transparent;
    private int _disabledIconColorRes = -1;

    public BaseDrawerItem()
    {
      IconTinted = false;
      Typeface = null;
    }

    protected int Identifier
    {
      get { return _identifier; }
      set { _identifier = value; }
    }

    protected Drawable Icon { get; set; }

    protected int IconRes
    {
      get { return _iconRes; }
      set { _iconRes = value; }
    }

    protected IIcon IIcon { get; set; }

    protected Drawable SelectedIcon { get; set; }

    protected int SelectedIconRes
    {
      get { return _selectedIconRes; }
      set { _selectedIconRes = value; }
    }

    protected string Name { get; set; }

    protected int NameRes
    {
      get { return _nameRes; }
      set { _nameRes = value; }
    }

    protected Object Tag { get; set; }

    protected bool Checkable
    {
      get { return _checkable; }
      set { _checkable = value; }
    }

    protected bool Enabled
    {
      get { return _enabled; }
      set { _enabled = value; }
    }

    protected Color SelectedColor
    {
      get { return _selectedColor; }
      set { _selectedColor = value; }
    }

    protected int SelectedColorRes
    {
      get { return _selectedColorRes; }
      set { _selectedColorRes = value; }
    }

    protected Color TextColor
    {
      get { return _textColor; }
      set { _textColor = value; }
    }

    protected int TextColorRes
    {
      get { return _textColorRes; }
      set { _textColorRes = value; }
    }

    protected Color SelectedTextColor
    {
      get { return _selectedTextColor; }
      set { _selectedTextColor = value; }
    }

    protected int SelectedTextColorRes
    {
      get { return _selectedTextColorRes; }
      set { _selectedTextColorRes = value; }
    }

    protected Color DisabledTextColor
    {
      get { return _disabledTextColor; }
      set { _disabledTextColor = value; }
    }

    protected int DisabledTextColorRes
    {
      get { return _disabledTextColorRes; }
      set { _disabledTextColorRes = value; }
    }

    protected Color IconColor
    {
      get { return _iconColor; }
      set { _iconColor = value; }
    }

    protected int IconColorRes
    {
      get { return _iconColorRes; }
      set { _iconColorRes = value; }
    }

    protected Color SelectedIconColor
    {
      get { return _selectedIconColor; }
      set { _selectedIconColor = value; }
    }

    protected int SelectedIconColorRes
    {
      get { return _selectedIconColorRes; }
      set { _selectedIconColorRes = value; }
    }

    protected Color DisabledIconColor
    {
      get { return _disabledIconColor; }
      set { _disabledIconColor = value; }
    }

    protected int DisabledIconColorRes
    {
      get { return _disabledIconColorRes; }
      set { _disabledIconColorRes = value; }
    }

    protected bool IconTinted { get; set; }

    protected Typeface Typeface { get; set; }

    public Color GetSelectedColor()
    {
      return SelectedColor;
    }

    public void SetSelectedColor(Color selectedColor)
    {
      SelectedColor = selectedColor;
    }

    public int GetSelectedColorRes()
    {
      return SelectedColorRes;
    }

    public void SetSelectedColorRes(int selectedColorRes)
    {
      SelectedColorRes = selectedColorRes;
    }

    public Color GetTextColor()
    {
      return TextColor;
    }

    public void SetTextColor(Color textColor)
    {
      TextColor = textColor;
    }

    public int GetTextColorRes()
    {
      return TextColorRes;
    }

    public void SetTextColorRes(int textColorRes)
    {
      TextColorRes = textColorRes;
    }

    public Color GetSelectedTextColor()
    {
      return SelectedTextColor;
    }

    public void SetSelectedTextColor(Color selectedTextColor)
    {
      SelectedTextColor = selectedTextColor;
    }

    public int GetSelectedTextColorRes()
    {
      return SelectedTextColorRes;
    }

    public void SetSelectedTextColorRes(int selectedTextColorRes)
    {
      SelectedTextColorRes = selectedTextColorRes;
    }

    public Color GetDisabledTextColor()
    {
      return DisabledTextColor;
    }

    public void SetDisabledTextColor(Color disabledTextColor)
    {
      DisabledTextColor = disabledTextColor;
    }

    public int GetDisabledTextColorRes()
    {
      return DisabledTextColorRes;
    }

    public void SetDisabledTextColorRes(int disabledTextColorRes)
    {
      DisabledTextColorRes = disabledTextColorRes;
    }

    public bool IsIconTinted()
    {
      return IconTinted;
    }

    public void SetIconTinted(bool iconTinted)
    {
      IconTinted = iconTinted;
    }

    public Object GetTag()
    {
      return Tag;
    }

    public void SetTag(Object tag)
    {
      Tag = tag;
    }

    public Drawable GetIcon()
    {
      return Icon;
    }

    public void SetIcon(Drawable icon)
    {
      Icon = icon;
    }

    public int GetIconRes()
    {
      return IconRes;
    }

    public void SetIconRes(int iconRes)
    {
      IconRes = iconRes;
    }

    public int GetSelectedIconRes()
    {
      return SelectedIconRes;
    }

    public void SetSelectedIconRes(int selectedIconRes)
    {
      SelectedIconRes = selectedIconRes;
    }

    public IIcon GetIIcon()
    {
      return IIcon;
    }

    //private @Override 
    public void SetIIcon(IIcon iicon)
    {
      IIcon = iicon;
    }

    public Drawable GetSelectedIcon()
    {
      return SelectedIcon;
    }

    public void SetSelectedIcon(Drawable selectedIcon)
    {
      SelectedIcon = selectedIcon;
    }

    public string GetName()
    {
      return Name;
    }

    public void SetName(string name)
    {
      Name = name;
      NameRes = -1;
    }

    public int GetNameRes()
    {
      return NameRes;
    }

    public void SetNameRes(int nameRes)
    {
      NameRes = nameRes;
      Name = null;
    }

    public int GetIdentifier()
    {
      return Identifier;
    }

    public void SetIdentifier(int identifier)
    {
      Identifier = identifier;
    }

    //private @Override 
    public bool IsEnabled()
    {
      return Enabled;
    }

    public new abstract string GetType();

    public abstract int GetLayoutRes();

    public abstract View ConvertView(LayoutInflater inflater, View convertView, ViewGroup parent);

    //private @Override 
    public bool IsCheckable()
    {
      return Checkable;
    }

    //private @Override 
    public void SetCheckable(bool checkable)
    {
      Checkable = checkable;
    }

    public int GetDisabledIconColorRes()
    {
      return DisabledIconColorRes;
    }

    public void SetDisabledIconColorRes(int disabledIconColorRes)
    {
      DisabledIconColorRes = disabledIconColorRes;
    }
    
    public Color GetDisabledIconColor()
    {
      return DisabledIconColor;
    }

    public void SetDisabledIconColor(Color disabledIconColor)
    {
      DisabledIconColor = disabledIconColor;
    }

    public int GetSelectedIconColorRes()
    {
      return SelectedIconColorRes;
    }

    public void SetSelectedIconColorRes(int selectedIconColorRes)
    {
      SelectedIconColorRes = selectedIconColorRes;
    }

    public Color GetSelectedIconColor()
    {
      return SelectedIconColor;
    }

    public void SetSelectedIconColor(Color selectedIconColor)
    {
      SelectedIconColor = selectedIconColor;
    }

    public int GetIconColorRes()
    {
      return IconColorRes;
    }

    public void SetIconColorRes(int iconColorRes)
    {
      IconColorRes = iconColorRes;
    }

    public Color GetIconColor()
    {
      return IconColor;
    }

    public void SetIconColor(Color iconColor)
    {
      IconColor = iconColor;
    }

    public Typeface GetTypeface()
    {
      return Typeface;
    }

    public void SetTypeface(Typeface typeface)
    {
      Typeface = typeface;
    }
  }


  /// <summary>
  /// <para>Created by wdcossey on 06.06.15.</para>
  /// <para>Original created by mikepenz on 03.02.15.</para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public abstract class BaseDrawerItem<T> : BaseDrawerItem, INameable<T>, IIconable<T>, ICheckable<T>, ITagable<T>,
    IIdentifyable<T>, ITypefaceable<T>
    where T : class 
  {

    public T WithIdentifier(int identifier)
    {
      Identifier = identifier;
      return this as T;
    }

    public T WithIcon(Drawable icon)
    {
      Icon = icon;
      return this as T;
    }

    public T WithIcon(int iconRes)
    {
      IconRes = iconRes;
      return this as T;
    }

    public T WithIcon(IIcon iicon)
    {
      IIcon = iicon;
      return this as T;
    }

    public T WithSelectedIcon(Drawable selectedIcon)
    {
      SelectedIcon = selectedIcon;
      return this as T;
    }

    public T WithSelectedIcon(int selectedIconRes)
    {
      SelectedIconRes = selectedIconRes;
      return this as T;
    }

    public T WithName(string name)
    {
      Name = name;
      NameRes = -1;
      return this as T;
    }

    public T WithName(int nameRes)
    {
      NameRes = nameRes;
      Name = null;
      return this as T;
    }

    public T WithTag(Object @object) 
    {
        Tag = @object;
        return this as T;
    }

    public T WithCheckable(bool checkable)
    {
      Checkable = checkable;
      return this as T;
    }

    public T WithEnabled(bool enabled)
    {
      Enabled = enabled;
      return this as T;
    }

    public T SetEnabled(bool enabled)
    {
      Enabled = enabled;
      return this as T;
    }

    public T WithSelectedColor(Color selectedColor)
    {
      SelectedColor = selectedColor;
      return this as T;
    }

    public T WithSelectedColorRes(int selectedColorRes)
    {
      SelectedColorRes = selectedColorRes;
      return this as T;
    }

    public T WithTextColor(Color textColor)
    {
      TextColor = textColor;
      return this as T;
    }

    public T WithTextColorRes(int textColorRes)
    {
      TextColorRes = textColorRes;
      return this as T;
    }

    public T WithSelectedTextColor(Color selectedTextColor)
    {
      SelectedTextColor = selectedTextColor;
      return this as T;
    }

    public T WithSelectedTextColorRes(int selectedColorRes)
    {
      SelectedTextColorRes = selectedColorRes;
      return this as T;
    }

    public T WithDisabledTextColor(Color disabledTextColor)
    {
      DisabledTextColor = disabledTextColor;
      return this as T;
    }

    public T WithDisabledTextColorRes(int disabledTextColorRes)
    {
      DisabledTextColorRes = disabledTextColorRes;
      return this as T;
    }

    public T WithIconColor(Color iconColor)
    {
      IconColor = iconColor;
      return this as T;
    }

    public T WithIconColorRes(int iconColorRes)
    {
      IconColorRes = iconColorRes;
      return this as T;
    }

    public T WithSelectedIconColor(Color selectedIconColor)
    {
      SelectedIconColor = selectedIconColor;
      return this as T;
    }

    public T WithSelectedIconColorRes(int selectedColorRes)
    {
      SelectedIconColorRes = selectedColorRes;
      return this as T;
    }

    public T WithDisabledIconColor(Color disabledIconColor)
    {
      DisabledIconColor = disabledIconColor;
      return this as T;
    }

    public T WithDisabledIconColorRes(int disabledIconColorRes)
    {
      DisabledIconColorRes = disabledIconColorRes;
      return this as T;
    }

    /// <summary>
    /// will tint the icon with the default (or set) colors
    /// (default and selected state)
    /// </summary>
    /// <param name="iconTintingEnabled"></param>
    /// <returns></returns>
    public T WithIconTintingEnabled(bool iconTintingEnabled)
    {
      IconTinted = iconTintingEnabled;
      return this as T;
    }

   [Obsolete]
    public T WithIconTinted(bool iconTinted)
    {
      IconTinted = iconTinted;
      return this as T;
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
      Typeface = typeface;
      return this as T;
    }
  }
}