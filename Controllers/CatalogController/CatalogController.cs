using BookManagement.Service;
using Microsoft.AspNetCore.Mvc;
using BookManagement.Models.Request;
namespace BookManagement.Controllers;

using Microsoft.AspNetCore.Authorization;
using Models.Auth;
using Models.Request.Catalog;
using Service.ICatalog;
[ApiController]
[Route("[controller]")]
public class CatalogController : ControllerBase
{
    private readonly ICatalogRepository _catalogRepository;

    public CatalogController(ICatalogRepository catalogRepository)
    {
        _catalogRepository = catalogRepository;
    }
    [Authorize(Roles = SystemPermission.ReadCatalog)]
    [HttpGet]
    public IActionResult GetAllCatalog()
    {
        return Ok(_catalogRepository.GetAllCatalog());
    }

    // [HttpGet]
    // public IActionResult 
    // {
    //     
    // }
    [Authorize(Roles = SystemPermission.CreateCatalog)]
    [HttpPost]
    public IActionResult CreatCatalog([FromBody] CreatCatalog request)
    {
        return Ok(_catalogRepository.CreatCatalog(request));
    }
    [Authorize(Roles = SystemPermission.UpdateCatalog)]
    [HttpPut("update-catalog/{id}")]
    public IActionResult UpdateCatalog(long id, [FromBody] UpdateCatalog request)
    {
        return Ok(_catalogRepository.UpdateCatalog(id,request));
    }
    [Authorize(Roles = SystemPermission.DeleteCatalog)]
    [HttpDelete("delete-catalog/{id}")]
    public IActionResult DeleteCatalog(long id)
    {
        return Ok(_catalogRepository.DeleteCatalog(id));
    }
}