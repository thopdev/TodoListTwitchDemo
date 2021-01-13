namespace Todo.Models
{
    public class LoaderItem
    {
        public string Text { get; }
        public string Url { get; }


        public LoaderItem(string text, string url)
        {
            Text = text;
            Url = url;
        }
    }
}