namespace ChatProject.API.DTOs
{
    public class RegisterDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        //// Profile Data
        //public string? DisplayName { get; set; }
        //public string? ProfilePictureUrl { get; set; }
        //public string? Bio { get; set; }
        //public DateTime? Birthdate { get; set; }
        //public string? Company { get; set; }
        //public string? Website { get; set; }
    }

}
