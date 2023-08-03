namespace AdvancedDatabaseandORMConcepts.Interfaces
{
    public interface IReportServiceLifetime
    {
        Guid Id { get; }
        ServiceLifetime Lifetime { get; }
    }
}
