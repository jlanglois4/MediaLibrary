using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MediaLibrary
{
    public class MovieDataContext
    {
        private static IServiceCollection serviceCollection = new ServiceCollection();

        private static ServiceProvider serviceProvider =
            serviceCollection.AddLogging(x => x.AddConsole()).BuildServiceProvider();

        private static ILogger<Program> logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger<Program>();
        private const string MovieFilePath = @"Files/movies.csv";

        public List<Movie> movieList = new List<Movie>();

        public MovieDataContext()
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
                List<string> genre;

                StreamReader streamReader = new(MovieFilePath);
                streamReader.ReadLine();

                while (!streamReader.EndOfStream)
                {
                    string entry = streamReader.ReadLine();

                    int quote = entry.IndexOf('"') - 1;
                    if (quote == 1)
                    {
                        entry = entry.Replace('"', ' ');
                    }

                    if (entry != "")
                    {
                        string[] movieDetails = entry.Split(',');
                        mediaID = int.Parse(movieDetails[0]);
                        title = movieDetails[1].Trim();
                        genre = movieDetails[2].Split('|').ToList();
                        Movie movie = new Movie(mediaID, title, genre);
                        movieList.Add(movie);
                        count++;
                    }
                }

                streamReader.Close();
                logger.LogInformation("Movies in file {movieCount}", count);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }
        }

        public void WriteNewMedia(Movie movie)
        {
            CreateNewFile();
            try
            {
                movie.mediaID = movieList[movieList.Count - 1].mediaID + 1;
                if (movie.title.IndexOf(',') != -1)
                {
                    movie.title = $"\"{movie.title}\"";
                }


                StreamWriter streamWriter = new(MovieFilePath, true);
                streamWriter.Flush();
                streamWriter.WriteLine($"{movie.mediaID},{movie.title},{string.Join("|", movie.genre)}");
                streamWriter.Close();

                logger.LogInformation("Movie id {movieID} added", movie.mediaID);
            }
            catch (Exception e)
            {
                logger.LogError(e.StackTrace);
                Console.WriteLine("Error adding movie");
            }
        }

        private void CreateNewFile()
        {
            if (!File.Exists(MovieFilePath))
            {
                Thread.Sleep(500);
                Console.WriteLine("File not found.");
                Console.WriteLine("Creating new file.\nPress anything to continue.");
                Console.ReadLine();
                StreamWriter streamWriter = new(MovieFilePath, true);
                streamWriter.WriteLine("movieID,title,genres");
                streamWriter.Close();
            }
        }
    }
}