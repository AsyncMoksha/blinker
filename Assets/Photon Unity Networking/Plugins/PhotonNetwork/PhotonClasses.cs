// ----------------------------------------------------------------------------
// <copyright file="PhotonClasses.cs" company="Exit Games GmbH">
//   PhotonNetwork Framework for Unity - Copyright (C) 2011 Exit Games GmbH
// </copyright>
// <summary>
//   
// </summary>
// <author>developer@exitgames.com</author>
// ----------------------------------------------------------------------------
using UnityEngine;
using System.Collections;

// Enums and classes

class PhotonNetworkMessages
{
    public const byte RPC = 200; //TODO: What value to use?
    public const byte SendSerialize = 201;
    public const byte Instantiation = 202;
    public const byte CloseConnection = 203;
    public const byte Destroy = 204;
    public const byte RemoveCachedRPCs = 205;

}

public enum PhotonTargets { All, Others, MasterClient, AllBuffered, OthersBuffered } //.MasterClientBuffered? .Server?
public enum NetworkLogLevel { ErrorsOnly, Informational, Full }


namespace Photon
{
    public class MonoBehaviour : UnityEngine.MonoBehaviour
    {
        public PhotonView photonView
        {
            get
            {
                return PhotonView.Get(this);
            }
        }
        new public PhotonView networkView
        {
            get
            {
                Debug.LogWarning("Why are you still using networkView? should be PhotonView?");
                return PhotonView.Get(this);
            }
        }
    }
}

public class PhotonViewID 
{
    private PhotonPlayer internalOwner;
    private int internalID = -1; // 1-256 (1-MAX_NETWORKVIEWS)
    
    public PhotonViewID(int ID, PhotonPlayer owner)
    {
        internalID = ID;
        internalOwner = owner;
    }

    public int ID
    {   
        // PLAYERNR*MAX_NETWORKVIEWS + internalID
        get
        {
            if(internalOwner == null)
            {
                //Scene ID
                return internalID;
            }
            else
            {
                return (internalOwner.ID*PhotonNetwork.MAX_VIEW_IDS) + internalID;
            }
        }
    }

    public bool isMine
    {
        get { return owner.isLocal; }
    }

    public PhotonPlayer owner
    {
        get
        {
            int ownerNR = ID / PhotonNetwork.MAX_VIEW_IDS;
            return PhotonPlayer.Find(ownerNR);
        }
    }

    public override string ToString()
    {
        return this.ID.ToString();
    }

    public override bool Equals(object p)
    {
        PhotonViewID pp = p as PhotonViewID;
        return (pp != null && this.ID == pp.ID);
    }

    public override int GetHashCode()
    {
        return this.ID;
    }

    [System.Obsolete("Used for compatibility with Unity networking only.")]
    public static PhotonViewID unassigned
    {
        get
        {
            return new PhotonViewID(-1, null);
        }
    }
}

public class PhotonMessageInfo
{
    /// <summary>
    /// To create an empty messageinfo only!
    /// </summary>
    public PhotonMessageInfo()
    {
        this.sender = PhotonNetwork.player;
        this.timeInt = (int)(PhotonNetwork.time*1000);
        this.photonView = null;
    }
    public PhotonMessageInfo(PhotonPlayer player, int timestamp, PhotonView view)
    {
        this.sender = player;
        this.timeInt = timestamp;
        this.photonView = view;
    }
    private int timeInt;
    public PhotonPlayer sender;
    public PhotonView photonView;
    public float timestamp
    {
        get { return (timeInt / 1000.0f); }
    }

    public override string ToString()
    {
        return string.Format("[PhotonMessageInfo: player='{1}' timestamp={0}]", this.timeInt, this.sender);
    }
}

public class PhotonStream
{
    bool write = false;
    Hashtable data;
    byte currentItem = 0;

    public PhotonStream(bool write, Hashtable hashtable)
    {
        this.write = write;
        data = hashtable;
    }

