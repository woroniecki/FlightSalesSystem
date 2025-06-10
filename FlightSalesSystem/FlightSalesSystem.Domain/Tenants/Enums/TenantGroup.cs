using FlightSalesSystem.Domain.Abstractions;

namespace FlightSalesSystem.Domain.Tenants.Enums;
public sealed class TenantGroup : Enumeration<TenantGroup>
{
    private TenantGroup(int value, string name) : base(value, name) { }

    public static readonly TenantGroup A = new(0, nameof(A));
    public static readonly TenantGroup B = new(1, nameof(B));
}
