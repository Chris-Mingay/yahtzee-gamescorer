namespace Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public virtual List<Round> Rounds { get; set; }
}