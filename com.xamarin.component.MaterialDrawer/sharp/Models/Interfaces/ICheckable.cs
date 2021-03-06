namespace com.xamarin.component.MaterialDrawer.Models.Interfaces
{
  /// <summary>
  /// <para>Created by wdcossey on 06.06.15.</para>
  /// <para>Original created by mikepenz on 03.02.15.</para>
  /// </summary>
  public interface ICheckable
  {
    bool IsCheckable();

    void SetCheckable(bool checkable);
  }

  public interface ICheckable<T>: ICheckable
  {
    T WithCheckable(bool checkable);
  }

}