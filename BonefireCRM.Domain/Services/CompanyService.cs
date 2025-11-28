using BonefireCRM.Domain.DTOs.Company;
using BonefireCRM.Domain.Entities;
using BonefireCRM.Domain.Exceptions;
using BonefireCRM.Domain.Infrastructure.Persistance;
using BonefireCRM.Domain.Mappers;
using BonefireCRM.SourceGenerator;
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
            var Company = await _companyRepository.GetByIdAsync(id, ct);
            if (Company is null)
            {
                return Option<GetCompanyDTO>.None;
            }

            return Company.MapToGetDto();
        }

        public IEnumerable<GetCompanyDTO> GetAllCompanies(GetAllCompaniesDTO getAllCompaniesDTO, CancellationToken ct)
        {
            var filterExpression = CompanyQueryExpressions.Filter(getAllCompaniesDTO);

            var sortExpression = CompanyQueryExpressions.Sort(getAllCompaniesDTO.SortBy);

            var skip = (getAllCompaniesDTO.PageNumber - 1) * getAllCompaniesDTO.PageSize;
            var take = getAllCompaniesDTO.PageSize;

            var companies = _companyRepository.GetAll(filterExpression, sortExpression, getAllCompaniesDTO.SortDirection, skip, take, ct);

            var getCompaniesResultDTO = companies.Select(c => c.MapToGetDto());

            return getCompaniesResultDTO;
        }

        public async Task<bool> DeleteCompanyAsync(Guid id, CancellationToken ct)
        {
            //Domain validations if needed

            var isDeleted = await _companyRepository.DeleteAsync(id, ct);

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
