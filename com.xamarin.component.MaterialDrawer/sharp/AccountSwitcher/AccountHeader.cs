using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Views;
using Android.Widget;
using com.xamarin.component.MaterialDrawer.Models.Interfaces;
using Java.Util;

namespace com.xamarin.component.MaterialDrawer.AccountSwitcher
{
  /// <summary>
  /// Created by mikepenz on 06.06.15.
  /// Original by mikepenz on 27.02.15.
  /// </summary>
  public class AccountHeader
  {
    protected internal const double NAVIGATION_DRAWER_ACCOUNT_ASPECT_RATIO = 9d/16d;

    protected internal const string BUNDLE_SELECTION_HEADER = "bundle_selection_header";

    private AccountHeaderBuilder mAccountHeaderBuilder;

    protected internal AccountHeader(AccountHeaderBuilder accountHeaderBuilder)
    {
      mAccountHeaderBuilder = accountHeaderBuilder;
    }

    /// <summary>
    /// Get the Root view for the Header
    /// </summary>
    /// <returns></returns>
    public View GetView()
    {
      return mAccountHeaderBuilder.mAccountHeaderContainer;
    }

    /// <summary>
    /// Set the drawer for the AccountHeader so we can use it for the select
    /// </summary>
    /// <param name="drawer"></param>
    public void SetDrawer(Drawer drawer)
    {
      mAccountHeaderBuilder.mDrawer = drawer;
    }

    /// <summary>
    /// Returns the header background view so the dev can set everything on it
    /// </summary>
    /// <returns></returns>
    public ImageView GetHeaderBackgroundView()
    {
      return mAccountHeaderBuilder.mAccountHeaderBackground;
    }

    /// <summary>
    /// Set the background for the Header
    /// </summary>
    /// <param name="headerBackground"></param>
    public void SetBackground(Drawable headerBackground)
    {
      mAccountHeaderBuilder.mAccountHeaderBackground.SetImageDrawable(headerBackground);
    }

    /// <summary>
    /// Set the background for the Header as resource
    /// </summary>
    /// <param name="headerBackgroundRes"></param>
    public void SetBackgroundRes(int headerBackgroundRes)
    {
      mAccountHeaderBuilder.mAccountHeaderBackground.SetImageResource(headerBackgroundRes);
    }

    /// <summary>
    /// Toggle the selection list (show or hide it)
    /// </summary>
    /// <param name="ctx"></param>
    public void ToggleSelectionList(Context ctx)
    {
      mAccountHeaderBuilder.ToggleSelectionList(ctx);
    }

    /// <summary>
    /// returns if the selection list is currently shown
    /// </summary>
    /// <returns></returns>
    public bool IsSelectionListShown()
    {
      return mAccountHeaderBuilder.mSelectionListShown;
    }

    /// <summary>
    /// returns the current list of profiles set for this header
    /// </summary>
    /// <returns></returns>
    public IList<IProfile> GetProfiles()
    {
      return mAccountHeaderBuilder.mProfiles;
    }

    /// <summary>
    /// Set a new list of profiles for the header
    /// </summary>
    /// <param name="profiles"></param>
    public void SetProfiles(IEnumerable<IProfile> profiles)
    {
      mAccountHeaderBuilder.mProfiles = profiles.ToList();
      mAccountHeaderBuilder.UpdateHeaderAndList();
    }

    /// <summary>
    /// Selects the given profile and sets it to the new active profile
    /// </summary>
    /// <param name="profile"></param>
    public void SetActiveProfile(IProfile profile)
    {
      SetActiveProfile(profile, false);
    }

    /// <summary>
    /// Selects the given profile and sets it to the new active profile
    /// </summary>
    /// <param name="profile"></param>
    /// <param name="fireOnProfileChanged"></param>
    public void SetActiveProfile(IProfile profile, bool fireOnProfileChanged)
    {
      bool isCurrentSelectedProfile = mAccountHeaderBuilder.SwitchProfiles(profile);
      if (fireOnProfileChanged && mAccountHeaderBuilder.mOnAccountHeaderListener != null)
      {
        mAccountHeaderBuilder.mOnAccountHeaderListener.OnProfileChanged(null, profile, isCurrentSelectedProfile);
      }
    }

    /// <summary>
    /// Selects a profile by its identifier
    /// </summary>
    /// <param name="identifier"></param>
    public void SetActiveProfile(int identifier)
    {
      SetActiveProfile(identifier, false);
    }

