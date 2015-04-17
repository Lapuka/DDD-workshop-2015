using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	private const string typeName = "DDD15042015";
	private const string gameName = "Testtest";
	private HostData[] hostList;
	public GameObject playerPrefab;
	
	private void StartServer()
	{
		Network.InitializeServer(4, 25000, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, gameName);
	}
	void OnGUI()
	{
		if (Input.GetKey("escape"))
		{
			Application.Quit ();
			Debug.Log ("Application Quited");
		}
		if (Input.GetKeyDown(KeyCode.Backspace))
		{
			Application.LoadLevel (Application.loadedLevel);
			Network.Disconnect ();
		}
		if (!Network.isClient && !Network.isServer)
		{
			GUIStyle gs = new GUIStyle();
			gs.fontSize = 50;
			gs.normal.textColor = Color.white;
			gs.alignment = TextAnchor.UpperCenter;
			GUI.Label(new Rect(Screen.width/2-150, 20, 300, 200), "IT Development Team", gs);
			if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server"))
				StartServer();
			
			if (GUI.Button(new Rect(100, 250, 250, 100), "Refresh Hosts"))
				RefreshHostList();
			
			if (hostList != null)
			{
				for (int i = 0; i < hostList.Length; i++)
				{
					if (GUI.Button(new Rect(400, 100 + (110 * i), 300, 100), hostList[i].gameName))
						JoinServer(hostList[i]);

				}
			}
		}
	}
	private void RefreshHostList()
	{
		MasterServer.RequestHostList(typeName);
	}
	
	void OnMasterServerEvent(MasterServerEvent msEvent)
	{
		if (msEvent == MasterServerEvent.HostListReceived)
			hostList = MasterServer.PollHostList();
	}
	private void JoinServer(HostData hostData)
	{
		Network.Connect(hostData);
		Debug.Log ("Client Connected to Server");
	}

	void OnServerInitialized()
	{
		SpawnPlayer();
		Debug.Log ("Host Connected to Server");
	}
	
	void OnConnectedToServer()
	{
		SpawnPlayer();
		Debug.Log ("Connected Player Spawned");
	}
	
	private void SpawnPlayer()
	{
		GameObject player = (GameObject) Network.Instantiate(playerPrefab, new Vector3(243.6f, 6.4f, 235.44f), Quaternion.identity, 0);;
		NetworkView playerNetwork = player.GetComponent<NetworkView>();
	}
	void OnPlayerDisconnected(NetworkPlayer player)
	{
		Debug.Log("Clean up after player " + player);
		Network.RemoveRPCs(player);
		Network.DestroyPlayerObjects(player);
	}
}
