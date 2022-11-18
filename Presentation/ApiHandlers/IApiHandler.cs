
namespace Presentation.ApiHandlers
{
    public interface IApiHandler
    {
        T GetAPI<T>(string url);
        string PostAPITokenWithModel(dynamic dynamicModel, string url);
        T PostAPIWithModel<T>(dynamic dynamicModel, string url);
        string PostAPIWithTokenModel<T>(dynamic dynamicModel, string url);
        string PostApiString(dynamic dynamicModel, string Url);
    }
}
