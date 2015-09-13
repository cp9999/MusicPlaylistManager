MusicPlaylistManager
====================

Utitlity to manage music playlists for Plex AND for the Plex channel plugin: MusicPlaylist. 
This is a Windows application that requires .Net framework 4.0

[2015-09-13]
1.  Added 'Logon to myPlex' option, needed when Plex home users are setup
2.  Added Premium Music libraries to server browser
3.  Support playlist per user
4.  Fixed several issues (e.g. like wrong ordering)

[2015-02-04]
1.  Fixed searching in music sections
2.	Added option to show the PMS server in a separate window


[2015-02-01]
PlaylistManager can now be used to maintain native plex playlists.
The following features are available
1.  Import playlists from the Music Playlist Channel into plex playlists
    Note: This requires either the Music Playlist channel to be installed on PMS 
          OR access to the data folder (or a copy) of the music playlist channel, typically:
		     ($Plex Media Server)\Plug-in Support\Data\com.plexapp.plugins.playlist\DataItems
2.  Create / maintain playlists (all stored locally at ($UserAppDataLocal)\PlexMusicPlaylists).
    Note: This requires connection to the running Plex Media Server where playlists are targetted for
3.  Generate SQL file per playlist with all sql commands to update / insert a playlist to the plex database
4.  and/or Update the sqlite database directly from the playlist manager
5.  Added Artist and Album to the playlist grid
6.  Added auto connect option

Known issue:
When using an UNC path for the .db file, make sure to use forward slashes (e.g. //../),
otherwise the .db file cannot be accessed.


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

3. Option "Match on filename" requires additional setup in case:
	A. your PMS server does NOT use windows style path AND your m3u file contains lines with relative path information
	   => Mapped Location for each Plex Location must be set to the full path (windows style) as seen by the client that runs this utility.
	B. your m3u file contains lines with full path information where the path style differs from the path style of your PMS server.
	   => Mapped Location for each Plex Location must be set to the full path in same path style as used in the m3u-file

			