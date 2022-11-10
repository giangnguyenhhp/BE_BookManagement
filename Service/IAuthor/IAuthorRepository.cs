namespace BookManagement.Service.IAuthor;

using System.Collections.Generic;
using Models;
using Models.Request.Author;
/// <summary>
/// Lớp mô tả các thành phần của Author mà không hiển thị chi tiết thực thi.
/// </summary>
public interface IAuthorRepository
{
    public List<Author> GetAllAuthor();
    public Author CreatAuthor(CreatAuthor request);
    public Author UpdateAuthor(long id, UpdateAuthor request);
    public Author DeleteAuthor(long id);
    public Task<List<Author>> GetAuthorById(long id);
}