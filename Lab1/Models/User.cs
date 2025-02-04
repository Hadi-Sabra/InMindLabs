namespace Lab1.Models;

using System.ComponentModel.DataAnnotations;

public class User
{
    //Validating the attributes of the User's class
    
    [Required(ErrorMessage = "ID is required")]
    public long Id { get; set; }
    
    [Required(ErrorMessage = "Name is required")]
    //This regex expression ensure that the name attribute contains at least 1 non-whitespace character 
    [RegularExpression(@"\S+", ErrorMessage = "Name cannot be empty or contain only spaces.")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Email is required"), EmailAddress(ErrorMessage = "Please provide a valid email address.")]
    public string Email { get; set; }
    
} 