using Lab1.Models;

namespace Lab1.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;


[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly LibrarydbContext _context;

    public BooksController(LibrarydbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [EnableQuery]
    public IActionResult GetBooks()
    {
        return Ok(_context.Books);
    }

    [HttpGet("published/{year}")]
    [EnableQuery]
    public IActionResult GetBooksByYear(int year)
    {
        return Ok(_context.Books.Where(b => b.PublishedYear == year));
    }

    [HttpGet("authors/groupedByYear")]
    [EnableQuery]
    public IActionResult GetAuthorsGroupedByYear()
    {
        var result = _context.Authors
            .Where(a => a.BirthDate.HasValue)
            .GroupBy(a => a.BirthDate.Value.Year)
            .Select(g => new { Year = g.Key, Authors = g.ToList() });

        return Ok(result);
    }

    [HttpGet("authors/groupedByYearAndCountry")]
    [EnableQuery]
    public IActionResult GetAuthorsGroupedByYearAndCountry()
    {
        var result = _context.Authors
            .Where((a => a.BirthDate.HasValue))
            .GroupBy(a => new { a.BirthDate.Value.Year, a.Country })
            .Select(g => new { g.Key.Year, g.Key.Country, Authors = g.ToList() });

        return Ok(result);
    }

    [HttpGet("totalBooks")]
    public IActionResult GetTotalBooks()
    {
        return Ok(_context.Books.Count());
    }

    [HttpGet("paginated")]
    [EnableQuery]
    public IActionResult GetPaginatedBooks()
    {
        return Ok(_context.Books);
    }
}
