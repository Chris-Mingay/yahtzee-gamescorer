using Application.Interfaces;
using Application.Rounds.Events;
using MediatR;

namespace Application.Emails.EventHandlers;

public class MaxScoreEventHandler : INotificationHandler<MaxScoreEvent>
{
    private readonly IEmailerService _emailerService;

    public MaxScoreEventHandler(IEmailerService emailerService)
    {
        _emailerService = emailerService;
    }
    public async Task Handle(MaxScoreEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Round {notification.RoundId} recorded a maximum score");

        await _emailerService.SendEmailAsync(new EmailOptions()
        {
            Subject = "Maximum Score!",
            PlainTextBody = "Wow, that's really something",
            FromAddress = "yahtzee@gamescorer.com",
            FromName = "Chris",
            RecipientAddress = "yahtzeefan@geocities.com",
            RecipientName = "Mr Y Fan"
        }, cancellationToken);
        
    }
}