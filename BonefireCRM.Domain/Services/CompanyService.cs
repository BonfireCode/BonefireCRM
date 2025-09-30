using BonefireCRM.Domain.DTOs.Company;
using BonefireCRM.Domain.Entities;
using BonefireCRM.Domain.Exceptions;
using BonefireCRM.Domain.Infrastructure.Persistance;
using BonefireCRM.Domain.Mappers;
using LanguageExt;

namespace BonefireCRM.Domain.Services
{
    public class CompanyService
    {
        private readonly IBaseRepository<Company> _companyRepository;

        public CompanyService(IBaseRepository<Company> companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<Option<GetCompanyDTO>> GetCompanyAsync(Guid id, CancellationToken ct)
        {
            var Company = await _companyRepository.GetAsync(id, ct);
            if (Company is null)
            {
                return Option<GetCompanyDTO>.None;
            }

            return Company.MapToGetDto();
        }

        public async Task<bool> DeleteCompanyAsync(Guid id, CancellationToken ct)
        {
            //Domain validations if needed

            var Company = new Company { Id = id };

            var isDeleted = await _companyRepository.DeleteAsync(Company, ct);

            return isDeleted;
        }

        public async Task<Fin<CreatedCompanyDTO>> CreateCompanyAsync(CreateCompanyDTO createCompanyDTO, CancellationToken ct)
        {
            //Domain validations if needed

            var Company = createCompanyDTO.MapToCompany();

            var createdCompany = await _companyRepository.AddAsync(Company, ct);
            if (createdCompany is null)
            {
                return Fin<CreatedCompanyDTO>.Fail(new AddEntityException<Company>());
            }

            return Fin<CreatedCompanyDTO>.Succ(createdCompany.MapToCreatedDto());
        }

        public async Task<Fin<UpdatedCompanyDTO>> UpdateCompanyAsync(UpdateCompanyDTO updateCompanyDTO, CancellationToken ct)
        {
            //Domain validations if needed

            var Company = updateCompanyDTO.MapToCompany();

            var updatedCompany = await _companyRepository.UpdateAsync(Company, ct);
            if (updatedCompany is null)
            {
                return Fin<UpdatedCompanyDTO>.Fail(new UpdateEntityException<Company>());
            }

            return Fin<UpdatedCompanyDTO>.Succ(updatedCompany.MapToUpdatedDto());
        }
    }
}
