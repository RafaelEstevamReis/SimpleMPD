# Simple MPD

A simple MPD implementation in C#

Does it compile?
> ![.NET Core](https://github.com/RafaelEstevamReis/SimpleMPD/workflows/.NET%20Core/badge.svg)


# Content
<!-- TOC -->
- [Simple MPD](#simple-mpd)
- [Content](#content)
  - [Protocol documentation](#protocol-documentation)
  - [What this repo does ?](#what-this-repo-does-)
<!-- /TOC -->

## Protocol documentation

[musicpd.org Protocol](https://www.musicpd.org/doc/html/protocol.html)

> The **MPD** command protocol exchanges line-based text records between client and server over TCP. Once the client is connected to the server, they conduct a conversation until the client closes the connection. The conversation flow is always initiated by the client.

## What this repo does ?

Today ? Basically nothing, I'm still writing it. 

It connects, receives the Version and waits for commands, which I implemented:

* Ping (I have to start on something ...)

