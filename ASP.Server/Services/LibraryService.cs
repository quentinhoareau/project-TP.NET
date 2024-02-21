using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASP.Server.Database;
using ASP.Server.Models;

namespace ASP.Server.Service
{
    public class LibraryService
    {
        public static Stats GetStatsOfBookById(Book book)
        {
            int maxWords = 0;
            int minWords = 0;
            double avgWords = 0;
            int medWords = 0;
            if (book != null)
            {
                var words = book.Content.Split(' ');
                maxWords = words.Max(p => p.Length);
                minWords = words.Min(p => p.Length);
                medWords = words.OrderBy(p => p.Length).ElementAt(words.Length / 2).Length;
                avgWords = double.Round(words.Average(p => p.Length), 2);
            }
            return new Stats()
            {
                MaxWords = maxWords,
                MinWords = minWords,
                MedWords = medWords,
                AvgWords = avgWords
            };
        }
    }
}
