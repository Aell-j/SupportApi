using SupportRequestAPI.Models;

namespace SupportRequestAPI.Services;

public interface ISupportService
{
    Task<LinkedList<SupportRequest>> GetAllAsync();
    Task<SupportRequest> CreateAsync(SupportRequest supportRequest);
    Task<SupportRequest> GetFirstAsync();
    Task<SupportRequest> ProcessFirstAsync();
    Task<int> GetCountAsync();
    Task<bool> DeleteAsync(Guid id);
}