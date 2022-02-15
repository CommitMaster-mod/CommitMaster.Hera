using CommitMaster.Hera.Core.Entities.Base;

namespace CommitMaster.Hera.Core.Entities
{
    public class Category : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        
        //Ef Core
        public IEnumerable<Course> Courses { get; set; }

        public Category()
        {
            Courses = new List<Course>();
        }
    }
}
