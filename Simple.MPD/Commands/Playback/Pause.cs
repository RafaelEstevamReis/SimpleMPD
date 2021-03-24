﻿using System.IO;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Commands
{
    public class Pause : ICommand
    {
        public enum State
        {
            Pause,
            Resume,
            Toggle
        }
        public State PauseState { get; set; }

        public Pause(State pauseState)
        {
            this.PauseState = pauseState;
        }

        public string CommandName => "Pause";

        public IResponse GetResponseProcessor()
        {
            return new Responses.Ok();
        }

        public async Task WriteAsync(StreamWriter stream)
        {
            if (PauseState == State.Toggle)
                await stream.WriteAsync($"pause\n");
            else
            {
                await stream.WriteAsync($"pause { (PauseState == State.Pause ? 1 : 0) } \n");
            }
        }
    }
}