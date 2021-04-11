using System;
using System.IO;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Commands
{
    /// <summary>
    /// Executes "Find"
    /// </summary>
    public class Find : ICommand
    {
        /// <summary>
        /// Expression to search/filter
        /// </summary>
        public string Expression { get; }
        /// <summary>
        /// Command name
        /// </summary>
        public string CommandName => "Find";
        /// <summary>
        /// Creates a new instance
        /// </summary>
        public Find(string expression)
        {
            Expression = expression;
        }
        /// <summary>
        /// Default Response processor
        /// </summary>
        public IResponse GetResponseProcessor()
        {
            return new Responses.SongInfoCollection();
        }
        /// <summary>
        /// Writes command to stream
        /// </summary>
        public async Task WriteAsync(StreamWriter stream)
        {
            var expr = Helper.EscapingHelper.Escape(Expression);
            await stream.WriteAsync($"find \"({expr})\"\n");
        }
        /// <summary>
        /// Builds an filter Expression
        /// </summary>
        public static string ExpressionBuilder(Tags tag, FilterMatch match, string search)
        {
            string strMatch = match switch
            {
                FilterMatch.Equals =>  "==",
                FilterMatch.Different =>  "!=",
                FilterMatch.Contains => "contains",
                _ => throw new ArgumentException("Invalid FilterMatch"),
            };

            string scapedSearch = Helper.EscapingHelper.Escape(search);

            return $"{tag} {strMatch} \"{scapedSearch}\"";
        }
    }
}
