using AdvancedDatabaseandORMConcepts.Interfaces;

namespace AdvancedDatabaseandORMConcepts.Classes
{
    internal sealed class ExampleScopedService : IExampleScopedService
    {
        Guid IReportServiceLifetime.Id { get; } = Guid.NewGuid();
    }
}
