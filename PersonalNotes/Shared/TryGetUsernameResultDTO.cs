using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalNotes.Shared;
public class TryGetUsernameResultDTO
{
    public required bool IsLoggedIn { get; set; }

    public required string Username { get; set; }
}
