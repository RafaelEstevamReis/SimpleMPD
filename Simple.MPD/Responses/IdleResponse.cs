using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Responses
{
    public class IdleResponse : IResponse
    {
        public Commands.Idle.SubSystems[] SubSystems { get; set; }

        public ICommand GetCommand()
        {
            return null;
        }

        public async Task ReadAsync(StreamReader stream)
        {
            var idleOptions = Enum.GetValues(typeof(Commands.Idle.SubSystems))
                                  .Cast<Commands.Idle.SubSystems>()
                                  .ToArray();
            var lst = new List<Commands.Idle.SubSystems>();

            await foreach (var line in Helper.ResponseHelper.ReadLinesAsync(stream))
            {
                Commands.Idle.SubSystems subSustem = idleOptions
                    .First(opt => opt.ToString().ToLower() == line.Split(':')[1].Trim());

                lst.Add(subSustem);
            }

            SubSystems = lst.ToArray();
        }
    }
}
