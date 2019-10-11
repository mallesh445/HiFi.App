using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HiFi.Services
{
    public interface IUserService
    {
        Task<int> TotalUsersCount();
    }
}
