namespace MediaLibrary
{
    public abstract class MediaService
    {
        public string pickedChoice { get; set; }

        public MediaService(string pickedChoice)
        {
            this.pickedChoice = pickedChoice;
        }
    }
}