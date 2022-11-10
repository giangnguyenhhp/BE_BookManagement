namespace BookManagement.Models.Request.Catalog;

using System.Collections.Generic;
public class UpdateCatalog
{
    public string Name { get; set; }
    public List<long> BookIds { get; set; }
}