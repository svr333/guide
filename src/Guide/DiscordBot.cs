using System.Threading.Tasks;
using Guide.Connection;
using Guide.Handlers;

namespace Guide
{
    public class Guide
    {
        private readonly IConnection connection;
        private readonly ICommandHandler commandHandler;

        public Guide(IConnection connection, ICommandHandler commandHandler)
        {
            this.connection = connection;
            this.commandHandler = commandHandler;
        }

        public async Task Run()
        {
            await connection.Connect();
            await commandHandler.InitializeAsync();
            await Task.Delay(-1);
        }
    }
}
