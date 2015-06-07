using System;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using com.xamarin.component.MaterialDrawer.Utils;
using Java.Lang;

namespace com.xamarin.component.MaterialDrawer.Models
{
  public class SwitchDrawerItem : BaseDrawerItem<SwitchDrawerItem>
  {
    private string _description;
    private int _descriptionRes = -1;

    private bool _switchEnabled = true;

    private bool _checkable;
    private bool _checked;
    private CompoundButton.IOnCheckedChangeListener _onCheckedChangeListener;

    public SwitchDrawerItem WithDescription(string description)
    {
      _description = description;
      return this;
    }

    public SwitchDrawerItem WithDescription(int descriptionRes)
    {
      _descriptionRes = descriptionRes;
      return this;
    }

    public SwitchDrawerItem WithChecked(bool @checked)
    {
      _checked = @checked;
      return this;
    }

    public SwitchDrawerItem WithSwitchEnabled(bool switchEnabled)
    {
      _switchEnabled = switchEnabled;
      return this;
    }

    public SwitchDrawerItem WithOnCheckedChangeListener(CompoundButton.IOnCheckedChangeListener onCheckedChangeListener)
    {
      _onCheckedChangeListener = onCheckedChangeListener;
      return this;
    }

    public SwitchDrawerItem WithCheckable(bool checkable)
    {
      _checkable = checkable;
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

    public bool IsChecked()
    {
      return _checked;
    }

    public void SetChecked(bool @checked)
    {
      _checked = @checked;
    }

    public bool IsSwitchEnabled()
    {
      return _switchEnabled;
    }

    public void SetSwitchEnabled(bool switchEnabled)
    {
      _switchEnabled = switchEnabled;
    }

    public CompoundButton.IOnCheckedChangeListener GetOnCheckedChangeListener()
    {
      return _onCheckedChangeListener;
    }

    public void SetOnCheckedChangeListener(CompoundButton.IOnCheckedChangeListener onCheckedChangeListener)
    {
      _onCheckedChangeListener = onCheckedChangeListener;
    }


    public bool IsCheckable()
    {
      return _checkable;
    }


    public void SetCheckable(bool checkable)
    {
      _checkable = checkable;
    }

    public override string GetType()
    {
      return "SWITCH_ITEM";
    }

    public override int GetLayoutRes()
    {
      return Resource.Layout.material_drawer_item_switch;
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
      Color color;
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
      int selectedTextColor = UIUtils.DecideColor(ctx, GetSelectedTextColor(), GetSelectedTextColorRes(),
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
          if (_switchEnabled)
          {
            viewHolder.SwitchView.Checked = !viewHolder.SwitchView.Checked;
          }
        };
        //TODO: wdcossey
        //    viewHolder.View.SetOnClickListener(new View.OnClickListener() {
        //        @Override
        //        public void onClick(View v) {
        //            if (switchEnabled) {
        //                viewHolder.switchView.setChecked(!viewHolder.switchView.isChecked());
        //            });
        //        }
        //    });
      }

      viewHolder.SwitchView.Checked = _checked;
      viewHolder.SwitchView.CheckedChange += delegate(object sender, CompoundButton.CheckedChangeEventArgs args)
      {
        _checked = args.IsChecked;

        if (GetOnCheckedChangeListener() != null)
        {
          GetOnCheckedChangeListener().OnCheckedChanged((CompoundButton) sender, args.IsChecked);
        }
      };
      //viewHolder.SwitchView.SetOnCheckedChangeListener(checkedChangeListener);
      viewHolder.SwitchView.Enabled = _switchEnabled;

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
      Drawable icon = UIUtils.DecideIcon(ctx, GetIcon(), GetIIcon(), GetIconRes(), iconColor, IsIconTinted());
      Drawable selectedIcon = UIUtils.DecideIcon(ctx, GetSelectedIcon(), GetIIcon(), GetSelectedIconRes(),
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

    private class ViewHolder : Java.Lang.Object
    {
      private readonly View _view;
      private readonly ImageView _icon;
      private readonly TextView _name;
      private readonly TextView _description;
      private readonly SwitchCompat _switchView;

      public ViewHolder(View view)
      {
        _view = view;
        _icon = (ImageView) view.FindViewById(Resource.Id.icon);
        _name = (TextView) view.FindViewById(Resource.Id.name);
        _description = (TextView) view.FindViewById(Resource.Id.description);
        _switchView = (SwitchCompat) view.FindViewById(Resource.Id.switchView);
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

      public SwitchCompat SwitchView
      {
        get { return _switchView; }
      }
    }

    //private CompoundButton.IOnCheckedChangeListener checkedChangeListener = new CompoundButton.IOnCheckedChangeListener() {
    //    @Override
    //    public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {
    //        checked = isChecked;

    //        if (getOnCheckedChangeListener() != null) {
    //            getOnCheckedChangeListener().onCheckedChanged(SwitchDrawerItem.this, buttonView, isChecked);
    //        }
    //    }
    //};
  }
}