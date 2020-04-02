using System;
using System.Collections.Generic;
using System.Text;

namespace Forms.Mobile.Services.Interfaces
{
    public interface INotificationsService
    {
        void ShowNotification(string title, string message);
    }
}
