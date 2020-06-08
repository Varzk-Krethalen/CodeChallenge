
namespace ClientModels.Interfaces
{
    public interface IUser
    {
        long UserID { get; set; }
        string Username { get; set; }
        UserType UserType { get; set; }

        IUser GetCopy();
    }
}
