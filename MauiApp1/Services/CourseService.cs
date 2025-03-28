using MauiApp1.Model;
using MauiApp2.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    public class CourseService
    {
        private readonly string _coursesFileName = "courses.json";
        private readonly string _studentsFileName = "students.json";
        private readonly string _updatesFileName = "updatecourses.json";

        public async Task<List<Courses>> LoadCoursesAsync()
        {
            try
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync(_coursesFileName);
                using var reader = new StreamReader(stream);
                var jsonData = await reader.ReadToEndAsync();
                
                if (string.IsNullOrEmpty(jsonData))
                {
                    Debug.WriteLine("courses.json is empty");
                    return new List<Courses>();
                }

                return JsonConvert.DeserializeObject<List<Courses>>(jsonData) ?? new List<Courses>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading courses.json: {ex.Message}");
                return new List<Courses>();
            }
        }

        public async Task<List<Student>> LoadStudentsAsync()
        {
            try
            {
                var filePath = Path.Combine(FileSystem.AppDataDirectory, _studentsFileName);
                
                if (!File.Exists(filePath))
                {
                    // ถ้าไม่มีไฟล์ ให้โหลดจากไฟล์ต้นฉบับใน Resources
                    using var stream = await FileSystem.OpenAppPackageFileAsync(_studentsFileName);
                    using var reader = new StreamReader(stream);
                    var jsonData = await reader.ReadToEndAsync();
                    
                    if (!string.IsNullOrEmpty(jsonData))
                    {
                        await File.WriteAllTextAsync(filePath, jsonData);
                    }
                }

                var json = await File.ReadAllTextAsync(filePath);
                return JsonConvert.DeserializeObject<List<Student>>(json) ?? new List<Student>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading students.json: {ex.Message}");
                return new List<Student>();
            }
        }

public async Task<bool> EnrollCourseAsync(string studentId, string courseId)
{
    try
    {
        var students = await LoadStudentsAsync();
        var student = students.FirstOrDefault(s => s.Id == studentId);
        
        if (student == null)
        {
            Debug.WriteLine($"Student not found: {studentId}");
            return false;
        }

        // ตรวจสอบเฉพาะว่าผู้ใช้คนนี้เคยลงวิชานี้ในเทอมปัจจุบันหรือไม่
        // ไม่ต้องตรวจสอบว่าคนอื่นลงวิชานี้หรือเปล่า
        if (student.CurrentTerm.EnrolledCourses.Contains(courseId))
        {
            Debug.WriteLine($"This student already enrolled in: {courseId}");
            return false;
        }

        // เพิ่มวิชาลงใน enrolled_courses ของผู้ใช้คนนี้
        student.CurrentTerm.EnrolledCourses.Add(courseId);

        // บันทึกข้อมูลนักเรียนที่อัปเดต
        await SaveStudentsAsync(students);

        // บันทึกประวัติการลงทะเบียน
        await TrackEnrollment(studentId, courseId, "เพิ่มวิชา");
        
        return true;
    }
    catch (Exception ex)
    {
        Debug.WriteLine($"Error enrolling course: {ex.Message}");
        return false;
    }
}

        private async Task SaveStudentsAsync(List<Student> students)
        {
            var json = JsonConvert.SerializeObject(students, Formatting.Indented);
            var filePath = Path.Combine(FileSystem.AppDataDirectory, _studentsFileName);
            await File.WriteAllTextAsync(filePath, json);
        }

        private async Task TrackEnrollment(string studentId, string courseId, string action)
        {
            try
            {
                var updates = new List<EnrollmentRecord>();
                var updatesFilePath = Path.Combine(FileSystem.AppDataDirectory, _updatesFileName);

                if (File.Exists(updatesFilePath))
                {
                    var existingJson = await File.ReadAllTextAsync(updatesFilePath);
                    updates = JsonConvert.DeserializeObject<List<EnrollmentRecord>>(existingJson) ?? new List<EnrollmentRecord>();
                }

                updates.Add(new EnrollmentRecord
                {
                    StudentId = studentId,
                    CourseId = courseId,
                    Action = action,
                    Timestamp = DateTime.Now
                });

                var json = JsonConvert.SerializeObject(updates, Formatting.Indented);
                await File.WriteAllTextAsync(updatesFilePath, json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error tracking enrollment: {ex.Message}");
                throw;
            }
        }
        public async Task<bool> DropCourseAsync(string studentId, string courseId)
{
    try
    {
        var students = await LoadStudentsAsync();
        var student = students.FirstOrDefault(s => s.Id == studentId);
        
        if (student == null)
        {
            Debug.WriteLine($"Student not found: {studentId}");
            return false;
        }

        // ตรวจสอบว่ามีวิชานี้ในรายการหรือไม่
        if (!student.CurrentTerm.EnrolledCourses.Contains(courseId))
        {
            Debug.WriteLine($"Course {courseId} not found in student's enrolled courses");
            return false;
        }

        // ถอนวิชา
        student.CurrentTerm.EnrolledCourses.Remove(courseId);

        // บันทึกข้อมูลนักเรียนที่อัปเดต
        await SaveStudentsAsync(students);

        // บันทึกประวัติการถอนวิชา
        await TrackEnrollment(studentId, courseId, "ถอนวิชา");
        
        return true;
    }
    catch (Exception ex)
    {
        Debug.WriteLine($"Error dropping course: {ex.Message}");
        return false;
    }
}
    }
    
}