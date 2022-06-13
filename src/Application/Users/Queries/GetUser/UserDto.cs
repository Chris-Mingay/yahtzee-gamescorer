namespace Application.Users.Queries.GetUser;

public class UserDto : GetAllUsers.UserDto
{
    public List<RoundDto> Rounds { get; set; }
}