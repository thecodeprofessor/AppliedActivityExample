using SQLite;

namespace AppliedActivity3.Models
{
    [Table("Courses")]
    public class Course
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Code { get; set; }
        public string CRN { get; set; }
        public string Name { get; set; }
    }
}