using System;
using RafaelEstevam.Simple.MPD;
using RafaelEstevam.Simple.MPD.Commands;
using RafaelEstevam.Simple.MPD.Netwroking;

Console.WriteLine("Hello World!");
MPD mpd = new MPD(new TcpConnection("192.168.0.13"));
mpd.DoPingAsync().Wait();

//var stats = mpd.GetStatsAsync().Result;
//var status = mpd.GetStatusAsync().Result;
var curr = mpd.GetCurrentSongAsync().Result;
//var cfg = mpd.GetConfigAsync().Result;
//var pairs = mpd.GetCommandsAsync().Result;
//pairs = mpd.GetNotCommandsAsync().Result;
//pairs = mpd.GetUrlHandlersAsync().Result;
//pairs = mpd.GetDecodersAsync().Result;
mpd.SetVolume(100);
mpd.SetVolume(0);
mpd.SetVolume(50);

mpd = mpd;



