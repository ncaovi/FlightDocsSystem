using System.ComponentModel.DataAnnotations;

namespace FlightDocsSystem.Model
{
    public class RoleModel
    {
        [Key]
        public int RoleId { get; set; }

        public string RoleName { get; set; }
    }
}
