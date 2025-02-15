using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Octopus.Server.Extensibility.HostServices.Mapping;
using Sashimi.Server.Contracts.Accounts;
using Sashimi.Server.Contracts.ServiceMessages;

namespace Sashimi.AzureCloudService
{
    class AzureSubscriptionTypeProvider : IAccountTypeProvider
    {
        public AccountType AccountType => AccountTypes.AzureSubscriptionAccountType;
        public Type ModelType => typeof(AzureSubscriptionDetails);
        public Type ApiType => typeof(AzureSubscriptionAccountResource);
        public IValidator Validator { get; } = new AzureSubscriptionValidator();
        public IVerifyAccount Verifier { get; } = new AzureSubscriptionAccountVerifier();
        public ICreateAccountDetailsServiceMessageHandler? CreateAccountDetailsServiceMessageHandler { get; } = null;

        public IEnumerable<(string key, object value)> GetFeatureUsage(IAccountMetricContext context)
        {
            var total = context.GetAccountDetails<AzureSubscriptionDetails>().Count();

            yield return ("azuresubscriptionaccount", total);
        }

        public void BuildMappings(IResourceMappingsBuilder builder)
        {
            builder.Map<AzureSubscriptionAccountResource, AzureSubscriptionDetails>();
        }
    }
}