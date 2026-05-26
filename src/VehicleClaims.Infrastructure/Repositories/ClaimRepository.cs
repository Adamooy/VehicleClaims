using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleClaims.Domain.Common;
using VehicleClaims.Domain.Entities;
using VehicleClaims.Domain.Interfaces;
using VehicleClaims.Infrastructure.Data;

namespace VehicleClaims.Infrastructure.Repositories
{
    public class ClaimRepository(ApplicationDbContext dbContext) : IClaimRepository
    {
        public async Task<IEnumerable<Claim>> GetClaims()
        {
            return await dbContext.Claims.ToListAsync();
        }
        public async Task<Claim> GetClaimsByIdAsync(Guid id)
        {
            return await dbContext.Claims.FirstOrDefaultAsync(x=>x.Id == id);
        }
        public async Task<Claim> AddClaimAsync(Claim entity)
        {
            dbContext.Claims.Add(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }
        // Do poprawy
        public async Task<Claim> UpdateClaimAsync(Guid claimId, Claim entity)
        {
            return entity;
        }
        public async Task<bool> DeleteClaimAsync(Guid claimId)
        {
            var claim = await dbContext.Claims.FirstOrDefaultAsync(x => x.Id == claimId);
            if (claim is not null)
            {
                dbContext.Claims.Remove(claim);
                return await dbContext.SaveChangesAsync() > 0;
            }
            return false;
        }
    }
}
