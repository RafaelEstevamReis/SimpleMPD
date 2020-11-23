# Simple MPD

A simple client MPD protocol implementation in C#

Does it compile?
> ![.NET Core](https://github.com/RafaelEstevamReis/SimpleMPD/workflows/.NET%20Core/badge.svg)

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

Today ? Basically nothing, I'm still writing it. 

It connects, receives the Version and waits for commands, which I implemented:

* Close - Closes the socket as docs request
* Ping
* Stats - General info
* Status - Current song info
* CurrentSong - Current song Name and File
* Config - Gets configuration, only in local endpoint
* Commands - List all available commands
* NotCommands - List all **un**available commands
* UrlHandlers - List all available Url Handlers
* Decoders - List all available decoders

Once this code does something more useful, this section wil be replaced