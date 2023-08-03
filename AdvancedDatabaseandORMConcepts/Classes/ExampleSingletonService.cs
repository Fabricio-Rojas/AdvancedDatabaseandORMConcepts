using AdvancedDatabaseandORMConcepts.Interfaces;

namespace AdvancedDatabaseandORMConcepts.Classes
{
    internal class ExampleSingletonService : IExampleSingletonService
    {
        Guid IReportServiceLifetime.Id { get; } = Guid.NewGuid();
    }
}
