using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WordPool : MonoBehaviour {

	public string File = "small";

	List<string> words;

	void Awake()
	{
		TextAsset textAsset = Resources.Load<TextAsset>("WordBanks/" + File);
		words = new List<string>(textAsset.text.Split(','));
	}

	public string GetWord()
	{
		int index = (int)Random.Range(0, words.Count - 1);
		string word = words[index];
		words.RemoveAt(index);
		return word;
	}

	public void ReturnWord(string word)
	{
		words.Add(word);
	}
}
