namespace BookManagement.Service.Impl.Book
{
    using Models;
    using BookManagement.Models.Request.Book;
    using IBook;
    using Microsoft.EntityFrameworkCore;
    public class BookRepository : IBookRepository
    {
        readonly MasterDbContext _context;

        public BookRepository(MasterDbContext context)
        {
            _context = context;
        }

        public List<Book> GetAllBook()
        {
            var books = _context.Book.Include(c => c.Catalogs).Include(c => c.Author).ToList();
            return books;
        }

        public List<Book> GetBookById(long id)
        {
            var book = _context.Book.Where(x => x.BookId == id).ToList();
            if (book == null)
            {
                throw new Exception("Book not existed!!");
            }

            return book;
        }

        public async Task<List<Book>> GetBookByAuthorName(string authorName)
        {
            var books = await _context.Book.Where(x => x.Author != null && x.Author.Name == authorName).ToListAsync();
            return books;
        }

        public List<Book> GetBookByCatalogName(string name)
        {
            var checkCatalog = _context.Catalog.FirstOrDefault(x => x.Name == name);
            if (checkCatalog == null) throw new Exception("Catalog not existed!");

            var books = _context.Book.Where(x => x.Catalogs.Contains(checkCatalog)).ToList();
            return books;
        }


        public Book CreatBook(CreatBook request)
        {
            var checkBook = _context.Book.FirstOrDefault(p => p.Name == request.Name);
            if (checkBook != null) throw new Exception("Book existed!!");

            var author = _context.Author.FirstOrDefault(a => a.AuthorId == request.AuthorId);
            if (author == null && request.AuthorId != 0) throw new Exception("Author not existed!!");

            var catalogs = _context.Catalog.Where(a => request.CatalogIds.Contains(a.CatalogId)).ToList();

            Book newBook = new Book
            {
                Name = request.Name,
                PublishDate = request.PublishDate,
                VNName = request.VNName,
                NameNXB = request.NameNXB,
                Author = author,
                Catalogs = catalogs,
                AuthorId = request.AuthorId,
            };
            _context.Book.Add(newBook);
            _context.SaveChanges();
            return newBook;
        }

        public Book UpdateBook(long id, UpdateBook request)
        {
            var book = _context.Book.Include(p => p.Catalogs).FirstOrDefault(p => p.BookId == id);
            if (book == null) throw new Exception("Book not existed");

            var author = _context.Author.FirstOrDefault(p => p.AuthorId == request.AuthorId);
            if (author == null) throw new Exception("Author not Existed!!!");

            var catalogs = _context.Catalog.Where(a => request.CatalogIds.Contains(a.CatalogId)).ToList();

            var books = _context.Book.Select(x => x.Name).ToList();


            book.Name = request.Name;
            if (books.Any(name => request.Name == name))
            {
                throw new Exception("Book Name existed!!!");
            }

            book.PublishDate = request.PublishDate;
            book.VNName = request.VNName;
            book.NameNXB = request.NameNXB;
            book.Author = author;
            book.Catalogs = catalogs;


            _context.SaveChanges();
            return book;
        }

        public Book DeleteBook(long id)
        {
            var checkBook = _context.Book.FirstOrDefault(p => p.BookId == id);
            if (checkBook == null) throw new Exception("Book not existed!!");

            _context.Book.Remove(checkBook);
            _context.SaveChanges();
            return checkBook;
        }
    }
}