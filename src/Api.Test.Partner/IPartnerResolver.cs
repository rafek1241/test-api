namespace Api.Test.Partner
{
    public interface IPartnerResolver<TService> where TService : IPartnerService
    {
        TService Resolve();
    }
}