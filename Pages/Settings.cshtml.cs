using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nursing_Planner.Models;
using Nursing_Planner.Services;

namespace Nursing_Planner.Pages
{
    public class SettingsModel : PageModel
    {
        public List<int> intervalList = new List<int>();

        public List<Patient> patientList = new List<Patient>();

        public List<string> displayTimesList = new List<string>();

        public Dictionary<string, string> taskList = new Dictionary<string, string>();

        public void OnGet()
        {
            List<int> intervals = SettingsService.GetIntervals();
            patientList = PatientService.GetAll();
            intervalList = intervals.GetRange(1, intervals.Count() - 1);
            taskList = SettingsService.GetAllTaskColours();

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

        public IActionResult OnPostAddPatient(string roomNumber)
        {
            Patient newPatient = new Patient(roomNumber);
            PatientService.Add(newPatient);
            return RedirectToAction("Get");
        }

        public IActionResult OnPostDelete(int Id)
        {
            PatientService.Delete(Id);
            return RedirectToAction("Get");
        }
        public IActionResult OnPostDeleteTask(string key)
        {
            SettingsService.RemoveColor(key);
            return RedirectToAction("Get");
        }

        public IActionResult OnPostChangeTask(string oldKey, string color, string newKey)
        {
            //check if the color exist
            if (!SettingsService.ColorExists(newKey))
            {
                SettingsService.ChangeColor(oldKey, newKey, color);
                // remove all references to the changed color
                SettingsService.RemoveColor(oldKey);
            }
            return RedirectToAction("Get");
        }

        public IActionResult OnPostAddTaskToList()
        {
            return RedirectToAction("Get");
        }
    }
}
