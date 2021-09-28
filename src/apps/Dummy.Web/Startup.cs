using CoreApp.Web;
using CoreLib.Builders;
using Microsoft.Extensions.Configuration;

namespace Dummy.Web
{
    public class Startup : StartupBase
    {
        public Startup(IConfiguration configuration) : base(configuration)
        {
        }

        public override void OnSetupApplication(IApplicationSetupBuilder applicationSetup)
        {
            applicationSetup
                // Add Azure AD Authentication from the CorpHost.Libs.Shared.Security.MsIdentity package
                //.AddMsIdentityAuthentication(Configuration)

                // Add app specific services.
                .AddDummyServices();
                
                //.AddMsGraphApi();
        }
    }
}
