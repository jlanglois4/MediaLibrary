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
                    "Welcome to the Media Library. Please pick a form of media.\n1. Movies.\n2. Shows.\n3. Videos.\nEnter anything else to exit the Media Library.");
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
                    default:
                        Console.WriteLine("Thank you for using the Media Library.");
                        choice = false;
                        break;
                }
            } while (choice);
        }
    }
}