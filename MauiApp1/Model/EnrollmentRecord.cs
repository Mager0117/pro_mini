namespace MauiApp2.Model
{
    public class EnrollmentRecord
    {
        public string StudentId { get; set; }
        public string CourseId { get; set; }
        public string Action { get; set; } // "enrolled" หรือ "dropped"
        public DateTime Timestamp { get; set; }
    }
}