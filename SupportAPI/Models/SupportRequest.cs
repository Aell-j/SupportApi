namespace SupportRequestAPI.Models;

public class SupportRequest
{
    public Guid Id { get; set; }
    public string ClientName { get; set; }
    public string Description { get; set; }
    public string Priority { get; set; }
    public DateTime CreatedAt { get; set; }
}