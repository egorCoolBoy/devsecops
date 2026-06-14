using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WebApplication5.Services;
using WebApplication5.Data;

namespace WebApplication5.Controllers;

[ApiController]
[Route("api/students")]

public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var students = await _studentService.GetStudents();
        return Ok(students);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post(CreateStudentDto createStudentDto)
    {
        var studentId = await _studentService.AddStudent(createStudentDto);
        if (studentId == null)
            return BadRequest();
        return Created($"", new { Id = studentId });
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var student = await _studentService.GetStudentById(id);
        if (student == null)
        {
            return NotFound();
        }
        return Ok(student);
    }


    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(Guid id,[FromBody] UpdateStudentDto studentDto)
    {
        var studentId = await _studentService.UpdateStudent(id, studentDto);
        if  (studentId == null)
        {
            return NotFound();
        }
        return Ok(new {Id = studentId});
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleteCheck = await _studentService.DeleteStudent(id);
        if (!deleteCheck)
            return NotFound();
        

        return Ok();
    }
}