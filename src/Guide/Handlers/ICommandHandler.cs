using System.Threading.Tasks;

namespace Guide.Handlers
{
    public interface ICommandHandler
    {
        Task InitializeAsync();
    }
}
