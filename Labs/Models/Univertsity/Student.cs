namespace Lab1.Models;

using System.ComponentModel.DataAnnotations;

public class Student
{
    [Required(ErrorMessage = "Student ID is required")]
    public long StudentId { get; set; }  

    [Required(ErrorMessage = "Full Name is required")]
    public string? FullName { get; set; }  

    [Required(ErrorMessage = "Email is required"), EmailAddress(ErrorMessage = "Invalid email format.")]
    public string? Email { get; set; }  
    
    public int FacultyId { get; set; }
    public int Age { get; set; }  
    
    public virtual Faculty? Faculty { get; set; }
}