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
                                    "obs", "blood test"
                                }
                            }
                        }
                    },
                    new Patient { Id = 2, RoomNumber = "6B3",
                        Task = new Dictionary<DateTime, List<string>> {
                            {
                                DateTime.Parse("09:00"), 
                                new() {
                                    "obs", "blood test"
                                }
                            },
                            {
                                DateTime.Parse("10:00"), 
                                new() {
                                    "obs"
                                }
                            }
                        }
                    }
                };
    }

    public static List<Patient> GetAll() => Patients;

    public static Patient? Get(int id) => Patients.FirstOrDefault(p => p.Id == id);
    public static Patient? Get(string roomNumber) => Patients.FirstOrDefault(p => p.RoomNumber == roomNumber);

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

    public static void AddPatientTask(string curr, string time, string task, int interval)
    {
        var currentPatient = Get(curr);
        if (!currentPatient.Task.ContainsKey(DateTime.Parse(time))){
            currentPatient.Task.Add(DateTime.Parse(time), new() {task});
        }
        else{
            if (!currentPatient.Task[DateTime.Parse(time)].Contains(task)){
                currentPatient.Task[DateTime.Parse(time)].Add(task);
            }
            
        }
        if (interval != 0){
            for (DateTime i = DateTime.Parse(time).AddMinutes(interval); i < SettingsService.GetEndTime().AddMinutes(interval); i = i.AddMinutes(interval)){
                if (!currentPatient.Task.ContainsKey(i)){
                    currentPatient.Task.Add(i, new() {task});
                }
                else{
                    if (!currentPatient.Task[DateTime.Parse(time)].Contains(task)){
                        currentPatient.Task[i].Add(task);
                    }
                }
            }
        }
    }
}