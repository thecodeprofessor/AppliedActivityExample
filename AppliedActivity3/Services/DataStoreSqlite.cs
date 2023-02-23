using AppliedActivity3.Models;
using SQLite;

namespace AppliedActivity3.Services
{
    class DataStoreSqlite : IDataStore
    {
        SQLiteAsyncConnection Database;

        private const string DatabaseFilename = "AppliedActivity3.db3";

        private const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;

        private static string DatabasePath => Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);

        async Task Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(DatabasePath, Flags);

            if (File.Exists(DatabasePath))
                return;

            await Database.CreateTableAsync<Student>();
            await Database.CreateTableAsync<Course>();

            await InsertSampleData();
        }

        private async Task InsertSampleData()
        {
            await SaveStudentAsync(new Student() {  FirstName = "Nathan", LastName = "Abourbih", Number = "A00000001"});
            await SaveCourseAsync(new Course() { Code = "ISP1004", Name = "Advanced Android", CRN = "12345" });
        }

        public async Task<List<Student>> GetStudentsAsync()
        {
            await Init();
            return await Database.Table<Student>().ToListAsync();
        }

        public async Task<Student> GetStudentAsync(int id)
        {
            await Init();
            return await Database.Table<Student>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveStudentAsync(Student student)
        {
            await Init();
            if (student.Id != 0)
            {
                return await Database.UpdateAsync(student);
            }
            else
            {
                return await Database.InsertAsync(student);
            }
        }

        public async Task<int> DeleteStudentAsync(Student student)
        {
            await Init();
            return await Database.DeleteAsync(student);
        }

        public async Task<List<Course>> GetCoursesAsync()
        {
            await Init();
            return await Database.Table<Course>().ToListAsync();
        }

        public async Task<Course> GetCourseAsync(int id)
        {
            await Init();
            return await Database.Table<Course>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveCourseAsync(Course course)
        {
            await Init();
            if (course.Id != 0)
            {
                return await Database.UpdateAsync(course);
            }
            else
            {
                return await Database.InsertAsync(course);
            }
        }

        public async Task<int> DeleteCourseAsync(Course course)
        {
            await Init();
            return await Database.DeleteAsync(course);
        }
    }
}
