using System.Threading.Tasks;
using Guide.Language;

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
