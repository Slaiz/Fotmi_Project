using System.Collections.Generic;
using System.Linq;
using SQLite;

namespace FotmiPortableLibrary
{
    // PhotoDatabase uses ADO.NET to create the [Items] table and create,read,update,delete data

    public class PhotoDatabase
    {
        static readonly object _locker = new object();

        private SQLiteConnection _database;

        public PhotoDatabase(SQLiteConnection conn)
        {
            _database = conn;
            // create the tables
            _database.CreateTable<PhotoItem>();
        }

        public IEnumerable<PhotoItem> GetItems()
        {
            lock (_locker)
            {
                return (from i in _database.Table<PhotoItem>() select i).ToList();
            }
        }

        public PhotoItem GetItem(int id)
        {
            lock (_locker)
            {
                return _database.Table<PhotoItem>().FirstOrDefault(x => x.ID == id);
            }
        }

        public int SaveItem(PhotoItem item)
        {
            lock (_locker)
            {
                if (item.ID != 0)
                {
                    _database.Update(item);
                    return item.ID;
                }

                return _database.Insert(item);
            }
        }

        public int DeleteItem(int id)
        {
            lock (_locker)
            {
                return _database.Delete<PhotoItem>(id);
            }
        }
    }
}