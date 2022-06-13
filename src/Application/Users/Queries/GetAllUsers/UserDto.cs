namespace Application.Users.Queries.GetAllUsers;

public class UserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public long RoundCount { get; set; }
    public long TotalScore { get; set; }
}