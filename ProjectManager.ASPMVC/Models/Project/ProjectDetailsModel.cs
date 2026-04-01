namespace ProjectManager.ASPMVC.Models.Project
{
    public class ProjectDetailsModel
    {
        public BLL.Entities.Project Project { get; set; }
        public IEnumerable<BLL.Entities.Employee> Members { get; set; }
        public IEnumerable<BLL.Entities.Post> Posts { get; set; }
        public IEnumerable<BLL.Entities.Employee> FreeEmployees { get; set; }
    }
}
