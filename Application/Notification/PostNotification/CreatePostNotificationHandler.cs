using MediatR;
using Serilog;

namespace Application.Notification.PostNotification;

public class CreatePostNotificationHandler : INotificationHandler<CreatePostNotification>
{
    public Task Handle(CreatePostNotification notification, CancellationToken cancellationToken)
    {
        Log.Information($"PostId: {notification.Post.Id}\nUserId:{notification.Post.UserId}");
        return Task.CompletedTask;
    }
}
