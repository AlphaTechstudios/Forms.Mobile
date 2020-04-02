using Forms.Mobile.Middleware;
using Forms.Mobile.Services.Interfaces;
using Xamarin.Forms;

namespace Forms.Mobile.Services
{
    public class NotificationsService : INotificationsService
    {
        private readonly INotificationManager notificationManager;
        public NotificationsService()
        {
            notificationManager = DependencyService.Get<INotificationManager>();
        }

        public void ShowNotification(string title, string message)
        {
            notificationManager.ScheduleNotification(title, message);
        }
    }
}
