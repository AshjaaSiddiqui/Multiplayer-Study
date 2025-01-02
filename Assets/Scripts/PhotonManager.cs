using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_InputField UserName;
    [SerializeField] TMP_InputField RoomName;
    [SerializeField] TMP_InputField Maxplayers;


    [SerializeField] GameObject Login_Panel;
    [SerializeField] GameObject Connecting_Panel;
    [SerializeField] GameObject Lobby_Panel;
    [SerializeField] GameObject CreateRoom_Panel;



    #region Unity_Methods

    #region Public_Methods

    public void SetActive_Gameobjects(GameObject gameobject, bool On_or_Off)
    {
        gameobject.SetActive(On_or_Off);
    }
    //public int some;
    //[ContextMenu("Turn on ")]
    //public void Turnon()
    //{
    //    some ^= 1;
    //    print(some);

      
    //}
    public string Generate_Random_String(int NumberOfAlphabets)
    {
        string GeneratedString = "";
        List<string> Alphabets = new List<string>();
        Alphabets.Add("a");
        Alphabets.Add("b");
        Alphabets.Add("c");
        Alphabets.Add("d");
        Alphabets.Add("e");
        Alphabets.Add("f");
        Alphabets.Add("g");
        Alphabets.Add("h");
        Alphabets.Add("i");
        Alphabets.Add("j");
        Alphabets.Add("k");
        Alphabets.Add("l");
        Alphabets.Add("m");
        Alphabets.Add("n");
        Alphabets.Add("o");
        Alphabets.Add("p");
        Alphabets.Add("q");
        Alphabets.Add("r");
        Alphabets.Add("s");
        Alphabets.Add("t");
        Alphabets.Add("u");
        Alphabets.Add("v");
        Alphabets.Add("w");
        Alphabets.Add("x");
        Alphabets.Add("y");
        Alphabets.Add("z");

        for (int i = 0; i < NumberOfAlphabets; i++)
        {

            GeneratedString += Alphabets[Random.Range(0,Alphabets.Count)];
        }
        Debug.LogError(GeneratedString);
        return GeneratedString;

    }

    #endregion

    #region UI_Methods
    public void On_Login_Click()
    {
        string Name = UserName.text;

        if (!string.IsNullOrEmpty(Name))
        {
            PhotonNetwork.LocalPlayer.NickName = Name;
            PhotonNetwork.ConnectUsingSettings();
            SetActive_Gameobjects(Connecting_Panel, true);
            SetActive_Gameobjects(Login_Panel, false);

        }
        else Debug.Log("Name is Empty");
    }


    public void On_RoomCreate_Click()
    {
        string Roomname = RoomName.text;

        if (string.IsNullOrEmpty(Roomname))
        {
            Roomname = Generate_Random_String(4);
            RoomName.text = Roomname;
        }
        if (!string.IsNullOrEmpty(Roomname))
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = int.Parse(Maxplayers.text);
            PhotonNetwork.CreateRoom(Roomname,roomOptions);
        }

    }

    public void Active_CreateRoom_Panel()
    {
        SetActive_Gameobjects(CreateRoom_Panel, true);
        Lobby_Panel.SetActive(false);
    }
    public void Deactive_CreateRoom_Panel()
    {
        SetActive_Gameobjects(CreateRoom_Panel, false);
    }


    #endregion
    // Start is called before the first frame update
    void Start()
    {
        SetActive_Gameobjects(Login_Panel, true);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Current Client State : " + PhotonNetwork.NetworkClientState);
    }
    #endregion



    #region Photon_Callbacks

    public override void OnConnected()
    {
        Debug.Log("Connected to Internet");

    }

    public override void OnConnectedToMaster()
    {
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + " Connected to photon");
        SetActive_Gameobjects(Lobby_Panel, true);
        SetActive_Gameobjects(Connecting_Panel, false);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log(RoomName.text + "Room Created");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.LocalPlayer.NickName +" Joined " + RoomName.text);
    }


    #endregion
}
