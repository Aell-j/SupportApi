using SupportRequestAPI.Models;

namespace SupportRequestAPI.Repositories;

public interface ISupportRepository
{
    Task<LinkedList<SupportRequest>> GetAllAsync();
    Task AddAsync(SupportRequest supportRequest);
    Task SaveAllAsync(LinkedList<SupportRequest> supportRequests);
}