using System.Collections.Generic;
using System.Linq;

namespace WinesApi.Api.Wine.CreateUpdateWine
{
    public class CreateUpdateWineService : ICreateUpdateWineService
    {
        private readonly ICreateUpdateWineRepository _createUpdateWineRepository;
        private readonly IValidateWineRepository _validateWineRepository;

        public CreateUpdateWineService(ICreateUpdateWineRepository createUpdateWineRepository,
            IValidateWineRepository validateWineRepository)
        {
            _createUpdateWineRepository = createUpdateWineRepository;
            _validateWineRepository = validateWineRepository;
        }

        public bool CreateWine(CreateWineRequest request, out IEnumerable<string> errors)
        {
            errors = _validateWineRepository.ValidateWineModel(request);
            if (errors.Any())
            {
                return false;
            }

            return _createUpdateWineRepository.CreateWine(request);
        }

        public bool UpdateWine(UpdateWineRequest request, out IEnumerable<string> errors)
        {
            errors = _validateWineRepository.ValidateWineModel(request);
            if (errors.Any())
            {
                return false;
            }

            return _createUpdateWineRepository.UpdateWine(request);
        }
    }
}