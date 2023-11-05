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
        private static void createTaskEntity() {

        }
    }
}