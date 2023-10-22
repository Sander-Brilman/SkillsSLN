﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalNotes.Shared;
public class LoginDTO
{
    [Required]
    public string UserName { get; set; } = "";

    [Required]
    public string Password { get; set; } = string.Empty;

    [Required]
    public bool IsPersistant { get; set; }
}
