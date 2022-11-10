namespace BookManagement.Controllers.BookController;

using Models.Auth;
using Models.Request.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.IBook;
[ApiController] [Route("[controller]")]
public class BookController : ControllerBase
{
    readonly IBookRepository _bookRepository;

    public BookController(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }
    // [Authorize(Roles = SystemPermission.ReadBook)]
    [HttpGet] public IActionResult GetAllBook()
    {
        return Ok(_bookRepository.GetAllBook());
    }
    
    [HttpGet("{id}")] public IActionResult GetBookById(long id)
    {
        return Ok(_bookRepository.GetBookById(id));
    }
    
    // [Authorize(Roles = SystemPermission.ReadBook)]
    [HttpGet("get-book-by-authorName/{authorName}")] 
    public async Task<IActionResult> GetBookByAuthorName(string authorName)
    {
        return Ok(await _bookRepository.GetBookByAuthorName(authorName));
    }
    
    // [Authorize(Roles = SystemPermission.ReadBook)]
    [HttpGet("get-book-by-catalogName/{catalogName}")]
    public IActionResult GetBookByCatalogName(string catalogName)
    {
        return Ok(_bookRepository.GetBookByCatalogName(catalogName));
    }
    
    
    [Authorize(Roles = SystemPermission.CreateBook)]
    [HttpPost] public IActionResult CreatBook([FromBody] CreatBook request)
    {
        return Ok(_bookRepository.CreatBook(request));
    }
    [Authorize(Roles = SystemPermission.UpdateBook)]
    [HttpPut("update-book/{id}")] public IActionResult UpdateBook(long id, [FromBody] UpdateBook request)
    {
        return Ok(_bookRepository.UpdateBook(id, request));
    }
    [Authorize(Roles = SystemPermission.DeleteBook)]
    [HttpDelete("delete-book/{id}")] public IActionResult DeleteBook(long id)
    {
        return Ok(_bookRepository.DeleteBook(id));
    }
}