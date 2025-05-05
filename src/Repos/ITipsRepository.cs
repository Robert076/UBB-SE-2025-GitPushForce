using System.Collections.Generic;
using Src.Model;

namespace Src.Repos
{
    public interface ITipsRepository
    {
        void GiveUserTipForLowBracket(string userCnp);
        void GiveUserTipForMediumBracket(string userCnp);
        void GiveUserTipForHighBracket(string userCnp);
        List<Tip> GetTipsForGivenUser(string userCnp);
    }
}
