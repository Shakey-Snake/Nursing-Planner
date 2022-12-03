using Nursing_Planner.Models;

namespace Nursing_Planner.Services;
public static class PatientService
{
    static List<Patient> Patients { get; }
    static int nextId = 3;
    
    static PatientService()
    {
        Patients = new List<Patient>
                {
                    
                    //TO DO
                    new Patient { Id = 1, RoomNumber = "1A2", 
                        Task = new Dictionary<DateTime, List<string>> {
                            {
                                DateTime.Parse("08:00"), 
                                new() {
                                    "obs", "blood"
                                }
                            }
                        }
                    },
                    new Patient { Id = 2, RoomNumber = "6B3",
                        Task = new Dictionary<DateTime, List<string>> {
                            {
                                DateTime.Parse("09:00"), 
                                new() {
                                    "check", "blood"
                                }
                            },
                            {
                                DateTime.Parse("10:00"), 
                                new() {
                                    "check"
                                }
                            }
                        }
                    }
                };
    }

    public static List<Patient> GetAll() => Patients;

    public static Patient? Get(int id) => Patients.FirstOrDefault(p => p.Id == id);

    public static void Add(Patient patient)
    {
        patient.Id = nextId++;
        Patients.Add(patient);
    }

    public static void Delete(int id)
    {
        var pateint = Get(id);
        if (pateint is null)
            return;

        Patients.Remove(pateint);
    }

    public static void Update(Patient patient)
    {
        var index = Patients.FindIndex(p => p.Id == patient.Id);
        if (index == -1)
            return;

        Patients[index] = patient;
    }
}