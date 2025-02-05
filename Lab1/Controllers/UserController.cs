using System.Globalization;
using Lab1.Models;
using Microsoft.AspNetCore.Mvc;
using Lab1.Services;

namespace Lab1.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("GetUsers")]
    public IActionResult GetUsers()
    {
        return Ok(_userService.GetAllUsers());
    }
    
    [HttpGet("{id}")]
    public IActionResult GetUserById(long id)
    {
        try
        {
            var user = _userService.GetUserById(id);
            
            return Ok(user);
            
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        } 
    }

    [HttpGet("name")]
    public IActionResult GetUserByName(string name)
    {
        try
        {
            var user = _userService.GetUsersByName(name);
            
            return Ok(user);
            
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        } 
    }
    
    [HttpPost]
    public IActionResult UpdateUserById([FromBody] UpdateUserRequest newUser)
    {
        try
        {
            var user = _userService.UpdateUser(newUser);
            
            return Ok(user);
            
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        } 
    }
    
    [HttpDelete]
    public IActionResult DeleteUserById(long id)
    {
        try
        {
            var deleted = _userService.DeleteUser(id);
            
            return Ok(deleted);
            
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        } 
    }
    
    [HttpGet("date")]
    public IActionResult GetDate([FromHeader(Name = "Accept-Language")] string language)
    {
        try
        {
            var culture = new System.Globalization.CultureInfo(language);
            return Ok(DateTime.UtcNow.ToString("D", culture));
        }
        catch (CultureNotFoundException)
        {
            return BadRequest("Invalid language format");
        }
    }

    [HttpGet("dateNoInput")]
    public string GetDateByLocale(string lang)
    {
        if (string.IsNullOrWhiteSpace(lang))
            throw new ArgumentException("Language code cannot be empty");
 
        try
        {
            var primaryLang = lang.Split(',')[0].Split('-')[0].Trim();//to take the lang only with no other attributes
 
            var availableCultures = CultureInfo.GetCultures(CultureTypes.AllCultures)
                                               .Select(c => c.TwoLetterISOLanguageName.ToLowerInvariant())
                                               .Distinct()
                                               .ToList();
 
            if (!availableCultures.Contains(primaryLang.ToLowerInvariant()))
                throw new ArgumentException($"Culture '{primaryLang}' is not supported");
 
            var culture = CultureInfo.GetCultureInfo(primaryLang);
            return DateTime.Now.ToString("D", culture);
        }
        catch (CultureNotFoundException)
        {
            throw new ArgumentException($"Invalid culture code: {lang}");
        }
    }

    
    [HttpPost("upload-image")]
    [Consumes("multipart/form-data")]
    public string UploadImage(IFormFile image)
    {
        if (image == null || image.Length == 0)
        {
            throw new ArgumentException("No file uploaded.");
        }

        if (!image.ContentType.StartsWith("image/"))
        {
            throw new ArgumentException("Uploaded file is not an image.");
        }

        try
        {
            
            var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            Directory.CreateDirectory(imagesPath); 
            
            var originalFileName = image.FileName;
            
            var filePath = Path.Combine(imagesPath, originalFileName);
            
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(stream);
            }

            return $"Image uploaded successfully as: {originalFileName}";
        }
        catch (IOException ex)
        {
            throw new InvalidOperationException("Error uploading the image", ex);
        }
    }



}
