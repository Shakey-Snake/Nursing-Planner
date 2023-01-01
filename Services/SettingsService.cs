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
    public static Dictionary<string, string> GetAllTaskColours() => settings.tasks;

    public static List<int> GetLinkedIntervals() => settings.intervals;

    public static List<int> GetIntervals() { return settings.intervals; }

    public static Dictionary<string, string> GetTasksColors() => settings.tasks;

    public static DateTime GetEndTime() => settings._endTime;

    public static bool ColorExists(string key)
    {
        if (settings.tasks.ContainsKey(key))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static void ChangeColor(string oldKey, string newKey, string newColour)
    {
        if (settings.tasks.ContainsKey(oldKey))
        {
            settings.tasks.Add(newKey, newColour);
            settings.tasks.Remove(oldKey);
        }
    }

    public static void RemoveColor(string key)
    {
        if (settings.tasks.ContainsKey(key))
        {
            // remove from the tasks list
            settings.tasks.Remove(key);
            // go through patients and remove all references to the deleted task
            foreach (var patient in PatientService.GetAll())
            {
                if (patient.Task != null)
                {
                    foreach (KeyValuePair<DateTime, List<string>> entry in patient.Task)
                    {
                        if (entry.Value.Contains(key))
                        {
                            entry.Value.Remove(key);
                        }
                    }
                }

            }
        }
    }

    public static void NewTask(string key, string colour)
    {
        settings.tasks.Add(key, colour);
    }

    public static void SetSettings(string start, string end, int interval)
    {
        settings.ChangeTimes(DateTime.Parse(start), DateTime.Parse(end), interval);
    }










}