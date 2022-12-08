using System.Drawing;
using Nursing_Planner.Models;

namespace Nursing_Planner.Services;
public static class SettingsService
{
    static Settings settings { get; set; }
    
    static SettingsService()
    {
        settings = new Settings(DateTime.Parse("08:00"), DateTime.Parse("15:00"), 15);
    }

    public static void SetCurrentPatient(string currentPatient)
    {
        settings.currentPatient = PatientService.GetAll().Where(patient => patient.RoomNumber == currentPatient).FirstOrDefault();
    }

    public static Patient GetCurrentPatient()
    {
       return settings.currentPatient;
    }

    public static List<DateTime> GetAllTimes() => settings.times;
    
    public static List<string> GetAllTasks() => new List<string>(settings.tasks.Keys);
    
    public static List<int> GetIntervals() => settings.intervals;
    public static Dictionary<string, string> GetTasksColors() => settings.tasks;

    public static DateTime GetEndTime() => settings._endTime;
    
    public static void ChangeColour(string key, string newColour){
        if (settings.tasks.ContainsKey(key)){
            settings.tasks[key] = newColour;
        }
    }

    public static void NewTask(string key, string colour){
        settings.tasks.Add(key, colour);
    }

    



    


}