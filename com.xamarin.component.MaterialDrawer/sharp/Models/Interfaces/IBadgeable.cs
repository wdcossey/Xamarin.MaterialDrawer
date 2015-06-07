namespace com.xamarin.component.MaterialDrawer.Models.Interfaces
{
  /// <summary>
  /// <para>Created by wdcossey on 06.06.15.</para>
  /// <para>Original created by mikepenz on 03.02.15.</para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public interface IBadgeable
  {
    string GetBadge();

    void SetBadge(string badge);
  }

  public interface IBadgeable<T> : IBadgeable
  {
    T WithBadge(string badge);
  }
}