using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlightDocsSystem.Model
{
    public class GroupPermission
    {
        [Key]
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public DateTime createdDate { get; set; }
        public string Note { get; set; }
        public List<UserModel> users { get; set; }
    }
}
