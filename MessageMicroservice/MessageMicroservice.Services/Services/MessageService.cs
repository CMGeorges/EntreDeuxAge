using MessageMicroservice.Domain.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MessageMicroservice.Services.Services
{

    public class MessageService
    {
        private readonly IMongoCollection<Message> _messages;

        public MessageService(IMongoDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var db = client.GetDatabase(settings.DatabaseName);
            _messages = db.GetCollection<Message>(settings.CollectionName);

        }

        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns>List<Message></returns>
        public async Task<List<Message>> GetAsync()
        {
            return await _messages.Find(Builders<Message>.Filter.Empty).ToListAsync();
        }

        /// <summary>
        /// GetByIdAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Message</returns>
        public async Task<Message> GetByIdAsync(string id)
        {
            return await _messages.Find(m => m.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// GetByGuidAsync
        /// </summary>
        /// <param name="guid"></param>
        /// <returns>List<Message></returns>
        public async Task<List<Message>> GetByGuidAsync(Guid guid)
        {
            return await _messages.Find(m => m.Author == guid || m.Guest == guid).ToListAsync();
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="message"></param>
        /// <returns>Task</returns>
        public Task Insert(Message message)
        {
            return _messages.InsertOneAsync(message);
        }

        /// <summary>
        /// DeleteAll
        /// </summary>
        /// <returns>Task<bool></returns>
        public async Task<bool> DeleteAll()
        {
            var size = await _messages.EstimatedDocumentCountAsync();
            var result = await _messages.DeleteManyAsync(Builders<Message>.Filter.Empty);
            return await Task.FromResult(result.IsAcknowledged && result.DeletedCount == size);
        }

        /// <summary>
        /// DeleteById
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Task<bool></returns>
        public async Task<bool> DeleteById(string id)
        {
            var result = await _messages.DeleteOneAsync(m => m.Id == id);
            return await Task.FromResult(result.IsAcknowledged && result.DeletedCount > 0);
        }

        /// <summary>
        /// UpdateByIdAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Task<Message></returns>
        public async Task<Message> UpdateByIdAsync(string id, Message message)
        {
            return await _messages.FindOneAndReplaceAsync(m => m.Id == id, message);
        }


    }
}
