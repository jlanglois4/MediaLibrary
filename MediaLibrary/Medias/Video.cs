using System.Collections.Generic;

namespace MediaLibrary
{
    public class Video : Media
    { 
        public string format { get; set; }
        public int length { get; set; }
        public List<int> regions { get; set; }
        
        
        public string Display()
        {
            return $"ID: {mediaID}\n" +
                   $"Title: {title}\n" +
                   $"Format: {format}\n" +
                   $"Length: {length}\n" +
                   $"Regions: {string.Join(" | ", regions)}\n";
        }
        
        public Video(int mediaID, string title, string format, int length, List<int> regions)
        {
            this.mediaID = mediaID;
            this.title = title;
            this.format = format;
            this.length = length;
            this.regions = regions;
        }
    }
}