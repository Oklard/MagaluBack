using MagaluAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly string _connectionString;

    public LoginController()
    {
        _connectionString = "Data Source=DESKTOP-I6L8BA5\\SQLEXPRESS;Initial Catalog=Magalu;Integrated Security=true;";
    }

    [HttpPost]
    public IActionResult Login(Usuario usuario)
    {
        if (usuario == null)
        {
            return BadRequest("Solicitação inválida");
        }

        if (usuario.Username == "Magalu" && usuario.Password == "m@galu123")
        {
            return Ok();
        }

        return Unauthorized("Credenciais inválidas");
    }

}
