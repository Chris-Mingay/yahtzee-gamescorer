namespace Application.Users.Queries.GetUser;

public class RoundDto
{
    public Guid Id { get; set; }
    public DateTime RecordedAt { get; set; }
    public string InputString { get; set; }
    public long Score { get; set; }
}