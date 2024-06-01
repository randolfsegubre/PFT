using Personal.Financial.Tracker.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal.Financial.Tracker.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> Authenticate (string userrname, string password);
        Task<IEnumerable<UserDto>> GetAllUsers();
    }
}
