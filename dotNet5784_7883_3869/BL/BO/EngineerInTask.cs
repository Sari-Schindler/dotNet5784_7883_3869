﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

/// <summary>
/// class for engineer in task
/// </summary>

public class EngineerInTask
{
    public required int ID {  get; set; }
    public string? Name { get; set; }
    public override string ToString() => this.ToStringProperty();

}
