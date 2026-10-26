using SupportRequestAPI.Models;
using SupportRequestAPI.Repositories;

namespace SupportRequestAPI.Services;

public class SupportService : ISupportService
{
    private readonly ISupportRepository _supportRepository;

    public SupportService(ISupportRepository supportRepository)
    {
        _supportRepository = supportRepository;
    }
    public async Task<LinkedList<SupportRequest>> GetAllAsync()
    {
        return await _supportRepository.GetAllAsync();
    }

    public async Task<SupportRequest> CreateAsync(SupportRequest supportRequest)
    {
        if (supportRequest == null)
        {
            throw new ArgumentException("Данные заявки не могут быть null");
        }

        if (string.IsNullOrWhiteSpace(supportRequest.ClientName) ||
            string.IsNullOrWhiteSpace(supportRequest.Description))
        {
            throw new ArgumentException("Заполнение имени и описания обязательны");
        }
        if (string.IsNullOrWhiteSpace(supportRequest.Priority))
            throw new ArgumentException("Приоритет обязателен");

        var requests = await _supportRepository.GetAllAsync();
        supportRequest.Id = Guid.NewGuid();
        supportRequest.CreatedAt = DateTime.UtcNow;
        
        await _supportRepository.AddAsync(supportRequest);
        
        return supportRequest;
    }

    public async Task<SupportRequest> GetFirstAsync()
    {
        var requests = await _supportRepository.GetAllAsync();
        if (requests.Count == 0)
        {
            throw new InvalidOperationException("Заявок нет");
        }
        return requests.First!.Value;
    }

    public async Task<SupportRequest> ProcessFirstAsync()
    {
        var requests = await _supportRepository.GetAllAsync();
        if (requests.Count == 0)
        {
            throw new InvalidOperationException("Заявок нет");
        }
        var request =  requests.First!.Value;
        requests.RemoveFirst();
        
        await _supportRepository.SaveAllAsync(requests);
        return request;
    }

    public async Task<int> GetCountAsync()
    {
        var requests = await _supportRepository.GetAllAsync();
        return requests.Count;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        
        var requests = await _supportRepository.GetAllAsync();
        foreach (var current in requests)
        {
            if (current.Id == id)
            {
                requests.Remove(current);
                await _supportRepository.SaveAllAsync(requests);
                return true;
            }
        }
        return false;
    }
}