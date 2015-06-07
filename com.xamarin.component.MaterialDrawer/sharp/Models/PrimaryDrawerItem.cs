using System;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using com.xamarin.component.MaterialDrawer.Models.Interfaces;
using com.xamarin.component.MaterialDrawer.Utils;

namespace com.xamarin.component.MaterialDrawer.Models
{
  public class PrimaryDrawerItem: BaseDrawerItem<PrimaryDrawerItem>, IColorfulBadgeable<PrimaryDrawerItem> 
  {
    private string _description;
    private int _descriptionRes = -1;

    private string _badge;
    private Color _badgeTextColor = Color.Black;

    public PrimaryDrawerItem WithDescription(string description) 
    {
        _description = description;
        return this;
    }

    public PrimaryDrawerItem WithDescription(int descriptionRes) 
    {
        _descriptionRes = descriptionRes;
        return this;
    }

    public PrimaryDrawerItem WithBadge(string badge) 
    {
        _badge = badge;
        return this;
    }


    public PrimaryDrawerItem WithBadgeTextColor(Color color) 
    {
        _badgeTextColor = color;
        return this;
    }

    public string GetDescription() 
    {
        return _description;
    }
    
    public void SetDescription(string description) 
    {
        _description = description;
    }

    public int GetDescriptionRes() 
    {
        return _descriptionRes;
    }

    public void SetDescriptionRes(int descriptionRes) 
    {
        _descriptionRes = descriptionRes;
    }

    public string GetBadge() 
    {
        return _badge;
    }


    public Color GetBadgeTextColor() 
    {
        return _badgeTextColor;
    }

    public void SetBadgeTextColor(Color color) 
    {
        _badgeTextColor = color;
    }

    public void SetBadge(string badge) 
    {
        _badge = badge;
    }

    public override string GetType() 
    {
        return "PRIMARY_ITEM";
    }

    public override int GetLayoutRes() 
    {
        return Resource.Layout.material_drawer_item_primary;
    }


