using System;
using System.IO;
using SQLite;
using Android.App;
using FotmiPortableLibrary;

namespace Fotmi_Android
{
    [Application]
    public class FotmiApp : Application
    {
        public static FotmiApp Current { get; private set; }

        public PhotoItemManager PhotoManager { get; set; }
        SQLiteConnection conn;

        public FotmiApp(IntPtr handle, global::Android.Runtime.JniHandleOwnership transfer)
            : base(handle, transfer)
        {
            Current = this;
        }

        public override void OnCreate()
        {
            base.OnCreate();

            var sqliteFilename = "PhotoItemDB.db3";
            string libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(libraryPath, sqliteFilename);
            conn = new SQLiteConnection(path);

            PhotoManager = new PhotoItemManager(conn);
        }
    }
}

