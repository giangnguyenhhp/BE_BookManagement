namespace BookManagement.Service.Impl.Catalog
{
    using Models;
    using BookManagement.Models.Request.Catalog;
    using ICatalog;
    using Microsoft.EntityFrameworkCore;
    public class CatalogRepository : ICatalogRepository
    {
        private readonly MasterDbContext _context;

        public CatalogRepository(MasterDbContext context)
        {
            _context = context;
        }

        public List<Catalog> GetAllCatalog()
        {
            var catalogs = _context.Catalog
                .Include(c => c.Books)
                .ThenInclude(c=>c.Author)
                .ToList();
            return catalogs;
        }

        public Catalog CreatCatalog(CreatCatalog request)
        {
            var checkCatalog = _context.Catalog.FirstOrDefault(p => p.Name == request.Name);
            if (checkCatalog != null) throw new Exception("Catalog Existed!!");

            var books = _context.Book.Where(a => request.BookIds.Contains(a.BookId)).ToList();

            var newCatalog = new Catalog
            {
                Name = request.Name,
                Books = books
            };
            _context.Catalog.Add(newCatalog);
            _context.SaveChanges();
            return newCatalog;
        }

        public Catalog UpdateCatalog(long id, UpdateCatalog request)
        {
            var catalog = _context.Catalog
                .Include(p=>p.Books)
                .FirstOrDefault(p => p.CatalogId == id);
            if (catalog == null) throw new Exception("Catalog not Existed!!");
        
            var books = _context.Book.Where(a => request.BookIds.Contains(a.BookId)).ToList();

            catalog.Name = request.Name;
            catalog.Books = books;

            _context.SaveChanges();
            return catalog;
        }
    
        public Catalog DeleteCatalog(long id)
        {
            var checkCatalog = _context.Catalog.FirstOrDefault(p => p.CatalogId == id);
            if (checkCatalog == null) throw new Exception("Catalog not Existed!!");

            _context.Catalog.Remove(checkCatalog);
            _context.SaveChanges();
            return checkCatalog;
        
        }
    }
}