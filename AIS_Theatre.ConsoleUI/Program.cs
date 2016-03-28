using AIS_Theatre.Data;
using AIS_Theatre.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintListOfEntities();
            Console.Write("Enter the index of entity: ");
            int indexEntity = Convert.ToInt32(Console.ReadLine());
            switch(indexEntity)
            {
                case 1:
                    PrintAuthorsFunctions();
                    break;
                case 2:
                    PrintGenresFunctions();
                    break;
                default:
                    Console.WriteLine("Oops! Please, it looks like thare isn't such index.");
                    break;
            }
            Console.Write("Enter the index of action: ");
            int indexAction = Convert.ToInt32(Console.ReadLine());
            switch (indexEntity)
            {
                case 1:
                    RunAuthorsActions(indexAction);
                    break;
                case 2:
                    
                    break;
                default:
                    Console.WriteLine("Oops! Please, it looks like there is no entity with such index.");
                    break;
            }
            Console.ReadKey();
        }

        private static void PrintListOfEntities()
        {
            Console.WriteLine("Choose the entity to work with form the list below by entering the index number");
            Console.WriteLine("Entities:");
            Console.WriteLine("1. Author");
            Console.WriteLine("2. Genre");
        }

        private static void PrintListOfGeneralFunctions()
        {
            Console.WriteLine("Choose the action from the list below by entering the index number");
            Console.WriteLine("Actions:");
            Console.WriteLine("1. Get All");
            Console.WriteLine("2. Save");
            Console.WriteLine("3. Update");
            Console.WriteLine("4. Delete");
        }

        private static void PrintAuthorsFunctions()
        {
            PrintListOfGeneralFunctions();
            Console.WriteLine("5. Get by Genre");
            Console.WriteLine("6. Get by Country");
        }

        private static void PrintGenresFunctions()
        {
            PrintListOfGeneralFunctions();
        }

        private static void RunAuthorsActions(int index)
        {
            switch(index)
            {
                case 1:
                    {
                        var unitOfWork = UnitOfWorkFactory.CreateInstance();
                        List<Author> authors = unitOfWork.AuthorRepository.GetAll();
                        unitOfWork.Commit();
                        foreach (Author author in authors)
                        {
                            Console.WriteLine(author.ToString());
                        }
                     }
                       
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                default:
                    Console.WriteLine("Oops! Please, it looks like there is no action with such index.");
                    break;
            }
        }
    }
}
