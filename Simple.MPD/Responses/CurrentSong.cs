using System;
using System.IO;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Responses
{
    public class CurrentSong : IResponse
    {
        public string File { get; set; }
        public string Name { get; set; }
        public int Pos { get; set; }
        public int Id { get; set; }

        public ICommand GetCommand()
        {
            return null;
        }

        public async Task ReadAsync(StreamReader stream)
        {
            var values = Helper.ResponseHelper.ReadValuesAsync(stream);
            await foreach (var pair in values)
            {
                switch (pair.Key.ToLower())
                {
                    case "file":
                        File = pair.Value.Trim();
                        break;
                    case "name":
                        Name = pair.Value.Trim();
                        break;

                    case "pos":
                        Pos = int.Parse(pair.Value);
                        break;
                    case "id":
                        Id = int.Parse(pair.Value);
                        break;
                }
            }
        }
    }
}