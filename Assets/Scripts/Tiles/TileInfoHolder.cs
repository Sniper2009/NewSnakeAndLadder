using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class TileInfoHolder : MonoBehaviour {
    [SerializeField] Text tileTextNum;

     public TileInfoHolder nextTile;
    public Vector3 thisTilePos;
    public Vector2 thisTileMinAnchor;
    public Vector2 thisTileMaxAnchor;

    public bool isSnakeHead;
    public bool isLadderHead;
    public TileInfoHolder snakeTail;
    public TileInfoHolder ladderEnd;
   public int tileNum;

    public bool hasCoin;
    public bool hasSafe;

    public int coinAmount;
    public int safeType;
    public int safeID;

    GameObject goodsChild;

    private void Awake()
    {
        string thisName = name.Split('e')[1];
       // nextTile = transform.parent.GetChild(transform.GetSiblingIndex() + 1).GetComponent<TileInfoHolder>();
        tileNum = System.Convert.ToInt32(thisName);
        tileTextNum.text = tileNum.ToString();
        string nextName = "Tile" + (tileNum + 1).ToString();
        if(GameObject.Find(nextName)==null)
        {
            Debug.Log("null: " + name + "    " + nextName);

        }
        else
         nextTile = GameObject.Find(nextName).GetComponent<TileInfoHolder>();
        //tileNum = transform.GetSiblingIndex() ;//start at 1 
        thisTilePos = GetComponent<RectTransform>().anchoredPosition;// + new Vector2(80, 0);
        thisTileMinAnchor = GetComponent<RectTransform>().anchorMin;
        thisTileMaxAnchor = GetComponent<RectTransform>().anchorMax;

        if(hasSafe==true)
        {
            ChestData data = transform.GetChild(0).GetComponent<SafePrefabData>().chestData;
           // safeType = data.safeType;
            safeID = data.chestID;
        }

        if(hasCoin==true)
        {
            coinAmount = transform.GetChild(0).GetComponent<PrefabCoinHolder>().CoinData.coinAmount;
        }

        SafePickup.OnSafePickupDone += OnSafePickedUp;
        SafePickup.OnSafeReplaced += UpdateDataForSafeReplace;

        CoinCollection.OnPickedUpCoin += DeleteCoin;
        SafePickup.OnRestoreSafe += OnSafeRestore;

    }

    void UpdateDataForSafeReplace(int tileID, bool isReplaced)
    {
        if (isReplaced == false)
            return;
        if(tileID==tileNum)
        {
            hasSafe = true;
            ChestData data = transform.GetChild(0).GetComponent<SafePrefabData>().chestData;
           // safeType = data.safeType;
            safeID = data.chestID;

        }
    }

    void DeleteCoin(int tileID)
    {
        if (tileID==tileNum)
        {
            hasCoin = false;
            Destroy(transform.GetChild(0).gameObject);
        }
    }


   

    public void OnSafePickedUp(int tileID)
    {
        if (tileID == tileNum)
        {
            goodsChild = transform.GetChild(0).gameObject;
            goodsChild.SetActive(false);
            hasSafe = false;
        }
    }


    public void OnSafeRestore(int ID,Transform newSafe)
    {
        //Debug.Log("typeID:  " + type + "  " + ID);
        if(safeID==ID&&goodsChild!=null)
        {
            goodsChild.transform.SetParent(newSafe, false);
            goodsChild.SetActive(true);
        }

    }

    private void OnDestroy()
    {
        SafePickup.OnSafePickupDone -= OnSafePickedUp;
        CoinCollection.OnPickedUpCoin -= DeleteCoin;
        SafePickup.OnRestoreSafe -= OnSafeRestore;
        SafePickup.OnSafeReplaced -= UpdateDataForSafeReplace;
    }
}
