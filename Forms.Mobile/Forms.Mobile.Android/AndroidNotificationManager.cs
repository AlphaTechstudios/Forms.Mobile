using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroidApp = Android.App.Application;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Forms.Mobile.Middleware;
using Xamarin.Forms;
using Android.Support.V4.App;
using Android.Graphics;

[assembly: Dependency(typeof(Forms.Mobile.Droid.AndroidNotificationManager))]
namespace Forms.Mobile.Droid
{
    public class AndroidNotificationManager : INotificationManager
    {
        private const string defaultChannelId = "LocalNotification";
        private const string defaultChannelName = "Ganeral notification";
        private const string defaultChannelDescription = "This is the ganeral notification for application.";
        private const int pendingIntendId = 0;
        private NotificationManager notificationManager;
        private bool channelInitialized;
        private int messageId;

        public void Initialize()
        {
            CreateNotificationChannel();
        }

        public void ReceiveNotification(string title, string message)
        {
            throw new NotImplementedException();
        }

        public void ScheduleNotification(string title, string message)
        {
            if (!channelInitialized)
            {
                CreateNotificationChannel();
            }

            Intent intent = new Intent(AndroidApp.Context, typeof(MainActivity));
            //Intent deleteIntent = new Intent(AndroidApp.Context, typeof());
            intent.PutExtra("title", title);
            intent.PutExtra("message", message);
            messageId++;

            PendingIntent pendingIntent = PendingIntent.GetActivity(AndroidApp.Context, pendingIntendId, intent, PendingIntentFlags.OneShot);
            NotificationCompat.Builder builder = new NotificationCompat.Builder(AndroidApp.Context, defaultChannelId)
                .SetContentIntent(pendingIntent)
                .SetDefaults(NotificationCompat.DefaultAll)
                .SetContentTitle(title)
                .SetContentText(message)
                .SetLargeIcon(BitmapFactory.DecodeResource(AndroidApp.Context.Resources, Resource.Mipmap.ic_launcher))
                .SetSmallIcon(Resource.Mipmap.ic_launcher)
                .SetAutoCancel(true);

            Notification notification = builder.Build();
            notificationManager.Notify(messageId, notification);
        }

        private void CreateNotificationChannel()
        {
            notificationManager = (NotificationManager)AndroidApp.Context.GetSystemService(AndroidApp.NotificationService);
            
            // If API >= 26 then we need to create a channel for notification.
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var channelNameJava = new Java.Lang.String(defaultChannelName);
                var channel = new NotificationChannel(defaultChannelId, channelNameJava, NotificationImportance.High)
                {
                    Description = defaultChannelDescription
                };

                notificationManager.CreateNotificationChannel(channel);
            }

            channelInitialized = true;

        }
    }
}