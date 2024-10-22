using Microsoft.Maui.Controls.PlatformConfiguration;
using SQLite;
using System.IO;
using System.Threading.Tasks;

namespace Chillax
{
    public static class DatabaseHelper
    {
        private static SQLiteAsyncConnection _databaseConnection;

        // Initialize the connection asynchronously
        public static async Task<SQLiteAsyncConnection> GetConnectionAsync()
        {
            if (_databaseConnection == null)
            {
                // Set the database path
                var databasePath = Path.Combine(FileSystem.AppDataDirectory, "ChillaxDatabase.db");

                // Create the SQLite connection
                _databaseConnection = new SQLiteAsyncConnection(databasePath);

                // Ensure the tables are created
                await _databaseConnection.CreateTableAsync<Riddle>();
                await _databaseConnection.CreateTableAsync<FunFact>();
                await _databaseConnection.CreateTableAsync<Puzzle>();
            }

            return _databaseConnection;
        }
        private static async Task CopyFileToAppDataDirectory()
        {
            // Define the source paths for all files in the resources
            var filesToCopy = new Dictionary<string, string>
    {
        { "riddles.txt", "riddles.txt" },
        { "funfacts.txt", "funfacts.txt" },
        { "puzzles.txt", "puzzles.txt" },
    };

            // Create the directory if it does not exist
            Directory.CreateDirectory(FileSystem.AppDataDirectory);

            foreach (var file in filesToCopy)
            {
                string sourceFilePath = file.Key; // Path in the app package
                string destinationFilePath = Path.Combine(FileSystem.AppDataDirectory, file.Value); // Path in app data directory

                
                // Check if the file already exists
                if (File.Exists(destinationFilePath))
                {
                    // Read the existing file content
                    var existingContent = await File.ReadAllTextAsync(destinationFilePath);

                    // Check if the file is empty
                    if (string.IsNullOrWhiteSpace(existingContent))
                    {
                        // If the file is empty, copy the new file
                        try
                        {
                            //var files = await FileSystem.OpenAppPackageFileAsync("riddles.txt");
                            
                           // Console.WriteLine($"Files in the app package: {files}");
                            // Open the file from the app's package
                            using (var stream = await FileSystem.OpenAppPackageFileAsync(sourceFilePath))
                            using (var reader = new StreamReader(stream))
                            using (var writer = new StreamWriter(destinationFilePath, false)) // Overwrite the file
                            {
                                // Copy the content
                                await writer.WriteAsync(await reader.ReadToEndAsync());
                            }
                            Console.WriteLine($"Successfully copied {file.Value} to {destinationFilePath} because it was empty.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error copying file {file.Value}: {ex.Message}");
                        }
                    }
                    else
                    {
                        // If the file contains data, log a message
                        Console.WriteLine($"{file.Value} already exists and contains data. Skipping copy.");
                    }
                }
                else
                {
                    // If the file does not exist, copy it
                    try
                    {
                        // Open the file from the app's package
                        using (var stream = await FileSystem.OpenAppPackageFileAsync(sourceFilePath))
                        using (var reader = new StreamReader(stream))
                        using (var writer = new StreamWriter(destinationFilePath, false)) // Overwrite the file
                        {
                            // Copy the content
                            await writer.WriteAsync(await reader.ReadToEndAsync());
                        }
                        Console.WriteLine($"Successfully copied {file.Value} to {destinationFilePath} because it was not found.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error copying file {file.Value}: {ex.Message}");
                    }
                }
            }
        }


        // Initialize database method
        public static async Task InitAsync()
        {
            await CopyFileToAppDataDirectory();
            await GetConnectionAsync();
        }
    }
}
