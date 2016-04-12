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
            while (true)
            {
                UnitOfWorkFactory.__Initialize(() => new UnitOfWork());
                PrintGenresFunctions();
                Console.Write("Enter the index of action: ");
                try
                {
                    int indexAction = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine();
                    RunGenresActions(indexAction);
                    Console.WriteLine();
                }
                catch (Exception)
                {
                    Environment.Exit(0);
                }
            }
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
        
        private static void PrintGenresFunctions()
        {
            PrintListOfGeneralFunctions();
        }

        private static void RunGenresActions(int index)
        {
            var uof = UnitOfWorkFactory.CreateInstance();
            List<Genre> genres = new List<Genre>();
            switch (index)
            {
                case 1:
                    {
                        genres = uof.GenreRepository.GetAll();
                        uof.Commit();
                        foreach (Genre genre in genres)
                        {
                            Console.WriteLine(genre.ToString());
                        }
                     }
                    break;
                case 2:
                    {
                        Console.Write("Enter the name of genre: ");
                        string name = Console.ReadLine();
                        try
                        {
                            uof.GenreRepository.Insert(new Genre(name));
                            uof.Commit();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Something gone wrong!");
                            Console.WriteLine(ex.Message.ToString());
                        }
                    }
                    break;
                case 3:
                    Console.Write("Enter the name of genre you want to change: ");
                    string nameOld = Console.ReadLine();
                    Console.Write("Enter the new name of genre: ");
                    string nameNew = Console.ReadLine();
                    genres = uof.GenreRepository.GetAll();
                    Genre genreToUpdate = new Genre(nameOld);
                    foreach (Genre genre in genres)
                    {
                        if (genre.Name == genreToUpdate.Name)
                        {
                            genreToUpdate = genre.Clone();
                        }
                    }
                    genreToUpdate.Name = nameNew;
                    try
                    {
                        uof.GenreRepository.Update(genreToUpdate);
                        uof.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Something gone worng!");
                        Console.WriteLine(ex.Message.ToString());
                    }
                    break;
                case 4:
                    Console.Write("Enter the name of genre you want to delete: ");
                    string nameToDelete = Console.ReadLine();
                    genres = uof.GenreRepository.GetAll();
                    Genre genreToDelete = new Genre(nameToDelete);
                    foreach (Genre genre in genres)
                    {
                        if (genre.Name == genreToDelete.Name)
                        {
                            genreToDelete = genre;
                        }
                    }
                    try
                    {
                        uof.GenreRepository.Delete(genreToDelete.Id);
                        uof.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Something gone wrong!");
                        Console.WriteLine(ex.Message.ToString());
                    }
                    break;
                default:
                    Console.WriteLine("Oops! Please, it looks like there is no action with such index.");
                    break;
            }
        }
    }
}
