using Domain.Entities;
using Domain.Interfaces;
using Repository.Context;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public User GetByLogin(string login)
        {
            return _context.User.FirstOrDefault(p => p.Login == login);
        }

        public void Add(User user)
        {
            _context.User.Add(user);
            _context.SaveChanges();
        }
    }
}