    /// <summary>
    /// Selects a profile by its identifier
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="fireOnProfileChanged"></param>
    public void SetActiveProfile(int identifier, bool fireOnProfileChanged)
    {
      if (mAccountHeaderBuilder.mProfiles != null)
      {
        foreach (var profile in mAccountHeaderBuilder.mProfiles)
        {
          if (profile is IIdentifyable)
          {
            if (profile.GetIdentifier() == identifier)
            {
              SetActiveProfile(profile, fireOnProfileChanged);
              return;
            }
          }
        }
      }
    }

    /// <summary>
    /// Helper method to update a profile using it's identifier
    /// </summary>
    /// <param name="newProfile"></param>
    public void UpdateProfileByIdentifier(IProfile newProfile)
    {
      if (mAccountHeaderBuilder.mProfiles != null && newProfile != null && newProfile.GetIdentifier() >= 0)
      {
        int found = -1;
        for (int i = 0; i < mAccountHeaderBuilder.mProfiles.Count; i++)
        {
          if (mAccountHeaderBuilder.mProfiles[i] is IIdentifyable)
          {
            if (mAccountHeaderBuilder.mProfiles[i].GetIdentifier() == newProfile.GetIdentifier())
            {
              found = i;
              break;
            }
          }
        }

        if (found > -1)
        {
          mAccountHeaderBuilder.mProfiles[found] = newProfile;
          mAccountHeaderBuilder.UpdateHeaderAndList();
        }
      }
    }

    /// <summary>
    /// Add new profiles to the existing list of profiles
    /// </summary>
    /// <param name="profiles"></param>
    public void AddProfiles(IProfile[] profiles)
    {
      if (mAccountHeaderBuilder.mProfiles == null)
      {
        mAccountHeaderBuilder.mProfiles = new List<IProfile>();
      }

      if (profiles != null)
      {
        mAccountHeaderBuilder.mProfiles.AddRange(profiles);
      }

      mAccountHeaderBuilder.UpdateHeaderAndList();
    }

    /// <summary>
    /// Add a new profile at a specific position to the list
    /// </summary>
    /// <param name="profile"></param>
    /// <param name="position"></param>
    public void AddProfile(IProfile profile, int position)
    {
      if (mAccountHeaderBuilder.mProfiles == null)
      {
        mAccountHeaderBuilder.mProfiles = new List<IProfile>();
      }

      mAccountHeaderBuilder.mProfiles.Insert(position, profile);

      mAccountHeaderBuilder.UpdateHeaderAndList();
    }

    /// <summary>
    /// remove a profile from the given position
    /// </summary>
    /// <param name="position"></param>
    public void RemoveProfile(int position)
    {
      if (mAccountHeaderBuilder.mProfiles != null && mAccountHeaderBuilder.mProfiles.Count > position)
      {
        mAccountHeaderBuilder.mProfiles.RemoveAt(position);
      }

      mAccountHeaderBuilder.UpdateHeaderAndList();
    }

    /// <summary>
    /// try to remove the given profile
    /// </summary>
    /// <param name="profile"></param>
    public void RemoveProfile(IProfile profile)
    {
      if (mAccountHeaderBuilder.mProfiles != null)
      {
        mAccountHeaderBuilder.mProfiles.Remove(profile);
      }

      mAccountHeaderBuilder.UpdateHeaderAndList();
    }

    /// <summary>
    /// Clear the header
    /// </summary>
    public void Clear()
    {
      mAccountHeaderBuilder.mProfiles = null;

      //calculate the profiles to set
      mAccountHeaderBuilder.CalculateProfiles();

      //process and build the profiles
      mAccountHeaderBuilder.BuildProfiles();
    }

    /// <summary>
    /// add the values to the bundle for saveInstanceState
    /// </summary>
    /// <param name="savedInstanceState"></param>
    /// <returns></returns>
    public Bundle SaveInstanceState(Bundle savedInstanceState)
    {
      if (savedInstanceState != null)
      {
        savedInstanceState.PutInt(BUNDLE_SELECTION_HEADER, mAccountHeaderBuilder.GetCurrentSelection());
      }
      return savedInstanceState;
    }


    public interface IOnAccountHeaderListener
    {

      /// <summary>
      /// the event when the profile changes
      /// </summary>
      /// <param name="view"></param>
      /// <param name="profile"></param>
      /// <param name="current"></param>
      /// <returns>if the event was consumed</returns>
      bool OnProfileChanged(View view, IProfile profile, bool current);
    }

    public interface IOnAccountHeaderSelectionViewClickListener
    {
      /// <summary>
      /// the event when the user clicks the selection list under the profile icons
      /// </summary>
      /// <param name="view"></param>
      /// <param name="profile"></param>
      /// <returns>if the event was consumed</returns>
      bool OnClick(View view, IProfile profile);
    }
  }
}