using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Src.Services
{
    public interface IMessagesService
    {
        public void GiveMessageToUser(string userCNP);
    }
}
