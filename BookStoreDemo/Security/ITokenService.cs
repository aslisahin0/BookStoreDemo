namespace BookStoreDemo.Security
{
    public interface ITokenService
    {
        JwtToken GenerateToken();

        //Doğrulama için bir metot (true - false olduğu için bool)
        bool ValidateToken(string token);
    }
}
