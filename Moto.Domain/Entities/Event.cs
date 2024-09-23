using Moto.Domain.Base;

namespace Moto.Domain.Entities;

public class Event : BaseEntity
{
    public DateTime OccuredOn { get; set; }

    public string Name { get; set; }

    public string Data { get; set; }

    public Event(string name, string data)
    {
        OccuredOn = DateTime.Now;
        Name = name;
        Data = data;
    }
}
