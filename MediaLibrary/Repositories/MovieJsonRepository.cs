using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace MediaLibrary.Repositories
{
    public class MovieJsonRepository : IRepository
    {
        private MovieDataContext _context = new MovieDataContext();
        private const string MovieJsonFilePath = @"Files/movies.json";
        private List<Movie> _readFromFileList = new List<Movie>();

        private static readonly JsonSerializerSettings _options
            = new() {NullValueHandling = NullValueHandling.Ignore};


        public MovieJsonRepository()
        {
            if (File.Exists(@"Files/movies.json"))
            {
                using StreamReader streamReader = new StreamReader(MovieJsonFilePath);
                string json = streamReader.ReadToEnd().Trim();
                var movies = JsonConvert.DeserializeObject<List<Movie>>(json);
                foreach (var m in movies)
                {
                    _readFromFileList.Add(m);
                }
            }
            else
            {
                ConvertToJson();
            }
        }

        public void ConvertToJson()
        {
            Console.WriteLine("Converting movies to JSON."); // delete afterwards
            _context.ReadMedia();
            foreach (var m in _context.movieList)
            {
                _readFromFileList.Add(m);
            }

            WriteJson();
        }

        private void WriteJson()
        {
            string json = JsonConvert.SerializeObject(_readFromFileList, Formatting.Indented, _options);
            File.WriteAllText(MovieJsonFilePath, json);
        }

        public void Write()
        {
            List<int> ids = _readFromFileList.Select(m => m.mediaID).ToList();
            int ID = ids.Max() + 1;
            string title;

            List<string> genre = new List<string>();
            Console.WriteLine("Enter movie title");
            title = Console.ReadLine();
            if (TestTitle(title))
            {
                bool b = true;
                while (b)
                {
                    Console.WriteLine(string.Format("1: Enter genre\n" +
                                                    "2: Exit"));
                    string input = Console.ReadLine();
                    switch (input)
                    {
                        case "1":
                            Console.Write("Enter genre: ");
                            string genreInput = Console.ReadLine();

                            if (genreInput != "")
                            {
                                genre.Add(genreInput);
                            }
                            else
                            {
                                Console.Write("Please enter a genre.");
                            }

                            break;
                        case "2":
                            Console.WriteLine("Exit");
                            if (genre.Count == 0)
                            {
                                genre.Add("N/A");
                            }

                            b = false;
                            break;
                    }
                }

                Movie movie = new Movie(ID, title, genre);
                _readFromFileList.Add(movie);
                WriteJson();
            }
            else
            {
                Console.WriteLine("Movie title already exists\n");
            }
        }

        public void Read()
        {
            List<string> list = new List<string>();
            foreach (var m in _readFromFileList)
            {
                list.Add(m.Display());
            }

            MediaReadService read = new MediaReadService();
            read.ListMedia(list);
        }

        private bool TestTitle(string newTitle)
        {
            List<string> titleList = _readFromFileList.Select(title => title.title.Replace('"', ' ').Trim().ToLower())
                .ToList();
            if (titleList == null || titleList.Contains(newTitle))
            {
                return false;
            }

            return true;
        }
    }
}