using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    GameObject HPUI;
    GameObject HPUIURA;
    GameObject BulletUI;
    GameObject ReloadUI;
    GameObject BossHPUI;
    public GameObject ExitUI;
    //public Text scrapText;
    public GameObject GunUI;
    public static bool ExitUIOn = true;
    public GunDeta Gundeta;
    // Start is called before the first frame update
    void Start()
    {
        //UIÇéÊìæ
        BulletUI = GameObject.Find("BulletUI");
        HPUI = GameObject.Find("HPUI");
        HPUIURA = GameObject.Find("HPUI(URA)");
        ReloadUI = GameObject.Find("ReloadUI");
        BossHPUI = GameObject.Find("BossHPUI");
        GunUI = GameObject.Find("GunUI");
        BossHPUI.GetComponent<Image>().fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GunUI.GetComponent<RawImage>().texture = Gundeta.GunTexture(GunDeta.Buki[Gun.whs % 4]);
        //scrapText.text = string.Format("Å~{0:0}", PlayerMoob.HaveScrap);
        BulletUI.GetComponent<Image>().fillAmount = Gun.BRe[1] / 32;
        BulletUI.GetComponent<Image>().fillAmount *= Gun.Zandan[0, Gun.whs % 4] / Gun.BRe[1];
        HPUIURA.GetComponent<Image>().fillAmount = PlayerMoob.MaxPHp / 500;
        HPUI.GetComponent<Image>().fillAmount = PlayerMoob.MaxPHp / 500;
        HPUI.GetComponent<Image>().fillAmount *= PlayerMoob.PHp / PlayerMoob.MaxPHp;
        ReloadUI.GetComponent<Image>().fillAmount = Gun.ReloadInterval / (Gun.BRe[2] * 120);
        Gun.ReloadInterval--;
        if (Input.GetKey(KeyCode.Escape) && ExitUIOn)
        {
            Instantiate(ExitUI, new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 0f));
            ExitUIOn = false;
        }
    }
}