
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Net;

namespace com.xamarin.component.MaterialDrawer.Models.Interfaces
{
  /// <summary>
  ///   <para>Created by wdcossey on 06.06.15.</para>
  ///   <para>Original created by mikepenz on 03.02.15.</para>
  /// </summary>

  public interface IProfile
  {
    string GetName();

    void SetName(string name);

    string GetEmail();

    void SetEmail(string email);

    Drawable GetIcon();

    Bitmap GetIconBitmap();

    Uri GetIconUri();

    void SetIcon(Drawable icon);

    void SetIconBitmap(Bitmap bitmap);

    void SetIcon(string url);

    void SetIcon(Uri uri);

    bool IsSelectable();

    int GetIdentifier();
  }

  public interface IProfile<T> : IProfile
  {
    T WithName(string name);

    T WithEmail(string email);

    T WithIcon(Drawable icon);

    T WithIcon(Bitmap bitmap);

    T WithIcon(string url);

    T WithIcon(Uri uri);

    T WithSelectable(bool selectable);

    T SetSelectable(bool selectable);
  }

  /*
   public interface IProfile<T> 
    {
      T WithName(string name);

      string GetName();

      void SetName(string name);

      T WithEmail(string email);

      string GetEmail();

      void SetEmail(string email);

      T WithIcon(Drawable icon);

      T WithIcon(Bitmap bitmap);

      T WithIcon(string url);

      T WithIcon(Uri uri);

      Drawable GetIcon();

      Bitmap GetIconBitmap();

      Uri GetIconUri();

      void SetIcon(Drawable icon);

      void SetIconBitmap(Bitmap bitmap);

      void SetIcon(string url);

      void SetIcon(Uri uri);

      T WithSelectable(bool selectable);

      bool IsSelectable();

      T SetSelectable(bool selectable);

      int GetIdentifier();
    }
   */
}