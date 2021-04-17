using System.Collections.Generic;

namespace WinesApi.Api.WineType
{
    public interface IWineTypeService
    {
        IEnumerable<WineTypeResponse> FindTypes(string searchTerm);
    }
}