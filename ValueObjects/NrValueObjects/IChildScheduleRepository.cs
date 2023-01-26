using ValueObjects.ValueObjects;

namespace ValueObjects.NrValueObjects;

public interface IChildScheduleRepository
{
    Task<List<ChildSchedule>> GetByChildNrPrimitive(long nr, CancellationToken cancellation);

    Task<List<ChildSchedule>> GetByChildNr(ChildNr nr, CancellationToken cancellation);
}