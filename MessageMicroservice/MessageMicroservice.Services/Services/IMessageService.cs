using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MessageMicroservice.Domain.Models;

namespace MessageMicroservice.Services.Services
{
    public interface IMessageService
    {
        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns>List<Message></returns>
        Task<List<Message>> GetAsync();

        /// <summary>
        /// GetByIdAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Message</returns>
        Task<Message> GetByIdAsync(string id);

        /// <summary>
        /// GetByGuidAsync
        /// </summary>
        /// <param name="guid"></param>
        /// <returns>List<Message></returns>
        Task<List<Message>> GetByGuidAsync(Guid guid);

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="message"></param>
        /// <returns>Task</returns>
        Task Insert(Message message);

        /// <summary>
        /// DeleteAll
        /// </summary>
        /// <returns>Task<bool></returns>
        Task<bool> DeleteAll();

        /// <summary>
        /// DeleteById
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Task<bool></returns>
        Task<bool> DeleteById(string id);

        /// <summary>
        /// UpdateByIdAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Task<Message></returns>
        Task<Message> UpdateByIdAsync(string id, Message message);
    }
}