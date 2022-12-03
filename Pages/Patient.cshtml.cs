using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nursing_Planner.Models;
using Nursing_Planner.Services;


namespace Nursing_Planner.Pages
{
    public class PatientModel : PageModel
    {
        public List<Patient> patients = new();
        [BindProperty]
        public Patient NewPatient { get; set; } = new();

        [BindProperty]
        public Patient CurrentPatient { get; set; }

        public void OnGet()
        {
            patients = PatientService.GetAll();
            CurrentPatient = SettingsService.GetCurrentPatient();
            if (CurrentPatient == null)
            {
                CurrentPatient = patients[0];
            }
        }

        public IActionResult OnPost()
        {
            SettingsService.SetCurrentPatient(Request.Form[nameof(CurrentPatient)]);
            return RedirectToAction("Get");
        }

        public IActionResult OnPostDelete(int id)
        {
            return RedirectToAction("Get");
        }
    }
}
