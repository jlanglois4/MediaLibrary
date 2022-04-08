using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MediaLibrary
{
    public class VideoDataContext
    {
        private static IServiceCollection serviceCollection = new ServiceCollection();

        private static ServiceProvider serviceProvider =
            serviceCollection.AddLogging(x => x.AddConsole()).BuildServiceProvider();

        private static ILogger<Program> logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger<Program>();
        private const string VideoFilePath = @"Files/videos.csv";

        public List<Video> videoList = new List<Video>();

        public void ReadMedia()
        {
            try
            {
                int count = 0;

                int mediaID;
                string title;
                string format;
                int length;

                StreamReader streamReader = new(VideoFilePath);
                streamReader.ReadLine();

                while (!streamReader.EndOfStream)
                {
                    List<int> regions = new List<int>();
                    string entry = streamReader.ReadLine();

                    int quote = entry.IndexOf('"') - 1;
                    if (quote == 1)
                    {
                        entry = entry.Replace('"', ' ');
                    }

                    if (entry != "")
                    {
                        string[] videoDetails = entry.Split(',');
                        mediaID = int.Parse(videoDetails[0]);
                        title = videoDetails[1].Trim();
                        format = videoDetails[2];
                        length = Convert.ToInt32(videoDetails[3]);
                        if (videoDetails[4].Contains('|'))
                        {
                            string[] array = videoDetails[4].Split('|');
                            foreach (string str in array)
                            {
                                regions.Add(int.Parse(str));
                            }
                        }
                        else
                        {
                            regions.Add(int.Parse(videoDetails[4]));
                        }

                        Video video = new Video(mediaID, title, format, length, regions);
                        videoList.Add(video);
                        count++;
                    }
                }

                streamReader.Close();
                logger.LogInformation("Movies in file {videoCount}", count);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                Thread.Sleep(500);
                Console.WriteLine("File not found.");
                if (!File.Exists(VideoFilePath))
                {
                    Console.WriteLine("Creating new file.\nPress anything to continue.");
                    Console.ReadLine();
                    StreamWriter streamWriter = new(VideoFilePath, true);
                    streamWriter.WriteLine("videoID,title,format,length,regions");
                    streamWriter.Close();
                }
            }
        }

        public void WriteNewMedia(Video video)
        {
            try
            {
                if (videoList.Count == 0)
                {
                    video.mediaID = 1;
                }
                else
                {
                    video.mediaID = videoList[videoList.Count - 1].mediaID + 1;
                }

                if (video.title.IndexOf(',') != -1)
                {
                    video.title = $"\"{video.title}\"";
                }

                StreamWriter streamWriter = new(VideoFilePath, true);
                streamWriter.WriteLine(
                    $"{video.mediaID},{video.title},{video.format},{video.length},{string.Join("|", video.regions)}");
                streamWriter.Close();

                logger.LogInformation("Video ID {videoID} added", video.mediaID);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                Console.WriteLine("Error adding video");
            }
        }
    }
}