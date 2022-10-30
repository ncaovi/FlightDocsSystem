using System;
using System.ComponentModel.DataAnnotations;

namespace FlightDocsSystem.Model
{
    public class DocumentList
    {
        [Key]
        public int DocumentId { get; set; }
        public string DocumentName { get; set; }
        public string Type { get; set; }
        public DateTime createdDate { get; set; }
        public string FlightNo { get; set; }

        public int UserId { get; set; }
        public UserModel User { get; set; }
    }
}
