using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookManagement.Models;

using System.Collections.Generic;
public class Author
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long AuthorId { get; set; } 
    public string Name { get; set; } 
    public string DOB { get; set; }
    public string HomeTown { get; set; }
    public List<Book> Books { get; set; }
}

