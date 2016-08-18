using System.Collections.Generic;
using Android.App;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using FotmiPortableLibrary;

namespace Fotmi_Android
{

    // Adapter that presents Photos in a row-view

    [Activity(Label = "PhotoItemListAdapter")]
    public class PhotoItemListAdapter : BaseAdapter<PhotoItem>
    {
        Activity _context = null;
        IList<PhotoItem> _photos  = new List<PhotoItem>();

        public PhotoItemListAdapter(Activity context, IList<PhotoItem> photos)
        {
            _context = context;
            _photos = photos;
        }

        public override PhotoItem this[int position]
        {
            get { return _photos[position]; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override int Count
        {
            get { return _photos.Count; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = (convertView ??
                    _context.LayoutInflater.Inflate(
                    Resource.Layout.PhotoItemList,
                    parent,
                    false)) as RelativeLayout;

            // Find references to each subview in the list item's view
            var txtName = view.FindViewById<TextView>(Resource.Id.NameText);
            var txtDescription = view.FindViewById<TextView>(Resource.Id.NotesText);
            var imvImage = view.FindViewById<ImageView>(Resource.Id.Image);

            //Assign item's values to the various subviews
            txtName.SetText(_photos[position].Name, TextView.BufferType.Normal);
            txtDescription.SetText(_photos[position].Notes, TextView.BufferType.Normal);

            byte[] i = _photos[position].Image;
            var l = _photos[position].Image.Length;

            Bitmap b = BitmapFactory.DecodeByteArray(i, 0, l);

            imvImage.SetImageBitmap(b);

            //Return the view
            return view;
        }
    }
}