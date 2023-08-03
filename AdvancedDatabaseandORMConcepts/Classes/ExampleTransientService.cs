using AdvancedDatabaseandORMConcepts.Interfaces;

namespace AdvancedDatabaseandORMConcepts.Classes
{
    internal sealed class ExampleTransientService : IExampleTransientService
    {
        Guid IReportServiceLifetime.Id { get; } = Guid.NewGuid();
    }
}
