using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalNotes.Shared;
public class LoginResultDTO
{
    public required bool Success { get; set; }

    public required string Message { get; set; } = "";

}
