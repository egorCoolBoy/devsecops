using Microsoft.AspNetCore.Mvc;
using WebApplication5.Data;

namespace WebApplication5.Services;

public interface IStudentService
{
    public Task<List<StudentDto>> GetStudents();
    public Task<StudentDto?> GetStudentById(Guid id);
    public Task<Guid?> AddStudent(CreateStudentDto studentDto);
    public Task<Guid?> UpdateStudent(Guid id, UpdateStudentDto studentDto);
    public Task<Boolean> DeleteStudent(Guid id);
}