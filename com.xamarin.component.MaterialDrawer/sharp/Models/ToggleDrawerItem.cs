using System;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;
using com.xamarin.component.MaterialDrawer.Utils;
using Object = Java.Lang.Object;

namespace com.xamarin.component.MaterialDrawer.Models
{
  public class ToggleDrawerItem : BaseDrawerItem<ToggleDrawerItem> 
  {
    private string _description;
    private int _descriptionRes = -1;

    private bool _toggleEnabled = true;

    private bool _checkable = false;
    private bool _checked = false;
    private CompoundButton.IOnCheckedChangeListener _onCheckedChangeListener = null;

    public ToggleDrawerItem WithDescription(string description) {
        _description = description;
        return this;
    }

    public ToggleDrawerItem WithDescription(int descriptionRes) {
        _descriptionRes = descriptionRes;
        return this;
    }

    public ToggleDrawerItem WithChecked(bool @checked) {
        _checked = @checked;
        return this;
    }

    public ToggleDrawerItem WithToggleEnabled(bool toggleEnabled) {
        _toggleEnabled = toggleEnabled;
        return this;
    }

    public ToggleDrawerItem WithOnCheckedChangeListener(CompoundButton.IOnCheckedChangeListener onCheckedChangeListener) {
        _onCheckedChangeListener = onCheckedChangeListener;
        return this;
    }

    public ToggleDrawerItem WithCheckable(bool checkable) {
        _checkable = checkable;
        return this;
    }

    public string GetDescription() 
    {
        return _description;
    }

    public void SetDescription(string description) {
        _description = description;
    }

    public int GetDescriptionRes() {
        return _descriptionRes;
    }

    public void SetDescriptionRes(int descriptionRes) {
        _descriptionRes = descriptionRes;
    }

    public bool IsChecked() {
        return _checked;
    }

    public void SetChecked(bool @checked) {
        _checked = @checked;
    }

    public bool IsToggleEnabled() {
        return _toggleEnabled;
    }

    public void SetToggleEnabled(bool toggleEnabled) {
        _toggleEnabled = toggleEnabled;
    }

    public CompoundButton.IOnCheckedChangeListener GetOnCheckedChangeListener() {
        return _onCheckedChangeListener;
    }

    public void SetOnCheckedChangeListener(CompoundButton.IOnCheckedChangeListener onCheckedChangeListener) {
        _onCheckedChangeListener = onCheckedChangeListener;
    }

    public bool IsCheckable() {
        return _checkable;
    }

    public void SetCheckable(bool checkable) {
        _checkable = checkable;
    }

    public override string GetType() {
        return "TOGGLE_ITEM";
    }

    public override int GetLayoutRes() {
        return Resource.Layout.material_drawer_item_toggle;
    }

    public override View ConvertView(LayoutInflater inflater, View convertView, ViewGroup parent)
    {
      var ctx = parent.Context;

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
      int color;
      if (IsEnabled())
      {
        color = UIUtils.DecideColor(ctx, GetTextColor(), GetTextColorRes(),
          Resource.Attribute.material_drawer_primary_text, Resource.Color.material_drawer_primary_text);
      }
      else
      {
        color = UIUtils.DecideColor(ctx, GetDisabledTextColor(), GetDisabledTextColorRes(),
          Resource.Attribute.material_drawer_hint_text, Resource.Color.material_drawer_hint_text);
      }
      var selectedTextColor = UIUtils.DecideColor(ctx, GetSelectedTextColor(), GetSelectedTextColorRes(),
        Resource.Attribute.material_drawer_selected_text, Resource.Color.material_drawer_selected_text);
      //get the correct color for the icon
      Color iconColor;
      if (IsEnabled())
      {
        iconColor = UIUtils.DecideColor(ctx, GetIconColor(), GetIconColorRes(),
          Resource.Attribute.material_drawer_primary_icon, Resource.Color.material_drawer_primary_icon);
      }
      else
      {
        iconColor = UIUtils.DecideColor(ctx, GetDisabledIconColor(), GetDisabledIconColorRes(),
          Resource.Attribute.material_drawer_hint_text, Resource.Color.material_drawer_hint_text);
      }
      var selectedIconColor = UIUtils.DecideColor(ctx, GetSelectedIconColor(), GetSelectedIconColorRes(),
        Resource.Attribute.material_drawer_selected_text, Resource.Color.material_drawer_selected_text);

      //set the background for the item
      UIUtils.SetBackground(viewHolder.View, UIUtils.GetDrawerItemBackground(selectedColor));

      //set the text for the name
      if (GetNameRes() != -1)
      {
        viewHolder.Name.SetText(GetNameRes());
      }
      else
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
      }
      else
      {
        viewHolder.Description.Visibility = ViewStates.Gone;
      }


