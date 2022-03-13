using Simple.MPD;
using Simple.MPD.Networking;
using System;

Console.WriteLine("Hello World!");

MPD mpd = new MPD(new TcpConnection("rasp"));
mpd.DoPingAsync().Wait();

var status = mpd.GetStatusAsync().Result;
var stats = mpd.GetStatsAsync().Result;
var list = mpd.GetQueue().Result;

//mpd.QueueClear().Wait();
//mpd.QueueAdd("Musicas Rafa").Wait();


//mpd.PlayAsync().Wait();
//mpd.QueueAdd("uiuaa.mp4").Wait();
//list = mpd.LsInfo("NAS/NAS/Musicas Le").Result;
//var db = Simple.MPD.Helper.DirectoryHelper.ReadAll(mpd, (d) => System.Console.WriteLine(d));
//var list = mpd.LsInfo("").Result;
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

//var notifier = new MpdNotifier(new TcpConnection("127.0.0.1"));
//notifier.NotifyEvent += (s, ev) =>
//{
//    foreach (var evnt in ev.SystemsChanged)
//    {
//        Console.WriteLine($"Event {evnt}");
//    }
//};
//notifier.NotifyStatusChange += (s, ev) =>
//{
//    Console.WriteLine($"[{string.Join(",", ev.Systems)}] [Vol:{ev.Status.Volume}] [{ev.Status.State}] {ev.CurrentSong.SongDisplayName}");
//};
//notifier.Start();

//Console.WriteLine("Press enter to stop notifying");
//Console.ReadLine();
//notifier.Stop();
