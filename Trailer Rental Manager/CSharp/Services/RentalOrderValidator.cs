using System;
using System.Globalization;

namespace Trailer_Rental_Manager.Services
{
    internal static class RentalOrderValidator
    {
        /// <summary>
        /// Same-day rentals are valid in the day-based rental model; only an end date before the start date is invalid.
        /// </summary>
        internal static bool IsDateRangeValid(DateTime startDate, DateTime endDate)
        {
            return endDate.Date >= startDate.Date;
        }

        internal static bool HasSelection(int selectedIndex)
        {
            return selectedIndex >= 0;
        }

        /// <summary>
        /// Validates the stored rental price before database constraints are reached.
        /// The billing model stays day-based; this only rejects missing, non-numeric, or negative values.
        /// </summary>
        internal static bool IsPriceValid(string price)
        {
            decimal parsedPrice;
            return TryParsePrice(price, out parsedPrice) && parsedPrice >= 0;
        }

        /// <summary>
        /// Parses rental prices using the current culture first and invariant culture second.
        /// This keeps existing comma/dot input behavior while letting callers reject negative values separately.
        /// </summary>
        internal static bool TryParsePrice(string price, out decimal parsedPrice)
        {
            parsedPrice = 0;

            if (string.IsNullOrWhiteSpace(price))
            {
                return false;
            }

            return decimal.TryParse(price.Trim(), NumberStyles.Number, CultureInfo.CurrentCulture, out parsedPrice) ||
                   decimal.TryParse(price.Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, out parsedPrice);
        }
    }
}
