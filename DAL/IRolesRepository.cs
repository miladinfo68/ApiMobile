using Model;
using System.Collections.Generic;

namespace DAL
{
    public interface IRolesRepository
    {
        UserRole SelectOne(int roleId);
        List<UserRole> SelectAll();
    }
}