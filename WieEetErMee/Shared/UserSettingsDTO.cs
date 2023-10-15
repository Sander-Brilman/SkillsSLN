using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WieEetErMee.Shared;
public class UserSettingsDTO
{
    [MaxLength(100)]
    public string Name { get; set; }

    public bool DefaultPresentOnMonday { get; set; } = false;

    public bool DefaultPresentOnTuesday { get; set; } = false;

    public bool DefaultPresentOnWednesday { get; set; } = false;

    public bool DefaultPresentOnThursday { get; set; } = false;

    public bool DefaultPresentOnFriday { get; set; } = false;

    public bool DefaultPresentOnSaturday { get; set; } = false;

    public bool DefaultPresentOnSunday { get; set; } = false;
}
