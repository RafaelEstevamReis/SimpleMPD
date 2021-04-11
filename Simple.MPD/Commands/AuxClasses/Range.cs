namespace Simple.MPD.Commands
{
    /// <summary>
    /// Represents a range of songs or entries
    /// </summary>
    public class Range
    {
        /// <summary>
        /// First entry
        /// </summary>
        public int Start { get; set; }
        /// <summary>
        /// End of the range, NOT included
        /// </summary>
        public int End { get; set; }
        /// <summary>
        /// Prints the format "START:END"
        /// </summary>
        public override string ToString()
        {
            return $"{Start}:{End}";
        }
    }
}
