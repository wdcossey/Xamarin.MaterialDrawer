using Android.Content;
using Android.Graphics.Drawables;
using Android.Net;
using Android.Util;
using Android.Widget;

namespace com.xamarin.component.MaterialDrawer.Utils
{
  public class DrawerImageLoader
  {
    private IDrawerImageLoader _imageLoader;

    private static readonly object SyncLock = new object();
    private static DrawerImageLoader _singleton;

    private DrawerImageLoader(IDrawerImageLoader loaderImpl)
    {
      _imageLoader = loaderImpl;
    }

    public static DrawerImageLoader Init(IDrawerImageLoader loaderImpl)
    {
      _singleton = new DrawerImageLoader(loaderImpl);
      return _singleton;
    }

    public static DrawerImageLoader Instance
    {
      get
      {
        if (_singleton == null)
        {
          lock (SyncLock)
          {
            if (_singleton == null)
            {
              _singleton = new DrawerImageLoader(new NullDrawerImageLoader());
            }
          }
        }
        return _singleton;
      }
    }

    public void SetImage(ImageView imageView, Uri uri)
    {
      if (_imageLoader != null)
      {
        var placeHolder = _imageLoader.Placeholder(imageView.Context);

        if (placeHolder == null)
        {
          placeHolder = UIUtils.GetPlaceHolder(imageView.Context);
        }

        _imageLoader.Set(imageView, uri, placeHolder);
      }
    }

    public void CancelImage(ImageView imageView)
    {
      if (_imageLoader != null)
      {
        _imageLoader.Cancel(imageView);
      }
    }

    public IDrawerImageLoader GetImageLoader()
    {
      return _imageLoader;
    }

    public void SetImageLoader(IDrawerImageLoader imageLoader)
    {
      _imageLoader = imageLoader;
    }

    public interface IDrawerImageLoader
    {
      void Set(ImageView imageView, Uri uri, Drawable placeholder);

      void Cancel(ImageView imageView);

      Drawable Placeholder(Context ctx);
    }

    private class NullDrawerImageLoader : IDrawerImageLoader
    {
      public void Set(ImageView imageView, Uri uri, Drawable placeholder)
      {
        //this won't do anything
        Log.Info("MaterialDrawer",
          "you have not specified a ImageLoader implementation through the DrawerImageLoader.init(IDrawerImageLoader) method");
      }

      public void Cancel(ImageView imageView)
      {

      }

      public Drawable Placeholder(Context ctx)
      {
        return null;
      }
    }

  }
}