using PAR.Application.Abstractions;

namespace PAR.Infrastructure.Time;

public class Clock : IClock
{
    public DateTime Current()
    {
        return DateTime.UtcNow;
    }
}