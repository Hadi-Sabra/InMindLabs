using Lab1.Models;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Lab1.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;


[ApiController]
[Route("odata/[controller]")]
public class LibraryController : ODataController
{
    private readonly LibrarydbContext _context;

    public LibraryController(LibrarydbContext context)
    {
        _context = context;
    }

    [EnableQuery]
    [HttpGet("/Books")]
    public IActionResult GetBooks()
    {
        return Ok(_context.Books);
    }

    [EnableQuery]
    [HttpGet("/Authors")]
    public IActionResult GetAuthors()
    {
        return Ok(_context.Authors);
    }

    /*
    * Getting all the books the url used was https://localhost:44390/Books
    * Filtering the books according to a specified year (1925 in this example) descending order the url used was https://localhost:44390/books?$filter=PublishedYear%20eq%201925&$orderby=ReleaseDate%desc
    * Grouping authors by birth year the url used was https://localhost:44390/authors?$apply=groupby((BirthDate), aggregate($count as AuthorCount))&$select=BirthDate,AuthorCount
      it will return the birthdates and number of authors born in the same year
    * For getting all the authors born in the same year and country the url used was https://localhost:44390/authors?$apply=groupby((year(BirthDate),Country))&$select=BirthDate,AuthorCount
      it will return the birthdates,countries and number of authors born in the same year
    * Calculating the number of total books in the library https://localhost:44390/books/$count
    *  Pagination https://localhost:44390/books?$top=10&$skip=20
        where $top is for the page size and $skip is for the page number 
    */
}
