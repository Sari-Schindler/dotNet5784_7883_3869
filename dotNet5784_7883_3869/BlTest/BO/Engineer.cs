﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class Engineer
{
    public int ID { get; set; }
    public required string Name { get; set; }
    public EngineerExperience Level { get; set; } 
    public required double Cost {  get; set; }      
    public string? Email { get; set; }
    // לבדוק מה עם התכונה של מזהה...

}
