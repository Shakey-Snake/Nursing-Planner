using System.Drawing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nursing_Planner.Models;
using Nursing_Planner.Services;


namespace Nursing_Planner.Pages
{
    public class PatientModel : PageModel
    {
        public List<Patient> patients = new();

        //0 references but when removed the app no longer works
        [BindProperty]
        public Patient NewPatient { get; set; } = new Patient();

        [BindProperty]
        public Patient CurrentPatient { get; set; }

        public List<DateTime> timesList { get; set; } = new();

        public List<string> displayTimesList { get; set; } = new();

        public List<string> taskList { get; set; } = new List<string>();

        public List<int> intervalList { get; set; } = new List<int>();

        public Dictionary<string, string> tasks { get; set; } = new Dictionary<string, string>();

        public DateTime time { get; set; }

        public string task { get; set; }

        public int interval { get; set; }

        public void OnGet()
        {
            patients = PatientService.GetAll();
            timesList = SettingsService.GetAllTimes();
            taskList = SettingsService.GetAllTasks();
            intervalList = SettingsService.GetIntervals();
            CurrentPatient = SettingsService.GetCurrentPatient();
            tasks = SettingsService.GetTasksColors();
            if (CurrentPatient == null)
            {
                if (patients.Count == 0)
                {
                    //might create some issues, multiple patients being created.
                    //if no GC, could cause memory OF
                    //other solutions are: 
                    //1. creating a blank patient above and reusing that
                    //more complex that could reduce code:
                    //2. having a patient with an id of -1 in the list, do not remove from the list
                    //this can use the code in the else statement and might reduce code elsewhere
                    CurrentPatient = new Patient(-1, "None");
                }
                else
                {
                    CurrentPatient = patients[0];
                }

            }

            foreach (var time in timesList)
            {
                displayTimesList.Add(time.ToString("HH:mm"));
            }

        }

        public IActionResult OnPost()
        {
            return RedirectToAction("Get");
        }

        public IActionResult OnPostChangePage(string curr)
        {
            SettingsService.SetCurrentPatient(curr);
            return RedirectToAction("Get");
        }

        public IActionResult OnPostAddPatientTask(string curr, string time, string task, int interval)
        {
            PatientService.AddPatientTask(curr, time, task, interval);
            return RedirectToAction("Get");
        }
    }
}