    public override View ConvertView(LayoutInflater inflater, View convertView, ViewGroup parent) 
    {
        var ctx = parent.Context;

        //get the viewHolder
        ViewHolder viewHolder;
        if (convertView == null) 
        {
            convertView = inflater.Inflate(GetLayoutRes(), parent, false);
            viewHolder = new ViewHolder(convertView);
            convertView.Tag = viewHolder;
        } else 
        {
            viewHolder = (ViewHolder) convertView.Tag;
        }

        //get the correct color for the background
        Color selectedColor = UIUtils.DecideColor(ctx, GetSelectedColor(), GetSelectedColorRes(), Resource.Attribute.material_drawer_selected, Resource.Color.material_drawer_selected);
        //get the correct color for the text
        int color;
        if (IsEnabled()) 
        {
          color = UIUtils.DecideColor(ctx, GetTextColor(), GetTextColorRes(), Resource.Attribute.material_drawer_primary_text, Resource.Color.material_drawer_primary_text);
        } 
        else 
        {
          color = UIUtils.DecideColor(ctx, GetDisabledTextColor(), GetDisabledTextColorRes(), Resource.Attribute.material_drawer_hint_text, Resource.Color.material_drawer_hint_text);
        }
        int selectedTextColor = UIUtils.DecideColor(ctx, GetSelectedTextColor(), GetSelectedTextColorRes(), Resource.Attribute.material_drawer_selected_text, Resource.Color.material_drawer_selected_text);
        //get the correct color for the icon
        Color iconColor;
        if (IsEnabled()) 
        {
          iconColor = UIUtils.DecideColor(ctx, GetIconColor(), GetIconColorRes(), Resource.Attribute.material_drawer_primary_icon, Resource.Color.material_drawer_primary_icon);
        } 
        else 
        {
          iconColor = UIUtils.DecideColor(ctx, GetDisabledIconColor(), GetDisabledIconColorRes(), Resource.Attribute.material_drawer_hint_text, Resource.Color.material_drawer_hint_text);
        }
        var selectedIconColor = UIUtils.DecideColor(ctx, GetSelectedIconColor(), GetSelectedIconColorRes(), Resource.Attribute.material_drawer_selected_text, Resource.Color.material_drawer_selected_text);

        //set the background for the item
        UIUtils.SetBackground(viewHolder.View, UIUtils.GetDrawerItemBackground(selectedColor));

        //set the text for the name
        if (GetNameRes() != -1) 
        {
            viewHolder.Name.SetText(GetNameRes());
        } else 
        {
          viewHolder.Name.Text = GetName();
        }

        //set the text for the description or hide
        viewHolder.Description.Visibility = ViewStates.Visible;

        if (GetDescriptionRes() != -1) 
        {
            viewHolder.Description.SetText(GetDescriptionRes());
        } 
        else if (GetDescription() != null) 
        {
            viewHolder.Description.Text = GetDescription();
        } else 
        {
            viewHolder.Description.Visibility = ViewStates.Gone;
        }

        //set the text for the badge or hide
        if (GetBadge() != null) 
        {
            viewHolder.Badge.Text = GetBadge();
            viewHolder.Badge.Visibility = ViewStates.Visible;
        } 
        else 
        {
            viewHolder.Badge.Visibility = ViewStates.Gone;
        }

        //set the colors for textViews
        viewHolder.Name.SetTextColor(UIUtils.GetTextColorStateList(color, selectedTextColor));
        viewHolder.Description.SetTextColor(UIUtils.GetTextColorStateList(color, selectedTextColor));
        if (_badgeTextColor != 0) 
        {
            viewHolder.Badge.SetTextColor(_badgeTextColor);
        } else {
            viewHolder.Badge.SetTextColor(UIUtils.GetTextColorStateList(color, selectedTextColor));
        }

        //define the typeface for our textViews
        if (GetTypeface() != null) 
        {
            viewHolder.Name.Typeface = GetTypeface();
            viewHolder.Description.Typeface = GetTypeface();
            viewHolder.Badge.Typeface = GetTypeface();
        }

        //get the drawables for our icon
        var icon = UIUtils.DecideIcon(ctx, base.GetIcon(), base.GetIIcon(), GetIconRes(), iconColor, IsIconTinted());
        var selectedIcon = UIUtils.DecideIcon(ctx, GetSelectedIcon(), base.GetIIcon(), GetSelectedIconRes(), selectedIconColor, IsIconTinted());

        //if we have an icon then we want to set it
        if (icon != null) {
            //if we got a different color for the selectedIcon we need a StateList
            if (selectedIcon != null) {
                viewHolder.Icon.SetImageDrawable(UIUtils.GetIconStateList(icon, selectedIcon));
            } else if (IsIconTinted()) {
                viewHolder.Icon.SetImageDrawable(new PressedEffectStateListDrawable(icon, iconColor, selectedIconColor));
            } else {
                viewHolder.Icon.SetImageDrawable(icon);
            }
            //make sure we display the icon
            viewHolder.Icon.Visibility = ViewStates.Visible;
        } else {
            //hide the icon
            viewHolder.Icon.Visibility = ViewStates.Gone;
        }

        return convertView;
    }

    private class ViewHolder : Java.Lang.Object
    {
      private readonly View _view;
      private readonly ImageView _icon;
      private readonly TextView _name;
      private readonly TextView _description;
      private readonly TextView _badge;

      public ViewHolder(View view)
      {
        _view = view;
        _icon = (ImageView) view.FindViewById(Resource.Id.icon);
        _name = (TextView) view.FindViewById(Resource.Id.name);
        _description = (TextView) view.FindViewById(Resource.Id.description);
        _badge = (TextView) view.FindViewById(Resource.Id.badge);
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

      public TextView Description
      {
        get { return _description; }
      }

      public TextView Badge
      {
        get { return _badge; }
      }
    }
  }
}