using System.ComponentModel.DataAnnotations;

namespace ProjectManager.ASPMVC.Models.Project
{
    public class CreateForm
    {
        [Required]
        public string Name { get; set; } = null!;   

        [Required] 
        public string Description { get;set ; } = null!;
    }
}
