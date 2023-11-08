﻿using Dal;
using DalApi;
using DO;
using System.Diagnostics;
using System.Numerics;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DalTest
{
    internal class Program
    {
        private static ITask? s_dalTask = new TaskImplementation(); //stage 1
        private static IEngineer? s_dalEngineer = new EngineerImplementation(); //stage 1
        private static IDependency? s_dalDependency = new DependencyImplementation(); //stage 1


        /// <summary>
        /// main menue for choose entity
        /// </summary>
        public static void allEntities()
        {
            int choiceEntity = 0;
            do
            {
                Console.WriteLine("please choose an option\n for Task press 1\n for Engineer press 2\n for Dependency press 3\n for exit press 0\n ");
                choiceEntity = (Convert.ToInt32(Console.ReadLine()));

                switch (choiceEntity)
                {
                    case 1:

                        TaskEntity();
                        break;

                    case 2:
                        EngineerEntity();
                        break;

                    case 3:

                        DependencyEntity();
                        break;

                    default:
                        break;
                }

            } while (choiceEntity != 0);
        }

        

     
        /// <summary>
        /// menue for choose engineer's method
        /// </summary>
        public static void EngineerEntity()
        {
            int choiceEngineer = 0;
            do
            {
                Console.WriteLine("please choose an option\n for add a Engineer press 1\n for show one Engineer press 2\n for show all the Engineers press 3\n for updating Engineer details press 4\n for delete Engineer press 5\n for exit press press 0\n ");
                choiceEngineer = (Convert.ToInt32(Console.ReadLine()));
                switch (choiceEngineer)
                {
                    case 1:
                        int id;
                        Console.WriteLine("enter engineer's id wanted\n");
                        id = Convert.ToInt32(Console.ReadLine());
                        s_dalEngineer!.Create(creatNewEngineer(id));
                        break;
                    case 2:
                        Console.WriteLine("enter engineer's id wanted\n");
                        Console.WriteLine(s_dalEngineer!.Read(Convert.ToInt32(Console.ReadLine())));
                        break;
                    case 3:
                        foreach (var engineer in s_dalEngineer!.ReadAll())
                        {
                            Console.WriteLine(engineer);
                        };
                        break;

                    case 4:
                        Console.WriteLine("enter engineer's id wanted\n");
                        id= Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine(s_dalEngineer!.Read(id));
                        updateEngineer(id);
                        //s_dalEngineer!.Update(creatNewEngineer(id));
                       
                        break;
                    case 5:
                        Console.WriteLine("enter engineer's id wanted\n");
                        s_dalEngineer!.Delete(Convert.ToInt32(Console.ReadLine()));
                        break;
                    default:
                        break;

                }


            } while (choiceEngineer != 0);
        }



        /// <summary>
        /// functuion for create new engineer's entity
        /// </summary>
        /// <param name="_ID">create new engineer with the wanted ID (if theres)</param>
        /// <returns>new engineer entity</returns>
        private static DO.Engineer creatNewEngineer(int _ID)
        {
            EngineerExperience _Level;
            double _Cost;
            Console.WriteLine("please enter the Engineer details\n");
            Console.WriteLine("enter the Engineers name\n");
            string _Name = Console.ReadLine();
            Console.WriteLine("enter the Engineers email\n");
            string _Email = Console.ReadLine();
            Console.WriteLine("enter the Engineers level\n");
            EngineerExperience.TryParse(Console.ReadLine(), out _Level);
            Console.WriteLine("enter the Engineers Cost\n");
            double.TryParse(Console.ReadLine()!, out _Cost);
            if (_Cost ==0) _Cost = 0;
            if (_Name == null) _Name = "";
            if (_Email == null) _Email = "";
            return new Engineer(_ID, _Name, _Email, _Level, _Cost);
 

        }

        /// <summary>
        /// update new details in spesific engineer
        /// </summary>
        /// <param name="_Id">update the engineer with the wanted ID</param>
        private static void updateEngineer(int _Id)
        {
            try
            {
                // לא עובד בדיקת אימייל
                Console.WriteLine("Enter ID of an Engineer");
                int _id;
                int.TryParse(Console.ReadLine()!, out _id);
                DO.Engineer? _engineer = s_dalEngineer!.Read(_Id);
                DO.Engineer temp = creatNewEngineer(_Id);
                string _Name=temp.Name;
                string _Email = Console.ReadLine();
                EngineerExperience _Level=temp.Level;
                double _Cost=temp.Cost;
                if (temp.Name is null) 
                {
                    _Name = _engineer.Name;
                }
                if (temp.Email== "")
                {
                    _Email = _engineer.Email;
                }
                EngineerExperience? _copmlexityLevel = tryParseNullableEngineerExperience(_engineer!.Level);
                if(temp.Cost is 0)
                {
                    _Cost = _engineer.Cost; 
                }
                s_dalEngineer.Update(new DO.Engineer(_id, _Name, _Email, _Level, _Cost));

            }
            catch (Exception newException)
            {
                Console.WriteLine(newException.Message);
            }
        }


        private static EngineerExperience? tryParseNullableEngineerExperience(EngineerExperience? previous)
        {
            EngineerExperience value;
            return EngineerExperience.TryParse(Console.ReadLine(), out value) ? value : previous;
        }


        /// <summary>
        /// menue for task's method
        /// </summary>
        public static void TaskEntity()
        {
            
            int choiceTask = 0;
            do
            {
                Console.WriteLine("please choose an option\n for add a task press 1\n for show one Task press 2\n for show all the tasks press 3\n for updating task details press 4\n for delete task press 5\n for exit press press 0\n ");
                choiceTask = (Convert.ToInt32(Console.ReadLine()));
                switch (choiceTask)
                {
                    case 1:
                        s_dalTask!.Create(createNewTask());
                        break;
                    case 2:
                        Console.WriteLine("Enter wanted task's ID");
                        Console.WriteLine(s_dalTask!.Read(Convert.ToInt32(Console.ReadLine())));
                        break;
                    case 3:
                        foreach (var item in s_dalTask!.ReadAll())
                            Console.WriteLine(item);
                        break;
                    case 4:
                        Console.WriteLine("Enter wanted task's ID");
                        int id = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine(s_dalTask!.Read(id));
                        updateTask(id);
                        //s_dalTask!.Update(createNewTask(id));
                        break;
                    case 5:
                        Console.WriteLine("Enter wanted task's ID");
                        s_dalTask!.Delete(Convert.ToInt32(Console.ReadLine()));
                        break;
                    default:
                        break;
                }
            } while (choiceTask != 0);
        }


        /// <summary>
        /// function for create new task
        /// </summary>
        /// <param name="id">create new task with the wanted ID</param>
        /// <returns>new task</returns>
        public static DO.Task createNewTask(int id = 0)
        {

            DateTime _createdAdt;
            DateTime _Start;
            DateTime _ScheduledDate;
            DateTime _DeadLine;
            DateTime _Complete;
            int _Engineerld;
            bool _Milestone;
            TaskLevel _ComplexityLevel;
            Console.WriteLine("please enter the task's details\n");
            Console.WriteLine("Enter the task's description\n");
            string ?_Description = Console.ReadLine();
            Console.WriteLine("enter the milestone\n");
            bool.TryParse(Console.ReadLine(), out _Milestone);
            Console.WriteLine("Enter the task's createdAdt\n");
            DateTime.TryParse(Console.ReadLine(), out _createdAdt);
            Console.WriteLine("Enter the start time\n");
            DateTime.TryParse(Console.ReadLine(), out _Start);
            Console.WriteLine("Enter the scheduled date\n");
            DateTime.TryParse(Console.ReadLine(), out _ScheduledDate);
            Console.WriteLine("Enter the deadline\n");
            DateTime.TryParse(Console.ReadLine(), out _DeadLine);
            Console.WriteLine("Enter the complete time\n");
            DateTime.TryParse(Console.ReadLine(), out _Complete);
            Console.WriteLine("Enter the deliverable");
            string ?_Deliverable = Console.ReadLine();
            Console.WriteLine("enter the engineer's Id\n");
            int.TryParse(Console.ReadLine(), out _Engineerld);
            Console.WriteLine("Enter the task's level");
            TaskLevel.TryParse(Console.ReadLine(), out _ComplexityLevel);
           // if(_Engineerld == 0)    
            return (new DO.Task(_Description, _Milestone, _createdAdt, _Start, _ScheduledDate, _DeadLine, _Complete, _Deliverable, _Engineerld, _ComplexityLevel,null,null,id));
        }


        /// <summary>
        /// update new details in spesific task
        /// </summary>
        /// <param name="_id">update the task with the wanted ID</param>
        private static void updateTask(int _id)
        {
            try
            {
                Console.WriteLine("Enter ID of a task");
                int.TryParse(Console.ReadLine()!, out _id);
                DO.Task? _task = s_dalTask!.Read(_id);
                DO.Task tempTask = createNewTask(_id);
                string _Description = tempTask.Description;
                DateTime _createdAdt;
                DateTime.TryParse(Console.ReadLine(), out _createdAdt);
                DateTime _Start = tempTask.Start;
                DateTime.TryParse(Console.ReadLine(), out _Start);
                DateTime _ScheduledDate = tempTask.ScheduledDate;
                DateTime.TryParse(Console.ReadLine(), out _ScheduledDate);
                DateTime _DeadLine = tempTask.DeadLine;
                DateTime.TryParse(Console.ReadLine(), out _DeadLine);
                DateTime _Complete = tempTask.Complete;
                DateTime.TryParse(Console.ReadLine(), out _Complete);
                string _Deliverable = tempTask.Deliverable;
                int _Engineerld = tempTask.Engineerld;
                TaskLevel _ComplexityLevel;
                TaskLevel.TryParse(Console.ReadLine(), out _ComplexityLevel);
                if (tempTask.Description is null)
                {
                    _Description = _task.Description;
                }
                if (_createdAdt == null)
                {
                    _createdAdt = _task.createdAdt;
                }
                if (_Start == null)
                    _Start = _task.Start;
                if (_ScheduledDate == null)
                    _ScheduledDate = _task.ScheduledDate;
                if (_DeadLine == null)
                    _DeadLine = _task.DeadLine;
                if (_Complete == null)
                    _Complete = _task.Complete;
                if (_Deliverable == null)
                    _Deliverable = _task.Deliverable;
                if (_Engineerld == 0)
                    _Engineerld = _task.Engineerld;
                s_dalTask.Update(new DO.Task(_Description, false, _createdAdt, _Start, _ScheduledDate, _DeadLine, _Complete, _Deliverable, _Engineerld, _ComplexityLevel));
            }
            catch (Exception newException)
            {
                Console.WriteLine(newException.Message);
            }
        }

        /// <summary>
        /// menue for chhose dependency's method
        /// </summary>

        public static void DependencyEntity()
            {
                int chioceDependency = 0;
                do
                {
                    Console.WriteLine("please choose an option\n for add a Dependency press 1\n for show one Dependency press 2\n for show all the Dependency press 3\n for updating Dependency details press 4\n for delete Dependency press 5\n for exit press press 0\n ");
                    chioceDependency = (Convert.ToInt32(Console.ReadLine()));

                    switch (chioceDependency)
                    {
                        case 1:
                            s_dalDependency!.Create(createNewDependency());
                            break;
                        case 2:
                            Console.WriteLine("Enter Dependency id\n");
                            Console.WriteLine(s_dalDependency!.Read(Convert.ToInt32(Console.ReadLine())));
                            break;
                        case 3:
                            foreach (var item in s_dalDependency!.ReadAll())
                            {
                                Console.WriteLine($"id: {item.ID}  id's task: {item.DependentTask}   id's pervious task: {item.previousIDTask}");
                            };
                            break;
                        case 4:
                            Console.WriteLine("Enter Dependency's id\n");
                            //s_dalDependency!.Update(createNewDependency(Convert.ToInt32(Console.ReadLine())));
                            int _id = Convert.ToInt32(Console.ReadLine());
                            updateDependency(_id);

                            break;
                        case 5:
                            Console.WriteLine("Enter Dependency id\n");
                            s_dalDependency!.Delete(Convert.ToInt32(Console.ReadLine()));
                            break;
                        default:
                            break;

                    }
                } while (chioceDependency != 0);
        }

        /// <summary>
        /// create a new dependency entity
        /// </summary>
        /// <param name="dependencyId">create new dependency with the wanted ID(if theres)</param>
        /// <returns>new dependency</returns>
            private static DO.Dependency createNewDependency(int dependencyId = 0)
            {
                int taskId;
                int prevTaskId;
                Console.WriteLine("Enter task id:");
                taskId = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter pervious task id:");
                prevTaskId = Convert.ToInt32(Console.ReadLine());
                return new DO.Dependency(taskId, prevTaskId, dependencyId);
            }


        /// <summary>
        /// update new details in spesific dependency
        /// </summary>
        /// <param name="id">update the dependency  with specific ID</param>
        private static void updateDependency(int id)
        {
            try
            {
                Console.WriteLine("Enter ID of dependency");
                int.TryParse(Console.ReadLine()!, out id);
                DO.Dependency? _dependency = s_dalDependency!.Read(id);
                DO.Dependency tempDependency = createNewDependency(id);
                int _DependentTask = tempDependency.DependentTask;
                int _previousIDTask = tempDependency.previousIDTask;
                if (_DependentTask == 0)
                {
                    _DependentTask = _dependency.DependentTask;
                }
                if (_previousIDTask == 0)
                {
                    _previousIDTask = _dependency.previousIDTask;
                }
                s_dalDependency.Update(new DO.Dependency(_DependentTask, _previousIDTask, id));
            }
            catch (Exception newException)
            {
                Console.WriteLine(newException.Message);
            }
        }

        /// <summary>
        /// main function
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
            {
                try
                {
                    Initialization.Do(s_dalDependency, s_dalEngineer, s_dalTask);
                    allEntities();
                }
                catch (Exception error)
                {
                    Console.WriteLine(error.ToString());
                }
            }
        }


    }




