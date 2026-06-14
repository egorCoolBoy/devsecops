using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Diagnostics;

namespace WebApplication5.vuln;

[ApiController]
[Route("api/[controller]")]
public class VulnerableController : ControllerBase
{
    // Hardcoded credentials
    private const string ConnectionString =
        "Server=localhost;Database=TestDb;User Id=sa;Password=Password123!;TrustServerCertificate=True";

    // SQL Injection
    [HttpGet("user")]
    public IActionResult GetUser(string username)
    {
        using SqlConnection connection = new SqlConnection(ConnectionString);
        connection.Open();

        string query =
            "SELECT * FROM Users WHERE Username = '" + username + "'";

        SqlCommand command = new SqlCommand(query, connection);

        using SqlDataReader reader = command.ExecuteReader();

        return Ok("Query executed");
    }

    // Command Injection
    [HttpGet("ping")]
    public IActionResult Ping(string host)
    {
        Process.Start("cmd.exe", "/c ping " + host);

        return Ok("Ping executed");
    }

    // Reflected XSS
    [HttpGet("hello")]
    public IActionResult Hello(string name)
    {
        return Content($"<h1>Hello {name}</h1>", "text/html");
    }

    // Information disclosure
    [HttpGet("error")]
    public IActionResult Error()
    {
        throw new Exception(
            "Connection string: Server=localhost;User=sa;Password=Password123!");
    }
}