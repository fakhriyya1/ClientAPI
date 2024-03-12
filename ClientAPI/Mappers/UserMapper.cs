using ClientAPI.DTOs;
using ClientAPI.Models;

namespace ClientAPI.Mappers
{
    public static class UserMapper
    {
        public static User ToUser(this UserDto user)
        {
            return new User
            {
                Age = user.Age,
                Name = user.Name,
                Surname = user.Surname
            };
        }
    }
}
