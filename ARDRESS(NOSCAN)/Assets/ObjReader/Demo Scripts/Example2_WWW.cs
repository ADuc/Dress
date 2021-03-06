// This example loads an OBJ file from the WWW, including the MTL file and any textures that might be referenced

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Example2_WWW : MonoBehaviour {

	public string objFileName = "http://www.starscenesoftware.com/objtest/Spot.obj";
	public string URL_API = "http://54.255.197.148/contents/";
	public Material standardMaterial;	// Used if the OBJ file has no MTL file
	ObjReader.ObjData objData;
	string loadingText = "";
	bool loading = false;
	public GameObject lableLoading;
	Change c;

	void Start () {


	}
	public IEnumerator Load (string objName, string mtlName, string pngName) {
		Debug.Log (" ------------------------------------------- " + objName + ", " + mtlName + ", " + pngName);
		c = GameObject.Find ("Canvas").GetComponent<Change> ();
		loading = true;
		if (objData != null && objData.gameObjects != null) {
			for (var i = 0; i < objData.gameObjects.Length; i++) {
				Destroy (objData.gameObjects[i]);
			}
		}
		if(!mtlName.Equals("") && !pngName.Equals("")) {
			objFileName = URL_API + objName;
			objData = ObjReader.use.ConvertFileAsync (objFileName, true, standardMaterial, mtlName, pngName);
		} else {
			objData = ObjReader.use.ConvertFileAsync (objFileName, true, standardMaterial);
		}
		objData = ObjReader.use.ConvertFileAsync (objFileName, true, standardMaterial);
		while (!objData.isDone) {
			loadingText = "Loading... " + (objData.progress*100).ToString("f0") + "%";
			if (Input.GetKeyDown (KeyCode.Escape)) {
				objData.Cancel();
				loadingText = "Cancelled download";
				loading = false;
				loadingText = "Error loading file";
				c.ExitPanel ();
				yield break;
			}
			yield return null;
		}
		loading = false;

		if (objData == null || objData.gameObjects == null) {
			loadingText = "Error loading file";
			c.ExitPanel ();
			yield return null;
			yield break;
		}

		//GameObject loadtext = GameObject.Find ("Canvas").transform.FindChild ("PanelPopupLoad").FindChild ("LoadText").gameObject;

		loadingText = "Import completed";
		GameObject objecLoad = GameObject.Find ("material0");
		GameObject model = GameObject.Find ("GameObjectModel").transform.FindChild("header").FindChild("default").gameObject;
		model.GetComponent<MeshFilter> ().mesh = objecLoad.GetComponent<MeshFilter> ().mesh;
		model.GetComponent<MeshRenderer> ().material = objecLoad.GetComponent<MeshRenderer> ().material;
		model.transform.position = new Vector3(model.transform.position.x,model.transform.position.y,model.transform.position.z);
		c.ExitPanel ();
		Destroy (objecLoad);
		objecLoad = GameObject.Find ("material0");
		if(objecLoad != null)
			Destroy (objecLoad);
		//FocusOnObjects();
	}





	void OnGUI () {
		if(lableLoading != null)
			lableLoading.GetComponent<Text>().text = loadingText;
		/*GUILayout.BeginArea (new Rect(10, 10, 400, 400));
		objFileName = GUILayout.TextField (objFileName, GUILayout.Width(400));
		GUILayout.Label ("Also try pig.obj, car.obj, and cube.obj");
		if (GUILayout.Button ("Import") && !loading) {
			StartCoroutine (Load("", "", ""));
		}
		GUILayout.Label (loadingText);
		GUILayout.EndArea();*/
	}
	
	void FocusOnObjects () {
		var cam = Camera.main;
		var bounds = new Bounds(objData.gameObjects[0].transform.position, Vector3.zero);
		for (var i = 0; i < objData.gameObjects.Length; i++) {
			bounds.Encapsulate (objData.gameObjects[i].GetComponent<Renderer>().bounds);
		}
		
		var maxSize = bounds.size;
		var radius = maxSize.magnitude / 2.0f;
	    var horizontalFOV = 2.0f * Mathf.Atan (Mathf.Tan (cam.fieldOfView * Mathf.Deg2Rad / 2.0f) * cam.aspect) * Mathf.Rad2Deg;
	    var fov = Mathf.Min (cam.fieldOfView, horizontalFOV);
	    var distance = radius / Mathf.Sin (fov * Mathf.Deg2Rad / 2.0f);
	
		cam.transform.position = bounds.center;
		cam.transform.Translate (-Vector3.forward * distance);
		cam.transform.LookAt (bounds.center);
	}
}