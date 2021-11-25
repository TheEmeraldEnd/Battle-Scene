using UnityEngine;
using UnityEngine.UI;


[ExecuteInEditMode]
public class Bar : MonoBehaviour {

    public GameObject background, foreground;
    public GameObject topBar,centerBar,bottomBar;
    public GameObject bar,fullBar;

    float d;
    Vector2 emptyTopPos;
    public Color[] color = new Color[4];
    public float persent;

    void Start () {
        centerBar.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, d);
        emptyTopPos = centerBar.transform.localPosition;
        if(color.Length > 0 && color.Length < 4)
        SetColor(color[0]);
    }
	
	// Update is called once per frame
	void Update () {
        if (color.Length != 4)
            Debug.LogError("dont change Color array size, equale 4 !!!");
        d = (109f / 100f)* persent ;

        centerBar.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, d);
        topBar.transform.localPosition = emptyTopPos + new Vector2(0,d) ;
        if (persent <= 0)
        {
            persent = 0;
            bar.SetActive(false);
            fullBar.SetActive(false);
            if (color.Length > 0 && color.Length < 5)
                SetColor(color[3]);
        }
        else if (persent > 0 && persent < 100)
        {
            bar.SetActive(true);
            fullBar.SetActive(false);
            if (d < 25   && color.Length > 0 && color.Length < 5) {
                SetColor(color[3]);
            }
            else if(persent > 25 && persent < 50 && color.Length > 0 && color.Length < 5)
            {
                SetColor(color[2]);
            }
            else if (persent > 50 && persent < 75 && color.Length > 0 && color.Length < 5)
            {
                SetColor(color[1]);
            }
            else if (persent > 75 && color.Length >0 && color.Length < 5)
            {
                SetColor(color[0]);
            }
        }
        else if (persent >= 100)
        {
            bar.SetActive(false);
            fullBar.SetActive(true);
            persent = 100;
            if ( color.Length > 0 && color.Length < 5)
            SetColor(color[0]);
        }
       
    }
    public void SetColor(Color color) {
        topBar.GetComponent<Image>().color = color;
        centerBar.GetComponent<Image>().color = color;
        bottomBar.GetComponent<Image>().color = color;
        fullBar.GetComponent<Image>().color = color;
    }
}
