using ValueObjects.ValueObjects;

namespace ValueObjects.IdValueObjects;

public interface IChildScheduleRepository
{
    Task<List<ChildSchedule>> GetByChildIdPrimitive(long id, CancellationToken cancellation);

    Task<List<ChildSchedule>> GetByChildId(ChildId id, CancellationToken cancellation);
}