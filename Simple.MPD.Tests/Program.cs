﻿using System;
using Simple.MPD;
using Simple.MPD.Commands;
using Simple.MPD.Netwroking;

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
mpd.SetVolumeAsync(100);
mpd.SetVolumeAsync(0);
mpd.SetVolumeAsync(50);

mpd = mpd;


