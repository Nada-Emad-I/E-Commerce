namespace DomainLayer.Contracts
{
    public interface IDataSeeding
    {
        Task DataSeedAsync();
        Task IdentityDataSeedAsync();
    }
}
