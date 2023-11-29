using DalApi;
using DO;
using Dal;
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
        //private static ITask? s_dalTask = new TaskImplementation(); //stage 1
        //private static IEngineer? s_dalEngineer = new EngineerImplementation(); //stage 1
        //private static IDependency? s_dalDependency = new DependencyImplementation(); //stage 1
        //static readonly IDal s_dal = new DalList(); //stage 2
        static readonly IDal s_dal = new Dal.DalXml(); //stage 3

        /// <summary>
        /// main menu for choose entity
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
                        s_dal!.Engineer.Create(creatNewEngineer(id));
                        break;
                    case 2:
                        Console.WriteLine("enter engineer's id wanted\n");
                        Console.WriteLine(s_dal!.Engineer.Read(Convert.ToInt32(Console.ReadLine())));
                        break;
                    case 3:
                        foreach (var engineer in s_dal!.Engineer.ReadAll())
                        {
                            Console.WriteLine(engineer);
                        };
                        break;

                    case 4:
                        Console.WriteLine("enter engineer's id wanted\n");
                        id = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine(s_dal!.Engineer.Read(id));
                        updateEngineer(id);

                        break;
                    case 5:
                        Console.WriteLine("enter engineer's id wanted\n");
                        s_dal!.Engineer.Delete(Convert.ToInt32(Console.ReadLine()));
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
            string? _Name = Console.ReadLine();
            Console.WriteLine("enter the Engineers email\n");
            string? _Email = Console.ReadLine();
            Console.WriteLine("enter the Engineers level\n");
            EngineerExperience.TryParse(Console.ReadLine(), out _Level);
            Console.WriteLine("enter the Engineers Cost\n");
            double.TryParse(Console.ReadLine()!, out _Cost);
            return new Engineer(_ID, _Name!, _Email!, _Level, _Cost);


        }

        /// <summary>
        /// update new details in spesific engineer
        /// </summary>
        /// <param name="_Id">update the engineer with the wanted ID</param>
        private static void updateEngineer(int _Id)
        {
            try
            {

                DO.Engineer? _engineer = s_dal!.Engineer.Read(_Id);
                if (_engineer is null)
                    throw new DalDoesNotExistException($"engineer with ID={_Id} does not exists\n");
                DO.Engineer temp = creatNewEngineer(_Id);
                string _Name = temp.Name;
                string? _Email = temp.Email;
                EngineerExperience _Level = temp.Level;
                double _Cost = temp.Cost;
                if (temp.Name is null)
                {
                    _Name = _engineer!.Name;
                }
                if (temp.Email == "")
                {
                    _Email = _engineer!.Email;
                }
                if (_Level.ToString() == "EIT")
                {
                    _Level = _engineer!.Level;
                }
                if (temp.Cost is 0)
                {
                    _Cost = _engineer!.Cost;
                }
                s_dal!.Engineer.Update(new DO.Engineer(_Id, _Name, _Email!, _Level, _Cost));

            }
            catch (DalDoesNotExistException newException)
            {
                Console.WriteLine(newException.Message);
            }
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
                        s_dal!.Task.Create(createNewTask());
                        break;
                    case 2:
                        Console.WriteLine("Enter wanted task's ID");
                        Console.WriteLine(s_dal!.Task.Read(Convert.ToInt32(Console.ReadLine())));
                        break;
                    case 3:
                        foreach (var item in s_dal!.Task.ReadAll())
                            Console.WriteLine(item);
                        break;
                    case 4:
                        Console.WriteLine("Enter wanted task's ID");
                        int id = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine(s_dal!.Task.Read(id));
                        updateTask(id);
                        break;
                    case 5:
                        Console.WriteLine("Enter wanted task's ID");
                        s_dal!.Task.Delete(Convert.ToInt32(Console.ReadLine()));
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
            string? _Description = Console.ReadLine();
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
            string? _Deliverable = Console.ReadLine();
            Console.WriteLine("enter the engineer's Id\n");
            int.TryParse(Console.ReadLine(), out _Engineerld);
            Console.WriteLine("Enter the task's level");
            TaskLevel.TryParse(Console.ReadLine(), out _ComplexityLevel);
            return (new DO.Task(_Description!, _Milestone, _createdAdt, _Start, _ScheduledDate, _DeadLine, _Complete, _Deliverable!, _Engineerld, _ComplexityLevel, null, null, id));
        }


        /// <summary>
        /// update new details in spesific task
        /// </summary>
        /// <param name="_id">update the task with the wanted ID</param>
        private static void updateTask(int _id)
        {
            try
            {
                DO.Task? _task = s_dal!.Task.Read(_id);
                if (_task is null)
                    throw new DalDoesNotExistException($"engineer with ID={_id} does not exists\n");
                DO.Task tempTask = createNewTask(_id);
                string _Description = tempTask.Description;
                bool? _Milestone = tempTask.Milestone;
                DateTime _createdAdt = tempTask.createdAdt;
                DateTime _Start = tempTask.Start;
                DateTime _ScheduledDate = tempTask.ScheduledDate;
                DateTime _DeadLine = tempTask.DeadLine;
                DateTime _Complete = tempTask.Complete;
                string _Deliverable = tempTask.Deliverable;
                int _Engineerld = tempTask.Engineerld;
                TaskLevel? _ComplexityLevel = tempTask.ComplexityLevel;
                if (tempTask.Description is null)
                    _Description = _task!.Description;
                if (tempTask.Milestone == false)
                    _Milestone = _task!.Milestone;
                if (_createdAdt == DateTime.MinValue)
                    _createdAdt = _task!.createdAdt;
                if (_Start == DateTime.MinValue)
                    _Start = _task!.Start;
                if (_ScheduledDate == DateTime.MinValue)
                    _ScheduledDate = _task!.ScheduledDate;
                if (_DeadLine == DateTime.MinValue)
                    _DeadLine = _task!.DeadLine;
                if (_Complete == DateTime.MinValue)
                    _Complete = _task!.Complete;
                if (_Deliverable == null)
                    _Deliverable = _task!.Deliverable;
                if (_Engineerld == 0)
                    _Engineerld = _task!.Engineerld;
                if (_ComplexityLevel.ToString() == "easy")
                    _ComplexityLevel = tempTask.ComplexityLevel;
                s_dal.Task.Update(new DO.Task(_Description, _Milestone, _createdAdt, _Start, _ScheduledDate, _DeadLine, _Complete, _Deliverable, _Engineerld, _ComplexityLevel, null, null, _id));
            }
            catch (DalDoesNotExistException newException)
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
                        s_dal!.Dependency.Create(createNewDependency());
                        break;
                    case 2:
                        Console.WriteLine("Enter Dependency id\n");
                        Console.WriteLine(s_dal!.Dependency.Read(Convert.ToInt32(Console.ReadLine())));
                        break;
                    case 3:
                        foreach (var item in s_dal!.Dependency.ReadAll())
                        {
                            Console.WriteLine($"id: {item!.ID}  id's task: {item.DependentTask}   id's pervious task: {item.previousIDTask}");
                        };
                        break;
                    case 4:
                        Console.WriteLine("Enter Dependency's id\n");
                        int _id = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine(s_dal!.Dependency.Read(_id));
                        updateDependency(_id);
                        break;
                    case 5:
                        Console.WriteLine("Enter Dependency id\n");
                        s_dal!.Dependency.Delete(Convert.ToInt32(Console.ReadLine()));
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
            int.TryParse(Console.ReadLine()!, out taskId);
            Console.WriteLine("Enter pervious task id:");
            int.TryParse(Console.ReadLine()!, out prevTaskId);
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

                DO.Dependency? _dependency = s_dal!.Dependency.Read(id);
                if (_dependency is null)
                    throw new DalDoesNotExistException($"engineer with ID={id} does not exists\n");
                DO.Dependency tempDependency = createNewDependency(id);
                int _DependentTask = tempDependency.DependentTask;
                int _previousIDTask = tempDependency.previousIDTask;
                if (_DependentTask == 0)
                {
                    _DependentTask = _dependency!.DependentTask;
                }
                if (_previousIDTask == 0)
                {
                    _previousIDTask = _dependency!.previousIDTask;
                }
                s_dal.Dependency.Update(new DO.Dependency(_DependentTask, _previousIDTask, id));
            }
            catch (DalDoesNotExistException newException)
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
                // Initialization.Do(s_dalDependency, s_dalEngineer, s_dalTask);//stage 1
                //Initialization.Do(s_dal); //stage 2
                allEntities();
            }
            catch (DalAlreadyExistsException error)
            {
                Console.WriteLine(error.ToString());
            }
            catch (DalDoesNotExistException error)
            {
                Console.WriteLine(error.ToString());
            }
            catch (DalDeletionImpossible error)
            {
                Console.WriteLine(error.ToString());
            }
            catch (NullReferenceException error)
            {
                Console.WriteLine(error.ToString());
            }
           
        }
    }


}




