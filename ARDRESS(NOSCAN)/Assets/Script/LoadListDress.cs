using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class LoadListDress : MonoBehaviour {
	private GameObject objBtDress = null;
	private int num = 10;
	public static List<GameObject> listCheck = new List<GameObject>();
	// Use this for initialization
	void Start () {
		objBtDress = transform.FindChild ("ButtonDress").gameObject;
		GameObject pnDress1 = transform.FindChild ("PanelMask1").transform.FindChild ("PanelDress1").gameObject;
		GameObject pnDress2 = transform.FindChild ("PanelMask2").transform.FindChild ("PanelDress2").gameObject;
		GameObject pnDress3 = transform.FindChild ("PanelMask3").transform.FindChild ("PanelDress3").gameObject;
		for (int i = 0; i < num*3; i++) {
			GameObject obj = GameObject.Instantiate(objBtDress, transform.position, transform.rotation) as GameObject;
			if(i/10 == 0)
				obj.transform.SetParent(pnDress1.transform);
			else if(i/10 == 1)
				obj.transform.SetParent(pnDress2.transform);
			else
				obj.transform.SetParent(pnDress3.transform);
			obj.name = i.ToString();
			obj.GetComponent<Image>().overrideSprite = (Sprite)Resources.Load<Sprite>("Dress/Dress ("+(i+1)+")");
			obj.transform.localScale = objBtDress.transform.localScale;
			GameObject objCheck = obj.transform.FindChild("imgCheck").gameObject;
			listCheck.Add(objCheck);
			objCheck.SetActive(false);
		}
		objBtDress.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
