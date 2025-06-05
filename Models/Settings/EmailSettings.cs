using System.ComponentModel.DataAnnotations;

namespace EducationalInstitution.Models.Settings;

public class EmailSettings
{
    [Required]
    [EmailAddress]
    public string? From { get; set; }

    [Required]
    public string? Server { get; set; }

    [Required]
    public string? Password { get; set; }

    public string? User { get; set; }

    [Required]
    public bool UseSsl { get; set; }

    [Required]
    public int Port { get; set; }
}
