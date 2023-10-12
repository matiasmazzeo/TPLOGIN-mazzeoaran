using System.Data.SqlClient;
using Dapper;
using System.Collections.Generic;

namespace TP_Login;

public static class BD
{
    private static string _connectionString = @"Server=MATTPC\SQLEXPRESS01;DataBase=DB_LOGIN;Trusted_Connection=True;";

    public static string AgregarUsuario(string Username, string Contrasena, string Nombre, string Email, int Telefono)
    {
        USUARIO VerificarUserName = null;
        string MensajeError = null;
        using (SqlConnection DB = new SqlConnection(_connectionString))
        {
            string SQL = "SELECT * FROM Usuario WHERE Username = @pUsername";
            VerificarUserName = DB.QueryFirstOrDefault<USUARIO>(SQL, new { @pUsername = Username });
            if (VerificarUserName == null)
            {
                SQL = "SELECT * FROM Usuario WHERE Email = @pEmail";
                VerificarUserName = DB.QueryFirstOrDefault<USUARIO>(SQL, new { @pUsername = Username, @pEmail = Email });
                if (VerificarUserName == null)
                {
                    SQL = "INSERT INTO Usuario(Username,Contrasena,Nombre,Email,Telefono) VALUES(@pUsername,@pContrasena,@pNombre,@pEmail,@pTelefono)";
                    DB.Execute(SQL, new { pUsername = Username, pContrasena = Contrasena, pNombre = Nombre, pEmail = Email, pTelefono = Telefono });
                }
                else{
                    MensajeError = "El Email se encuentra en uso, intente Iniciar Sesión o utilizar otro Email";    
                }
            }
            else
            {
                MensajeError = "El Nombre de Usuario se encuentra en uso";
            }
            return MensajeError;
        }
    }
public static USUARIO TraerUsuario(string datoEnviado, string campoEnviado)
{
    USUARIO user = null;
    using (SqlConnection DB = new SqlConnection(_connectionString))
    {
        string SQL = $"SELECT * FROM Usuario WHERE {campoEnviado} = @pdatoEnviado";
        user = DB.QueryFirstOrDefault<USUARIO>(SQL, new { pdatoEnviado = datoEnviado });
        return user;
    }
}
    public static void UpdateContrasena(string UserName, string nuevaContrasena)
    {
        using (SqlConnection DB = new SqlConnection(_connectionString))
        {
            string SQL = "UPDATE Usuario SET Contrasena = @pnuevaContrasena WHERE UserName = @pUserName";
            DB.Execute(SQL, new { pnuevaContrasena = nuevaContrasena, pUserName = UserName });
        }
    }
}