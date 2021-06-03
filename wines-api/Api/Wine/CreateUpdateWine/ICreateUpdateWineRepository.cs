namespace WinesApi.Api.Wine.CreateUpdateWine
{
    public interface ICreateUpdateWineRepository
    {
        bool CreateWine(CreateWineRequest request);

        bool UpdateWine(UpdateWineRequest request);
    }
}