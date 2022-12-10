using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nursing_Planner.Models;
using Nursing_Planner.Services;

namespace Nursing_Planner.Pages
{
    public class SettingsModel : PageModel
    {
        public List<int> intervalList = new List<int>();

        public List<string> displayTimesList = new List<string>();

        public void OnGet()
        {
            List<int> intervals = SettingsService.GetIntervals();
            intervalList = intervals.GetRange(1, intervals.Count() - 1);

            for (DateTime i = DateTime.Parse("00:00"); i < DateTime.Parse("00:00").AddDays(1); i = i.AddHours(1))
            {
                displayTimesList.Add(i.ToString("HH:mm"));
            }

        }

        public IActionResult OnPostChangeSettings(string start, string end, int interval, params string[] patient)
        {
            SettingsService.SetSettings(start, end, interval);
            PatientService.SetPatients(patient);
            return RedirectToAction("Get");
        }
    }
}
