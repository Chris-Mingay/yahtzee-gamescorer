namespace Domain.Entities;

public class Round
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime RecordedAt { get; set; }
    public string InputString { get; set; }
    public string Ruleset { get; set; }
    public long Score { get; set; }
    
    public virtual User User { get; set; }
}