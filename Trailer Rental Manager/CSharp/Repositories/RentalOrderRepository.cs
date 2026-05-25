using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using Trailer_Rental_Manager.Operations;
using Trailer_Rental_Manager.Services;

namespace Trailer_Rental_Manager.Repositories
{
    internal static class RentalOrderRepository
    {
        internal static DataTable GetOverview()
        {
            const string sql = @"
                SELECT
                    R.RentalOrderId AS 'Auftragsnummer',
                    C.FirstName AS 'Vorname',
                    C.LastName AS 'Nachname',
                    T.TrailerName AS 'Anhängername',
                    T.TrailerType AS 'Anhängertyp',
                    R.StartDate AS 'Beginndatum',
                    R.EndDate AS 'Enddatum',
                    R.Price AS 'Preis'
                FROM RentalOrder AS R
                INNER JOIN Customer AS C ON R.CustomerId = C.CustomerId
                INNER JOIN Trailer AS T ON T.TrailerId = R.TrailerId
                ORDER BY R.StartDate DESC, R.RentalOrderId DESC;";

            return DBOperations.ExecuteDataTable(sql);
        }

        internal static DataTable GetExport()
        {
            const string sql = @"
                SELECT
                    RentalOrderId,
                    CustomerId,
                    TrailerId,
                    StartDate,
                    EndDate,
                    Price
                FROM RentalOrder
                ORDER BY RentalOrderId;";

            return DBOperations.ExecuteDataTable(sql);
        }

        internal static DataTable GetDetailsById(int rentalOrderId)
        {
            const string sql = @"
                SELECT
                    R.RentalOrderId,
                    C.CustomerId,
                    C.FirstName,
                    C.LastName,
                    T.TrailerId,
                    T.TrailerName,
                    T.TrailerType,
                    R.StartDate,
                    R.EndDate,
                    R.Price
                FROM RentalOrder AS R
                INNER JOIN Customer AS C ON R.CustomerId = C.CustomerId
                INNER JOIN Trailer AS T ON T.TrailerId = R.TrailerId
                WHERE R.RentalOrderId = @RentalOrderId;";

            return DBOperations.ExecuteDataTable(sql, new SQLiteParameter("@RentalOrderId", rentalOrderId));
        }

        internal static void Insert(int customerId, int trailerId, string startDate, string endDate, string price)
        {
            ValidateStoredDates(startDate, endDate);

            const string sql = @"
                INSERT INTO RentalOrder
                (
                    CustomerId,
                    TrailerId,
                    StartDate,
                    EndDate,
                    Price
                )
                VALUES
                (
                    @CustomerId,
                    @TrailerId,
                    @StartDate,
                    @EndDate,
                    @Price
                );";

            DBOperations.ExecuteNonQuery(
                sql,
                new SQLiteParameter("@CustomerId", customerId),
                new SQLiteParameter("@TrailerId", trailerId),
                new SQLiteParameter("@StartDate", startDate),
                new SQLiteParameter("@EndDate", endDate),
                new SQLiteParameter("@Price", DBOperations.ParseRequiredDecimal(price, "Price"))
            );
        }

        internal static void InsertMany(IEnumerable<RentalOrderImportRecord> records)
        {
            if (records == null)
            {
                throw new ArgumentNullException(nameof(records));
            }

            const string sql = @"
                INSERT INTO RentalOrder
                (
                    CustomerId,
                    TrailerId,
                    StartDate,
                    EndDate,
                    Price
                )
                VALUES
                (
                    @CustomerId,
                    @TrailerId,
                    @StartDate,
                    @EndDate,
                    @Price
                );";

            DBOperations.ExecuteInTransaction((connection, transaction) =>
            {
                foreach (RentalOrderImportRecord record in records)
                {
                    DBOperations.ExecuteNonQuery(
                        connection,
                        transaction,
                        sql,
                        new SQLiteParameter("@CustomerId", record.CustomerId),
                        new SQLiteParameter("@TrailerId", record.TrailerId),
                        new SQLiteParameter("@StartDate", record.StartDate),
                        new SQLiteParameter("@EndDate", record.EndDate),
                        new SQLiteParameter("@Price", record.Price)
                    );
                }
            });
        }

        internal static void Update(int rentalOrderId, int trailerId, int customerId, string startDate, string endDate, string price)
        {
            ValidateStoredDates(startDate, endDate);

            const string sql = @"
                UPDATE RentalOrder
                SET
                    CustomerId = @CustomerId,
                    TrailerId = @TrailerId,
                    StartDate = @StartDate,
                    EndDate = @EndDate,
                    Price = @Price
                WHERE RentalOrderId = @RentalOrderId;";

            DBOperations.ExecuteNonQuery(
                sql,
                new SQLiteParameter("@RentalOrderId", rentalOrderId),
                new SQLiteParameter("@CustomerId", customerId),
                new SQLiteParameter("@TrailerId", trailerId),
                new SQLiteParameter("@StartDate", startDate),
                new SQLiteParameter("@EndDate", endDate),
                new SQLiteParameter("@Price", DBOperations.ParseRequiredDecimal(price, "Price"))
            );
        }

