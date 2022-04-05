using System.Collections.Generic;

namespace MediaLibrary
{
    public class Show : Media
    {
        public int season { get; set; }
        public int episode { get; set; }
        public List<string> writers { get; set; }
        
        public string Display()
        {
            return $"ID: {mediaID}\n" +
                   $"Title: {title}\n" +
                   $"Season: {season}\n" +
                   $"Episode: {episode}\n" +
                   $"Writers: {string.Join(" | ", writers)}\n";
        }

        public Show(int mediaID, string title, int season, int episode, List<string> writers)
        {
            this.mediaID = mediaID;
            this.title = title;
            this.season = season;
            this.episode = episode;
            this.writers = writers;
        }
    }
}