//Defines the Patient
using System.ComponentModel.DataAnnotations;

namespace Nursing_Planner.Models;

public class Settings
{
    public TimeOnly timetable { get; set; }

    public int interval { get; set; }

    public string? previousPatient { get; set; }

    public Patient currentPatient { get; set; }
}