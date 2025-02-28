using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatProject.Domain.Entities
{
    public class Profile
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string Bio { get; set; }
        public DateTime? Birthdate { get; set; }
        public string? Company { get; set; }
        public string? Website { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        // Foreign key to the ApplicationUser table
        public string UserId { get; set; }
        public ApplicationUser User { get; set; } // Navigation property
    }

}
