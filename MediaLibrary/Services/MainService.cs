using System;

namespace MediaLibrary
{
    public class MainService
    {
        public MainService()
        {
            var choice = true;
            do
            {
                Console.WriteLine(
                    "Welcome to the Media Library. Please pick a form of media.\n" +
                    "1. Movies.\n" +
                    "2. Shows.\n" +
                    "3. Videos.\n" +
                    "4. Search.\n" +
                    "Enter anything else to exit the Media Library.");
                string pickedChoice = Console.ReadLine();
                switch (pickedChoice)
                {
                    case "1":
                        new MovieJsonService(pickedChoice);
                        break;
                    case "2":
                        new ShowService(pickedChoice);
                        break;
                    case "3":
                        new VideoService(pickedChoice);
                        break;
                    case "4":
                        SearchService searchService = new SearchService();
                        searchService.SearchMedia();
                        break;
                    default:
                        Console.WriteLine("Thank you for using the Media Library.");
                        choice = false;
                        break;
                }
            } while (choice);
        }
    }
}