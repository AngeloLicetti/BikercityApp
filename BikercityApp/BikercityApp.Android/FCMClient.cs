using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.App;
using Android.Util;
using BikercityApp.Helpers;
using Firebase.Iid;
using Firebase.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BikercityApp.Droid
{
	public static class Constants
	{
		public const string ListenConnectionString2 = "Endpoint=sb://fcmnamespace.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=ogrb/sMMCMUn6VSFKHjq/YOQj6o0vW2pSc8pcjUrUec=";
		public const string NotificationHubName2 = "fcm_notification_hub";
	}

	[Service]
	[IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
	public class MyFirebaseIIDService : FirebaseInstanceIdService
	{
		private const string TAG = "MyFirebaseIIDService";

		public override async void OnTokenRefresh()
		{
			var refreshedToken = FirebaseInstanceId.Instance.Token;
			BikercityApp.Helpers.Settings.FCMToken = refreshedToken;
			if (Settings.idCliente > 0)
			{
				await NotificationHelper.registerToken(refreshedToken);
			}
			//Log.Debug(TAG, "Refreshed token: " + refreshedToken);
			base.OnTokenRefresh();
		}
		//public void RegistrationOnHubServerAsync(string token, List<string> tags)
		//{
		//	try
		//	{

		//		// Register with Notification Hubs
		//		hub = new NotificationHub(Constants.NotificationHubName,
		//								  Constants.ListenConnectionString, Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity);

		//		string Body = "{\"aps\":{\"category\":\"CATEGORY\",\"alert\":\"$(message)\",\"sound\":\"default\"},\"data\":{\"message\":\"$(messageAndroid)\"}}";
		//		var regID = hub.RegisterTemplate(token, "tag", Body, tags.ToArray()).RegistrationId;

		//		//Log.Debug(TAG, $"Successful registration of ID {regID}");

		//	}
		//	catch (Exception ex)
		//	{
		//	}
		//}

		//public override async void OnTokenRefresh()
		//{
		//	var refreshedToken = FirebaseInstanceId.Instance.Token;
		//	Log.Debug(TAG, "Refreshed token: " + refreshedToken);
		//	var push = TodoItemManager.DefaultManager.CurrentClient.GetPush();
		//	await SendRegistrationToServerAsync(refreshedToken, push.InstallationId);
		//	base.OnTokenRefresh();
		//}

		//private async Task SendRegistrationToServerAsync(string token, string installationId)
		//{
		//	Settings.FCMToken = token;
		//	Settings.InstallationIdAndroid = installationId;
		//	if (!String.IsNullOrEmpty(Settings.UserId))
		//	{
		//		Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
		//		{
		//			NotificationHelper.SendInstallationAndroid();
		//		});
		//	}
		//}
	}
	public class NotificationModel
	{
		public string NotificationType { get; set; }
		public string To { get; set; }
		public string ToName { get; set; }
		public string From { get; set; }
		public string FromName { get; set; }
		public string Subject { get; set; }
		public string Message { get; set; }

		//registrar dispositivo notification hub
		public string UserId { get; set; }
		public string InstallationId { get; set; }
		public string Handle { get; set; }
		public List<string> Tags { get; set; }
		public string Platform { get; set; }
	}


	[Service]
	[BroadcastReceiver]
	[IntentFilter(new[] {
	  Android.Content.Intent.ActionBootCompleted})]
	[IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
	public class MyFirebaseMessagingService : FirebaseMessagingService
	{
		private const string TAG = "MyFirebaseMsgService";

		public override void OnMessageReceived(RemoteMessage message)
		{
			Log.Debug(TAG, "From: " + message.From);
			if (message.GetNotification() != null)
			{
				//These is how most messages will be received
				Log.Debug(TAG, "Notification Message Body: " + message.GetNotification().Body);
				SendNotification(message.GetNotification().Body, message.GetNotification().Title, null);
			}
			else
			{
				//Only used for debugging payloads sent from the Azure portal
				SendNotification(message.Data.Values.First(), null, null);
			}
		}

		private void SendNotification(string messageBody)
		{
			var intent = new Intent(this, typeof(MainActivity));
			intent.AddFlags(ActivityFlags.ClearTop);
			var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);

			var notificationBuilder = new Notification.Builder(this)
						.SetContentTitle("FCM Message")
						.SetSmallIcon(Resource.Drawable.ic_event)
						.SetContentText(messageBody)
						.SetAutoCancel(true)
						.SetContentIntent(pendingIntent);

			var notificationManager = NotificationManager.FromContext(this);

			notificationManager.Notify(0, notificationBuilder.Build());
		}

		//public override void OnMessageReceived(RemoteMessage message)
		//{
		//    try
		//    {
		//        var body = message.Data["message"];
		//        SendNotification(body, message.Data);
		//    }
		//    catch (Exception)
		//    {
		//    }
		//}

		private void SendNotification(string messageBody, string title, IDictionary<string, string> data)
		{
			createNotification(title, messageBody);
		}

		private NotificationManager notifManager;
		private static int NOTIFY_ID = 0;

		private void createNotification(string title, string desc)
		{
			try
			{
				//Create an intent to show ui
				var intent = new Intent(this.ApplicationContext, typeof(MainActivity));
				intent.PutExtra("NotificationModelKey", "");

				//Create notification
				string id = "1";
				Intent intent2;
				PendingIntent pendingIntent2;
				NotificationCompat.Builder builder2;
				if (notifManager == null)
				{
					notifManager = (NotificationManager)GetSystemService(Context.NotificationService);
				}
				if (Build.VERSION.SdkInt >= Build.VERSION_CODES.O)
				{
					var importance = NotificationManager.ImportanceHigh;
					NotificationChannel mChannel = notifManager.GetNotificationChannel(id);
					if (mChannel == null)
					{
						mChannel = new NotificationChannel(id, title, importance);
						mChannel.EnableVibration(true);
						mChannel.SetVibrationPattern(new long[] { 100, 200, 300, 400, 500, 400, 300, 200, 400 });
						notifManager.CreateNotificationChannel(mChannel);
					}
					builder2 = new NotificationCompat.Builder(this.ApplicationContext, id);
					intent2 = new Intent(this.ApplicationContext, typeof(MainActivity));
					intent2.SetFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);
					pendingIntent2 = PendingIntent.GetActivity(this.ApplicationContext, 0, intent2, 0);
					builder2.SetContentTitle(title)  // required
						   .SetSmallIcon(Resource.Mipmap.icon) // required
						   .SetContentText(desc)  // required
						   .SetDefaults((int)NotificationDefaults.All)
						   .SetAutoCancel(true)
						   .SetOnlyAlertOnce(true)
						   .SetLargeIcon(BitmapFactory.DecodeResource(this.Resources, Resource.Mipmap.icon_round))
						   .SetContentIntent(pendingIntent2)
						   .SetPriority((int)Notification.PriorityHigh)
						   .SetStyle(new NotificationCompat.BigTextStyle().BigText(desc))
						   .SetVibrate(new long[] { 100, 200, 300, 400, 500, 400, 300, 200, 400 });

				}
				else
				{
					builder2 = new NotificationCompat.Builder(this.ApplicationContext);
					intent2 = new Intent(this.ApplicationContext, typeof(MainActivity));
					intent2.SetFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);
					pendingIntent2 = PendingIntent.GetActivity(this.ApplicationContext, 0, intent2, 0);
					builder2.SetContentTitle(title)                // required
						   .SetSmallIcon(Resource.Mipmap.icon) // required
						   .SetContentText(desc)  // required
						   .SetOnlyAlertOnce(true)
						   .SetDefaults((int)NotificationDefaults.All)
						   .SetLargeIcon(BitmapFactory.DecodeResource(this.Resources, Resource.Mipmap.icon_round))
						   .SetAutoCancel(true)
						   .SetContentIntent(pendingIntent2)
						   .SetStyle(new NotificationCompat.BigTextStyle().BigText(desc))
						   .SetVibrate(new long[] { 100, 200, 300, 400, 500, 400, 300, 200, 400 })
						   .SetPriority((int)Notification.PriorityHigh);
				}
				Notification notification2 = builder2.Build();
				notifManager.Notify(NOTIFY_ID, notification2);
				NOTIFY_ID++; // ID of notification
			}
			catch (Exception ex)
			{
				//Do nothing
			}
		}
	}
}






