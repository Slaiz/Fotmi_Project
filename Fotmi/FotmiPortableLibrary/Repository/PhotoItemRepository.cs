using System.Collections.Generic;
using SQLite;

namespace FotmiPortableLibrary
{
    public class PhotoItemRepository:IRepository
    {
        private readonly PhotoDatabase _db;

        public PhotoItemRepository(SQLiteConnection conn)
        {
            _db = new PhotoDatabase(conn);
        }

        public PhotoItem GetPhoto(int id)
        {
            return _db.GetItem(id);
        }

        public IEnumerable<PhotoItem> GetPhotos()
        {
            return _db.GetItems();
        }

        public int SavePhoto(PhotoItem item)
        {
            return _db.SaveItem(item);
        }

        public int DeletePhoto(int id)
        {
            return _db.DeleteItem(id);
        }
    }
}

