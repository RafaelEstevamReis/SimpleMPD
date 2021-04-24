namespace Simple.MPD.Helper
{
    public class Database : Directory
    {
        public Database(Directory root)
        {
            Name = root.Name;
            Directories = root.Directories;
            Files = root.Files;
        }
    }
    public class Directory
    {
        public string Name { get; set; }
        public Directory[] Directories { get; set; }
        public Responses.SongInfo[] Files { get; set; }

        public override string ToString() => Name;
    }
}