using System;

namespace MediaLibrary
{
    public class MovieService : MediaService
    {
        
        public MovieService(string pickedChoice) : base(pickedChoice)
        {
            MovieRepository movieRepository = new MovieRepository();
            var choice = true;
            do
            {
                Console.WriteLine("1. List movies.\n2. Add movie.\nEnter anything else to exit.");
                pickedChoice = Console.ReadLine();
                switch (pickedChoice)
                {
                    case "1":
                        movieRepository.Read();
                        break;
                    case "2":
                        movieRepository.Write();
                        break;
                    default:
                        choice = false;
                        break;
                }
            } while (choice);
        }
    }
}