namespace com.xamarin.component.MaterialDrawer.Models.Interfaces
{
  /// <summary>
  /// <para>Created by wdcossey on 06.06.15.</para>
  /// <para>Original created by mikepenz on 03.02.15.</para>
  /// </summary>
  public interface IIdentifyable
  {
    int GetIdentifier();

    void SetIdentifier(int identifier);
  }

  public interface IIdentifyable<T>: IIdentifyable
  {
    T WithIdentifier(int identifier);
  }
}