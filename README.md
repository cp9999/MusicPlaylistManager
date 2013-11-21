MusicPlaylistManager
====================

Utitlity to manage music playlists for Plex channel plugin: MusicPlaylist. 
This is a Windows application that requires .Net framework 4.0

[2013-11-23]
Added import function for m3u playlist files.
Tracks in the m3u playlist can be matched with tracks in the Plex database in two ways:
1. "Match on filename"
    => Find matching track in the plex database based on matching folder structure AND filename
	Possible in case plex folders are equal to folder structure in the m3u file. 
	See also Note 3. below
2. "Match on title"
    => Find matching track based or searching for track title in the Plex database
	In case of an Extended m3u file format, both the Artist and the Title are retrieved from the m3u file for each track.

Notes: 
1. Both the Artist and the Title column can be edited in the import grid. The "Match on title" uses the changed values when executed (existing matches will be cleared first).

2. If any matches are available for a line, right click on the match icon (or button "Select a match") will show a popup menu that allows selecting a different track. The matching enties in the popup show matching indicators:
	F = This is an exact match on Folder-structure and Filename (highest priority)
	T = Matches the Title exactly
	A = Matches the Artist exactly
	N = Matches the filename (with or without same folder)

3. Option "Match on filename" requires additional setup. All the folders that are assigned to the sections of the Plex Media Server must be mapped to the full path as seen by the client that runs this utility.
Example:
	=> Plex Media Server(Ubuntu)
		- Music section:
			- Folder: /mnt/music		(Mount point for NFS share for music)
	=> The same music folder can be accesed on the windows client at: \\NAS\Music
	This requires the following mapping:
		Plex Location 	= /mnt/music
		Mapped location	= \\NAS\Music		(or //NAS/Music, which is also allowed on Windows)
			