namespace Lab1.Models;

public class StudentsCourses
{
    public int StudentsCoursesId { get; set; }
    
    public int CourseId { get; set; }
    
    public int StudentId { get; set; }
    
    public virtual Student? Student { get; set; }
    
    public virtual Course? Course { get; set; }
}