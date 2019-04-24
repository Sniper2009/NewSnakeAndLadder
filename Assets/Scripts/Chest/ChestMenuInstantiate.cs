using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestMenuInstantiate : MonoBehaviour {


    [SerializeField] GameObject chestMenuPrefab;
    [SerializeField] Transform chestHolder;
	// Use this for initialization
	void Awake () {
        ChestSaver.OnNewChest += CreateChest;
	}
	
	void CreateChest(SaveableChest chest)
    {
        GameObject temp = Instantiate(chestMenuPrefab, chestHolder, false);
        temp.GetComponent<ChestMenuBehaviour>().AssignValues(chest);
    }


    private void OnDestroy()
    {
        ChestSaver.OnNewChest -= CreateChest;
    }
}
