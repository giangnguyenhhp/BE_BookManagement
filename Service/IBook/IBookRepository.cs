namespace BookManagement.Service.IBook;

using System.Collections.Generic;
using BookManagement.Models;
using BookManagement.Models.Request.Book;
public interface IBookRepository
{
    public List<Book> GetAllBook();
    public Book CreatBook(CreatBook request);
    public Book UpdateBook(long id, UpdateBook request);
    public Book DeleteBook(long id);

    public Task<List<Book>> GetBookByAuthorName(string authorName);
    public List<Book> GetBookByCatalogName(string name);
    public List<Book> GetBookById(long id);
}