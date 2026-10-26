using System.Text.Json;
using System.Text.Json.Serialization;
using SupportRequestAPI.Models;

namespace SupportRequestAPI.Repositories;

public class SupportRepository : ISupportRepository
{
    private LinkedList<SupportRequest> _supportRequests = new();
    
    public Task<LinkedList<SupportRequest>> GetAllAsync()
    {
        return Task.FromResult(new LinkedList<SupportRequest>(_supportRequests));
    }

    public Task AddAsync(SupportRequest supportRequest)
    {
        try
        {
            _supportRequests.AddLast(supportRequest);
            return Task.CompletedTask;
        }
        catch (IOException ex)
        {
            throw new InvalidOperationException("Не удалось сохранить заяку", ex);
        }
    }
    
    public Task SaveAllAsync(LinkedList<SupportRequest> requests)
    {
        _supportRequests = new LinkedList<SupportRequest>(requests);
        return Task.CompletedTask;
    }
}