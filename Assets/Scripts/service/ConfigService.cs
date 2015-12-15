using UnityEngine;
using SimpleJSON;
using System.Collections;

public class ConfigService : MonoBehaviour {

	private string uri;

	// Use this for initialization
	void Start () {
		uri = "http://localhost/db.json";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI()
	{
		if (GUILayout.Button("Start Polling Config from: " + uri, GUILayout.Height(100)))
		{	
			//poll every 2 seconds
			InvokeRepeating("SendRequest", 0, 2);
		}

	}

	//start coroutine for async handling
	void SendRequest(){
		StartCoroutine(RequestConfig());
	}

	/**
	 * Async Request Function
	 * */
	IEnumerator RequestConfig(){

		WWW www = new WWW(uri);

		//wait for result with yield
		yield return www;
		if (www.error == null)
		{
			ProcessJson(www.data);
		}
		else
		{
			Debug.Log("ERROR: " + www.error);
		}     
	
	}

	/**
	 * A simple Json Parser
	 * */
	void ProcessJson(string jsonString){
		JSONClass obj = JSON.Parse(jsonString).AsObject;
		JSONClass productObj = obj ["product"].AsObject;		
		JSONArray attributeGroupArray = productObj ["attributeGroups"].AsArray;

		JSONArray exteriorArray = null;
		JSONArray interiorArray = null;

		for (int i = 0; i < attributeGroupArray.Count; i++) {
			JSONClass groupClass = attributeGroupArray[i].AsObject;
			string name = groupClass["name"];

			if(name == "Exterior"){
				exteriorArray = groupClass["attributes"].AsArray;
			}
			if(name == "Interior"){
				interiorArray = groupClass["attributes"].AsArray;
			}
		}

		//parse exterior
		print (exteriorArray);
		for (int i = 0; i < exteriorArray.Count; i++) {
			JSONClass attributeClass = exteriorArray[i].AsObject;
			string name = attributeClass["name"];

			if(name == "Farbe"){
				JSONArray values = attributeClass["selectedValues"].AsArray;
				string hexString = values[0];
				print(hexString);

				Color color = new Color();
				ColorUtility.TryParseHtmlString (hexString.ToUpper(), out color);
				
				GameObject.Find("Karosserie").GetComponent<Renderer>().material.color = color;
				GameObject.Find("mirror_left").GetComponent<Renderer>().material.color = color;
				GameObject.Find("mirror_right").GetComponent<Renderer>().material.color = color;
			}
		}
		//TODO: parse interior
	}
}
