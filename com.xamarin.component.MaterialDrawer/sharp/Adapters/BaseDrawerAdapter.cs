using System.Collections.Generic;
using Android.Widget;
using com.xamarin.component.MaterialDrawer.Models.Interfaces;

namespace com.xamarin.component.MaterialDrawer.Adapters
{
  public abstract class BaseDrawerAdapter : BaseAdapter
  {

    public void DataUpdated()
    {
      MapTypes();
      NotifyDataSetChanged();
    }

    public void MapTypes()
    {
      if (GetTypeMapper() == null)
      {
        SetTypeMapper(new HashSet<string>());
      }

      if (GetDrawerItems() != null)
      {
        foreach (var drawerItem in GetDrawerItems())
        {
          GetTypeMapper().Add(drawerItem.GetType());
        }

      }
    }

    public override int GetItemViewType(int position)
    {
      if (GetDrawerItems() != null && GetDrawerItems().Count > position && GetTypeMapper() != null)
      {
        var i = 0;
        foreach (var type in GetTypeMapper())
        {
          if (type.Equals(GetDrawerItems()[position].GetType()))
          {
            return i;
          }
          i++;
        }
      }
      return -1;
    }


    public override int ViewTypeCount
    {
      get
      {
        //WTF this is only called once ?!
        //This means for now i only allow 50 viewtTypes^
        return 50;
        /*
        if (getTypeMapper() != null) {
            return getTypeMapper().size();
        } else {
            return -1;
        }
        */
      }
    }

    public abstract IList<IDrawerItem> GetDrawerItems();

    public abstract void SetDrawerItems(IList<IDrawerItem> drawerItems);

    public abstract HashSet<string> GetTypeMapper();

    public abstract void SetTypeMapper(HashSet<string> typeMapper);

    public abstract void ResetAnimation();
  }

  public abstract class BaseDrawerAdapter<T> : BaseDrawerAdapter
  {
  }

}