using Android;
using Android.App;
using Android.OS;
using Android.Widget;
using FotmiPortableLibrary;
using Fotmi_Android;

namespace Fotmi_Android
{
    [Activity(Label = "PhotoItemActivity")]
    /// <summary>
    /// View/edit a Task
    /// </summary>
    public class PhotoItemActivity : Activity
    {
        PhotoItem photo = new PhotoItem();
        Button cancelDeleteButton;
        EditText notesTextEdit;
        EditText nameTextEdit;
        Button saveButton;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            int photoID = Intent.GetIntExtra("PhotoID", 0);
            if (photoID > 0)
            {
                photo = FotmiApp.Current.PhotoManager.GetPhoto(photoID);
            }

            // set our layout to be the home screen
            SetContentView(Resource.Layout.PhotoDetails);

            nameTextEdit = FindViewById<EditText>(Resource.Id.NameText);
            notesTextEdit = FindViewById<EditText>(Resource.Id.NotesText);
            saveButton = FindViewById<Button>(Resource.Id.SaveButton);


            // find all our controls
            cancelDeleteButton = FindViewById<Button>(Resource.Id.CancelDeleteButton);

            // set the cancel delete based on whether or not it's an existing photo
            cancelDeleteButton.Text = (photo.ID == 0 ? "Cancel" : "Delete");

            nameTextEdit.Text = photo.Name;
            notesTextEdit.Text = photo.Notes;

            // button clicks 
            cancelDeleteButton.Click += (sender, e) => { CancelDelete(); };
            saveButton.Click += (sender, e) => { Save(); };
        }

        void Save()
        {
            photo.Name = nameTextEdit.Text;
            photo.Notes = notesTextEdit.Text;

            FotmiApp.Current.PhotoManager.SavePhoto(photo);
            Finish();
        }

        void CancelDelete()
        {
            if (photo.ID != 0)
            {
                FotmiApp.Current.PhotoManager.DeletePhoto(photo.ID);
            }
            Finish();
        }

    }
}