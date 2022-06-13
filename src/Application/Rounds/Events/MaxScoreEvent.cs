using MediatR;

namespace Application.Rounds.Events;

/// <summary>
/// Notification sent out when a max score is hit (30 points)
/// </summary>
public class MaxScoreEvent : INotification
{
    public Guid RoundId { get; set; }

    public MaxScoreEvent(Guid roundId)
    {
        RoundId = roundId;
    }
}