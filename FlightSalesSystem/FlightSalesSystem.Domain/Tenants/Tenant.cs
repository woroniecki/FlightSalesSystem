using FlightSalesSystem.Domain.Abstractions;
using FlightSalesSystem.Domain.Tenants.Enums;

namespace FlightSalesSystem.Domain.Tenants;
public class Tenant : AggregateRoot
{
    private Tenant(Guid id, string name, TenantGroup group) : base(id)
    {
        Name = name;
        Group = group;
    }

    public string Name { get; private set; }
    public TenantGroup Group { get; private set; }

    public static Tenant Create(string name, TenantGroup group)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or empty", nameof(name));
        if (group == null)
            throw new ArgumentNullException(nameof(group), "Group cannot be null");

        return new Tenant(
            id: Guid.NewGuid(),
            name: name,
            group: group);
    }
}
