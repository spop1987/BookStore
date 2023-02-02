using BookStore.Models.Dtos;
using BookStore.Models.Entities;

namespace BookStore.Models.Translators
{
    public interface IToDtoTranslator
    {
        PersonDto ToPersonDto(Person personDto);
        ICollection<PersonDto> ToPersonsDto(IEnumerable<Person> persons);

        UserDto ToUserDto(User user);

    }
    public class ToDtoTranslator : IToDtoTranslator
    {
        public ToDtoTranslator()
        {
        }

        public PersonDto ToPersonDto(Person person)
        {
            return new PersonDto
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                ActivityLog = ToActivityLogDto(person)
            };
        }
        public ICollection<PersonDto> ToPersonsDto(IEnumerable<Person> persons)
        {
            throw new NotImplementedException();
        }

        public UserDto ToUserDto(User user)
        {
            // return new UserDto
            // {
            //     FullName = user.FullName,
            //     Password = user.Password,
            //     RefreshToken = user.RefreshToken,
            //     RefreshTokenExpiryTime = user.RefreshTokenExpiryTime,
            //     UserName = user.UserName
            // };
            throw new NotImplementedException();
        }

        private ActivityLogDto ToActivityLogDto(Person person)
        {
            return new ActivityLogDto
            {
                createdBy = person.CreateBy,
                createdDate = person.CreatedDate,
                createdSystem = person.CreatedSystem,
                updatedBy = person.UpdatedBy,
                updatedDate = person.UpdatedDate,
                updatedSystem = person.UpdatedSystem
            };
        }
    }
}