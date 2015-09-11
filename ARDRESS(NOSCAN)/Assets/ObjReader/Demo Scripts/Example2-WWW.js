// This example loads an OBJ file from the WWW, including the MTL file and any textures that might be referenced

#pragma strict

var objFileName = "http://www.starscenesoftware.com/objtest/Spot.obj";
//var objFileName = "http://54.255.197.148/contents/dee20bb1783f8c2532284e63075f5eaeaf757d74.obj";
private var URL_API = "http://54.255.197.148/contents/";
private var objName = "d27e2a6e06b167417785c43b9fecae01544182cf.obj";
private var mtlName = "6c7d422e96494dde2b56ff2e10dddda62089a6d2.mtl";
private var pngName = "ad43ca70d8562da5fa89eccbd76ee936b502cdaa.jpg";
var standardMaterial : Material;	// Used if the OBJ file has no MTL file
private var objData : ObjReader.ObjData;
private var loadingText = "";
private var loading = false;

function Load () {
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
	while (!objData.isDone) {
		loadingText = "Loading... " + (objData.progress*100).ToString("f0") + "%";
		if (Input.GetKeyDown (KeyCode.Escape)) {
			objData.Cancel();
			loadingText = "Cancelled download";
			loading = false;
			return;
		}
		yield;
	}
	loading = false;
	if (objData == null || objData.gameObjects == null) {
		loadingText = "Error loading file";
		yield;
		return;
	}
	
	loadingText = "Import completed";
//	var numberOfObjects = objData.gameObjects.Length;
//	loadingText = "Generated " + numberOfObjects + " GameObject" + ((numberOfObjects > 1)? "s" : "");
	FocusOnObjects();
}

function OnGUI () {
	GUILayout.BeginArea (Rect(10, 10, 400, 400));
	objFileName = GUILayout.TextField (objFileName, GUILayout.Width(400));
	GUILayout.Label ("Also try pig.obj, car.obj, and cube.obj");
	if (GUILayout.Button ("Import") && !loading) {
		Load();
	}
	GUILayout.Label (loadingText);
	GUILayout.EndArea();
}

function FocusOnObjects () {
	var cam = Camera.main;
	var bounds = new Bounds(objData.gameObjects[0].transform.position, Vector3.zero);
	for (var i = 0; i < objData.gameObjects.Length; i++) {
		bounds.Encapsulate (objData.gameObjects[i].GetComponent(Renderer).bounds);
	}
	
	var maxSize = bounds.size;
	var radius = maxSize.magnitude / 2.0;
    var horizontalFOV = 2.0 * Mathf.Atan (Mathf.Tan (cam.fieldOfView * Mathf.Deg2Rad / 2.0) * cam.aspect) * Mathf.Rad2Deg;
    var fov = Mathf.Min (cam.fieldOfView, horizontalFOV);
    var distance = radius / Mathf.Sin (fov * Mathf.Deg2Rad / 2.0);

	cam.transform.position = bounds.center;
	cam.transform.Translate (-Vector3.forward * distance);
	cam.transform.LookAt (bounds.center);
}