using InnoApi.Dtos.Users;
using InnoApi.Models;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace InnoApi.Mappers
{
    public static class UserMappers
    {
        public static UserDto ToUserDto(this User userModel)
        {
            return new UserDto
            {
                ID = userModel.ID,
                ADI = userModel.ADI,
                SOYADI = userModel.SOYADI,
                KULLANICI_ADI = userModel.KULLANICI_ADI,
            };
        }
        public static User ToUserFromCreateDTO(this CreateUserRequestDto userDto)
        {
            return new User
            {
                ID = userDto.ID,
                ADI = userDto.ADI,
                SOYADI = userDto.SOYADI,
                KULLANICI_ADI = userDto.KULLANICI_ADI,
            };
        }

        
    }
}
