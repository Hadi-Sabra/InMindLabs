namespace Lab1.Models;

using System.ComponentModel.DataAnnotations;
/*
 I made this class to be able to pass the attributes the user wants to update in the "User" model
in a class rather than passing them one by one. In this way the attributes are validated
,and are also optional for the user so he can either change the email or leave it as it is
without it being required 
*/
public class UpdateUserRequest
{
    
 [Required(ErrorMessage = "ID is required")]
 public long Id { get; set; }
    
 [Required(ErrorMessage = "Name is required")]
 //This regex expression ensure that the name attribute contains at least 1 non-whitespace character 
 [RegularExpression(@"\S+", ErrorMessage = "Name cannot be empty or contain only spaces.")]
 public string NewName { get; set; }
    
 [EmailAddress(ErrorMessage = "Please provide a valid email address.")]
 public string? Email { get; set; }
}