using SQLite;

namespace AppliedActivity3.Models
{
    [Table("Students")]
    public class Student
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Number { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
