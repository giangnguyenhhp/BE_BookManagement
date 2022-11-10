namespace BookManagement.Models.Request.Book;

using System.Collections.Generic;
public class UpdateBook
{
    public string Name { get; set; }
    public string PublishDate { get; set; }
    public string VNName { get; set; }
    public string NameNXB { get; set; }
    public long AuthorId { get; set; }
    public List<long> CatalogIds { get; set; }
}