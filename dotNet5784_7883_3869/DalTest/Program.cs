using Dal;
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
                            createTaskEntity();
                        }
                    case 2:
                        {
                            createEngineerEntity();
                        }
                    case 3:
                        {
                            createDependencyEnity();
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
}