using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookManagement.Models;

using System.Collections.Generic;
public class Catalog
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long CatalogId { get; set; }
    public string Name { get; set; }

    public List<Book> Books { get; set; }
}