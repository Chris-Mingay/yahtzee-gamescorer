using Application.Rounds.Events;
using MediatR;

namespace Application.Emails.EventHandlers;

public class MaxScoreEventHandler : INotificationHandler<MaxScoreEvent>
{
    public Task Handle(MaxScoreEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Round {notification.RoundId} recorded a maximum score");
        Console.WriteLine("TODO - Send an email");
        return Task.CompletedTask;
    }
}