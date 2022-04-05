using System;

namespace MediaLibrary
{
    public class ShowService : MediaService
    {
        public ShowService(string pickedChoice) : base(pickedChoice)
        {
            ShowRepository showRepository = new ShowRepository();
            var choice = true;
            do
            {
                Console.WriteLine($"1. List shows.\n2. Add show.\nEnter anything else to exit.");
                pickedChoice = Console.ReadLine();
                switch (pickedChoice)
                {
                    case "1":
                        showRepository.Read();
                        break;
                    case "2":
                        showRepository.Write();
                        break;
                    default:
                        choice = false;
                        break;
                }
            } while (choice);
        }
    }
}