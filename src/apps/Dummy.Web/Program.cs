using CoreApp.Web;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace Dummy.Web
{
    public class Program: ProgramBase<Startup>
    {
        public static async Task<int> Main(string[] args) => await Start(args);

        public static IHostBuilder CreateHostBuilder(string[] args) => CreateWebHostBuilder(args);
    }
}
