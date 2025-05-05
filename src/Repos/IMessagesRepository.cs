using System;
using System.Collections.Generic;
using Src.Model;

namespace Src.Repos
{
    public interface IMessagesRepository
    {
        void GiveUserRandomMessage(string userCnp);
        void GiveUserRandomRoastMessage(string userCnp);
        List<Message> GetMessagesForGivenUser(string userCnp);
    }
}
