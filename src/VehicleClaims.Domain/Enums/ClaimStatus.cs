using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleClaims.Domain.Enums
{
    public enum ClaimStatus
    {
        Submitted = 1,
        UnderReview = 2,
        Assessed = 3,
        Approved = 4,
        Rejected = 5,
        Closed = 6
    }
}
