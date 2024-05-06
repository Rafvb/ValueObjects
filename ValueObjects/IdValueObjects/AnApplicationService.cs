using ValueObjects.PrimitiveObsession;
using ValueObjects.ValueObjects;

namespace ValueObjects.IdValueObjects;

public sealed class AnApplicationService(IChildScheduleRepository childScheduleRepository)
{
    public async Task<List<ChildSchedule>> Get(PrimitiveChild primitiveChild, CancellationToken cancellation)
    {
        return await childScheduleRepository.GetByChildIdPrimitive(primitiveChild.Id, cancellation);
    }

    public async Task<List<ChildSchedule>> Get(Child child, CancellationToken cancellation)
    {
        return await childScheduleRepository.GetByChildId(child.Id, cancellation);
    }
}