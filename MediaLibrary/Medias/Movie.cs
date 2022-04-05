using System.Collections.Generic;

namespace MediaLibrary
{
    public class Movie : Media
    {
        internal List<string> genre { get; set; }
        
        public string Display()
        {
            return $"ID: {mediaID}\n" +
                   $"Title: {title}\n" +
                   $"Genres: {string.Join(" | ", genre)}\n";
        }

        public Movie(int mediaID, string title, List<string> genre)
        {
            this.mediaID = mediaID;
            this.title = title;
            this.genre = genre;
        }
        
    }
}