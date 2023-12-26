using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlTest.BO;

public class Task
{
    public required string Description { get; set; }
    //public bool? Milestone { get; set; } //אבן דרך קשורה (מזהה וכינוי)
    public required DateTime CreatedDateTask { get; set; }
    public required DateTime EstimatedStartTime { get; set; }
    public required DateTime StartTime { get; set; }
    public required Status TaskStatus { get; set; }

    //רשימת תלויות (מסוג משימה-ברשימה)
    public required DateTime TimeEstimatedLeft { get; set; }
    public required DateTime DeadLine {  get; set; }
    public required DateTime CompleteDate { get; set; }
    public required string productDescription { get; set; }
    public TaskLevel? ComplexityLevel {  get; set; }
    public string? nickName {  get; set; }  
    public string? Comments { get; set; }
    public int ID { get; init; }
    //לבדוק אם קיימת מזהה ושם מהנדס שהקוצה למשימה

}