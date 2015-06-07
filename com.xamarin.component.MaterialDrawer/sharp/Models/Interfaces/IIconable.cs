using Android.Graphics.Drawables;

namespace com.xamarin.component.MaterialDrawer.Models.Interfaces
{
  /// <summary>
  /// <para>Created by wdcossey on 06.06.15.</para>
  /// <para>Original created by mikepenz on 03.02.15.</para>
  /// </summary>
  public interface IIconable
  {
    Drawable GetIcon();

    IIcon GetIIcon();

    void SetIcon(Drawable icon);

    void SetIIcon(IIcon iicon);
  }

  public interface IIconable<T> : IIconable
  {
    T WithIcon(Drawable icon);

    T WithIcon(IIcon iicon);
  }
}