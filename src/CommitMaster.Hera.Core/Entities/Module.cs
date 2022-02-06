using CommitMaster.Hera.Core.Entities.Base;

namespace CommitMaster.Hera.Core.Entities
{
    public class Module : Entity
    {
        public string Name { get; set; }
        public int Order { get; set; }
        
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
        
        public IEnumerable<Lesson> Lessons { get; set; }
    }
}
