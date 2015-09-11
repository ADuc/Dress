using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LoadObjModel : MonoBehaviour {
	List<ObjModel> listObjModel = new List<ObjModel>(); 
	List<GameObject> listButton = new List<GameObject>();
	string URL = "http://54.255.197.148/api/v1/contents";
	// Use this for initialization
	void Start () {
		WWW www = new WWW(URL);
		while (!www.isDone) ;
		if (!string.IsNullOrEmpty(www.error)) {
			Debug.Log(www.error);
		} else {
			JSONObject jsonObject = new JSONObject(www.text);
			JSONObject jsonData = jsonObject.GetField("data");
			button = gameObject.transform.FindChild ("Button").gameObject;
			ParserData(jsonData);
			button.SetActive(false);
		}
	}

	void buttonSetup(Button button) {

		//button.onClick.RemoveAllListeners();
		//Add your new event
		//button.onClick.AddListener(() => handleButton(button));
	}
	string key = "";
	GameObject objButton = null;
	GameObject button = null;
	void ParserData(JSONObject obj){
		switch(obj.type){
		case JSONObject.Type.OBJECT:
			for(int i = 0; i < obj.list.Count; i++){
				key = (string)obj.keys[i];
				JSONObject j = (JSONObject)obj.list[i];
				ParserData(j);
			}
			break;
		case JSONObject.Type.ARRAY:
			foreach(JSONObject j in obj.list){
				listObjModel.Add(new ObjModel());
				objButton = GameObject.Instantiate(button, transform.position, transform.rotation) as GameObject;
				objButton.transform.SetParent(transform);
				objButton.transform.localScale = transform.localScale;
				ParserData(j);
			}
			break;
		case JSONObject.Type.STRING:
			if(key.Equals("name")) {
				listObjModel[listObjModel.Count - 1].Name = obj.str;
				objButton.transform.FindChild("Text").GetComponent<Text>().text = obj.str;
			} else if(key.Equals("obj"))
				listObjModel[listObjModel.Count - 1].Obj = obj.str;
			else if(key.Equals("mesh"))
				listObjModel[listObjModel.Count - 1].Mtl = obj.str;
			else if(key.Equals("jpg"))
				listObjModel[listObjModel.Count - 1].Png = obj.str;
			break;
		case JSONObject.Type.NUMBER:
			if(key.Equals("id"))
				listObjModel[listObjModel.Count - 1].Id = (int)obj.n;
			break;
		case JSONObject.Type.BOOL:
			Debug.Log(obj.b);
			break;
		case JSONObject.Type.NULL:
			Debug.Log("NULL");
			break;
			
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
