using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class Change : MonoBehaviour {

	public GameObject LoadPanel;
	static int numberObject = 4;
	public GameObject[] ADress = new GameObject[numberObject];
	public GameObject[] Text = new GameObject[3];
	private Example2_WWW objGetModel;
	//public GameObject[] Button = new GameObject[numberObject];
	//public GameObject a = new GameObject();
	void Start () {
		objGetModel = GameObject.Find ("ObjectManager").GetComponent<Example2_WWW> ();
	}
	public void Load()
	{
		LoadPanel.SetActive (true);
		LoadPanel.transform.FindChild ("PanelLoad").gameObject.SetActive (true);
		Time.timeScale = 0;

	}

	public void ExitPanel()
	{
		LoadPanel.SetActive (false);
		Time.timeScale = 1;
	}


	public void ChangeDress(int j)
	{
		string name;


		for (int i = 0; i < numberObject; i++)
		{
			if (i == j)
			{
				ADress[i].active = true;
			}
			else
			{
				ADress[i].active = false;
			}
		}
	}
	
	
	public void ChangeButton(int j)
	{
		
		for (int i = 0; i < numberObject; i++)
		{
			if (i == j)
			{
				//Button[i].
				
			}
			else
			{
				//Button[i].image.color = Color.green;
			}
		}
	}

	private int lastIndexDress = -1;
	public void ChoseDress(GameObject obj)
	{
		if (lastIndexDress != -1) {
			LoadListDress.listCheck[lastIndexDress].SetActive(false);
		}
		int currindex = Convert.ToInt32 (obj.name);
		LoadListDress.listCheck[currindex].SetActive(true);
		lastIndexDress = currindex;
		ChangeDress(currindex);
	}


	private GameObject lastModel;
	public void SelectModel(GameObject bt)
	{
		if (lastModel != null) {
			lastModel.transform.FindChild("imgCheck").gameObject.GetComponent<Image>().overrideSprite = (Sprite)Resources.Load<Sprite>("UnCheckicon");
		}
		bt.transform.FindChild("imgCheck").gameObject.GetComponent<Image>().overrideSprite = (Sprite)Resources.Load<Sprite>("Checkicon");
		lastModel = bt;
		LoadPanel.transform.FindChild ("PanelLoad").gameObject.SetActive (false);

		ObjModel objModel = LoadObjModel.listObjModel [Convert.ToInt32 (bt.name)];
		int begin = 10;
		StartCoroutine(objGetModel.Load (objModel.Obj.Substring(begin), objModel.Mtl.Substring(begin), objModel.Png.Substring(begin)));
	}

	public void SaveObj(GameObject obj) {
		EditorObjExporter.ExportEachToSingle (obj);
	}
}
