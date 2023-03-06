using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;

using Microsoft.CodeAnalysis;

using System.Diagnostics;

namespace TruckingIndustryAPI.Helpers.Google.Calendar
{
    public static class GoogleCalendatrHandler
    {
        static readonly string[] Scopes = { CalendarService.Scope.Calendar, DriveService.Scope.Drive };
        static readonly string ApplicationName = "Google Calendar TruckingIndustryAPI";

        public static void AddEvent(DateTime pStart, DateTime pEnd, string[] pEmails, string? pDescription = null, string? pLocation = null)
        {
            UserCredential credential;

            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
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

            var ev = new Event();
            EventDateTime start = new EventDateTime();
            start.DateTime = pStart;

            EventDateTime end = new EventDateTime();
            end.DateTime = pEnd;

            ev.Start = start;
            ev.End = end;
            ev.Summary = "Рейс";
            ev.Description = pDescription;
            ev.Location = pLocation;
            ev.Attendees = new List<EventAttendee>()
            {
                new EventAttendee{Email = "fuhega45@gmail.com"},
                new EventAttendee{Email = "ulianadolidovich@gmail.com"}
            };

            var calendarId = "primary";
            Event recurringEvent = service.Events.Insert(ev, calendarId).Execute();
        }

        public static void QucikAdd()
        {
            

           /* // List events.
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
            }*/


            
           /* Debug.WriteLine($"Event created {ev.HtmlLink}");*/
        }
    }
}

