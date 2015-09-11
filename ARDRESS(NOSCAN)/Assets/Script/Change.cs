using UnityEngine;
using System.Collections;
using System;

public class Change : MonoBehaviour {

	public GameObject Loadbutton, LoadPanel;
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
	
	public void ChoseButton(int i)
	{
		ChangeDress(i);
		//ChangeButton(i);
	}



	public void SelectModel(GameObject bt)
	{
		ExitPanel ();
		ObjModel objModel = LoadObjModel.listObjModel [Convert.ToInt32 (bt.name)];
		int begin = 10;
		StartCoroutine(objGetModel.Load (objModel.Obj.Substring(begin), objModel.Mtl.Substring(begin), objModel.Png.Substring(begin)));
		//StartCoroutine (objGetModel.Load ("", "", ""));
	}
	/*
    int j = 0;
    public void changeObjLeft()
    {
        Adress[j].active = false;
        j = (int)Mathf.Round((j - 1) % 3);
        Debug.Log(j);
        Adress[j].active = true;
    }
    public void changeObjRight()
    {
        Adress[j].active = false;
        j = (int)Mathf.Round((j + 1) % 3);
        Debug.Log(j);
        Adress[j].active = true;
    }

    static void DisableActive(GameObject[] Dress)
    {
        for (int i = 0; i < numberObject; i++)
        {
            Dress[i].active = false;
        }
    }
     * */

}
