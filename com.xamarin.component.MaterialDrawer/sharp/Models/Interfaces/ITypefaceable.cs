using Android.Graphics;

namespace com.xamarin.component.MaterialDrawer.Models.Interfaces
{
  /// <summary>
  ///   <para>Created by wdcossey on 06.06.15.</para>
  ///   <para>Original created by mikepenz on 03.02.15.</para>
  /// </summary>
  public interface ITypefaceable
  {
    Typeface GetTypeface();

    void SetTypeface(Typeface typeface);
  }

  /// <summary>
  ///   <para>Created by wdcossey on 06.06.15.</para>
  ///   <para>Original created by mikepenz on 03.02.15.</para>
  /// </summary>
  public interface ITypefaceable<T> : ITypefaceable
  {
    T WithTypeface(Typeface typeface);
  }
}