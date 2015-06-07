namespace com.xamarin.component.MaterialDrawer.Models.Interfaces
{
  /// <summary>
  /// <para>Created by wdcossey on 06.06.15.</para>
  /// <para>Original created by mikepenz on 03.02.15.</para>
  /// </summary>
  public interface INameable
  {
    string GetName();

    int GetNameRes();

    void SetName(string name);

    void SetNameRes(int nameRes);
  }

  public interface INameable<T> : INameable
  {
    T WithName(string name);

    T WithName(int nameRes);
  }
}