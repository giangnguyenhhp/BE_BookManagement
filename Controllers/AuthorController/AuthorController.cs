namespace BookManagement.Controllers.AuthorController;

using Models.Request.Author;
using Service.IAuthor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Auth;

[ApiController]
[Route("[controller]")]
public class AuthorController : ControllerBase
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorController(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    [HttpGet]
    [Authorize(Roles= SystemPermission.ReadAuthor)]
    public IActionResult GetAllAuthor()
    {
        return Ok(_authorRepository.GetAllAuthor());
    }
 
    [HttpGet("author/{id}")]
    [Authorize(Roles = "Author.Read")]
    public IActionResult GetAuthor(long id)
    {
        return Ok( _authorRepository.GetAuthorById(id));
    }
    

    [HttpPost]
    [Authorize(Roles = SystemPermission.CreateAuthor)]
    public IActionResult CreatAuthor([FromBody] CreatAuthor request)
    {
        return Ok(_authorRepository.CreatAuthor(request));
    }

    [HttpPut("update-author/{id}")]
    [Authorize(Roles = SystemPermission.UpdateAuthor)]
    public IActionResult UpdateAuthor(long id, [FromBody] UpdateAuthor request)
    {
        return Ok(_authorRepository.UpdateAuthor(id, request));
    }

    [HttpDelete("delete-author/{id}")]
    [Authorize(Roles = SystemPermission.DeleteAuthor)]
    public IActionResult DeleteAuthor(long id)
    {
        return Ok(_authorRepository.DeleteAuthor(id));
    }
    
}