using System.IO;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Commands
{
    public class Idle : ICommand
    {
        public enum SubSystems
        {
            /// <summary>
            /// the song database has been modified after update
            /// </summary>
            Database,

            /// <summary>
            /// a database update has started or finished. 
            /// If the database was modified during the update, the database event is also emitted.
            /// </summary>
            Update,

            /// <summary>
            /// a stored playlist has been modified, renamed, created or deleted
            /// </summary>
            Stored_Playlist,

            /// <summary>
            /// the queue (i.e. the current playlist) has been modified
            /// </summary>
            Playlist,

            /// <summary>
            /// the player has been started, stopped or seeked or tags of the currently playing song have changed (e.g. received from stream)
            /// </summary>
            Player,

            /// <summary>
            /// the volume has been changed
            /// </summary>
            Mixer,

            /// <summary>
            /// an audio output has been added, removed or modified (e.g. renamed, enabled or disabled)
            /// </summary>
            Output,

            /// <summary>
            /// options like repeat, random, crossfade, replay gain
            /// </summary>
            Options,

            /// <summary>
            /// a partition was added, removed or changed
            /// </summary>
            Partition,

            /// <summary>
            /// the sticker database has been modified.
            /// </summary>
            Sticker,

            /// <summary>
            /// a client has subscribed or unsubscribed to a channel
            /// </summary>
            Subscription,

            /// <summary>
            /// a message was received on a channel this client is subscribed to; this event is only emitted when the queue is empty
            /// </summary>
            Message,

            /// <summary>
            /// a neighbor was found or lost
            /// </summary>
            Neighbor,

            /// <summary>
            /// the mount list has changed
            /// </summary>
            Mount,
        }

        public string CommandName => "Idle";

        public IResponse GetResponseProcessor()
        {
            return new Responses.IdleResponse();
        }

        public async Task WriteAsync(StreamWriter stream)
        {
            await stream.WriteAsync("idle\n");
        }
    }
}
