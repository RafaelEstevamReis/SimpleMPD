# Simple MPD

A simple client MPD (Music Player Daemon) protocol implementation in C#

Does it compile?
> ![.NET Core](https://github.com/RafaelEstevamReis/SimpleMPD/workflows/.NET%20Core/badge.svg)

## Installing

Get from NuGet and start testing

Can I mess with it ?
> [![NuGet](https://buildstats.info/nuget/Simple.MPD)](https://www.nuget.org/packages/Simple.MPD)

> PM> Install-Package Simple.MPD

## Compatibility

High compatibility, currently supports:
* .Net 6
* .Net Core 3.1
* .Net Standard 2.0
  * .NetCore 2.0+
  * .Net Framework 4.6.1+
  * Mono 5.4+
  * Xamarin.iOS 10.14+
  * Xamarin.Android 8.0+
  * UWP 10.0.16299+
  * Unity 2018.1+

# Table of Contents
<!-- TOC -->
- [Simple MPD](#simple-mpd)
  - [Installing](#installing)
  - [Compatibility](#compatibility)
- [Table of Contents](#table-of-contents)
  - [What is MPD ?](#what-is-mpd-)
  - [Protocol documentation](#protocol-documentation)
  - [What this lib does ?](#what-this-lib-does-)
  - [License](#license)
<!-- /TOC -->

## What is MPD ?

MPD is a Server-Side application for playing music

This repository is a C# implementation of the Client-Side protocol, this code allow you to communicate with a MPD

MPD Website: https://www.musicpd.org/

[MPD Documentation](https://www.musicpd.org/doc/html/user.html)

## Protocol documentation

[musicpd.org Protocol](https://www.musicpd.org/doc/html/protocol.html)

> The **MPD** command protocol exchanges line-based text records between client and server over TCP. Once the client is connected to the server, they conduct a conversation until the client closes the connection. The conversation flow is always initiated by the client.

## What this lib does ?

Currently it has all crucial commands to understand and control the MPD server

Current implemented commands

* Ping
* Close - Closes the socket as docs request
* Config - Gets configuration, only in local endpoint
* Commands - List all available commands
* NotCommands - List all **un**available commands
* UrlHandlers - List all available Url Handlers
* Decoders - List all available decoders
* Idle - Waits for commands
* Stats - General info
* Status - Current song info
* CurrentSong - Current song Name and File
* Consume - Set Consume mode On/Off
* CrossFade - Set cross fade seconds
* Random - Set Random mode On/Off
* Repeat - Set Repeat mode On/Off
* SetVol - Set volume value
* Single - Set single mode
* Play / Pause / Stop
* Next / Previous
* PlayListInfo - Get Queue song's
* Clear - Clear queue
* Add - Adds (recursively) to the queue
* AddId - Add a file to the queue and return it's Id
* Delete - Deletes a song from the queue
* DeleteId - Deletes the song SongId from the queue
* Shuffle - Shuffles the queue
* Swap - Swap two songs
* Move - Move song to position
* ListAll - Lists all songs and directories in URI. Do not use this command
* LsInfo - Lists the contents of the directory URI
* Find - find files - Case-Sensitive
* List - list tags
* Search - find files - Case-Insensitive
* ListPlayLists - List all playlists
* ListPlaylistInfo - List files in playlist
* Rename - Rename a playlist
* Rm - Delete a playlist
* Save - Save the queue as a playlist
* Load - Load the queue as a playlist
* PlaylistDelete - Deletes SongPos from the playlist
* PlaylistMove - Moves the song at position FROM to the position TO


## License

This library is licensed under the **MIT License**

A short and simple permissive license with conditions only requiring preservation of copyright and license notices. Licensed works, modifications, and larger works may be distributed under different terms and without source code.
