using Microsoft.Xrm.Sdk;
using SalesPlugins.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesPlugins.Controllers
{
    public class LeadController : Controller, IPlugin
    {
        public LeadController()
        {
            ControllerEntitySchemaName = "lead";
        }

        public override void CreateAction_PreValidation(IPluginExecutionContext context, IOrganizationService service, Entity entity)
        {
            throw new NotImplementedException();
        }


        public override void UpdateAction_PostOperation(IPluginExecutionContext context, IOrganizationService service, Entity entity)
        {
            throw new NotImplementedException();
        }

    }
}
