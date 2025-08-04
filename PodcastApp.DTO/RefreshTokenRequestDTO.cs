using PodcastApp.DTO.Attributes;

namespace PodcastApp.DTO
{
    public record RefreshTokenRequestDTO
    (
        [SmartRequired]
        string? RefreshToken
    );
}
