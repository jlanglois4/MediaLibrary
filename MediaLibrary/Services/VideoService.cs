using System;

namespace MediaLibrary
{
    public class VideoService : MediaService
    {
        public VideoService(string pickedChoice) : base(pickedChoice)
        {
            VideoRepository videoRepository = new VideoRepository();
            var choice = true;
            do
            {
                Console.WriteLine($"1. List videos.\n2. Add video.\nEnter anything else to exit.");
                pickedChoice = Console.ReadLine();
                switch (pickedChoice)
                {
                    case "1":
                        videoRepository.Read();
                        break;
                    case "2":
                        videoRepository.Write();
                        break;
                    default:
                        choice = false;
                        break;
                }
            } while (choice);
        }
    }
}