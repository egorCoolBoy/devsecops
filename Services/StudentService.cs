using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;
namespace WebApplication5.Services;

public class StudentService : IStudentService
{
    private StudentContext _dbcontext;

    public StudentService(StudentContext db)
    {
        _dbcontext = db;
    }

    public async Task<List<StudentDto>> GetStudents()
    {
         var students = await _dbcontext.Students.Where(s => s.Name.StartsWith("s")).OrderBy(s => s.Digit).ToListAsync();
        var studentsDto = students.Select(student => new StudentDto
        {
            Id = student.Id,
            Name = student.Name,
            Surname = student.Surname,
            Digit = student.Digit
        }).ToList();
        return studentsDto;
    }

    public async Task<StudentDto?> GetStudentById(Guid id)
    {
        var student = await _dbcontext.Students.FindAsync(id);
        if (student == null)
            return null;
        var studentDto = new StudentDto
        {
            Id = student.Id,
            Name = student.Name,
            Surname = student.Surname,
            Digit = student.Digit
        };
        return studentDto;
    }

    public async Task<Guid?> AddStudent(CreateStudentDto studentDto)
    {
        if (studentDto.Name == null || studentDto.Surname == null ||studentDto.Digit == null)
            return null;
        var student = new Student { Name = studentDto.Name, Surname = studentDto.Surname, Digit = studentDto.Digit };
        await _dbcontext.Students.AddAsync(student);
        await _dbcontext.SaveChangesAsync();
        return student.Id;
    }

    public async Task<Guid?> UpdateStudent(Guid id, UpdateStudentDto studentDto)
    {
        var student = await _dbcontext.Students.FindAsync(id);
        if  (student == null)
        {
            return null;
        }
        student.Name = studentDto.Name ?? student.Name;;
        student.Surname = studentDto.Surname?? student.Surname;

        await _dbcontext.SaveChangesAsync();
        return student.Id;
    }

    public async Task<Boolean> DeleteStudent(Guid id)
    {
        var student = await _dbcontext.Students.FindAsync(id);
        if (student == null)
            return false;
        _dbcontext.Students.Remove(student);
        await _dbcontext.SaveChangesAsync();
        return true;
    }
}