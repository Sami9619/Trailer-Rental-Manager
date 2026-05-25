using System;
using System.IO;

namespace TrailerRentalManager.Tests
{
    internal static class TestDatabase
    {
        internal static string CreateTemporaryDirectory()
        {
            string directory = Path.Combine(Path.GetTempPath(), "TrailerRentalManagerTests_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(directory);
            return directory;
        }

        internal static string CreateConnectionString(string databasePath)
        {
            return "Data Source=" + databasePath + ";Version=3;Foreign Keys=True;";
        }

        internal static void DeleteDirectory(string directory)
        {
            if (Directory.Exists(directory))
            {
                Directory.Delete(directory, true);
            }
        }
    }
}
