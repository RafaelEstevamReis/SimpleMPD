using System;
using RafaelEstevam.Simple.MPD;
using RafaelEstevam.Simple.MPD.Commands;
using RafaelEstevam.Simple.MPD.Netwroking;

Console.WriteLine("Hello World!");
MPD mpd = new MPD(new TcpConnection("192.168.0.13"));
mpd.DoPingAsync().Wait();

var stats = mpd.GetStatusAsync().Result;
stats = stats;



