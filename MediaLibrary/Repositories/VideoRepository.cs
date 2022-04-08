using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MediaLibrary
{
    public class VideoRepository : IRepository
    {
        private VideoDataContext _context = new VideoDataContext();

        public void Write()
        {
            int ID = 0;
            string title;
            string format;
            int length;
            List<int> regions = new List<int>();

            Console.WriteLine("Enter video title");
            title = Console.ReadLine();
            if (TestTitle(title))
            {
                do
                {
                    Console.WriteLine("Enter the video's format");
                    try
                    {
                        format = Console.ReadLine();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Please enter a valid video format.");
                        format = null;
                    }
                } while (format == null);

                do
                {
                    Console.WriteLine("Enter the video's length");
                    try
                    {
                        length = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Please enter a valid video length.");
                        length = 0;
                    }
                } while (length == 0);

                bool b = true;

                while (b)
                {
                    Console.WriteLine(string.Format("1: Enter region\n" +
                                                    "2: Exit"));
                    string input = Console.ReadLine();
                    switch (input)
                    {
                        case "1":
                            Console.Write("Enter region: ");

                            if (!int.TryParse(Console.ReadLine(), out var regionInput))
                            {
                                Console.Write("Please enter a region.");
                            }
                            else
                            {
                                regions.Add(regionInput);
                            }

                            break;
                        case "2":
                            Console.WriteLine("Exit");
                            if (regions.Count == 0)
                            {
                                regions.Add(0);
                            }

                            b = false;
                            break;
                    }
                }

                _context.WriteNewMedia(new Video(ID, title, format, length, regions));
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
            foreach (Video m in _context.videoList)
            {
                list.Add(m.Display());
            }

            MediaReadService mediaReadService = new MediaReadService();
            mediaReadService.ListMedia(list);
        }

        private bool TestTitle(string newTitle)
        {
            List<string> titleList = _context.videoList.Select(title => title.title.Replace('"', ' ').Trim().ToLower())
                .ToList();

            if (titleList != null || titleList.Contains(newTitle))
            {
                return false;
            }

            return true;
        }
    }
}