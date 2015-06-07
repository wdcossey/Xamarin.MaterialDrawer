using Android.Views;
using Java.Lang;

namespace com.xamarin.component.MaterialDrawer.Models.Interfaces
{
  /// <summary>
  /// <para>Created by wdcossey on 06.06.15.</para>
  /// <para>Original created by mikepenz on 03.02.15.</para>
  /// </summary>
  public interface IDrawerItem
  {
    int GetIdentifier();

    Object GetTag();

    bool IsEnabled();

    string GetType();

    int GetLayoutRes();

    View ConvertView(LayoutInflater inflater, View convertView, ViewGroup parent);
  }
}