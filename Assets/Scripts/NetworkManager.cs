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
		if (!Network.isClient && !Network.isServer)
		{
			if (GUI.Button(new Rect(Screen.width/2, Screen.height/2, 250, 100), "Start Server"))
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
//		Debug.Log ("At Beginning of SpawnPlayer");
//		Object o = Network.Instantiate(playerPrefab, new Vector3(243.6f, 6.4f, 235.44f), Quaternion.identity, 0);
//		GameObject[] players=GameObject.FindGameObjectsWithTag("Player");
//		GameObject player = (GameObject) o;
//		NetworkView playerNetwork = player.GetComponent<NetworkView>();

		Debug.Log ("At Beginning of SpawnPlayer");

		//GameObject[] players=GameObject.FindGameObjectsWithTag("Player");
		GameObject player = (GameObject) Network.Instantiate(playerPrefab, new Vector3(243.6f, 6.4f, 235.44f), Quaternion.identity, 0);;
		NetworkView playerNetwork = player.GetComponent<NetworkView>();


		if (playerNetwork.isMine)
		{
			Debug.Log ("Creating Camera");
			player.GetComponent<OVRGamepadController>().enabled = true;
			player.GetComponent<OVRPlayerController>().enabled = true;
			player.GetComponentInChildren<OVRCameraRig>().enabled = true;
			player.GetComponentInChildren<OVRManager>().enabled = true;

		}
	}
}
