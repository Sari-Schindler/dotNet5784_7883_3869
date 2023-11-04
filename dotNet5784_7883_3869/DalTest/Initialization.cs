

namespace DalTest;
using DalApi;
using DO;
using System;
using System.Data.Common;


public static class Initialization
{
    private static ITask? s_dalTask;
    private static IDependency? s_dalDependency;   
    private static IEngineer? s_dalEngineer;


    //fill 100 Task's entities 
    private static void createTask()
    {
        string[] EngineerNames = new string[]
        {
            "Dani Levi",
            "Eli Amar",
            "Yair Cohen",
            "Ariela Levin",
            "Dina Klein",
            "Shira Israelof",
            "Lea Katz",
            "Rachel Shapira",
            "Alice",
            "Bob",
            "Charlie",
            "David",
            "Emma",
            "Frank",
            "Grace",
            "Hannah",
            "Isaac",
            "Julia",
            "Kevin",
            "Linda",
            "Michael",
            "Nora",
            "Oliver",
            "Penny",
            "Quincy",
            "Rachel",
            "Samuel",
            "Tina",
            "Ulysses",
            "Victoria",
            "William",
            "Xander",
            "Yvonne",
            "Zachary",
            "Sophia",
            "Ethan",
            "Ava",
            "Liam",
            "Mia",
            "Noah",
            "Olivia"
        };
        foreach (var _name in EngineerNames)
        {
            int _id;
            string _email;
            int MIN_ID = 100000000, MAX_ID = 999999999;
            do
                _id = s_rand.Next(MIN_ID, MAX_ID);
            while (s_dalStudent!.Read(_id) != null);
            do
            {
                _id = s_rand.Next(MIN_ID, MAX_ID);
            }
            while (s_dalEngineer!.Read(_id) != null);
            _email = $"{_name}@gmail.com";
            int level = (_name.Length) % 3;
            EngineerExperience _level = (EngineerExperience)level;

            double _cost = s_rand.Next(100, 350);
            Engineer newEngineer=new(_id,_name, _email, _level, _cost);
            s_dalStudent!.Create(newEngineer);
        }
    }


    //private static void createEngineer(EngineerExperience engineerExperience)
    //{
    //    const int MIN_ID = 200000000;
    //    const int MAX_ID = 400000000;
    //    string[] engineerNames =
    //    {
    //    "Dani Levi", "Eli Amar", "Yair Cohen",
    //    "Ariela Levin", "Dina Klein", "Shira Israelof"
    //    };

    //    foreach (var _name in engineerNames)
    //    {
    //        int _id;
    //        string _email;
    //        double _cost;

    //        _email = _name.Replace(" ", ".");//Replaces the space between the names with a point to match an email address
    //        do
    //        {
    //            _id = s_rand.Next(MIN_ID, MAX_ID);
    //        }
    //        while (s_dalEngineer!.Read(_id) != null);
    //        _email = $"{_email}@gmail.com";

    //        //Converting an int to an enum type in order to get an enum value in a specific place.
    //        int _levelNumber = _id % 5;
    //        EngineerExperience _level = (EngineerExperience)_levelNumber;

    //        _cost = (double)s_rand.Next(100, 500);

    //        Engineer newEngineer = new(_id, _name, _email, _level, _cost);

    //        s_dalEngineer!.Create(newEngineer);
    //    }
    }



    public static class Initialization
{
    private static IDependency? s_dalDependency; //stage 1
    private static IEngineer? s_dalEngineer; //stage 1
    private static ITask? s_dalTask; //stage 1

    private static readonly Random s_rand = new();



}
    }
    //private static void createStudents()
    //{
    //    string[] studentNames =
    //    {
    //    "Dani Levi",
    //    "Eli Amar",
    //    "Yair Cohen",
    //    "Ariela Levin",
    //    "Dina Klein",
    //    "Shira Israelof"
    //};

    //    foreach (var _name in studentNames)
    //    {
    //        int _id;
    //        do
    //            _id = s_rand.Next(MIN_ID, MAX_ID);
    //        while (s_dalStudent!.Read(_id) != null);

    //        bool? _b = (_id % 2) == 0 ? true : false;
    //        Year _year =
    //        (Year)s_rand.Next((int)Year.FirstYear, (int)Year.ExtraYear + 1);

    //        DateTime start = new DateTime(1995, 1, 1);
    //        int range = (DateTime.Today - start).Days;
    //        DateTime _bdt = start.AddDays(s_rand.Next(range));

    //        Student newStu = new(_id, _name, null, _b, _year, _bdt);

    //        s_dalStudent!.Create(newStu);





    private static readonly Random s_rand = new();
}



