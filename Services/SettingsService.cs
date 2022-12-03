using Nursing_Planner.Models;

namespace Nursing_Planner.Services;
public static class SettingsService
{
    static Settings settings { get; set; }
    
    static SettingsService()
    {
        settings = new Settings();
    }

    public static void SetCurrentPatient(string currentPatient)
    {
        settings.currentPatient = PatientService.GetAll().Where(patient => patient.RoomNumber == currentPatient).FirstOrDefault();
    }

    public static Patient GetCurrentPatient()
    {
       return settings.currentPatient;
    }



}