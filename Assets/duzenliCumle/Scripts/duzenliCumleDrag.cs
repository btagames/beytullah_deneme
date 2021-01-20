using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class duzenliCumleDrag : EventTrigger
{
    public int neededindex;
    public GameObject control;
    public Vector3 ba;
    public int ansright;
    public Collider2D col;
    public GameObject canvas,itemParent,konum;
    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
        ba = transform.position;
        this.transform.SetParent(canvas.transform);
        itemParent.GetComponent<GridLayoutGroup>().enabled = false;
    }
    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        shouldLerp = false;
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.gameObject.layer != collision.gameObject.layer)
        {
            Physics2D.IgnoreCollision(this.gameObject.GetComponent<BoxCollider2D>(), collision);
        }
        if (collision.name == this.name)
        {
            col = collision;
            ansright = 1;
        }
        else
            ansright = -1;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (this.gameObject.layer != collision.gameObject.layer)
        {
            Physics2D.IgnoreCollision(this.gameObject.GetComponent<BoxCollider2D>(), collision);
        }
        if (collision.name == this.name)
        {
            col = collision;
            ansright = 1;
        }
        else
            ansright = -1;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        ansright = 0;
    }
    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        if (ansright == 1)
        {
           // konum.GetComponent<kelimeKonumController>()._konumList.Add(ba);
          //  konum.GetComponent<kelimeKonumController>()._indexList.Add(this.transform.GetSiblingIndex());
           // col.gameObject.GetComponent<kelimeKonumController>().cevapVer();
            this.gameObject.transform.position = col.transform.position;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            this.GetComponent<EventTrigger>().enabled = false;
            col.gameObject.SetActive(false);

        }
        else if (ansright == 0)
        {
          //  itemParent.GetComponent<GridLayoutGroup>().enabled = true;
            this.transform.SetParent(itemParent.transform);
            transform.position = ba;
        }
        else if (ansright == -1)
        {
           // itemParent.GetComponent<GridLayoutGroup>().enabled = true;
            this.transform.SetParent(itemParent.transform);
            transform.position = ba;
        }
    }
    public void cevapYanlis()
    {
        //Debug.Log("CALİSTİ");
  
        this.transform.SetParent(itemParent.transform);
        transform.position = ba;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
    void Start()
    {
        konum = GameObject.Find("konum");
        canvas = GameObject.Find("Canvas");
        itemParent = GameObject.Find("ItemParent");
        control = GameObject.Find("controller");
        Input.multiTouchEnabled = false;
    }


    private bool shouldLerp = false;
    public float timeStartedLerping;
    public float lerpTime = 0.5f;
    public Vector2 bi;
    public void startLerping()
    {
        timeStartedLerping = Time.time;
        shouldLerp = true;

        this.GetComponent<BoxCollider2D>().enabled = true;
        this.GetComponent<EventTrigger>().enabled = true;

    }
    public Vector3 Lerp(Vector3 start, Vector3 end, float timeStartedLerping, float lerpTime = 0.5f)
    {
        float timeSinceStarted = Time.time - timeStartedLerping;
        float percentageComplete = timeSinceStarted / lerpTime;
        var result = Vector3.Lerp(start, end, percentageComplete);
        return result;
    }
    // Update is called once per frame
    void Update()
    {
        if (shouldLerp)
        {
            transform.position = Lerp(bi, ba, timeStartedLerping, lerpTime);
        }
    }
}
