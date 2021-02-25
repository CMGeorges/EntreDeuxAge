using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using BlazorEntre2Ages.Models;

namespace BlazorEntre2Ages.Services
{
    public class MessageService : IMessageService
    {
        private List<Message> messages;

        public List<Message> Messages
        {
            get { return messages; }
            set { messages = value; }
        }

        public MessageService()
        {
            Messages = new List<Message>() { };
        }

        public event Func<List<Message>,Task> OnChangeAsync;

        public void HandleMessage(Message message)
        {
            Messages.Add(message);
            this.OnChangeAsync?.Invoke(Messages.TakeLast(10).ToList());
        }

        public async Task<List<Message>> GetMessagesAsync()
        {
            return await Task.FromResult(Messages);
        }
    }
}
