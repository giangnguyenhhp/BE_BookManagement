using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookManagement.Models
{
    using System.Collections.Generic;
    /// <summary>
    /// Lớp mô tả sách
    /// </summary>
    public class Book
    {
        /// <summary>
        /// Tạo khóa chính, Id tự tạo tăng dần
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long BookId { get; set; }
        public string Name { get; set; }
        public string PublishDate { get; set; }
        public string VNName { get; set; }
        public string NameNXB { get; set; }
    
        public List<Catalog> Catalogs { get; set; }

        // Thêm ? khởi tạo biến có thể nhận giá trị null
        public Author? Author { get; set; }
        public long AuthorId { get; set; }
    }
}





