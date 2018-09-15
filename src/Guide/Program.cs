using System.Threading.Tasks;

namespace Guide
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await InversionOfControl.Container.GetInstance<Guide>().Run();
        }
    }
}
