namespace EventBus.Messages.Events;

public class BaseIntegrationEvent(Guid correlationId, DateTime creationDate)
{
    public Guid CorrelationId { get; set; } = correlationId;
    public DateTime CreationDate { get; set; } = creationDate;

    public BaseIntegrationEvent() : this(Guid.NewGuid(), DateTime.UtcNow) { }
}
