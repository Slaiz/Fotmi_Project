using SQLite;

namespace FotmiPortableLibrary
{

    // PhotoItem business object

    public class PhotoItem
    {
        public PhotoItem()
        {
        }

        // SQLite attributes
        [PrimaryKey, AutoIncrement]

        public int ID { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public byte[] Image { get; set; }
    }
}