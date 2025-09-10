namespace MapleLeaf.App;

public class AppSettings
{
    public ApplicationSettings Application { get; set; } = new();

    public class ApplicationSettings
    {
        public string Title { get; set; } = "MapleLeaf Pizza";
    }
}
