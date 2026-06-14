namespace WebApplication5.Data;

public class UpdateStudentDto
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int? Digit { get; set; }
}

public class CreateStudentDto
{
    public Guid? Id { get; set; } 
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int Digit { get; set; }
}      

public class StudentDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Digit { get; set; }
}

