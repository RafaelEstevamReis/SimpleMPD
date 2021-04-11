namespace Simple.MPD.Commands
{
    public class Range
    {
        public int Start { get; set; }
        public int End { get; set; }

        public override string ToString()
        {
            return $"{Start}:{End}";
        }
    }
}
