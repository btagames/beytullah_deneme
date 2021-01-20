using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class duzenliCumleController : MonoBehaviour
{


    public GameObject itemParent, Item, bosItem;
    public GameObject ItemKonum;
    public List<GameObject> _bosObjeList = new List<GameObject>();
    public List<soruClass> _soruList = new List<soruClass>();
    [System.Serializable]
    public class soruClass
    {
        public List<string> _kelimeler = new List<string>();
        public List<string> _sesGecenKelime = new List<string>();
        public int kelimeSayisi;
        public AudioClip cumleSesi;
        public string dogruCevap;

    }
    public List<string> _secilenKelimelerList = new List<string>();
    public int rndSoru;

    private int soruSayisi;
    public int dogruSay;
    public string kodID;

    void Start()
    {
        kelimeAdd();
    }

    void Update()
    {

    }

    public void kelimeAdd()
    {
        itemParent.GetComponent<GridLayoutGroup>().enabled = true;
        ItemKonum.GetComponent<GridLayoutGroup>().enabled = true;
        for (int i = 0; i < _soruList.Count; i++)
        {
            GameObject obj = Instantiate(Item, Item.transform.position, Quaternion.identity);
            obj.transform.SetParent(itemParent.transform);
            obj.transform.localScale = new Vector3(1, 1, 1);
            int randomKelime = Random.Range(0, _soruList[i]._sesGecenKelime.Count);
            int secilenKelime = int.Parse(_soruList[i]._sesGecenKelime[randomKelime]);
            obj.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = _soruList[i]._kelimeler[secilenKelime];
            obj.name = _soruList[i]._kelimeler[secilenKelime];
            _secilenKelimelerList.Add(obj.name);
            
        }

        StartCoroutine(soruAdd());
    }



    IEnumerator soruAdd()
    {
        yield return new WaitForSeconds(.5f);
        itemParent.GetComponent<GridLayoutGroup>().enabled = false;
        rndSoru = Random.Range(0, _soruList.Count);

        for (int i = 0; i < _soruList[rndSoru]._kelimeler.Count; i++)
        {
            string kelimeAra = _secilenKelimelerList.Where(x => x.Equals(_soruList[rndSoru]._kelimeler[i])).FirstOrDefault();
            Debug.Log(kelimeAra);

            if(kelimeAra == null)
            {

                GameObject obj = Instantiate(Item, Item.transform.position, Quaternion.identity);
                obj.transform.SetParent(ItemKonum.transform);
                obj.transform.localScale = new Vector3(1, 1, 1);
                obj.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = _soruList[rndSoru]._kelimeler[i];
                obj.name = _soruList[rndSoru]._kelimeler[i];
                obj.GetComponent<EventTrigger>().enabled = false;



            }
            else
            {
                GameObject obj = Instantiate(bosItem, bosItem.transform.position, Quaternion.identity);
                obj.transform.SetParent(ItemKonum.transform);
                obj.transform.localScale = new Vector3(1, 1, 1);
                obj.name = _soruList[rndSoru]._kelimeler[i];
            }
        }

        yield return new WaitForSeconds(.1f);
        ItemKonum.GetComponent<GridLayoutGroup>().enabled = false;
    }


    public void cevapControl()
    {
        int say = 0;
        for (int i = 0; i < ItemKonum.transform.childCount; i++)
        {
            if(ItemKonum.transform.GetChild(i).gameObject.name == _soruList[rndSoru]._kelimeler[i])
            {
                say++;
            }
        }


        if(say == _soruList[rndSoru]._kelimeler.Count)
        {

        }
    }
}
  


