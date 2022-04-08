using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MediaLibrary
{
    public class ShowDataContext
    {
        private static IServiceCollection serviceCollection = new ServiceCollection();

        private static ServiceProvider serviceProvider =
            serviceCollection.AddLogging(x => x.AddConsole()).BuildServiceProvider();

        private static ILogger<Program> logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger<Program>();
        private const string ShowFilePath = @"Files/shows.csv";

        public List<Show> showList = new List<Show>();

        public ShowDataContext()
        {
            CreateNewFile();
        }
        public void ReadMedia()
        {
            try
            {
                int count = 0;

                int mediaID;
                string title;
                int season;
                int episode;

                StreamReader streamReader = new(ShowFilePath);
                streamReader.ReadLine();

                while (!streamReader.EndOfStream)
                {
                    List<string> writers = new List<string>();
                    string entry = streamReader.ReadLine();

                    int quote = entry.IndexOf('"') - 1;
                    if (quote == 1)
                    {
                        entry = entry.Replace('"', ' ');
                    }

                    if (entry != "")
                    {
                        string[] showDetails = entry.Split(',');
                        mediaID = int.Parse(showDetails[0]);
                        title = showDetails[1].Trim();
                        season = Convert.ToInt32(showDetails[2]);
                        episode = Convert.ToInt32(showDetails[3]);
                        if (showDetails[4].Contains('|'))
                        {
                            string[] array = showDetails[4].Split('|');
                            foreach (string str in array)
                            {
                                writers.Add(str);
                            }
                        }
                        else
                        {
                            writers.Add(showDetails[4]);
                        }

                        Show show = new Show(mediaID, title, season, episode, writers);
                        showList.Add(show);
                        count++;
                    }
                }

                streamReader.Close();
                logger.LogInformation("Movies in file {showCount}", count);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }
        }

        public void WriteNewMedia(Show show)
        {
            try
            {
                if (showList.Count == 0)
                {
                    show.mediaID = 1;
                }
                else
                {
                    show.mediaID = showList[showList.Count - 1].mediaID + 1;
                }

                if (show.title.IndexOf(',') != -1)
                {
                    show.title = $"\"{show.title}\"";
                }

                StreamWriter streamWriter = new(ShowFilePath, true);
                streamWriter.WriteLine(
                    $"{show.mediaID},{show.title},{show.season},{show.episode},{string.Join("|", show.writers)}");
                streamWriter.Close();

                logger.LogInformation("Show ID {showID} added", show.mediaID);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                Console.WriteLine("Error adding show");
            }
        }

        private void CreateNewFile()
        {
            if (!File.Exists(ShowFilePath))
            {
                Thread.Sleep(500);
                Console.WriteLine("File not found.");
                Console.WriteLine("Creating new file.\nPress anything to continue.");
                Console.ReadLine();
                StreamWriter streamWriter = new(ShowFilePath, true);
                streamWriter.WriteLine("showID,title,season,episode,writers");
                streamWriter.Close();
            }
        }
    }
}