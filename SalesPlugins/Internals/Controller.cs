using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesPlugins.Internals
{
    public class Controller
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

            if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity)
            {
                Entity entity = (Entity)context.InputParameters["Target"];

                IOrganizationServiceFactory serviceFactory =
                    (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
                IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

                try
                {
                    if (context.MessageName == PluginMessages.Create.ToString())
                    {
                        Create(context, service, entity);
                    }
                    else if (context.MessageName == PluginMessages.Update.ToString())
                    {
                        Update(context, service, entity);
                    }
                    else
                    {
                        Default(context, service, entity);
                    }
                }
                catch (Exception ex)
                {
                    throw new InvalidPluginExecutionException(ex.Message);
                }
            }
            else if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is EntityReference)
            {
                EntityReference entityReference = (EntityReference)context.InputParameters["Target"];

                IOrganizationServiceFactory serviceFactory =
                    (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
                IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

                try
                {
                    if (context.MessageName == PluginMessages.Delete.ToString())
                    {
                        Delete(context, service, entityReference);
                    }
                    else if (context.MessageName == PluginMessages.Associate.ToString())
                    {
                        Associate(context, service, entityReference);
                    }
                    else if (context.MessageName == PluginMessages.Disassociate.ToString())
                    {
                        Disassociate(context, service, entityReference);
                    }
                    else if (context.MessageName == PluginMessages.Retrieve.ToString())
                    {
                        Retrieve(context, service, entityReference);
                    }
                    else if (context.MessageName == PluginMessages.Assign.ToString())
                    {
                        Assign(context, service, entityReference);
                    }
                    else if (context.MessageName == PluginMessages.GrantAccess.ToString())
                    {
                        GrantAccess(context, service, entityReference);
                    }
                    else if (context.MessageName == PluginMessages.RevokeAccess.ToString())
                    {
                        RevokeAccess(context, service, entityReference);
                    }
                    else if (context.MessageName == PluginMessages.ModifyAccess.ToString())
                    {
                        ModifyAccess(context, service, entityReference);
                    }
                    else
                    {
                        Default(context, service, entityReference);
                    }
                }
                catch (Exception ex)
                {
                    throw new InvalidPluginExecutionException(ex.Message);
                }
            }
            else if (context.InputParameters.Contains("Query") && context.InputParameters["Query"] is QueryExpression)
            {
                QueryExpression queryExpression = (QueryExpression)context.InputParameters["Query"];
                IOrganizationServiceFactory serviceFactory =
                    (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
                IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);
                try
                {
                    if (context.MessageName == PluginMessages.RetrieveMultiple.ToString())
                    {
                        RetrieveMultiple(context, service, queryExpression);
                    }
                    else
                    {
                        throw new InvalidPluginExecutionException("The specified step is not available for this EntryPoint");
                    }
                }
                catch (Exception ex)
                {

                    throw new InvalidPluginExecutionException(ex.Message);
                }
            }
            else
            {
                IOrganizationServiceFactory serviceFactory =
                    (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
                IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

                Default(context, service);
            }
        }

        public void Create(IPluginExecutionContext context, IOrganizationService service, Entity entity)
        {
            if (context.Stage == (int)PluginStage.PreValidation)
            {
                CreateAction_PreValidation(context, service, entity);
            }
            else if (context.Stage == (int)PluginStage.PreOperation)
            {
                CreateAction_PreOperation(context, service, entity);
            }
            else if (context.Stage == (int)PluginStage.PostOperation)
            {
                CreateAction_PostOperation(context, service, entity);
            }
        }

        public void Delete(IPluginExecutionContext context, IOrganizationService service, EntityReference entityReference)
        {
            if (context.Stage == (int)PluginStage.PreValidation)
            {
                DeleteAction_PreValidation(context, service, entityReference);
            }
            else if (context.Stage == (int)PluginStage.PreOperation)
            {
                DeleteAction_PreOperation(context, service, entityReference);
            }
            else if (context.Stage == (int)PluginStage.PostOperation)
            {
                DeleteAction_PostOperation(context, service, entityReference);
            }
        }

        public void Retrieve(IPluginExecutionContext context, IOrganizationService service, EntityReference entityReference)
        {
            if (context.Stage == (int)PluginStage.PreValidation)
            {
                RetrieveAction_PreValidation(context, service, entityReference);
            }
            else if (context.Stage == (int)PluginStage.PreOperation)
            {
                RetrieveAction_PreOperation(context, service, entityReference);
            }
            else if (context.Stage == (int)PluginStage.PostOperation)
            {
                RetrieveAction_PostOperation(context, service, entityReference);
            }
        }

        public void RevokeAccess(IPluginExecutionContext context, IOrganizationService service, EntityReference entityReference)
        {
            if (context.Stage == (int)PluginStage.PreValidation)
            {
                RevokeAccessAction_PreValidation(context, service, entityReference);
            }
            else if (context.Stage == (int)PluginStage.PreOperation)
            {
                RevokeAccessAction_PreOperation(context, service, entityReference);
            }
            else if (context.Stage == (int)PluginStage.PostOperation)
            {
                RevokeAccessAction_PostOperation(context, service, entityReference);
            }
        }

        public void GrantAccess(IPluginExecutionContext context, IOrganizationService service, EntityReference entityReference)
        {
            if (context.Stage == (int)PluginStage.PreValidation)
            {
                GrantAccessAction_PreValidation(context, service, entityReference);
            }
            else if (context.Stage == (int)PluginStage.PreOperation)
            {
                GrantAccessAction_PreOperation(context, service, entityReference);
            }
            else if (context.Stage == (int)PluginStage.PostOperation)
            {
                GrantAccessAction_PostOperation(context, service, entityReference);
            }
        }

        public void ModifyAccess(IPluginExecutionContext context, IOrganizationService service, EntityReference entityReference)
        {
            if (context.Stage == (int)PluginStage.PreValidation)
            {
                ModifyAccessAction_PreValidation(context, service, entityReference);
            }
            else if (context.Stage == (int)PluginStage.PreOperation)
            {
                ModifyAccessAction_PreOperation(context, service, entityReference);
            }
            else if (context.Stage == (int)PluginStage.PostOperation)
            {
                ModifyAccessAction_PostOperation(context, service, entityReference);
            }
        }

        public void RetrieveMultiple(IPluginExecutionContext context, IOrganizationService service, QueryExpression queryExpression)
        {
            if (context.Stage == (int)PluginStage.PreValidation)
            {
                RetrieveMultipleAction_PreValidation(context, service, queryExpression);
            }
            else if (context.Stage == (int)PluginStage.PreOperation)
            {
                RetrieveMultipleAction_PreOperation(context, service, queryExpression);
            }
            else if (context.Stage == (int)PluginStage.PostOperation)
            {
                RetrieveMultipleAction_PostOperation(context, service, queryExpression);
            }
        }

        public void Update(IPluginExecutionContext context, IOrganizationService service, Entity entity)
        {
            if (context.Stage == (int)PluginStage.PreValidation)
            {
                UpdateAction_PreValidation(context, service, entity);
            }
            else if (context.Stage == (int)PluginStage.PreOperation)
            {
                UpdateAction_PreOperation(context, service, entity);
            }
            else if (context.Stage == (int)PluginStage.PostOperation)
            {
                UpdateAction_PostOperation(context, service, entity);
            }
        }

        public void Associate(IPluginExecutionContext context, IOrganizationService service, EntityReference entityReference)
        {
            if (context.Stage == (int)PluginStage.PreValidation)
            {
                AssociateAction_PreValidation(context, service, entityReference);
            }
            else if (context.Stage == (int)PluginStage.PreOperation)
            {
                AssociateAction_PreOperation(context, service, entityReference);
            }
            else if (context.Stage == (int)PluginStage.PostOperation)
            {
                AssociateAction_PostOperation(context, service, entityReference);
            }
        }

        public void Assign(IPluginExecutionContext context, IOrganizationService service, EntityReference entityReference)
        {
            if (context.Stage == (int)PluginStage.PreValidation)
            {
                AssignAction_PreValidation(context, service, entityReference);
            }
            else if (context.Stage == (int)PluginStage.PreOperation)
            {
                AssignAction_PreOperation(context, service, entityReference);
            }
            else if (context.Stage == (int)PluginStage.PostOperation)
            {
                AssignAction_PostOperation(context, service, entityReference);
            }
        }

        public void Disassociate(IPluginExecutionContext context, IOrganizationService service, EntityReference entityReference)
        {
            if (context.Stage == (int)PluginStage.PreValidation)
            {
                AssignAction_PreValidation(context, service, entityReference);
            }
            else if (context.Stage == (int)PluginStage.PreOperation)
            {
                AssignAction_PreOperation(context, service, entityReference);
            }
            else if (context.Stage == (int)PluginStage.PostOperation)
            {
                AssignAction_PostOperation(context, service, entityReference);
            }
            DisassociateAction(context, service, entityReference);
        }

        public void Default(IPluginExecutionContext context, IOrganizationService service, EntityReference entityReference)
        {
            if (context.Stage == (int)PluginStage.PreValidation)
            {
                DefaultAction_PreValidation(context, service, entityReference);
            }
            else if (context.Stage == (int)PluginStage.PreOperation)
            {
                DefaultAction_PreOperation(context, service, entityReference);
            }
            else if (context.Stage == (int)PluginStage.PostOperation)
            {
                DefaultAction_PostOperation(context, service, entityReference);
            }

        }

        public void Default(IPluginExecutionContext context, IOrganizationService service, Entity entity)
        {
            if (context.Stage == (int)PluginStage.PreValidation)
            {
                DefaultAction_PreValidation(context, service, entity);
            }
            else if (context.Stage == (int)PluginStage.PreOperation)
            {
                DefaultAction_PreOperation(context, service, entity);
            }
            else if (context.Stage == (int)PluginStage.PostOperation)
            {
                DefaultAction_PostOperation(context, service, entity);
            }
        }

        public void Default(IPluginExecutionContext context, IOrganizationService service)
        {
            DefaultAction(context, service);
        }








        public virtual void CreateAction_PreValidation(IPluginExecutionContext context, IOrganizationService service, Entity entity)
        {
        }

        public virtual void CreateAction_PreOperation(IPluginExecutionContext context, IOrganizationService service, Entity entity)
        {
        }

        public virtual void CreateAction_PostOperation(IPluginExecutionContext context, IOrganizationService service, Entity entity)
        {
        }

        public virtual void UpdateAction_PreValidation(IPluginExecutionContext context, IOrganizationService service, Entity entity)
        {
        }

        public virtual void UpdateAction_PreOperation(IPluginExecutionContext context, IOrganizationService service, Entity entity)
        {
        }

        public virtual void UpdateAction_PostOperation(IPluginExecutionContext context, IOrganizationService service, Entity entity)
        {
        }

        public virtual void DeleteAction_PreValidation(IPluginExecutionContext context, IOrganizationService service, EntityReference entityReference)
        {
        }

        public virtual void DeleteAction_PreOperation(IPluginExecutionContext context, IOrganizationService service, EntityReference entityReference)
        {
        }

        public virtual void DeleteAction_PostOperation(IPluginExecutionContext context, IOrganizationService service, EntityReference entityReference)
        {
        }

        public virtual void AssociateAction_PreValidation(IPluginExecutionContext context, IOrganizationService service, EntityReference entityReference)
        {
        }

        public virtual void AssociateAction_PreOperation(IPluginExecutionContext context, IOrganizationService service, EntityReference entityReference)
        {
        }

        public virtual void AssociateAction_PostOperation(IPluginExecutionContext context, IOrganizationService service, EntityReference entityReference)
        {
        }

        public virtual void DisassociateAction(IPluginExecutionContext context, IOrganizationService service, EntityReference entityReference)
        {
        }

        public virtual void RetrieveAction_PreValidation(IPluginExecutionContext context, IOrganizationService service, EntityReference entityReference)
        {
        }

        public virtual void RetrieveAction_PreOperation(IPluginExecutionContext context, IOrganizationService service, EntityReference entityReference)
        {
        }

        public virtual void RetrieveAction_PostOperation(IPluginExecutionContext context, IOrganizationService service, EntityReference entityReference)
        {
        }

        public virtual void RetrieveMultipleAction_PreValidation(IPluginExecutionContext context, IOrganizationService service, QueryExpression queryExpression)
        {
        }

        public virtual void RetrieveMultipleAction_PreOperation(IPluginExecutionContext context, IOrganizationService service, QueryExpression queryExpression)
        {
        }

        public virtual void RetrieveMultipleAction_PostOperation(IPluginExecutionContext context, IOrganizationService service, QueryExpression queryExpression)
        {
        }

        public virtual void AssignAction_PreValidation(IPluginExecutionContext context, IOrganizationService service, EntityReference entityReference)
        {
        }

        public virtual void AssignAction_PreOperation(IPluginExecutionContext context, IOrganizationService service, EntityReference entityReference)
        {
        }

        public virtual void AssignAction_PostOperation(IPluginExecutionContext context, IOrganizationService service, EntityReference entityReference)
        {
        }

        public virtual void RevokeAccessAction_PreValidation(IPluginExecutionContext context, IOrganizationService service, EntityReference entityReference)
        {
        }

        public virtual void RevokeAccessAction_PreOperation(IPluginExecutionContext context, IOrganizationService service, EntityReference entityReference)
        {
        }

        public virtual void RevokeAccessAction_PostOperation(IPluginExecutionContext context, IOrganizationService service, EntityReference entityReference)
        {
        }

        public virtual void GrantAccessAction_PreValidation(IPluginExecutionContext context, IOrganizationService service, EntityReference entityReference)
        {
        }

        public virtual void GrantAccessAction_PreOperation(IPluginExecutionContext context, IOrganizationService service, EntityReference entityReference)
        {
        }

        public virtual void GrantAccessAction_PostOperation(IPluginExecutionContext context, IOrganizationService service, EntityReference entityReference)
        {
        }

        public virtual void ModifyAccessAction_PreValidation(IPluginExecutionContext context, IOrganizationService service, EntityReference entityReference)
        {
        }

        public virtual void ModifyAccessAction_PreOperation(IPluginExecutionContext context, IOrganizationService service, EntityReference entityReference)
        {
        }

        public virtual void ModifyAccessAction_PostOperation(IPluginExecutionContext context, IOrganizationService service, EntityReference entityReference)
        {
        }

        public virtual void DefaultAction_PreValidation(IPluginExecutionContext context, IOrganizationService service, EntityReference entityReference)
        {
        }

        public virtual void DefaultAction_PreOperation(IPluginExecutionContext context, IOrganizationService service, EntityReference entityReference)
        {
        }

        public virtual void DefaultAction_PostOperation(IPluginExecutionContext context, IOrganizationService service, EntityReference entityReference)
        {
        }

        public virtual void DefaultAction_PreValidation(IPluginExecutionContext context, IOrganizationService service, Entity entity)
        {
        }

        public virtual void DefaultAction_PreOperation(IPluginExecutionContext context, IOrganizationService service, Entity entity)
        {
        }

        public virtual void DefaultAction_PostOperation(IPluginExecutionContext context, IOrganizationService service, Entity entity)
        {
        }

        public virtual void DefaultAction(IPluginExecutionContext context, IOrganizationService service)
        {
        }

    }

    public enum PluginStage
    {
        PreValidation = 10,
        PreOperation = 20,
        PostOperation = 40
    }

    public enum PluginMessages
    {
        Assign,
        Create,
        Delete,
        GrantAccess,
        ModifyAccess,
        Retrieve,
        RetrieveMultiple,
        RetrievePrincipalAccess,
        RetrieveSharedPrincipalsAndAccess,
        RevokeAccess,
        SetState,
        Update,
        Associate,
        Disassociate
    }

    public enum DefaultMessages
    {
        Close
    }
}
