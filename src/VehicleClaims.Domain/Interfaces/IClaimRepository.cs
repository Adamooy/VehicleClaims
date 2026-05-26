using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleClaims.Domain.Entities;

namespace VehicleClaims.Domain.Interfaces
{
    public interface IClaimRepository
    {
        Task<IEnumerable<Claim>> GetClaims();
        Task<Claim> GetClaimsByIdAsync(Guid id);
        Task<Claim> AddClaimAsync(Claim entity);
        Task<Claim> UpdateClaimAsync(Guid claimId, Claim entity);
        Task<bool> DeleteClaimAsync(Guid claimId);
    }
}
