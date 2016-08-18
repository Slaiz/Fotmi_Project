using System.Collections.Generic;
using FotmiPortableLibrary.Service;
using SQLite;

namespace FotmiPortableLibrary
{
    // Manager classes are an abstraction on the data access layers

    public class PhotoItemService:IService
    {
        private readonly IRepository _repository;

        public PhotoItemService(IRepository repository)
        {
            _repository = repository;
        }

        public PhotoItem GetPhoto(int id)
        {
            return _repository.GetPhoto(id);
        }

        public IList<PhotoItem> GetPhotos()
        {
            return new List<PhotoItem>(_repository.GetPhotos());
        }

        public int SavePhoto(PhotoItem item)
        {
            return _repository.SavePhoto(item);
        }

        public int DeletePhoto(int id)
        {
            return _repository.DeletePhoto(id);
        }
    }
}