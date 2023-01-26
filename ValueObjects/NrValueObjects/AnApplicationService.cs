using ValueObjects.PrimitiveObsession;
using ValueObjects.ValueObjects;

namespace ValueObjects.NrValueObjects;

public sealed class AnApplicationService
{
    private readonly IChildScheduleRepository _childScheduleRepository;

    public AnApplicationService(IChildScheduleRepository childScheduleRepository)
    {
        _childScheduleRepository = childScheduleRepository;
    }

    public async Task<List<ChildSchedule>> Get(PrimitiveChild primitiveChild, CancellationToken cancellation)
    {
        return await _childScheduleRepository.GetByChildNrPrimitive(primitiveChild.Nr, cancellation);
    }

    public async Task<List<ChildSchedule>> Get(Child child, CancellationToken cancellation)
    {
        return await _childScheduleRepository.GetByChildNr(child.Nr, cancellation);
    }
}