using System.Threading.Tasks;
using BlazorEntre2Ages.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorEntre2Ages.Pages
{
    public class CalendarPage : ComponentBase
    {
        private readonly Task<AuthenticationState> _authenticationStateTask;

        public CalendarPage(Task<AuthenticationState> authenticationStateTask)
        {
            _authenticationStateTask = authenticationStateTask;
        }

        public CalendarPage()
        {
            
        }
        
        public async Task<Event> ConvertToEvent(Appointment a)
        {
            var user = (await _authenticationStateTask).User;
            var @event = new Event()
            {
                EpochStart = a.Start.ToFileTime(),
                EpochEnd = a.End.ToFileTime(),
                Status = a.Status,
                Subject = a.Subject,
                Author = user.Identity.Name,
                Guest = a.GuestEmail
            };
            return @event;
        }
    }
}