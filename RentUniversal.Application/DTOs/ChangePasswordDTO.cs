namespace RentUniversal.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object (DTO) used when a user requests to change their password.
    /// Contains the current (old) password for verification and the desired new password.
    /// </summary>
    public class ChangePasswordDTO 
    {
        /// <summary>
        /// The user's current password (used to verify identity before allowing a change).
        /// </summary>
        public string OldPassword { get; set; } = "";
        
        /// <summary>
        /// The new password the user wants to set.
        /// </summary>
        public string NewPassword { get; set; } = "";
    }
    
}
