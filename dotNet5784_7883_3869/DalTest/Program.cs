﻿using Dal;
using DalApi;
using System.Diagnostics;
using System.Xml.Serialization;

namespace DalTest
{
    internal class Program
    {
        private static ITask? s_dalTask = new TaskImplementation(); //stage 1
        private static IEngineer? s_dalEngineer = new EngineerImplementation(); //stage 1
        private static IDependency? s_dalDependency = new DependencyImplementation(); //stage 1

        static void Main(string[] args)
        {
            Initialization.Do(s_dalTask, s_dalEngineer, s_dalDependency);

            do
            {
                Console.WriteLine("please choose an option\n for Task press 1\n for Engineer press 2\n for Dependency press 3\n for exit press 0\n ");
                Console.ReadLine(choiceEntity);
                //כאן להוציא לפונקציה


                switch (choiceEntity)
                {
                    case 1:
                        {
                            TaskEntity();
                        }
                    case 2:
                        {
                            EngineerEntity();
                        }
                    case 3:
                        {
                            DependencyEnity();
                        }
                    default:
                }

            } while (choiceEntity != 0);


        }
        private static void createTaskEntity()
        {

        }





















        private static void createEngineerEntity()
        {
            do
            {
                Console.WriteLine("please choose an option\n for add a Engineer press 1\n for show one Engineer press 2\n for show all the Engineers press 3\n for updating Engineer details press 4\n for delete Engineer press 5\n for exit press press 0\n ");
                Console.ReadLine(choiceEngineer);
                switch (choiceEngineer)
                {
                    case 1:
                        creatNewEngineer();
                        break;
                    case 2:
                        int id;
                        Console.WriteLine("enter id wanted\n");
                        Console.ReadLine(id);
                        showOneEngineer(id);
                        break;
                    case 3:

                    case 4:

                    case 5:

                    default:
                        break;
                }
            } while (choiceEngineer != 0);
        }
        private static void creatNewEngineer()
        {
            try
            {
                int _ID;
                string _Name;
                string _Email;
                EngineerExperience _Level;
                double _Cost;
                Console.WriteLine("please enter the Engineer details\n");
                Console.WriteLine("enter the Engineers id\n");
                int.TryParse(Console.ReadLine()!, out _ID);
                Console.WriteLine("enter the Engineers name\n");
                Console.ReadLine(_Name);
                Console.WriteLine("enter the Engineers email\n");
                Console.ReadLine(_Email);
                Console.WriteLine("enter the Engineers level\n");
                EngineerExperience.TryParse(Console.ReadLine(), out _Level);
                Console.WriteLine("enter the Engineers Cost\n");
                double.TryParse(Console.ReadLine()!, out _Cost);
                Engineer newEngineer = new (_ID, _Name, _Email, _Level, _Cost);
                s_dalEngineer!.Create(newEngineer);
                Console.WriteLine(newEngineer.Id);

               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private static void showOneEngineer(int id)
        {
            foreach(var engineer in s_dalEngineer)
            {
                if(engineer.Id == id) 
                engineer.Show();
            }
        }



        }
        private static void TaskEntity()
        {
            int Id;
            do
            {
                Console.WriteLine("please choose an option\n for add a task press 1\n for show one Task press 2\n for show all the tasks press 3\n for updating task details press 4\n for delete task press 5\n for exit press press 0\n ");
                Console.ReadLine(choiceTask);
                switch (choiceTask)
                {
                    case 1:
                        createNewTask();
                        break;
                    case 2:
                        Console.WriteLine("Enter wanted ID");
                        Console.ReadLine(Id);
                        showTask(Id);
                        break;
                    case 3:
                        showAllTasks();
                        break;
                    case 4:
                        updateTask();
                    default:
                        break;
                }
            } while (choiceTask != 0);
        }

        private static void createNewTask()
        {
            try
            {
                string _Description;
                DateTime _Start;
                DateTime _ScheduledDate;
                DateTime _DeadLine;
                DateTime _Complete;
                string _Deliverable;
                int _Engineerld;
                TaskLevel _ComplexityLevel;
                Console.WriteLine("please enter the task's details\n");
                Console.WriteLine("Enter the task's description\n");
                Console.ReadLine(_Description);
                Console.WriteLine("Enter the start time\n");
                DateTime.TryParse(Console.ReadLine(), out _Start);
                Console.WriteLine("Enter the scheduled date\n");
                DateTime.TryParse(Console.ReadLine(), out _ScheduledDate);
                Console.WriteLine("Enter the deadline\n");
                DateTime.TryParse(Console.ReadLine(), out _DeadLine);
                Console.WriteLine("Enter the complete time\n");
                DateTime.TryParse(Console.ReadLine(), out _Complete);
                Console.WriteLine("Enter the deliverable");
                Console.ReadLine(_Deliverable);
                Console.WriteLine("enter the engineer's Id\n");
                Console.ReadLine(_Engineerld);
                Console.WriteLine("Enter the task's level");
                TaskLevel.TryParse(Console.ReadLine(), out _ComplexityLevel);
                DO.Task newTask = new(_Description, false, null, _Start, _ScheduledDate, _DeadLine, _Complete, _Deliverable, _Engineerld, _ComplexityLevel);
                s_dalTask!.Create(newTask);
                Console.WriteLine(newTask.Id);
            }
            catch (error)
            {
                throw new Exception("can't make new task");
            }
        }


        private static void showTask(int Id)
        {
            foreach (var item in s_dalTask)
            {
                if(item.Id == Id) {
                    Console.WriteLine(item);
                }
            }
        }


        //show all the tasks in the task's array
        private static void showAllTasks()
        {
            foreach (Task item in s_dalTask!.Read())
            {
                if (item.ID != 0)
                {
                    Console.WriteLine(item);
                }
            }
        }
    }

}