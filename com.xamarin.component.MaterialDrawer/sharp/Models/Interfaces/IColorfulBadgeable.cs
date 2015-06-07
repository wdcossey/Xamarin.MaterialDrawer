using Android.Graphics;

namespace com.xamarin.component.MaterialDrawer.Models.Interfaces
{

  /// <summary>
  /// <para>Created by wdcossey on 06.06.15.</para>
  /// <para>Original created by mikepenz on 03.02.15.</para>
  /// </summary>
  public interface IColorfulBadgeable : IBadgeable
  {
    Color GetBadgeTextColor();

    void SetBadgeTextColor(Color color);
  }

  /// <summary>
  /// <para>Created by wdcossey on 06.06.15.</para>
  /// <para>Original created by mikepenz on 03.02.15.</para>
  /// </summary>
  public interface IColorfulBadgeable<T> : IColorfulBadgeable
  {
    T WithBadgeTextColor(Color color);
  }
}