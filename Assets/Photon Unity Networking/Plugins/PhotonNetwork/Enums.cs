// ----------------------------------------------------------------------------
// <copyright file="Enums.cs" company="Exit Games GmbH">
//   PhotonNetwork Framework for Unity - Copyright (C) 2011 Exit Games GmbH
// </copyright>
// <summary>
//   
// </summary>
// <author>developer@exitgames.com</author>
// ----------------------------------------------------------------------------
using ExitGames.Client.Photon;
using ExitGames.Client.Photon.Lite;

public enum ConnectionState
{
    Disconnected,
    Connecting,
    Connected,
    Disconnecting,
    InitializingApplication
}

public enum PeerState
{
    Uninitialized,
    PeerCreated,
    Connecting,
    Connected,
    Queued,
    Authenticated,
    JoinedLobby,
    DisconnectingFromMasterserver,
    ConnectingToGameserver,
    ConnectedToGameserver,
    Joining,
    Joined,
    Leave,
    Leaving,
    Left,
    DisconnectingFromGameserver,
    ConnectingToMasterserver,
    ConnectedComingFromGameserver,
    QueuedComingFromGameserver,
    Disconnect,
    Disconnecting,
    Disconnected
}

public enum JoinType
{
    CreateGame,
    JoinGame,
    JoinRandomGame
}

public class OperationCode
{
    public const byte Authenticate = 230;
    public const byte JoinLobby = 229;
    public const byte LeaveLobby = 228;
    public const byte CreateGame = 227;
    public const byte JoinGame = 226;
    public const byte JoinRandomGame = 225;
    // public const byte CancelJoinRandom = 224; // obsolete, cause JoinRandom no longer is a "process". now provides result immediately
    public const byte Leave = (byte)LiteOpCode.Leave;
    public const byte RaiseEvent = (byte)LiteOpCode.RaiseEvent;
    public const byte SetProperties = (byte)LiteOpCode.SetProperties;
    public const byte GetProperties = (byte)LiteOpCode.GetProperties;
}

public class ParameterCode
{
    public const byte Address = 230;
    public const byte PeerCount = 229;
    public const byte GameCount = 228;
    public const byte MasterPeerCount = 227;
    public const byte UserId = 225;
    public const byte ApplicationId = 224;
    public const byte Position = 223;
    public const byte GameList = 222;
    public const byte Secret = 221;
    public const byte AppVersion = 220;
    public const byte AzureNodeInfo = 210;	// only used within events, so use: EventCode.AzureNodeInfo
    public const byte AzureLocalNodeId = 209;
    public const byte AzureMasterNodeId = 208;

    public const byte GameId = (byte)LiteOpKey.GameId;
    public const byte Broadcast = (byte)LiteOpKey.Broadcast;
    public const byte ActorList = (byte)LiteOpKey.ActorList;
    public const byte ActorNr = (byte)LiteOpKey.ActorNr;
    public const byte ActorProperties = (byte)LiteOpKey.ActorProperties;
    public const byte CustomEventContent = (byte)LiteOpKey.Data;
    public const byte Data = (byte)LiteOpKey.Data;
    public const byte Code = (byte)LiteOpKey.Code;
    public const byte GameProperties = (byte)LiteOpKey.GameProperties;
    public const byte Properties = (byte)LiteOpKey.Properties;
    public const byte TargetActorNr = (byte)LiteOpKey.TargetActorNr;
    public const byte ReceiverGroup = (byte)LiteOpKey.ReceiverGroup;
    public const byte Cache = (byte)LiteOpKey.Cache;
}

public class EventCode
{
    public const byte GameList = 230;
    public const byte GameListUpdate = 229;
    public const byte QueueState = 228;
    public const byte Match = 227;
    public const byte AppStats = 226;
    public const byte AzureNodeInfo = 210;
    public const byte Join = (byte)LiteEventCode.Join;
    public const byte Leave = (byte)LiteEventCode.Leave;
    public const byte SetProperties = (byte)LiteEventCode.PropertiesChanged;
}

// Photon properties, internally set by PhotonNetwork (PhotonNetwork builtin properties)
public class ActorProperties
{
    public const byte PlayerName = 255; // was: 1
}

public class GameProperties
{
    public const byte MaxPlayers = 255;
    public const byte IsVisible = 254;
    public const byte IsOpen = 253;
    public const byte PlayerCount = 252;
    public const byte Removed = 251;
}

public class ErrorCode
{
    // server - Photon low(er) level: <= 0
    public const int OperationNotAllowedInCurrentState = -3;
    public const int InvalidOperationCode = -2;
    public const int InternalServerError = -1;
    public const int Ok = 0;

    // server - PhotonNetwork: 0x7FFF and down
    // logic-level error codes start with short.max

    /// <summary>Authentication failed. Possible cause: AppId is unknown to Photon (in cloud service).</summary>
    public const int InvalidAuthentication = 0x7FFF;
    public const int GameIdAlreadyExists = 0x7FFF - 1;
    public const int GameFull = 0x7FFF - 2;
    public const int GameClosed = 0x7FFF - 3;
    public const int AlreadyMatched = 0x7FFF - 4;
    public const int ServerFull = 0x7FFF - 5;
    public const int UserBlocked = 0x7FFF - 6;
    public const int NoRandomMatchFound = 0x7FFF - 7;
    public const int GameDoesNotExist = 0x7FFF - 9;
}

/// <summary>
/// This enum makes up the set of MonoMessages sent by Photon Unity Networking.
/// Implement any of these constant names as method and it will be called
/// in the respective situation.
/// </summary>
/// <example>
/// Implement: 
/// public void OnLeftRoom() { //some work }
/// </example>
public enum PhotonNetworkingMessage
{
    OnConnectedToPhoton,
    OnLeftRoom,
    OnMasterClientSwitched,
    OnPhotonCreateGameFailed,
    OnPhotonJoinGameFailed,
    OnCreatedRoom,
    OnJoinedLobby,
    OnLeftLobby,
    OnDisconnectedFromPhoton,
    OnFailedToConnectToPhoton,
    OnReceivedRoomList,
    OnReceivedRoomListUpdate,
    OnJoinedRoom,
    OnPhotonPlayerConnected,
    OnPhotonPlayerDisconnected,
    OnPhotonRandomJoinFailed,
}