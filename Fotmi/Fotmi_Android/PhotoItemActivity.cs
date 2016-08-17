using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Media;
using Android.OS;
using Android.Provider;
using Android.Views;
using Android.Widget;
using FotmiPortableLibrary;
using File = Java.IO.File;
using Environment = Android.OS.Environment;
using Uri = Android.Net.Uri;

namespace Fotmi_Android
{
    public static class AppHelp
    {
        public static File File;
        public static File Dir;
        public static Bitmap Bitmap;
    }

    [Activity(Label = "PhotoItemActivity")]

    // View/edit a Task

    public class PhotoItemActivity : Activity
    {
        PhotoItem photo = new PhotoItem();
        ImageView _imageView;
        byte[] _byteData;
        EditText notesTextEdit;
        EditText nameTextEdit;
        Button saveButton;
        Button cancelDeleteButton;
        Button captureButton;

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            // Make it available in the gallery

            Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
            Uri contentUri = Uri.FromFile(AppHelp.File);
            mediaScanIntent.SetData(contentUri);
            SendBroadcast(mediaScanIntent);

            // Display in ImageView. We will resize the bitmap to fit the display
            // Loading the full sized image will consume to much memory 
            // and cause the application to crash.

            int height = Resources.DisplayMetrics.HeightPixels;
            int width = _imageView.Height;

            AppHelp.Bitmap = AppHelp.File.Path.LoadAndResizeBitmap(width, height);

            if (AppHelp.Bitmap != null)
            {
                _imageView.SetImageBitmap(AppHelp.Bitmap);

                using (var stream = new MemoryStream())
                {
                    AppHelp.Bitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
                    _byteData = stream.ToArray();
                }

                AppHelp.Bitmap = null;
            }

            // Dispose of the Java side bitmap.
            GC.Collect();
        }

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
            captureButton = FindViewById<Button>(Resource.Id.CaptureButton);

            _imageView = FindViewById<ImageView>(Resource.Id.ImvImage);

            // find all our controls
            cancelDeleteButton = FindViewById<Button>(Resource.Id.CancelDeleteButton);

            // set the cancel delete based on whether or not it's an existing photo
            cancelDeleteButton.Text = (photo.ID == 0 ? "Cancel" : "Delete");

            nameTextEdit.Text = photo.Name;
            notesTextEdit.Text = photo.Notes;

            if (photo.ID == 0)
            {
                _imageView.SetImageResource(Resource.Drawable.Icon);

            }
            else
            {
                byte[] i = photo.Image;
                var l = photo.Image.Length;

                Bitmap b = BitmapFactory.DecodeByteArray(i, 0, l);

                _imageView.SetImageBitmap(b);

                using (var stream = new MemoryStream())
                {
                    b.Compress(Bitmap.CompressFormat.Png, 0, stream);
                    _byteData = stream.ToArray();
                }
            }

            // button clicks 
            cancelDeleteButton.Click += (sender, e) => { CancelDelete(); };
            saveButton.Click += (sender, e) =>
            {
                if (_byteData != null)
                {
                    Save();
                }
                else
                {
                    Toast.MakeText(this, "Please capture photo", ToastLength.Long).Show();
                }
            };

            if (IsThereAnAppToTakePictures())
            {
                CreateDirectoryForPictures();

                captureButton.Click += TakeAPicture;
            }
        }

        void Save()
        {
            photo.Name = nameTextEdit.Text;
            photo.Notes = notesTextEdit.Text;
            photo.Image = _byteData;

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

        private void CreateDirectoryForPictures()
        {
            AppHelp.Dir = new File(
                Environment.GetExternalStoragePublicDirectory(
                    Environment.DirectoryPictures), "Fotmi_Android");
            if (!AppHelp.Dir.Exists())
            {
                AppHelp.Dir.Mkdirs();
            }
        }

        private bool IsThereAnAppToTakePictures()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            IList<ResolveInfo> availableActivities =
                PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
        }

        private void TakeAPicture(object sender, EventArgs eventArgs)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);

            AppHelp.File = new File(AppHelp.Dir, String.Format("Images_{0}.jpg", Guid.NewGuid()));

            intent.PutExtra(MediaStore.ExtraOutput, Uri.FromFile(AppHelp.File));

            StartActivityForResult(intent, 0);
        }
    }
}