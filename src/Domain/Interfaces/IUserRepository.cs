using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        User? GetByLogin(string login);
        void Add(User user);
    }
}
