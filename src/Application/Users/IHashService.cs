namespace OOP_WebApp.Application.Users;

public interface IHashService
{
    byte[] GenerateSalt();
    string HashPassword(string password, byte[] salt);
}