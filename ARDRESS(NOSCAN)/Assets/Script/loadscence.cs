using UnityEngine;
using System.Collections;

public class loadscence : MonoBehaviour {

	public void LoadScence(int index)
	{
		Application.LoadLevel(index);
	}
}
