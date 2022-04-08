using System;
using System.Collections.Generic;
using System.Linq;
using MediaLibrary.Repositories;

namespace MediaLibrary
{
    public class SearchService
    {
        public void SearchMedia()
        {
            MovieJsonRepository movieJsonRepository = new MovieJsonRepository();
            VideoRepository videoRepository = new VideoRepository();
            ShowRepository showRepository = new ShowRepository();

            List<string> mediaList = movieJsonRepository.GetJsonMovieList().Select(m => "Movie\n" + m.Display()).ToList();
            mediaList.AddRange(showRepository.GetContext().Select(v => "Show\n" + v.Display()));
            mediaList.AddRange(videoRepository.GetContext().Select(s => "Video\n" + s.Display()));

            var boolean = false;
            do
            {
                try
                {
                    Console.WriteLine("Enter title you wish to find.");
                    var titleChoice = Console.ReadLine().ToLower();

                    List<string> media = new List<string>();

                    int count = 0;
                    foreach (var title in mediaList.Where(title => title.ToLower().Contains(titleChoice)))
                    {
                        media.Add(title);
                        count++;
                    }

                    Console.WriteLine(count + " matches");
                    
                    foreach (var result in media)
                    {
                        Console.WriteLine(result);
                    }



                    Console.WriteLine("1. Search again.\n" +
                                      "Enter anything else to exit.");
                    var choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "1":
                            break;
                        default:
                            boolean = false;
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Please enter a proper title.");
                    throw;
                }

            } while (!boolean);
        }
    }
}