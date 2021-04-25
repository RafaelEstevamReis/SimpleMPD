using Simple.MPD.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Simple.MPD.Commands
{
    /// <summary>
    /// Executes "Move"
    /// </summary>
    public class Move : ICommand
    {
        /// <summary>
        /// Command name
        /// </summary>
        public string CommandName => "Move";
        /// <summary>
        /// Range of songs
        /// </summary>
        public Range Range { get; }
        /// <summary>
        /// From song
        /// </summary>
        public int From { get; }
        /// <summary>
        /// To queue position
        /// </summary>
        public int ToPosition { get; }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public Move(Range range, int to)
        {
            Range = range ?? throw new ArgumentNullException(nameof(range));
            ToPosition = to;
        }
        /// <summary>
        /// Creates a new instance
        /// </summary>
        public Move(int from, int to)
        {
            From = from;
            ToPosition = to;
        }
        /// <summary>
        /// Default Response processor
        /// </summary>
        public IResponse GetResponseProcessor()
        {
            return new Responses.Ok();
        }
        /// <summary>
        /// Writes command to stream
        /// </summary>
        public async Task WriteAsync(StreamWriter stream)
        {
            if (Range == null) // FROM
            {
                await stream.WriteAsync($"move {From} {ToPosition}\n");
            }
            else // Range
            {
                await stream.WriteAsync($"move {Range} {ToPosition}\n");
            }
        }
    }
}
