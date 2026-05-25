using System.Globalization;

namespace Trailer_Rental_Manager.Services
{
    internal static class TrailerValidator
    {
        internal static bool HasRequiredData(string trailerName, string trailerType)
        {
            return !string.IsNullOrWhiteSpace(trailerName) &&
                   !string.IsNullOrWhiteSpace(trailerType);
        }

        internal static bool AreOptionalNumericFieldsValid(params string[] values)
        {
            foreach (string value in values)
            {
                if (!IsOptionalNumberValid(value))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Optional trailer dimension fields may be empty, but entered values must parse as numbers.
        /// Both current culture and invariant culture are accepted to match repository parsing.
        /// </summary>
        internal static bool IsOptionalNumberValid(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return true;
            }

            decimal result;
            string trimmedValue = value.Trim();
            return decimal.TryParse(trimmedValue, NumberStyles.Number, CultureInfo.CurrentCulture, out result) ||
                   decimal.TryParse(trimmedValue, NumberStyles.Number, CultureInfo.InvariantCulture, out result);
        }

        internal static bool IsValid(
            string trailerName,
            string trailerType,
            string maxPayload,
            string height,
            string width,
            string length)
        {
            return HasRequiredData(trailerName, trailerType) &&
                   AreOptionalNumericFieldsValid(maxPayload, height, width, length);
        }
    }
}
