﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface IMilestone
{
    //יצירת לו"ז
    public BO.Milestone getMilestoneDetials(int ID);
    public BO.Milestone updateMilestone(int ID);

}