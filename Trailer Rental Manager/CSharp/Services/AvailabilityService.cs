using System;

namespace Trailer_Rental_Manager.Services
{
    internal static class AvailabilityService
    {
        /// <summary>
        /// Determines whether two day-based rental periods overlap.
        /// The comparison is inclusive because a same-day rental such as 2026-05-26 to 2026-05-26 is valid.
        /// </summary>
        internal static bool OverlapsInclusive(DateTime existingStartDate, DateTime existingEndDate, DateTime filterStartDate, DateTime filterEndDate)
        {
            return existingStartDate.Date <= filterEndDate.Date &&
                   existingEndDate.Date >= filterStartDate.Date;
        }
    }
}
