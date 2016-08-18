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

        public PhotoItemService PhotoService { get; set; }
        SQLiteConnection _conn;

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
            _conn = new SQLiteConnection(path);

            IRepository repository = new PhotoItemRepository(_conn);

            PhotoService = new PhotoItemService(repository);
        }
    }
}

