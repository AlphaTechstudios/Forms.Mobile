using Forms.Mobile.Services.Interfaces;
using Prism.Commands;
using Prism.Navigation;

namespace Forms.Mobile.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly INotificationsService notificationsService;

        public DelegateCommand LocalNotificationCommand { get; private set; }

        public MainPageViewModel(INavigationService navigationService, INotificationsService notificationsService)
            : base(navigationService)
        {
            Title = "Main Page";

            LocalNotificationCommand = new DelegateCommand(ShowNotification);
            this.notificationsService = notificationsService;
        }

        private void ShowNotification()
        {
            notificationsService.ShowNotification("New Message", "This is a local notification!");
        }
    }
}
