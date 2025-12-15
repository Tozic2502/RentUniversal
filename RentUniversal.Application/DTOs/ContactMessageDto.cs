namespace RentUniversal.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object (DTO) representing a contact form submission.
    /// Used to carry basic sender details and the message content from the client to the API.
    /// </summary>
    public class ContactMessageDto
    {
        /// <summary>
        /// The sender's name as provided in the contact form.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The sender's email address (used for follow-up communication).
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// The message content submitted by the sender.
        /// </summary>
        public string Message { get; set; } = string.Empty;
    }
}