namespace TicTacToe.Services.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TicTacToe.Data;
    using TicTacToe.Domain.Interfaces;
    using TicTacToe.Models;
    using TicTacToe.Models.User;

    public class UserService : IUserService
    {
        private IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public User GetUser(Guid id)
        {
            return userRepository.GetUser(id);
        }

        public IEnumerable<UserTableModel> SearchUsers(UserQueryModel query)
        {
            return userRepository.SearchUsers(query);
        }

        public Guid UpsertUser(User user)
        {
            if (user.IsNew())
            {
                var userId = userRepository.InsertUser(user);

                return userId;
            }
            else
            {
                userRepository.UpdateUser(user);
            }

            return user.Id.Value;
        }

        public void DeleteUser(Guid id)
        {
            userRepository.DeleteUser(id);
        }

        public User FindUserId(string userName, string password)
        {
            return userRepository.FindUserId(userName, password);
        }

        public IEnumerable<Nomenclature> GetAllRoles()
        {
            var roles = userRepository.GetAllRoles();

            if (roles.Count() != 0)
            {
                return roles;
            }
            else
            {
                return new List<Nomenclature>()
                {
                    new Nomenclature()
                    {
                        Id = Guid.Empty,
                        Name = "Нямате създадени роли"

                    }
                };
            }

        }

        public bool VerifyUserName(string userName)
        {
            return userRepository.VerifyUserName(userName);
        }
    }
}
