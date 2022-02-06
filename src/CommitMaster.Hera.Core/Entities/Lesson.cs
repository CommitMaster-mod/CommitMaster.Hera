using CommitMaster.Hera.Core.Entities.Base;

namespace CommitMaster.Hera.Core.Entities
{
    public class Lesson : Entity
    {
        public string Title { get; set; }
        public string  Description { get; set; }
        public Video Video { get; set; }
        //public Guid NextLessonId { get; set; }
        
        public Module Module { get; set; }
        public Guid ModuleId { get; set; }
    }

    public class Video
    {
        public string VideoUrl { get; set; }
    }
}
