using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using System.Web;

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
        // if (!Regex.IsMatch(host, @"^[a-zA-Z0-9.\-]+$"))
        //     return BadRequest("Invalid host format");
        //
        // var process = new Process();
        // process.StartInfo.FileName = "ping";
        // process.StartInfo.Arguments = host;
        // process.StartInfo.UseShellExecute = false; // не через cmd.exe
        // process.Start();
        //
        // return Ok("Ping executed");
        
        
        Process.Start("cmd.exe", "/c ping " + host);

        return Ok("Ping executed");
    }

    

    // Information disclosure
    [HttpGet("error")]
    public IActionResult Error()
    {
        throw new Exception(
            "Connection string: Server=localhost;User=sa;Password=Password123!");
    }
}