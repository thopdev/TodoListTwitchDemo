namespace Todo.Models
{
    public class LoaderItem
    {
        public string Text { get; }
        public string Url { get; }
        public string UserName { get; set; }


        public LoaderItem(string text, string url)
        {
            Text = text;
            Url = url;
        }

        public LoaderItem(string text, string url, string userName)
        {
            Text = text;
            Url = url;
            UserName = userName;
        }
    }
}