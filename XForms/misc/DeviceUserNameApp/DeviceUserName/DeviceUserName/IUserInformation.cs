using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DeviceUserName
{
    public interface IUserInformation
    {
        Task<string> GetUserName();
    }
}
