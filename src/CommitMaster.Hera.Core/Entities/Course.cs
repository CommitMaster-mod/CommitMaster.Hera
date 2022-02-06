using CommitMaster.Hera.Core.Entities.Base;
using CommitMaster.Hera.Core.Entities.VOs;

namespace CommitMaster.Hera.Core.Entities
{
    public class Course : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Cover Cover { get; set; }
        
        //Relation
        public Category Category { get; set; }
        public Guid CategoryId { get; set; }
        
        //Ef core
        public IEnumerable<Module> Modules { get; set; }
    }

}
