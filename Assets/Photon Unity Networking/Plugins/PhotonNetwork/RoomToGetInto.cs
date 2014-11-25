// ----------------------------------------------------------------------------
// <copyright file="RoomToGetInto.cs" company="Exit Games GmbH">
//   PhotonNetwork Framework for Unity - Copyright (C) 2011 Exit Games GmbH
// </copyright>
// <summary>
//   Represents the room to be joined (before join finishes)
// </summary>
// <author>developer@exitgames.com</author>
// ----------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// Used for the room to get into and as current room.
/// </summary>
    class RoomToGetInto : Room
    {
        internal string ServerAddress { get; set; }
        internal bool IsLocalClientInside { get; set; } // keeps state if the local client is already in the game or still going to join it on gameserver
        internal JoinType TypeOfJoin { get; set; } // stores the way this client got/gets into this game: create, join or joinrandom

        // if this is true, the game properties will be set by this client
        internal bool IsCreatedByMe 
        { 
            get
            {
                return (this.TypeOfJoin == JoinType.CreateGame);
            } 
        }

        internal RoomToGetInto(string roomName, Hashtable properties, JoinType jointype) : base(roomName, properties)
        {
            this.TypeOfJoin = jointype;
        }
    }
