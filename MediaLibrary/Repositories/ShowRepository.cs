using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MediaLibrary
{
    public class ShowRepository : IRepository
    {
        private ShowDataContext _context = new ShowDataContext();
        private List<Show> showList = new List<Show>();

        public ShowRepository()
        {
            foreach (var s in _context.showList)
            {
                showList.Add(s);
            }
        }
        public List<Show> GetShowList()
        {
            return showList;
        }
        public void Write()
        {
            int ID = 0;
            string title;
            int season;
            int episode;
            List<string> writers = new List<string>();

            Console.WriteLine("Enter show title");
            title = Console.ReadLine();
            if (TestTitle(title))
            {
                do
                {
                    Console.WriteLine("Enter the show's season");
                    try
                    {
                        season = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Please enter a valid season number.");
                        season = 0;
                    }
                } while (season == 0);

                do
                {
                    Console.WriteLine("Enter the show's episode");
                    try
                    {
                        episode = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Please enter a valid season number.");
                        episode = 0;
                    }
                } while (episode == 0);

                bool b = true;
                while (b)
                {
                    Console.WriteLine(string.Format("1: Enter writer\n" +
                                                    "2: Exit"));
                    string input = Console.ReadLine();
                    switch (input)
                    {
                        case "1":
                            Console.Write("Enter writer: ");
                            string writerInput = Console.ReadLine();

                            if (writerInput != "")
                            {
                                writers.Add(writerInput);
                            }
                            else
                            {
                                Console.Write("Please enter a writer.");
                            }

                            break;
                        case "2":
                            if (writers.Count == 0)
                            {
                                writers.Add("N/A");
                            }

                            Console.WriteLine("Exit");
                            b = false;
                            break;
                    }
                }

                _context.WriteNewMedia(new Show(ID, title, season, episode, writers));
            }
            else
            {
                Console.WriteLine("Movie title already exists\n");
            }
        }

        public void Read()
        {
            _context.ReadMedia();
            List<String> list = new();
            foreach (Show m in _context.showList)
            {
                list.Add(m.Display());
            }

            MediaReadService mediaReadService = new MediaReadService();
            mediaReadService.ListMedia(list);
        }

        private bool TestTitle(string newTitle)
        {
            List<string> titleList = _context.showList.Select(title => title.title.Replace('"', ' ').Trim().ToLower())
                .ToList();

            if (titleList == null || titleList.Contains(newTitle))
            {
                return false;
            }

            return true;
        }
    }
}