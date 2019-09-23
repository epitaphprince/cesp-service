using System;
using CESP.Core.Contracts;
using CESP.Database.Context.Payments.Models;
using Microsoft.Extensions.Options;

namespace CESP.Dal.Repositories.Cesp
{
    public class CespResourceProvider : ICespResourceProvider
    {
        private readonly string _resourceBaseUrl;

        public CespResourceProvider(IOptions<ResourceStorage> resourceStorage)
        {
            _resourceBaseUrl = resourceStorage.Value.ResourceBaseUrl;
        }

        public string GetImagesBasePath()
        {
            return _resourceBaseUrl;
        }

        public string GetFullUrl(string resourcePartUrl)
        {
            if (String.IsNullOrEmpty(resourcePartUrl))
            {
                return null;
            }

            return GetImagesBasePath().TrimEnd('/') + "/" + resourcePartUrl.TrimStart('/');
        }
    }
}