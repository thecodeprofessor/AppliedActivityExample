using EmployeeApplication.Models;
using SQLite;

namespace EmployeeApplication.Services
{
    class EmployeeDataStoreSqlite : IEmployeeDataStore
    {
        SQLiteAsyncConnection Database;

        private const string DatabaseFilename = "Employees.db3";

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

            await Database.CreateTableAsync<Employee>();

            var randomService = DependencyService.Get<IRandomUser>();
            var randomUsers = await randomService.GetRandomUsersAsync(20);

            foreach (var randomUser in randomUsers)
            {
                var firstName = randomUser.name.first.ToString();
                var lastName = randomUser.name.last.ToString();
                var name = $"{firstName} {lastName}";
                var email = randomUser.email.ToString();
                var image = randomUser.picture.medium.ToString();

                await SaveEmployeeAsync(new Employee() { Name = name, Email = email, Image = image });
            }
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            await Init();
            return await Database.Table<Employee>().ToListAsync();
        }

        public async Task<Employee> GetEmployeeAsync(int id)
        {
            await Init();
            return await Database.Table<Employee>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveEmployeeAsync(Employee employee)
        {
            await Init();
            if (employee.Id != 0)
            {
                return await Database.UpdateAsync(employee);
            }
            else
            {
                return await Database.InsertAsync(employee);
            }
        }

        public async Task<int> DeleteEmployeeAsync(Employee employee)
        {
            await Init();
            return await Database.DeleteAsync(employee);
        }
    }
}
