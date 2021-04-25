namespace Simple.MPD.Helper
{
    public class MpdDirectory
    {
        public string Name { get; set; }
        public MpdDirectory[] Directories { get; set; }
        public Responses.SongInfo[] Files { get; set; }

        public override string ToString() => Name;
    }
}