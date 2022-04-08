using System;
using System.IO;
using MediaLibrary.Repositories;

namespace MediaLibrary
{
    public class MovieJsonService : MediaService
    {
        public MovieJsonService(string pickedChoice) : base(pickedChoice)
        {
            MovieJsonRepository movieJsonRepository = new MovieJsonRepository();

            var choice = true;
            do
            {
                Console.WriteLine("1. List movies.\n2. Add movie.\nEnter anything else to exit.");
                pickedChoice = Console.ReadLine();
                switch (pickedChoice)
                {
                    case "1":
                        movieJsonRepository.Read();
                        break;
                    case "2":
                        movieJsonRepository.Write();
                        break;
                    default:
                        choice = false;
                        break;
                }
            } while (choice);
        }
    }
}