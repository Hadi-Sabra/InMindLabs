namespace Lab1.Models;

public class Faculty
{
    public int FacultyId { get; set; }
    
    public string FacultyName { get; set; }
    
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}