using System.Collections.Generic;
using SQLite;

namespace FotmiPortableLibrary
{
    /// <summary>
    /// Manager classes are an abstraction on the data access layers
    /// </summary>

    public class PhotoItemManager
    {
        PhotoItemRepository repository;

        public PhotoItemManager(SQLiteConnection conn)
        {
            repository = new PhotoItemRepository(conn);
        }

        public PhotoItem GetPhoto(int id)
        {
            return repository.GetTask(id);
        }

        public IList<PhotoItem> GetPhotos()
        {
            return new List<PhotoItem>(repository.GetTasks());
        }

        public int SavePhoto(PhotoItem item)
        {
            return repository.SaveTask(item);
        }

        public int DeletePhoto(int id)
        {
            return repository.DeleteTask(id);
        }
    }
}