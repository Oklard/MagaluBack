using MagaluAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.SqlClient;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly string _connectionString;

    public LoginController()
    {
        _connectionString = "Data Source=DESKTOP-I6L8BA5;Initial Catalog=Magalu;Integrated Security=True;";
    }

    [HttpPost]
    public IActionResult Login(Usuario usuario)
    {
        if (usuario == null)
        {
            return BadRequest("Solicitação inválida");
        }

        if (ValidarCredenciais(usuario.Username, usuario.Password))
        {
            AdicionarCookieAutenticacao(usuario.Username);

            return Ok();
        }

        return Unauthorized("Credenciais inválidas");
    }

    private bool ValidarCredenciais(string username, string password)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM Usuario WHERE Username = @Username AND Password = @Password";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    int result = (int)command.ExecuteScalar();

                    return result > 0;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao validar credenciais: {ex.Message}");
            return false;
        }
    }

    private void AdicionarCookieAutenticacao(string username)
    {
        var option = new CookieOptions
        {
            Expires = DateTime.Now.AddDays(1)
        };
        Response.Cookies.Append("UsuarioAutenticado", username, option);
    }
}
