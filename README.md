# Simple MPD

A simple client MPD (Music Player Daemon) protocol implementation in C#

Does it compile?
> ![.NET Core](https://github.com/RafaelEstevamReis/SimpleMPD/workflows/.NET%20Core/badge.svg)
 
Can I mess with it ?
> [![NuGet](https://buildstats.info/nuget/Simple.MPD)](https://www.nuget.org/packages/Simple.MPD)

<!-- TOC -->
- [Simple MPD](#simple-mpd)
  - [What is MPD ?](#what-is-mpd-)
  - [Protocol documentation](#protocol-documentation)
  - [What this repo does ?](#what-this-repo-does-)
<!-- /TOC -->

## What is MPD ?

MPD is a Server-Side application for playing music

This repository is a C# implementation of the Client-Side protocol, this code allow you to communicate with a MPD

MPD Website: https://www.musicpd.org/

[MPD Documentation](https://www.musicpd.org/doc/html/user.html)

## Protocol documentation

[musicpd.org Protocol](https://www.musicpd.org/doc/html/protocol.html)

> The **MPD** command protocol exchanges line-based text records between client and server over TCP. Once the client is connected to the server, they conduct a conversation until the client closes the connection. The conversation flow is always initiated by the client.

## What this repo does ?

Today ? Some cool stuff but not all, I'm still writing it. 

It connects, receives the Version and waits for commands, which I implemented:

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
* Crossfade - Set crossfade seconds
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
* Delete - Deletes a song from the playlist
* DeleteId - Deletes the song SONGID from the playlist
* Shuffle - Shuffles the queue
* ListAll - Lists all songs and directories in URI. Do not use this command
* LsInfo - Lists the contents of the directory URI
* Find - find files - Case-Sensitive
* Search - find files - Case-Insensitive
* ListplayLists - List all playlists
* ListPlaylistInfo - List files in playlist
* Rename - Rename a playlist
* Rm - Delete a playlist
* Save - Save the queue as a playlist
* Load - Load the queue as a playlist
* PlaylistDelete - Deletes SONGPOS from the playlist
* PlaylistMove - Moves the song at position FROM to the position TO


Once this code does something more useful, this section wil be replaced
