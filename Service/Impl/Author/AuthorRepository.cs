namespace BookManagement.Service.Impl.Author
{
    using Models;
    using BookManagement.Models.Request.Author;
    using IAuthor;
    using Microsoft.EntityFrameworkCore;
    public class AuthorRepository : IAuthorRepository
    {
        readonly MasterDbContext _context;

        public AuthorRepository(MasterDbContext context)
        {
            _context = context;
        }

        public List<Author> GetAllAuthor()
        {
            var authors = _context.Author.Include(c => c.Books).ThenInclude(c => c.Catalogs).ToList();

            return authors;
        }
        public async Task<List<Author>> GetAuthorById(long id)
        {
            var author =await _context.Author.Include(c => c.Books).ThenInclude(c => c.Catalogs).Where(x => x.AuthorId == id).ToListAsync();
            ;
            if (author == null) throw new Exception("Author not existed!!");

            return author;
        }

        public Author CreatAuthor(CreatAuthor request)
        {
            var checkAuthor = _context.Author.FirstOrDefault(p => p.Name == request.Name);
            if (checkAuthor != null) throw new Exception("Author existed!!");
        
            var newAuthor = new Author
            {
                Name = request.Name,
                DOB = request.DOB,
                HomeTown = request.HomeTown,
            };
            _context.Author.Add(newAuthor);
            _context.SaveChanges();
            return newAuthor;
        }

        public Author UpdateAuthor(long id, UpdateAuthor request)
        {
            var author = _context.Author
                .Include(p=>p.Books)
                .FirstOrDefault(p => p.AuthorId == id);
            if (author == null) throw new Exception("Author not existed!!");
            
            var books = _context.Book.Where(a => request.BookId.Contains(a.BookId)).ToList();

            author.Name = request.Name;
            author.DOB = request.DOB;
            author.HomeTown = request.HomeTown;
            author.Books = books;
            _context.SaveChanges();
            return author;

        }

        public Author DeleteAuthor(long id)
        {
            var checkAuthor = _context.Author.Include(p => p.Books).FirstOrDefault(p => p.AuthorId == id);
            if (checkAuthor == null) throw new Exception("Author not existed!!");
            _context.Author.Remove(checkAuthor);
            _context.SaveChanges();
            return checkAuthor;
        }
    }
}