        internal static void Delete(int rentalOrderId)
        {
            const string sql = "DELETE FROM RentalOrder WHERE RentalOrderId = @RentalOrderId;";
            DBOperations.ExecuteNonQuery(sql, new SQLiteParameter("@RentalOrderId", rentalOrderId));
        }

        /// <summary>
        /// Uses half-open date ranges [StartDate, EndDate). This means a trailer can be rented again on the return date.
        /// </summary>
        internal static bool IsTrailerRentedInPeriod(int trailerId, DateTime startDate, DateTime endDate, int? ignoredRentalOrderId = null)
        {
            string sql = @"
                SELECT COUNT(*)
                FROM RentalOrder
                WHERE TrailerId = @TrailerId
                  AND StartDate < @EndDate
                  AND EndDate > @StartDate";

            if (ignoredRentalOrderId.HasValue)
            {
                sql += " AND RentalOrderId <> @IgnoredRentalOrderId";
            }

            object result = DBOperations.ExecuteScalar(
                sql,
                new SQLiteParameter("@TrailerId", trailerId),
                new SQLiteParameter("@StartDate", DBOperations.ToDatabaseDate(startDate)),
                new SQLiteParameter("@EndDate", DBOperations.ToDatabaseDate(endDate)),
                new SQLiteParameter("@IgnoredRentalOrderId", ignoredRentalOrderId.HasValue ? (object)ignoredRentalOrderId.Value : DBNull.Value)
            );

            return Convert.ToInt32(result) > 0;
        }

        /// <summary>
        /// Home overview uses inclusive day overlap so same-day rentals appear rented on their rental date.
        /// </summary>
        internal static DataTable GetRentedTrailers(DateTime startDate, DateTime endDate)
        {
            const string sql = @"
                SELECT
                    T.TrailerName AS 'Anhängername',
                    T.TrailerType AS 'Anhängertyp',
                    C.FirstName AS 'Vorname',
                    C.LastName AS 'Nachname',
                    R.EndDate AS 'Vermietet bis',
                    R.Price AS 'Preis'
                FROM RentalOrder AS R
                INNER JOIN Customer AS C ON R.CustomerId = C.CustomerId
                INNER JOIN Trailer AS T ON T.TrailerId = R.TrailerId
                WHERE R.StartDate <= @EndDate
                  AND R.EndDate >= @StartDate
                ORDER BY R.EndDate;";

            return DBOperations.ExecuteDataTable(
                sql,
                new SQLiteParameter("@StartDate", DBOperations.ToDatabaseDate(startDate)),
                new SQLiteParameter("@EndDate", DBOperations.ToDatabaseDate(endDate))
            );
        }

        /// <summary>
        /// Excludes trailers from the Home overview when any rental order overlaps the selected days inclusively.
        /// </summary>
        internal static DataTable GetAvailableTrailers(DateTime startDate, DateTime endDate)
        {
            const string sql = @"
                SELECT
                    T.TrailerName AS 'Anhängername',
                    T.TrailerType AS 'Typ'
                FROM Trailer AS T
                WHERE NOT EXISTS
                (
                    SELECT 1
                    FROM RentalOrder AS R
                    WHERE R.TrailerId = T.TrailerId
                      AND R.StartDate <= @EndDate
                      AND R.EndDate >= @StartDate
                )
                ORDER BY T.TrailerName;";

            return DBOperations.ExecuteDataTable(
                sql,
                new SQLiteParameter("@StartDate", DBOperations.ToDatabaseDate(startDate)),
                new SQLiteParameter("@EndDate", DBOperations.ToDatabaseDate(endDate))
            );
        }

        internal static DataTable GetRevenueByTrailer()
        {
            const string sql = @"
                SELECT
                    T.TrailerName AS 'Anhängername',
                    T.TrailerType AS 'Typ',
                    COALESCE(SUM(R.Price), 0.0) AS 'Umsatz'
                FROM Trailer AS T
                LEFT JOIN RentalOrder AS R ON R.TrailerId = T.TrailerId
                GROUP BY T.TrailerId, T.TrailerName, T.TrailerType
                ORDER BY T.TrailerName;";

            return DBOperations.ExecuteDataTable(sql);
        }

        private static void ValidateStoredDates(string startDate, string endDate)
        {
            DateTime parsedStartDate;
            DateTime parsedEndDate;

            if (!DBOperations.TryFromDatabaseDate(startDate, out parsedStartDate))
            {
                throw new FormatException("StartDate must use yyyy-MM-dd.");
            }

            if (!DBOperations.TryFromDatabaseDate(endDate, out parsedEndDate))
            {
                throw new FormatException("EndDate must use yyyy-MM-dd.");
            }

            if (!RentalOrderValidator.IsDateRangeValid(parsedStartDate, parsedEndDate))
            {
                throw new FormatException("EndDate must be greater than or equal to StartDate.");
            }
        }
    }
}
