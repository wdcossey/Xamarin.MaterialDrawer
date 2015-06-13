using System.Linq;
using Android.Annotation;
using Android.Graphics;
using Android.Graphics.Drawables;

namespace com.xamarin.component.MaterialDrawer.Utils
{

/*
  http://stackoverflow.com/questions/7979440/android-cloning-a-drawable-in-order-to-make-a-statelistdrawable-with-filters
  http://stackoverflow.com/users/2075875/malachiasz
*/

  //[SuppressLint(Value = new[] {"InlinedApi"})]
  public sealed class PressedEffectStateListDrawable : StateListDrawable
  {

    private readonly Color _color;
    private readonly Color _selectionColor;

    public PressedEffectStateListDrawable(Drawable drawable, Color color, Color selectionColor)
      : base()
    {

      drawable = drawable.Mutate();

      AddState(new[] {Android.Resource.Attribute.StateActivated}, drawable);
      AddState(new int[] {}, drawable);

      _color = color;
      _selectionColor = selectionColor;
    }

    protected override bool OnStateChange(int[] states)
    {
      var isStatePressedInArray = states.Any(a => a == Android.Resource.Attribute.StateActivated);

      base.SetColorFilter(isStatePressedInArray ? _selectionColor : _color, PorterDuff.Mode.SrcIn);

      return base.OnStateChange(states);
    }

    public override bool IsStateful
    {
      get { return true; }
    }
  }
}