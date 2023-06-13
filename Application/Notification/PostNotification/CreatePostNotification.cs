using Domain.Models.Entities;
using MediatR;

namespace Application.Notification.PostNotification
{
    public class CreatePostNotification : INotification
    {

        public Post Post { get; set; }
    }
}
