----ATC LOGON----
TCP Connection | Client -> Server
$DISERVER:CLIENT:(NETWORK FSD VERSION):(22 char token) | Server -> Client
$ID(callsign):SERVER:(client code):(client string):3:2:(networkID):(num) | Client -> Server
%(callsign):(frequency 1xx.yyy):0:(protocol version):(rating):(lat):(lon) | Client -> Server //Position Update
#TMserver:(callsign):Welcome to PTSIM! | Server -> Client MSOD
-----------------

----PILOT LOGON----
TCP Connection | Client -> Server
//Pause Here until Wiresharking vPilot/xPilot
-------------------

----LOGOFF----
#DA(callsign):SERVER | Client -> Server //Disconnect ATC
#DP(callsign):SERVER | Client -> Server //Disconnect Pilot (best guess, fix when wiresharking vPilot)

#TM(callsign):(callsign):(message) | Client -> Server -> Client //Private Message
#DLSERVER:*:0:0 //Heartbeat, basically like a ping to all clients to let them know the server is still alive
$AX(callsign):SERVER:METAR:(ICAO) | Client -> Server
$ARserver:(callsign):METAR:(metar) |  Server -> Client
$CQ(callsign):(controller):ATIS | Client -> Server //Request ATIS

$CR(requestee):(requester):ATIS:V:(voice server address, without "http://") | Server -> Client
...
$CR(requestee):(requester):ATIS:T:(line of ATIS text) | Server -> Client
$CR(requestee):(requester):ATIS:T:(line of ATIS text) | Server -> Client
...
$CR(requestee):(requester):ATIS:E:(total number of lines, including this line) | Server -> Client

$CQ(requester):(requestee):RN | Client -> Server //Real Name
$CR(requestee):(requester):RN:(realname ICAO)::(rating) | Server -> Client
