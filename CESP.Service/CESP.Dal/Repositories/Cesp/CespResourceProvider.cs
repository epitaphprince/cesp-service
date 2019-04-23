using System;
using CESP.Core.Contracts;
using Microsoft.Extensions.Options;

namespace CESP.Dal.Repositories.Cesp
{
    public class CespResourceProvider: ICespResourceProvider
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
            var url = new Uri(new Uri(GetImagesBasePath()), resourcePartUrl);
            return url.OriginalString;
        }
    }
}