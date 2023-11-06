using Dal;
using DalApi;
using DO;
using System.Diagnostics;
using System.Xml.Serialization;

namespace DalTest
{
    internal class Program
    {
        private static ITask? s_dalTask = new TaskImplementation(); //stage 1
        private static IEngineer? s_dalEngineer = new EngineerImplementation(); //stage 1
        private static IDependency? s_dalDependency = new DependencyImplementation(); //stage 1

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

        

        //engineer

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
                        s_dalEngineer!.Create(creatNewEngineer());
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
                        s_dalEngineer!.Update(creatNewEngineer());
                        break;
                    case 5:
                        Console.WriteLine("enter engineer's id wanted\n");
                        s_dalEngineer!.Read(Convert.ToInt32(Console.ReadLine()));
                        break;
                    default:
                        break;

                }


            } while (choiceEngineer != 0);
        }

        private static DO.Engineer creatNewEngineer()
        {

            int _ID;
            EngineerExperience _Level;
            double _Cost;
            Console.WriteLine("please enter the Engineer details\n");
            Console.WriteLine("enter the Engineers id\n");
            int.TryParse(Console.ReadLine()!, out _ID);
            Console.WriteLine("enter the Engineers name\n");
            string _Name = Console.ReadLine();
            Console.WriteLine("enter the Engineers email\n");
            string _Email = Console.ReadLine();
            Console.WriteLine("enter the Engineers level\n");
            EngineerExperience.TryParse(Console.ReadLine(), out _Level);
            Console.WriteLine("enter the Engineers Cost\n");
            double.TryParse(Console.ReadLine()!, out _Cost);
            return new Engineer(_ID, _Name, _Email, _Level, _Cost);
        }


        //task entity
        public static void TaskEntity()
        {
            int Id;
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
                        s_dalTask!.Read(Convert.ToInt32(Console.ReadLine()));
                        break;
                    case 3:
                        foreach (var item in s_dalTask!.ReadAll())
                            Console.WriteLine(item);
                        break;
                    case 4:
                        Console.WriteLine("Enter wanted task's ID");
                        s_dalTask!.Update(createNewTask(Convert.ToInt32(Console.ReadLine())));
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


        public static DO.Task createNewTask(int id = 0000)
        {

            DateTime _createdAdt;
            DateTime _Start;
            DateTime _ScheduledDate;
            DateTime _DeadLine;
            DateTime _Complete;
            int _Engineerld;
            TaskLevel _ComplexityLevel;
            Console.WriteLine("please enter the task's details\n");
            Console.WriteLine("Enter the task's description\n");
            string _Description = Console.ReadLine();
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
            string _Deliverable = Console.ReadLine();
            Console.WriteLine("enter the engineer's Id\n");
            _Engineerld = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the task's level");
            TaskLevel.TryParse(Console.ReadLine(), out _ComplexityLevel);
            return (new DO.Task(_Description, false, _createdAdt, _Start, _ScheduledDate, _DeadLine, _Complete, _Deliverable, _Engineerld, _ComplexityLevel));
        }

        //dependency

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
                            s_dalDependency!.Update(createNewDependency(Convert.ToInt32(Console.ReadLine())));
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


            private static DO.Dependency createNewDependency(int dependencyId = 0000)
            {
                int taskId;
                int prevTaskId;
                Console.WriteLine("Enter task id:");
                taskId = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter pervious task id:");
                prevTaskId = Convert.ToInt32(Console.ReadLine());
                return new DO.Dependency(taskId, prevTaskId, dependencyId);
            }

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



