using System.Threading.Tasks;
using Guide.Connection;
using Guide.Handlers;
using Guide.Services;

namespace Guide
{
    public class Guide
    {
        private readonly IConnection connection;
        private readonly ICommandHandler commandHandler;
        private readonly ServicesBootstrapper servicesBootstrapper;

        public Guide(IConnection connection, ICommandHandler commandHandler, ServicesBootstrapper servicesBootstrapper)
        {
            this.connection = connection;
            this.commandHandler = commandHandler;
            this.servicesBootstrapper = servicesBootstrapper;
        }

        public async Task Run()
        {
            await connection.Connect();
            await commandHandler.InitializeAsync();
            await Task.Delay(-1);
        }
    }
}
