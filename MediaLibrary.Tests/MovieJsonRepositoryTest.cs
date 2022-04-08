using System;
using System.Collections.Generic;
using System.IO;
using MediaLibrary.Repositories;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace MediaLibrary.Tests
{
    public class MovieJsonRepositoryTest
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public MovieJsonRepositoryTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void TestConvert()
        {
            using StreamReader streamReader = new StreamReader(@"Files/movies.json");
            string json = streamReader.ReadToEnd().Trim();
            streamReader.Close();

            List<string> genre = new List<string>();
            genre.Add("Adventure");
            genre.Add("Animation");
            genre.Add("Children");
            genre.Add("Comedy");
            genre.Add("Fantasy");

            Movie movie = new Movie(1, "Toy Story (1995)", genre);

            List<Movie> movieList = new List<Movie>();
            movieList.Add(movie);

            JsonSerializerSettings _options
                = new() {NullValueHandling = NullValueHandling.Ignore};
            string expected = JsonConvert.SerializeObject(movieList, Formatting.Indented, _options);


            _testOutputHelper.WriteLine(expected);

            _testOutputHelper.WriteLine(json);

            Assert.Equal(expected, json);
        }
    }
}