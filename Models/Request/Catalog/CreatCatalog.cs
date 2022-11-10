namespace BookManagement.Models.Request.Catalog;

using System.Collections.Generic;
public class CreatCatalog
{
    public string Name { get; set; }
    public List<long> BookIds { get; set; }

}