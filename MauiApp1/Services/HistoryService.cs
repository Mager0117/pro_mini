using MauiApp1.Model;
using MauiApp2.Model;
using Newtonsoft.Json;

namespace MauiApp1.Services
{
    public class HistoryService
    {
        private readonly string _fileName = "updatecourses.json";

        public async Task<List<EnrollmentRecord>> LoadHistoryAsync()
        {
            try
            {
                var filePath = Path.Combine(FileSystem.AppDataDirectory, _fileName);
                
                if (!File.Exists(filePath))
                {
                    return new List<EnrollmentRecord>();
                }

                var json = await File.ReadAllTextAsync(filePath);
                return JsonConvert.DeserializeObject<List<EnrollmentRecord>>(json) 
                    ?? new List<EnrollmentRecord>();
            }
            catch
            {
                return new List<EnrollmentRecord>();
            }
        }
    }
}