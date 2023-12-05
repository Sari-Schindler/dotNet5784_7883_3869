

namespace DalTest;
using DalApi;
using DO;
using System;
using System.Data.Common;
using System.Numerics;
using System.Security.Cryptography;
using System.Xml.Linq;


public static class Initialization
{
   
    private static IDal? s_dal; //stage 2

    private static readonly Random s_rand = new();


    /// <summary>
    /// create new enginner
    /// </summary>    
    private static void createEngineer()
    {
        string[] EngineerNames =
        {
            "Dani",
            "Eli",
            "Yair",
            "Ariela",
            "Dina",
            "Shira",
            "Lea",
            "Rachel",
            "Alice",
            //"Bob",
            //"Charlie",
            //"David",
            //"Emma",
            //"Frank",
            //"Grace",
            //"Hannah",
            //"Isaac",
            //"Julia",
            //"Kevin",
            //"Linda",
            //"Michael",
            //"Nora",
            //"Oliver",
            //"Penny",
            //"Quincy",
            //"Rachel",
            //"Samuel",
            //"Tina",
            //"Ulysses",
            //"Victoria",
            //"William",
            //"Xander",
            //"Yvonne",
            //"Zachary",
            //"Sophia",
            //"Ethan",
            //"Ava",
            //"Liam",
            //"Mia",
            //"Noah",
            //"Olivia"
        };
        foreach (var _name in EngineerNames)
        {
            int _id;
            string _email;
            int MIN_ID = 200000000, MAX_ID = 400000000;
            do
                _id = s_rand.Next(MIN_ID, MAX_ID);
            while (s_dal!.Engineer.Read(_id) != null);
            do
            {
                _id = s_rand.Next(MIN_ID, MAX_ID);
            }
            while (s_dal!.Engineer.Read(_id) != null);
            _email = $"{_name}@gmail.com";
            int level = (_name.Length) % 3;
            EngineerExperience _level = (EngineerExperience)level;

            double _cost = s_rand.Next(100, 350);
            Engineer newEngineer = new(_id, _name, _email, _level, _cost);

            s_dal!.Engineer.Create(newEngineer);
        }

    }


    /// <summary>
    /// create new task
    /// </summary>
    private static void createTask()
    {
        string[] _descriptionArray =
        {
                "write code A",
                "go over code A",
                "debbug code A",
                "write code B",
                "go over code B",
                "debbug code B",
                "write code C",
                "go over code C",
                "debbug code C"
            };
        string[] _deliverableArray =
        {
                "code A",
                "code B",
                "code C"
        };
        string[] _commentsArray =
        {
                "Important Task",
                "fun task",
                "very important task"
            };
        for (int i = 0; i < 10; i++)
        {
            var AllEngineer = s_dal!.Engineer.ReadAll().ToList();
            string _description = _descriptionArray[i % 8];
            string _deliverable = _deliverableArray[s_rand.Next(0, 3)];
            bool _mileStone = false;
            DateTime _CreatedAdt= DateTime.Now; 
            DateTime _Start = _CreatedAdt.Add(TimeSpan.FromHours(i));
            DateTime _ScheduledDate = _CreatedAdt.Add(TimeSpan.FromDays(i % 20));
            DateTime _DeadLine = _ScheduledDate.Add(TimeSpan.FromDays(7)); ;
            DateTime _Complete = _ScheduledDate.Add(TimeSpan.FromHours(-i % 24));
            int _level = i % 4;
            TaskLevel _ComplexityLevel = (TaskLevel)_level;
            Task _newTask = new DO.Task(_description, _mileStone, _CreatedAdt, _Start, _ScheduledDate, _DeadLine, _Complete,_deliverable, AllEngineer[i % (AllEngineer.Count())]!.ID, _ComplexityLevel);

            s_dal!.Task.Create(_newTask);
        }
    }


    /// <summary>
    /// create new dependency
    /// </summary>
    private static void createDependency()
    {
        var AllTasks = s_dal!.Task!.ReadAll().ToList();
        int _DependentTask;
        int _previousIDTask;
        for (int i = 0; i < 10; i++)
        {
            _DependentTask = AllTasks[i % 8]!.ID; //left few tasks undependency
            _previousIDTask = AllTasks[(i % 6)]!.ID;
            Dependency _tempDependency = new Dependency(_DependentTask, _previousIDTask);
            s_dal!.Dependency.Create(_tempDependency);
        }
    }



    /// <summary>
    /// reset all entities
    /// </summary>
    /// <param name="dalDependency">erset the dependency's entity</param>
    /// <param name="dalEngineer">erset the engineer's entity</param>
    /// <param name="dalTask">erset the task's entity</param>
    /// <exception cref="Exception"></exception>
    //public static void Do(IDependency? dalDependency, IEngineer? dalEngineer , ITask? dalTask )//stage1
    public static void Do(IDal dal) //stage 2
    {

        s_dal = dal ?? throw new NullReferenceException("DAL object can not be null!"); //stage 2

        createEngineer();
        createTask();
        createDependency();
    }
}