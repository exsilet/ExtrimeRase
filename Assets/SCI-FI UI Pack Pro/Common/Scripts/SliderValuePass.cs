using UnityEngine;
using UnityEngine.UI;

public class SliderValuePass : MonoBehaviour {

	Text progress;
	
	private void Start () 
	{
		progress = GetComponent<Text>();
	}
	
	public  void UpdateProgress (float content) 
	{
		progress.text = Mathf.Round( content*100) +"%";
	}
}
