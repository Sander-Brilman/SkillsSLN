using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WieEetErMee.Shared;
public class UserPresenceDTO
{
    public DateOnly Date { get; set; }

    public string Name { get; set; } = "";

    public bool IsPresent { get; set; }
}
