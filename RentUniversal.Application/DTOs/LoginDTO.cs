namespace RentUniversal.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object (DTO) used for login/authentication requests.
    /// Carries the credentials provided by the user from the client to the API.
    /// </summary>
    public class LoginDTO
    {
        /// <summary>
        /// Username/display name entered by the user.
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// Email address used to identify the user during login.
        /// </summary> 
        public string Email { get; set; } = "";

        /// <summary>
        /// password provided by the user for authentication.
        /// </summary>
        public string Password { get; set; } = "";
        public int IdentificationId { get; set; }
    }
}