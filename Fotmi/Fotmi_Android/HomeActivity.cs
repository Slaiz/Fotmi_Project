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

        PhotoItemListAdapter _photoList;
        IList<PhotoItem> _photos;
        Button addPhotoButton;
        ListView photoListView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Home);

            photoListView = FindViewById<ListView>(Resource.Id.photoList);
            addPhotoButton = FindViewById<Button>(Resource.Id.AddButton);
            
            // 
            if (addPhotoButton != null)
            {
                addPhotoButton.Click += (sender, e) => {
                    StartActivity(typeof(PhotoItemActivity));
                };
            }

            // 
            if (photoListView != null)
            {
                photoListView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => {
                    var photoDetails = new Intent(this, typeof(PhotoItemActivity));
                    photoDetails.PutExtra("PhotoID", _photos[e.Position].ID);
                    StartActivity(photoDetails);
                };
            }
        }

        protected override void OnResume()
        {
            base.OnResume();

            _photos = FotmiApp.Current.PhotoService.GetPhotos();

            // create our adapter
            _photoList = new PhotoItemListAdapter(this, _photos);

            //Hook up our adapter to our ListView
            photoListView.Adapter = _photoList;
        }
    }
}

