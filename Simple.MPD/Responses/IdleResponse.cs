using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Responses
{
    /// <summary>
    /// Idle response
    /// </summary>
    public class IdleResponse : IResponse
    {
        /// <summary>
        /// Get subsystems
        /// </summary>
        public Commands.Idle.SubSystems[] SubSystems { get; set; }
        /// <summary>
        /// Read response from stream
        /// </summary>
        public Task ReadAsync(StreamReader stream)
        {
            var idleOptions = Enum.GetValues(typeof(Commands.Idle.SubSystems))
                                  .Cast<Commands.Idle.SubSystems>()
                                  .ToArray();
            var lst = new List<Commands.Idle.SubSystems>();

            foreach (var line in Helper.ResponseHelper.ReadLines(stream))
            {
                Commands.Idle.SubSystems subSustem = idleOptions
                    .First(opt => opt.ToString().ToLower() == line.Split(':')[1].Trim());

                lst.Add(subSustem);
            }

            SubSystems = lst.ToArray();

            return Helper.FrameworkHelper.GetCompletedTask();
        }
    }
}
