
namespace ClientModels.Interfaces
{
    public interface IUser
    {
        long UserID { get; set; }
        string Username { get; set; }
        //string Password { get; set; } //hash, not actual password
        UserType UserType { get; set; }

        IUser GetCopy();
    }
}
