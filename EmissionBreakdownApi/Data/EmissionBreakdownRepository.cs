using Atmoz.EmissionBreakdownApi.Models;
using AutoMapper;
using EmissionBreakdownApi.DTOs;
using EmissionBreakdownApi.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace EmissionBreakdownApi.Data
{
    public class EmissionBreakdownRepository : IEmissionBreakdownRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public EmissionBreakdownRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void CreateAsync(EmissionBreakdownRowDTO row)
        {
            var data = _mapper.Map<EmissionBreakdownRow>(row);
            _context.Add(data);
        }

        public void DeleteAsync(EmissionBreakdownRow row)
        {
            _context.EmissionBreakdownRow.Remove(row);
        }

        public async Task<EmissionBreakdownRow> GetByIdAsync(long id)
        {
            return await _context.EmissionBreakdownRow
                .Include(c => c.Category)
                .Include(sc => sc.SubCategory)
                .Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<object>> QueryAsync(EmissionBreakdownQueryParameters parameters)
        {
            var query = _context.EmissionBreakdownRow
                .Include(c => c.Category)
                .Include(sc => sc.SubCategory)
                .AsQueryable();

            if (!string.IsNullOrEmpty(parameters.CategoryId))
            {
                query = query.Where(e => e.Category.Id == parameters.CategoryId);
            }

            if (!string.IsNullOrEmpty(parameters.SubCategoryId))
            {
                query = query.Where(e => e.SubCategory.Id == parameters.SubCategoryId);
            }

            if (!string.IsNullOrEmpty(parameters.SortField))
            {
                query = query.OrderBy(parameters.SortField);
            }

            if (!string.IsNullOrEmpty(parameters.PageToken))
            {
                var lastRecordId = DecodePageToken(parameters.PageToken);
                query = query.Where(e => e.Id >= lastRecordId);
            }

            var result = await query.Take(parameters.PageSize + 1).ToListAsync();

            // Generate a new PageToken for the next page if there are more records
            string nextPageToken = null;
            if (result.Count > parameters.PageSize)
            {
                var lastRecordInPage = result.Last();
                nextPageToken = EncodePageToken(lastRecordInPage.Id);
                result = result.Take(parameters.PageSize).ToList();
            }

            return result.Select(x => new
            {
                PageToken = nextPageToken,
                Category = x.Category,
                SubCategory = x.SubCategory,
                TonsOfCO2 = x.TonsOfCO2,
                Id = x.Id,
            });

        }

        private string EncodePageToken(int lastRecordId)
        {
            var bytes = BitConverter.GetBytes(lastRecordId);
            return Convert.ToBase64String(bytes);
        }

        private int DecodePageToken(string pageToken)
        {
            var bytes = Convert.FromBase64String(pageToken);
            return BitConverter.ToInt32(bytes, 0);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
