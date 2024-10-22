using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Chillax
{
    public static class DataManager
    {
        // Add Riddle
        public static async Task AddRiddle(Riddle riddle)
        {
            var db = await DatabaseHelper.GetConnectionAsync();
            await db.InsertAsync(riddle);
        }

        // Get a random riddle based on age group
        public static async Task<Riddle> GetRandomRiddleForAge(int ageGroup)
        {
            var db = await DatabaseHelper.GetConnectionAsync();
            var riddles = await db.Table<Riddle>()
                                  .Where(r => r.AgeGroup == ageGroup)
                                  .ToListAsync();

            if (riddles.Count == 0) return null; // Handle empty list scenario

            var random = new Random();
            return riddles[random.Next(riddles.Count)];
        }

        // Get a random fun fact based on age group
        public static async Task<FunFact> GetRandomFunFactForAge(int ageGroup)
        {
            var db = await DatabaseHelper.GetConnectionAsync();
            var facts = await db.Table<FunFact>()
                                .Where(f => f.AgeGroup == ageGroup)
                                .ToListAsync();

            if (facts.Count == 0) return null; // Handle empty list scenario

            var random = new Random();
            return facts[random.Next(facts.Count)];
        }

        // Get a random puzzle based on age group
        public static async Task<Puzzle> GetRandomPuzzleForAge(int ageGroup)
        {
            var db = await DatabaseHelper.GetConnectionAsync();
            var puzzles = await db.Table<Puzzle>()
                                  .Where(p => p.AgeGroup == ageGroup)
                                  .ToListAsync();

            if (puzzles.Count == 0) return null; // Handle empty list scenario

            var random = new Random();
            return puzzles[random.Next(puzzles.Count)];
        }

        // Get all riddles
        public static async Task<List<Riddle>> GetAllRiddlesAsync()
        {
            var db = await DatabaseHelper.GetConnectionAsync();
            return await db.Table<Riddle>().ToListAsync();
        }

        // Import riddles from file
        public static async Task ImportRiddlesFromFile(string filePath)
        {
            var db = await DatabaseHelper.GetConnectionAsync();

            var lines = await File.ReadAllLinesAsync(filePath); // Async file read

            foreach (var line in lines)
            {
                // Assume each line is formatted as: "Question|Answer|AgeGroup"
                var parts = line.Split('|');
                if (parts.Length == 3 && int.TryParse(parts[2], out int ageGroup))
                {
                    var riddle = new Riddle
                    {
                        Question = parts[0],
                        Answer = parts[1],
                        AgeGroup = ageGroup
                    };
                    await db.InsertAsync(riddle); // Insert each riddle into the database
                }
            }
        }
        // Import funfacts from file
        public static async Task ImportFunFactsFromFile(string filePath)
        {
            var db = await DatabaseHelper.GetConnectionAsync();

            var lines = await File.ReadAllLinesAsync(filePath); // Async file read

            foreach (var line in lines)
            {
                // Assume each line is formatted as: "Fact|AgeGroup"
                var parts = line.Split('|');
                if (parts.Length == 2 && int.TryParse(parts[1], out int ageGroup))
                {
                    var funFact = new FunFact
                    {
                        Fact = parts[0],
                        AgeGroup = ageGroup
                    };
                    await db.InsertAsync(funFact); // Insert each fun fact into the database
                }
            }
        }

        // Import puzzle from file
        public static async Task ImportPuzzlesFromFile(string filePath)
        {
            var db = await DatabaseHelper.GetConnectionAsync();

            var lines = await File.ReadAllLinesAsync(filePath); // Async file read

            foreach (var line in lines)
            {
                // Assume each line is formatted as: "Question|Answer|AgeGroup"
                var parts = line.Split('|');
                if (parts.Length == 3 && int.TryParse(parts[2], out int ageGroup))
                {
                    var puzzle = new Puzzle
                    {
                        Challenge = parts[0],
                        Solution = parts[1],
                        AgeGroup = ageGroup
                    };
                    await db.InsertAsync(puzzle); // Insert each riddle into the database
                }
            }
        }
    }
}
