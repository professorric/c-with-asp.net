using DatarRadioCheck.Models;
using Microsoft.AspNetCore.Mvc;


namespace DatarRadioCheck.Controllers
{
    public class VoteController : Controller
    {
        // Automatically get Desktop path
        private readonly string filePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            "votes.txt"
        );

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(VoteViewModel model)
        {
            if (model != null && !string.IsNullOrEmpty(model.SelectedOption))
            {
                SaveVoteToFile(model.SelectedOption);
            }

            return View(model);
        }

        private void SaveVoteToFile(string vote)
        {
            try
            {
                // Ensure the file exists before writing
                if (!System.IO.File.Exists(filePath))
                {
                    System.IO.File.Create(filePath).Close();
                }

                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine(vote);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error writing to file: {ex.Message}");
            }
        }

        public IActionResult Results()
        {
            var votes = ReadVotesFromFile();
            return View(votes);
        }

        private List<string> ReadVotesFromFile()
        {
            List<string> votes = new List<string>();

            try
            {
                if (System.IO.File.Exists(filePath))
                {
                    votes = System.IO.File.ReadAllLines(filePath).ToList();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error reading file: {ex.Message}");
            }

            return votes;
        }
    }
}
