using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FotmiPortableLibrary.Service
{
    public interface IService
    {
        PhotoItem GetPhoto(int id);
        IList<PhotoItem> GetPhotos();
        int SavePhoto(PhotoItem item);
        int DeletePhoto(int id);

    }
}