using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using FotmiPortableLibrary;

namespace Fotmi_Android
{
    [Activity(Label = "Fotmi", MainLauncher = true, Icon = "@drawable/icon")]
    public class HomeActivity : Activity
    {

        PhotoItemListAdapter photoList;
        IList<PhotoItem> photos;
        Button addPhotoButton;
        ListView photoListView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Home);

            photoListView = FindViewById<ListView>(Resource.Id.photoList);
            addPhotoButton = FindViewById<Button>(Resource.Id.AddButton);

            // wire up add task button handler
            if (addPhotoButton != null)
            {
                addPhotoButton.Click += (sender, e) => {
                    StartActivity(typeof(PhotoItemActivity));
                };
            }

            // wire up Photo click handler
            if (photoListView != null)
            {
                photoListView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => {
                    var taskDetails = new Intent(this, typeof(PhotoItemActivity));
                    taskDetails.PutExtra("PhotoID", photos[e.Position].ID);
                    StartActivity(taskDetails);
                };
            }
        }

        protected override void OnResume()
        {
            base.OnResume();

            photos = FotmiApp.Current.PhotoManager.GetPhotos();

            // create our adapter
            photoList = new PhotoItemListAdapter(this, photos);

            //Hook up our adapter to our ListView
            photoListView.Adapter = photoList;
        }
    }
}

