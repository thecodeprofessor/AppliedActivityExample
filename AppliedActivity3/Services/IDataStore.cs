using AppliedActivity3.Models;

namespace AppliedActivity3.Services
{
    interface IDataStore
    {
        Task<List<Student>> GetStudentsAsync();
        Task<Student> GetStudentAsync(int id);
        Task<int> SaveStudentAsync(Student student);
        Task<int> DeleteStudentAsync(Student student);

        Task<List<Course>> GetCoursesAsync();
        Task<Course> GetCourseAsync(int id);
        Task<int> SaveCourseAsync(Course course);
        Task<int> DeleteCourseAsync(Course course);
    }
}
