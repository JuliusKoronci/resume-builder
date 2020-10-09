using System;
using Domain.Common;

namespace Domain.Entities
{
    public class Profile : AuditableEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string WebSite { get; set; }
    }
}
