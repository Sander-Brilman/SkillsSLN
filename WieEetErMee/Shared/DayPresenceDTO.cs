﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WieEetErMee.Shared;
public class DayPresenceDTO
{
    public DateOnly Date { get; set; }

    public List<UserPresenceDTO> TotalPresence { get; set; } = new();
}
