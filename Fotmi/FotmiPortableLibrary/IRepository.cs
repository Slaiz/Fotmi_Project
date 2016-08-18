using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FotmiPortableLibrary
{
    public interface IRepository
    {
        PhotoItem GetPhoto(int id);

        IEnumerable<PhotoItem> GetPhotos();

        int SavePhoto(PhotoItem item);

        int DeletePhoto(int id);
    }
}
