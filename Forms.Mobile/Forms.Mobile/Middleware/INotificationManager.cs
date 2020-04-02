namespace Forms.Mobile.Middleware
{
    public interface INotificationManager
    {
        void Initialize();
        void ScheduleNotification(string title, string message);
        void ReceiveNotification(string title, string message);
    }
}
