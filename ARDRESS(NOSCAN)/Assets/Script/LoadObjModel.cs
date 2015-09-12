using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LoadObjModel : MonoBehaviour {
	public static List<ObjModel> listObjModel = new List<ObjModel>(); 
	string URL = "http://54.255.197.148/api/v1/contents";

	// Use this for initialization
	IEnumerator Start () {
		button = gameObject.transform.FindChild ("Button").gameObject;
		button.SetActive (false);
		WWW www = new WWW(URL);
		yield return www;
		if (www.error != null) {
			Debug.Log(www.error);
		} else {
			JSONObject jsonObject = new JSONObject(www.data);
			JSONObject jsonData = jsonObject.GetField("data");
			ParserData(jsonData);
			button.SetActive(false);
		}
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
				int count = listObjModel.Count;
				Color color =  Color.red; 
				Color.TryParseHexString("#E1E1E1", out color);
				if(count % 2 == 0) objButton.GetComponent<Image>().color = color;
				objButton.name = count.ToString();
				objButton.SetActive(true);
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
