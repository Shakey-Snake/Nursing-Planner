//Defines the Patient
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace Nursing_Planner.Models;

public class Settings
{
    public List<int> intervals = new List<int> { 0, 5, 10, 15, 20, 30, 60};

    public DateTime _startTime { get; set; }

    public DateTime _endTime { get; set; }

    public int _interval { get; set; }

    public string? previousPatient { get; set; }

    public Patient? currentPatient { get; set; }

    public List<DateTime> times { get; } = new List<DateTime>();

    public Dictionary<string, string> tasks;

    public Settings (DateTime startTime, DateTime endTime, int interval )
    {
       _startTime = startTime;
       _endTime = endTime;
       _interval = interval;

       for (DateTime i = startTime; i < endTime.AddMinutes(interval); i = i.AddMinutes(interval)){
            times.Add(i);
       }

       tasks = new Dictionary<string, string>
       {
            {"obs", "blue"},
            {"blood test", "red"},
            {"blood pressure", "orange"},
            {"other", "black"}
       };
    }

    public void ChangeTimes(DateTime startTime, DateTime endTime, int interval )
    {
       _startTime = startTime;
       _endTime = endTime;
       _interval = interval;

       for (DateTime i = startTime; i < endTime.AddMinutes(interval); i.AddMinutes(interval)){
            times.Add(i);
       }
    }
}

