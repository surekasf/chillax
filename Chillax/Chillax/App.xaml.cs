namespace Chillax
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();


            // Use AppShell as the main entry point
            MainPage = new AppShell();
            // Initialize the database asynchronously when the app starts
            InitializeAppAsync();
        }
        private async void InitializeAppAsync()
        {
            // Initialize the database
            await DatabaseHelper.InitAsync();
           

            // Add sample data to the database (seeding)
            await SeedDatabase();

            // Check if username and age are already saved
            if (Preferences.ContainsKey("username") && Preferences.ContainsKey("age"))
            {
                var ageString = Preferences.Get("age", 6);
               
                
                MainPage = new NavigationPage(new MainPage(ageString)); // Pass the integer
                
               
            }
            else
            {
                // If no preferences exist, navigate to UserProfilePage
                MainPage = new NavigationPage(new UserProfilePage());
            }
        }

        private static async Task<Stream> LoadRiddlesFileAsync()
        {
            try
            {
                // Load the file from the Resources/Raw folder using OpenAppPackageFileAsync
                Stream stream = await FileSystem.OpenAppPackageFileAsync("riddles.txt");
                Console.WriteLine("riddles.txt file loaded successfully.");
                return stream;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading riddle file: {ex.Message}");
                return null;
            }
        }

        // Seeding the database with sample data
        private static async Task SeedDatabase()
        {
            
            // Paths for each file type
            string riddleFilePath = Path.Combine(FileSystem.AppDataDirectory, "riddles.txt");
            
            string funFactFilePath = Path.Combine(FileSystem.AppDataDirectory, "funfacts.txt");
            string puzzleFilePath = Path.Combine(FileSystem.AppDataDirectory, "puzzles.txt");

            var db = await DatabaseHelper.GetConnectionAsync();

            // Check if any riddles exist; if not, seed data
            var existingRiddles = await DataManager.GetAllRiddlesAsync();
            if (existingRiddles == null || !existingRiddles.Any())
            {
                await DataManager.ImportRiddlesFromFile(riddleFilePath);
            }
            // Check if any fun facts exist; if not, seed data
            var existingFunFacts = await db.Table<FunFact>().ToListAsync();
            if (existingFunFacts == null || !existingFunFacts.Any())
            {
                await DataManager.ImportFunFactsFromFile(funFactFilePath);
            }
            // Check if any puzzles exist; if not, seed data
            var existingPuzzles = await db.Table<Puzzle>().ToListAsync();
            if (existingPuzzles == null || !existingPuzzles.Any())
            {
                await DataManager.ImportPuzzlesFromFile(puzzleFilePath);
            }

            
        }

    }
}
