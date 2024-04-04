using ValueObjects.PrimitiveObsession;
using ValueObjects.ValueObjects;

namespace ValueObjects.NrValueObjects;

public sealed class AnApplicationService(IChildScheduleRepository childScheduleRepository)
{
    public async Task<List<ChildSchedule>> Get(PrimitiveChild primitiveChild, CancellationToken cancellation)
    {
        return await childScheduleRepository.GetByChildNrPrimitive(primitiveChild.Nr, cancellation);
    }

    public async Task<List<ChildSchedule>> Get(Child child, CancellationToken cancellation)
    {
        return await childScheduleRepository.GetByChildNr(child.Nr, cancellation);
    }
}