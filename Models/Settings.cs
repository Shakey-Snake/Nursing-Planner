//Defines the Patient
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace Nursing_Planner.Models;

public class Settings
{
    public List<int> intervals = new List<int> { 0, 5, 10, 15, 20, 30, 60, 90, 120, 240 };

    public DateTime _startTime { get; set; }

    public DateTime _endTime { get; set; }

    public int _interval { get; set; }

    public string? previousPatient { get; set; }

    public Patient? currentPatient { get; set; }

    public List<DateTime> times { get; set; } = new List<DateTime>();

    public Dictionary<string, string> tasks;

    public Settings(DateTime startTime, DateTime endTime, int interval)
    {
        _startTime = startTime;
        _endTime = endTime;
        _interval = interval;

        for (DateTime i = _startTime; i < _endTime.AddMinutes(_interval); i = i.AddMinutes(_interval))
        {
            times.Add(i);
        }

        tasks = new Dictionary<string, string>
        {
            {"OBS", "#0000FF"},
            {"Bloods", "#FF0000"},
            {"IV Med", "#FFA500"},
            {"BGL", "#FFFF00"},
            {"Turn", "#43A6C6"},
            {"Feed", "#00FF00"},
            {"PCA", "#0E3B06"},
            {"Documentation", "#32ED10"},
            {"Other", "#000000"}
       };
    }

    public void ChangeTimes(DateTime startTime, DateTime endTime, int interval)
    {
        _startTime = startTime;
        if (endTime < startTime)
        {
            _endTime = endTime.AddDays(1);
        }
        else
        {
            _endTime = endTime;
        }
        _interval = interval;

        times = new List<DateTime>();



        for (DateTime i = _startTime; i < _endTime.AddMinutes(_interval); i = i.AddMinutes(_interval))
        {
            times.Add(i);
        }


    }
}

