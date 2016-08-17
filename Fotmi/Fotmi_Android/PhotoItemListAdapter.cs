using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FotmiPortableLibrary;

namespace Fotmi_Android
{
    /// <summary>
    /// Adapter that presents Photos in a row-view
    /// </summary>
    [Activity(Label = "PhotoItemListAdapter")]
    public class PhotoItemListAdapter : BaseAdapter<PhotoItem>
    {
        Activity context = null;
        IList<PhotoItem> photos = new List<PhotoItem>();

        public PhotoItemListAdapter(Activity context, IList<PhotoItem> photos) : base()
        {
            this.context = context;
            this.photos = this.photos;
        }

        public override PhotoItem this[int position]
        {
            get { return photos[position]; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override int Count
        {
            get { return photos.Count; }
        }

        public override Android.Views.View GetView(int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
        {
            // Get our object for position
            var item = photos[position];

            //Try to reuse convertView if it's not  null, otherwise inflate it from our item layout
            // gives us some performance gains by not always inflating a new view
            // will sound familiar to MonoTouch developers with UITableViewCell.DequeueReusableCell()

            //			var view = (convertView ?? 
            //					context.LayoutInflater.Inflate(
            //					Resource.Layout.TaskListItem, 
            //					parent, 
            //					false)) as LinearLayout;
            //			// Find references to each subview in the list item's view
            //			var txtName = view.FindViewById<TextView>(Resource.Id.NameText);
            //			var txtDescription = view.FindViewById<TextView>(Resource.Id.NotesText);
            //			//Assign item's values to the various subviews
            //			txtName.SetText (item.Name, TextView.BufferType.Normal);
            //			txtDescription.SetText (item.Notes, TextView.BufferType.Normal);

            var view = (convertView ?? context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItemChecked, parent, false)) as CheckedTextView;
            view.SetText(item.Name == "" ? "<new photo>" : item.Name, TextView.BufferType.Normal);

            //Finally return the view
            return view;
        }
    }
}