    public bool isWriting
    {
        get { return write; }
    }
    public bool isReading
    {
        get { return !write; }
    }

    public object ReceiveNext()
    {
        if (write)
        {
            Debug.LogError("Error: you cannot read this stream that you are writing!");
            return null;
        }
        object obj = data[currentItem];
        currentItem++;
        return obj;
    }

    public void SendNext(object obj)
    {
        if (!write)
        {
            Debug.LogError("Error: you cannot write/send to this stream that you are reading!");
            return;
        }
        data[currentItem] = obj;
        currentItem++;
    }





    public Hashtable ToHashtable()
    {
        return data;
    }


    public void Serialize(ref bool myBool)
    {
        if (write)
        {
            data[currentItem] = myBool;
            currentItem++;
        }
        else
        {
            if (data.ContainsKey(currentItem)) 
                myBool = (bool)data[currentItem];
            currentItem++;
        }
    }
    public void Serialize(ref int myInt)
    {
        if (write)
        {
            data[currentItem] = myInt;
            currentItem++;
        }
        else
        {
            if (data.ContainsKey(currentItem)) 
                myInt = (int)data[currentItem];
            currentItem++;
        }
    }
    public void Serialize(ref string value)
    {
        if (write)
        {
            data[currentItem] = value;
            currentItem++;
        }
        else
        {
            if (data.ContainsKey(currentItem)) 
                value = (string)data[currentItem];
            currentItem++;
        }
    }
    public void Serialize(ref char value)
    {
        if (write)
        {
            data[currentItem] = value;
            currentItem++;
        }
        else
        {
            if (data.ContainsKey(currentItem)) 
                value = (char)data[currentItem];
            currentItem++;
        }
    }
    public void Serialize(ref short value)
    {
        if (write)
        {
            data[currentItem] = value;
            currentItem++;
        }
        else
        {
            if (data.ContainsKey(currentItem)) 
                value = (short)data[currentItem];
            currentItem++;
        }
    }
    public void Serialize(ref float obj)
    {
        if (write)
        {
            data[currentItem] = obj;
            currentItem++;
        }
        else
        {
            if (data.ContainsKey(currentItem)) 
                obj = (float)data[currentItem];
            currentItem++;
        }
    }
    public void Serialize(ref PhotonPlayer obj)
    {
        if (write)
        {
            data[currentItem] = obj;
            currentItem++;
        }
        else
        {
            if (data.ContainsKey(currentItem)) 
                obj = (PhotonPlayer)data[currentItem];
            currentItem++;
        }
    }
    public void Serialize(ref Vector3 obj)
    {
        if (write)
        {
            data[currentItem] = obj;
            currentItem++;
        }
        else
        {
            if(data.ContainsKey(currentItem))
                obj = (Vector3)data[currentItem];
            currentItem++;
        }
    }
    public void Serialize(ref Vector2 obj)
    {
        if (write)
        {
            data[currentItem] = obj;
            currentItem++;
        }
        else
        {
            if (data.ContainsKey(currentItem))
                obj = (Vector2)data[currentItem];
            currentItem++;
        }
    }
    public void Serialize(ref Quaternion obj)
    {
        if (write)
        {
            data[currentItem] = obj;
            currentItem++;
        }
        else
        {
            if (data.ContainsKey(currentItem)) 
                obj = (Quaternion)data[currentItem];
            currentItem++;
        }
    }
    public void Serialize(ref PhotonViewID obj)
    {
        if (write)
        {
            data[currentItem] = obj.ID;
            currentItem++;
        }
        else
        {
            int ID = (int)data[currentItem];
            currentItem++;
            
            int internalID = ID % PhotonNetwork.MAX_VIEW_IDS;
            int actorID = ID / PhotonNetwork.MAX_VIEW_IDS;
            PhotonPlayer owner = null;
            if (actorID > 0)
            {
                owner = PhotonPlayer.Find(actorID);
            }
            obj = new PhotonViewID(internalID, owner);
            
        }
    }
}