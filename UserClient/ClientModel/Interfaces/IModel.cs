using ClientModels.RemoteModelObjects;
using System.Collections.Generic;

namespace ClientModels.Interfaces
{
    public interface IModel
    {
        IUser GetUser(long userId);
        List<IUser> GetUsers();
        bool ValidateUser(RemoteUser user);
        bool AddUser(IUser user, string newPass);
        bool UpdateUserName(long userId, string name);
        bool UpdateUserPass(long userId, string pass);
        bool UpdateUserType(long userId, UserType userType);
        bool DeleteUser(long userId);

        IRanking GetAllChallengeRanking();
        IRanking GetRanking(long challengeID);

        IChallenge GetChallenge(long challengeId);
        List<IChallenge> GetChallenges();
        IChallengeResult ValidateChallenge(long challengeId, string challengeCode);
        bool AddChallenge(IChallenge challenge);
        bool UpdateChallenge(IChallenge challenge);
        bool DeleteChallenge(long challengeId);


        IChallenge NewChallengeInstance();
        IRanking NewRankingInstance(); //TODO: Remove
        ITest NewTestInstance();
        IChallengeResult NewChallengeResultInstance(); //TODO: Remove
        IUser NewUserInstance();
    }
}
