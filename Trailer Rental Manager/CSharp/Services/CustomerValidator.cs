namespace Trailer_Rental_Manager.Services
{
    internal static class CustomerValidator
    {
        /// <summary>
        /// Validates the compact gender values stored by the application.
        /// The values are intentionally limited to the existing database check constraint.
        /// </summary>
        internal static bool IsValidGender(string gender)
        {
            string normalizedGender = (gender ?? string.Empty).Trim().ToLowerInvariant();
            return normalizedGender == "m" ||
                   normalizedGender == "w" ||
                   normalizedGender == "d";
        }

        internal static bool HasRequiredNameParts(string firstName, string lastName)
        {
            return !string.IsNullOrWhiteSpace(firstName) &&
                   !string.IsNullOrWhiteSpace(lastName);
        }

        internal static bool IsValid(string gender, string firstName, string lastName)
        {
            return IsValidGender(gender) &&
                   HasRequiredNameParts(firstName, lastName);
        }
    }
}
