using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Chillax
{
    public class Riddle
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int AgeGroup { get; set; }  // Store age group for filtering
    }

    public class FunFact
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Fact { get; set; }
        public int AgeGroup { get; set; }
    }

    public class Puzzle
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Challenge { get; set; }
        public string Solution { get; set; }
        public int AgeGroup { get; set; }
    }
}
