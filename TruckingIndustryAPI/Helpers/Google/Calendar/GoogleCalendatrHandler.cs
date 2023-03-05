using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;

using System.Diagnostics;

namespace TruckingIndustryAPI.Helpers.Google.Calendar
{
    public static class GoogleCalendatrHandler
    {
        static string[] Scopes = { CalendarService.Scope.Calendar, DriveService.Scope.Drive };
        static string ApplicationName = "Google Calendar API .NET Quickstart";

        public static void AddEvent(DateTime pStart, DateTime pEnd, string[] pEmails)
        {

        }

        public static void QucikAdd()
        {
            UserCredential credential;

            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Debug.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Google Calendar API service.
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define parameters of request.
            EventsResource.ListRequest request = service.Events.List("primary");
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 10;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            // List events.
            Events events = request.Execute();
            Debug.WriteLine("Upcoming events:");
            if (events.Items != null && events.Items.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    string when = eventItem.Start.DateTime.ToString();
                    if (String.IsNullOrEmpty(when))
                    {
                        when = eventItem.Start.Date;
                    }
                    Debug.WriteLine("{0} ({1})", eventItem.Summary, when);
                }
            }
            else
            {
                Debug.WriteLine("No upcoming events found.");
            }


            var ev = new Event();
            EventDateTime start = new EventDateTime();
            start.DateTime = new DateTime(2023, 2, 26, 10, 0, 0);

            EventDateTime end = new EventDateTime();
            end.DateTime = new DateTime(2023, 2, 26, 10, 30, 0);


            ev.Start = start;
            ev.End = end;
            ev.Summary = "New Event";
            ev.Description = "Description...";
            ev.Attendees = new List<EventAttendee>()
            {
                new EventAttendee{Email = "fuhega45@gmail.com"},
                new EventAttendee{Email = "ulianadolidovich@gmail.com"}
            };

            var calendarId = "primary";
            Event recurringEvent = service.Events.Insert(ev, calendarId).Execute();
            Debug.WriteLine($"Event created {ev.HtmlLink}");
        }
    }
}

