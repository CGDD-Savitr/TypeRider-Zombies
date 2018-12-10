using System.Collections;
using System.Collections.Generic;
using TypeRider.Assets.Classes;
using TypeRider.Assets.Interfaces;
using UnityEngine;
using UnityEngine.UI;

public class ChangeDifficulty : MonoBehaviour {

	public Text text;

	int selected = 1;

	IDifficulty[] difficulties = new IDifficulty[]
	{
		new EasyDifficulty(),
		new NormalDifficulty(),
		new HardDifficulty()
	};

	void Start () 
	{
		select(selected);
	}
	
	void select(int index)
	{
		CrossSceneRegistry.Difficulty = difficulties[index];
		text.text = CrossSceneRegistry.Difficulty.Name;
	}
	public void NextDifficulty()
	{
		select(++selected % difficulties.Length);
	}
}
