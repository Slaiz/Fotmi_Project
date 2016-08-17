using System;
using System.Collections.Generic;
using SQLite;

namespace FotmiPortableLibrary
{
    public class PhotoItemRepository
    {
        PhotoDatabase db = null;

        public PhotoItemRepository(SQLiteConnection conn)
        {
            db = new PhotoDatabase(conn);
        }

        public PhotoItem GetTask(int id)
        {
            return db.GetItem(id);
        }

        public IEnumerable<PhotoItem> GetTasks()
        {
            return db.GetItems();
        }

        public int SaveTask(PhotoItem item)
        {
            return db.SaveItem(item);
        }

        public int DeleteTask(int id)
        {
            return db.DeleteItem(id);
        }
    }
}

