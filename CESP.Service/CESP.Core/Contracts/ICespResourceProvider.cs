namespace CESP.Core.Contracts
{
    public interface ICespResourceProvider
    {
        string GetImagesBasePath();

        string GetFullUrl(string resourcePartUrl);
    }
}