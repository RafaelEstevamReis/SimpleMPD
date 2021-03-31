using System;
using System.IO;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Commands
{
    public class Search : ICommand
    {
        public string Expression { get; }
        public string CommandName => "Search";

        public Search(string expression)
        {
            Expression = expression;
        }

        public IResponse GetResponseProcessor()
        {
            return new Responses.SongInfoCollection();
        }

        public async Task WriteAsync(StreamWriter stream)
        {
            var expr = Helper.EscapingHelper.Escape(Expression);
            await stream.WriteAsync($"search \"({expr})\"\n");
        }

        //public static string ExpressionBuilder(Tags tag, FilterMatch match, string search)
        //{
        //    string strMatch = match switch
        //    {
        //        FilterMatch.Equals => "==",
        //        FilterMatch.Different => "!=",
        //        FilterMatch.Contains => "contains",
        //        _ => throw new ArgumentException("Invalid FilterMatch"),
        //    };

        //    string scapedSearch = Helper.EscapingHelper.Escape(search);

        //    return $"{tag} {strMatch} \"{scapedSearch}\"";
        //}
    }
}
