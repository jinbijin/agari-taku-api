using System.ComponentModel.DataAnnotations;

namespace AgariTakuServer.Models
{
    public class LobbyUserModel
    {
        [Required]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "Lobby code must consist of 8 characters.")]
        public string LobbyId { get; set; } = string.Empty;

        [Required]
        [StringLength(12, ErrorMessage = "User name must be at most 12 characters long.")]
        public string UserName { get; set; } = string.Empty;
    }
}
