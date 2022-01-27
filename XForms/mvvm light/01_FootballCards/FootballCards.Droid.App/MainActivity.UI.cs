using Android.Widget;

namespace FootballCards.Droid.App
{
   public partial class MainActivity
   {
      private TextView _capacityTextView;
      private EditText _editShufflesEditText;
      private Button _showMapButton;
      private Button _shuffleButton;
      private TextView _stadiumTextView;
      private TextView _teamNameTextView;

      // Create the public properties for the object

      public Button ShuffleButton => _shuffleButton
                                     ?? (_shuffleButton = FindViewById<Button>(Resource.Id.btnShuffle));

      public Button ShowMapButton => _showMapButton ?? (_showMapButton = FindViewById<Button>(Resource.Id.btnShowMap));

      public TextView TeamNameTextView =>
         _teamNameTextView ?? (_teamNameTextView = FindViewById<TextView>(Resource.Id.txtTeamName));

      public TextView StadiumTextView =>
         _stadiumTextView ?? (_stadiumTextView = FindViewById<TextView>(Resource.Id.txtStadium));

      public TextView CapacityTextView =>
         _capacityTextView ?? (_capacityTextView = FindViewById<TextView>(Resource.Id.txtCapacity));

      public EditText ShufflesEditText =>
         _editShufflesEditText ?? (_editShufflesEditText = FindViewById<EditText>(Resource.Id.editShuffles));
   }
}