      if (!IsCheckable())
      {
        viewHolder.View.Click += delegate(object sender, EventArgs args)
        {
          if (_toggleEnabled) {
              viewHolder.Toggle.Checked = !viewHolder.Toggle.Checked;
          }
        };
        //todo: wdcossey ^^ check above!!!
        //viewHolder.view.setOnClickListener(new View.OnClickListener() {
        //    @Override
        //    public void onClick(View v) {
        //        if (toggleEnabled) {
        //            viewHolder.toggle.setChecked(!viewHolder.toggle.isChecked());
        //        }
        //    }
        //});
      }

      viewHolder.Toggle.Checked = _checked;

      viewHolder.Toggle.CheckedChange += delegate(object sender, CompoundButton.CheckedChangeEventArgs args)
      {
        _checked = args.IsChecked;

        if (GetOnCheckedChangeListener() != null) {
          GetOnCheckedChangeListener().OnCheckedChanged((CompoundButton)sender, args.IsChecked);
        }
      };

      //todo: wdcossey ^^ check above!!!
      //viewHolder.Toggle.SetOnCheckedChangeListener(_checkedChangeListener);
      
      viewHolder.Toggle.Enabled = _toggleEnabled;

      //set the colors for textViews
      viewHolder.Name.SetTextColor(UIUtils.GetTextColorStateList(color, selectedTextColor));
      viewHolder.Description.SetTextColor(UIUtils.GetTextColorStateList(color, selectedTextColor));

      //define the typeface for our textViews
      if (GetTypeface() != null)
      {
        viewHolder.Name.Typeface = GetTypeface();
        viewHolder.Description.Typeface = GetTypeface();
      }

      //get the drawables for our icon
      var icon = UIUtils.DecideIcon(ctx, GetIcon(), GetIIcon(), GetIconRes(), iconColor, IsIconTinted());
      var selectedIcon = UIUtils.DecideIcon(ctx, GetSelectedIcon(), GetIIcon(), GetSelectedIconRes(),
        selectedIconColor, IsIconTinted());

      //if we have an icon then we want to set it
      if (icon != null)
      {
        //if we got a different color for the selectedIcon we need a StateList
        if (selectedIcon != null)
        {
          viewHolder.Icon.SetImageDrawable(UIUtils.GetIconStateList(icon, selectedIcon));
        }
        else if (IsIconTinted())
        {
          viewHolder.Icon.SetImageDrawable(new PressedEffectStateListDrawable(icon, iconColor, selectedIconColor));
        }
        else
        {
          viewHolder.Icon.SetImageDrawable(icon);
        }
        //make sure we display the icon
        viewHolder.Icon.Visibility = ViewStates.Visible;
      }
      else
      {
        //hide the icon
        viewHolder.Icon.Visibility = ViewStates.Gone;
      }

      return convertView;
    }

    private class ViewHolder : Object
    {
      private readonly View _view;
      private readonly ImageView _icon;
      private readonly TextView _name;
      private readonly TextView _description;
      private readonly ToggleButton _toggle;

      public ViewHolder(View view)
      {
        _view = view;
        _icon = (ImageView) view.FindViewById(Resource.Id.icon);
        _name = (TextView) view.FindViewById(Resource.Id.name);
        _description = (TextView) view.FindViewById(Resource.Id.description);
        _toggle = (ToggleButton) view.FindViewById(Resource.Id.toggle);
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

      public ToggleButton Toggle
      {
        get { return _toggle; }
      }
    }

    //TODO: wdcossey
    //private CompoundButton.IOnCheckedChangeListener _checkedChangeListener = new CompoundButton.IOnCheckedChangeListener() {
    //    @Override
    //    public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {
    //        checked = isChecked;

    //        if (getOnCheckedChangeListener() != null) {
    //            getOnCheckedChangeListener().onCheckedChanged(ToggleDrawerItem.this, buttonView, IsChecked);
    //        }
    //    }
    //};
}

}