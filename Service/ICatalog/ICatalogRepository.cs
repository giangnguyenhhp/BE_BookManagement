namespace BookManagement.Service.ICatalog;

using System.Collections.Generic;
using BookManagement.Models;
using BookManagement.Models.Request.Catalog;
public interface ICatalogRepository
{
    public List<Catalog> GetAllCatalog();
    public Catalog CreatCatalog(CreatCatalog request);
    public Catalog DeleteCatalog(long id);
    public Catalog UpdateCatalog(long id, UpdateCatalog request);
}