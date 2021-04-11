using Model;

namespace DAL
{
    public interface IProfileRepository
    {
        MasterProfile Select(string code,string password);
    }
}