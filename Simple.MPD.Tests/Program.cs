using System;
using System.IO;
using System.Threading;
using Simple.MPD;
using Simple.MPD.Commands;
using Simple.MPD.Netwroking;

Console.WriteLine("Hello World!");

MPD mpd = new MPD(new TcpConnection("127.0.0.1"));
mpd.DoPingAsync().Wait();
//mpd.PlayAsync().Wait();
//var playList = mpd.LsInfo("NAS/NAS/Musicas Le").Result;
//var stats = mpd.GetStatsAsync().Result;
//var status = mpd.GetStatusAsync().Result;
//var curr1 = mpd.GetCurrentSongAsync().Result;
//var cfg = mpd.GetConfigAsync().Result;
//var pairs = mpd.GetCommandsAsync().Result;
//pairs = mpd.GetNotCommandsAsync().Result;
//pairs = mpd.GetUrlHandlersAsync().Result;
//pairs = mpd.GetDecodersAsync().Result;
//mpd.SetVolumeAsync(100);
//mpd.SetVolumeAsync(0);
//mpd.SetVolumeAsync(50);

var notifier = new MpdNotifier(new TcpConnection("127.0.0.1"));
notifier.NotifyEvent += (s, ev) =>
{
    foreach (var evnt in ev.SystemsChanged)
    {
        Console.WriteLine($"Event {evnt}");
    }
};
notifier.NotifyStatusChange += (s, ev) =>
{
    Console.WriteLine($"[{string.Join(",", ev.Systems)}] [Vol:{ev.Status.Volume}] [{ev.Status.State}] {ev.CurrentSong.SongDisplayName}");
};
notifier.Start();

Console.WriteLine("Press enter to stop notifying");
Console.ReadLine();
notifier.Stop();



mpd = mpd;
mpd = mpd;
mpd = mpd;
mpd = mpd;
mpd = mpd;
mpd = mpd;

