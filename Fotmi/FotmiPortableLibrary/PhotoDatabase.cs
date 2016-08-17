using System.Collections.Generic;
using System.Linq;
using SQLite;

namespace FotmiPortableLibrary
{
    // PhotoDatabase uses ADO.NET to create the [Items] table and create,read,update,delete data

    public class PhotoDatabase
    {
        static object locker = new object();

        public SQLiteConnection database;

        public string path;

        public PhotoDatabase(SQLiteConnection conn)
        {
            database = conn;
            // create the tables
            database.CreateTable<PhotoItem>();
        }

        public IEnumerable<PhotoItem> GetItems()
        {
            lock (locker)
            {
                return (from i in database.Table<PhotoItem>() select i).ToList();
            }
        }

        public PhotoItem GetItem(int id)
        {
            lock (locker)
            {
                return database.Table<PhotoItem>().FirstOrDefault(x => x.ID == id);
            }
        }

        public int SaveItem(PhotoItem item)
        {
            lock (locker)
            {
                if (item.ID != 0)
                {
                    database.Update(item);
                    return item.ID;
                }
                else
                {
                    return database.Insert(item);
                }
            }
        }

        public int DeleteItem(int id)
        {
            lock (locker)
            {
                return database.Delete<PhotoItem>(id);
            }
        }
    }
}