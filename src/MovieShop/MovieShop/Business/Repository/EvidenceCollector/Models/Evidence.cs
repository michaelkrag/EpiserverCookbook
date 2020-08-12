using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieShop.Business.Repository.EvidenceCollector.Models
{
    public class Evidence
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string ContentId { get; set; }
        public string Event { get; set; }
        public DateTime Date { get; set; }
        public string SessionId { get; set; }
    }
}