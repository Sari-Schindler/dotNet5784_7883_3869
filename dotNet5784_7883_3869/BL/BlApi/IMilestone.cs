﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

/// <summary>
/// Include all methods of the milestone
/// </summary>
public interface IMilestone
{
    //יצירת לו"ז
    //public BO.Milestone Read(int ID);
    public BO.Milestone Update(int ID);

}
