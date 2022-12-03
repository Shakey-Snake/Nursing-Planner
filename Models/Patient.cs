//Defines the Patient
using System.ComponentModel.DataAnnotations;

namespace Nursing_Planner.Models;

public class Patient
{
    public int Id { get; set; }

    [Required]
    public string? RoomNumber { get; set; }

    //key pair where first is time, second is the action
    [Required]
    public Dictionary<DateTime, List<string>>? Task { get; set; }
}