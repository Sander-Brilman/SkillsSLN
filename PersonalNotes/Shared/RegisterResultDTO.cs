namespace PersonalNotes.Shared;

public class RegisterResultDTO
{
    public required  bool Success { get; set; }

    public required string[] Message { get; set; } = Array.Empty<string>();
}
