using Atmoz.EmissionBreakdownApi.Models;
using EmissionBreakdownApi.DTOs;

namespace EmissionBreakdownApi.Interfaces
{
    public interface IEmissionBreakdownRepository
    {
        Task<bool> SaveAllAsync();
        Task<EmissionBreakdownRow> GetByIdAsync(long id);
        void CreateAsync(EmissionBreakdownRowDTO row);
        void DeleteAsync(EmissionBreakdownRow row);
		//Task GetWithOffsetPagination(string pageToken, int pageSize);
        Task<IEnumerable<object>> QueryAsync(EmissionBreakdownQueryParameters parameters);
    }
}
