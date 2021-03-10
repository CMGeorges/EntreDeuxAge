using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorEntre2Ages.Models;

namespace BlazorEntre2Ages.Services
{
    public interface IEventService
    {
        public Task<List<Event>> GetAll();
        public Task<Event> Create(Event @event);
        public Task<Event> Update(Event @event);
        public Task<bool> Delete(Event @event);
    }
}