using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject karePrefabs;
    [SerializeField] Transform karelerPaneli;
    [SerializeField] Transform soruPaneli;
    [SerializeField] Text soruText;
    [SerializeField] Sprite[] kareSprite;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] AudioSource audioSource;


    public AudioClip rightClick;
    public AudioClip wrongClick;
    public AudioClip backgroundMusic;


    private GameObject[] karelerDizisi = new GameObject[25];
    private GameObject gecerliKare;
    List<int> bolumDegerleriListesi = new List<int>();

    private int bolunenSayi, bolenSayi;
    int kacinciSoru;
    int dogruSonuc;
    int butonDegeri;
    int kalanHak;
    string sorununZorlukDerecesi;

    bool butonaBasilsinMi;

    KalanHakManager kalanhaklarManager;
    ScoreManager scoreManager;
    private void Awake()
    {
        
        kalanHak = 3;

        audioSource = GetComponent<AudioSource>();

        gameOverPanel.GetComponent<RectTransform>().localScale = Vector3.zero;

        kalanhaklarManager = Object.FindObjectOfType<KalanHakManager>();
        scoreManager = Object.FindObjectOfType<ScoreManager>();

        kalanhaklarManager.KalanHakKontrolEt(kalanHak);
    }
    void Start()
    {
        butonaBasilsinMi = false;
        soruPaneli.GetComponent<RectTransform>().localScale = Vector3.zero;
        KareleriOlustur();
    }

    
  public void KareleriOlustur()
    {
        for (int i = 0; i < 25; i++)
        {
            GameObject kare = Instantiate(karePrefabs,karelerPaneli);
            kare.transform.GetChild(1).GetComponent<Image>().sprite = kareSprite[Random.Range(0, kareSprite.Length)]; 
            kare.transform.GetComponent<Button>().onClick.AddListener(() => ButonaBasildi());
            karelerDizisi[i] = kare;
        }
        BolumDegerleriniTexteYazdir();

        StartCoroutine(DoFadeRoutine());

        Invoke("SoruPaneliAc", 2.2f);
    }
    public void ButonaBasildi()
    {
        if (butonaBasilsinMi)
        {
            
            butonDegeri = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Text>().text);

            gecerliKare = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

            SonucuKontrolEt();
        }       
    }

    void SonucuKontrolEt()
    {
        if (butonDegeri == dogruSonuc)
        {
            audioSource.PlayOneShot(rightClick);
            gecerliKare.transform.GetChild(1).GetComponent<Image>().enabled = true;
            gecerliKare.transform.GetChild(0).GetComponent<Text>().text = "";
            gecerliKare.transform.GetComponent<Button>().interactable = false;
            scoreManager.ScoreArttir(sorununZorlukDerecesi);

            bolumDegerleriListesi.RemoveAt(kacinciSoru);

            if (bolumDegerleriListesi.Count > 0)
            {
                SoruPaneliAc();
            }
            else
            {
                GameOver();
            }    
            
        }
        else
        {
            audioSource.PlayOneShot(wrongClick);
            kalanHak--;
            kalanhaklarManager.KalanHakKontrolEt(kalanHak);
        }
        if (kalanHak <= 0)
        {
            GameOver();
        }

    }

    void GameOver()
    {
        butonaBasilsinMi = false;
        gameOverPanel.GetComponent<RectTransform>().DOScale(1, 0.3f).SetEase(Ease.OutBack);
    }

    IEnumerator DoFadeRoutine()
    {
        foreach (var kare in karelerDizisi)
        {
            kare.GetComponent<CanvasGroup>().DOFade(1, 0.2f);
            yield return new WaitForSeconds(0.07f);
        }
    }

    void BolumDegerleriniTexteYazdir()
    {
        foreach(var kare in karelerDizisi)
        {
            int rastgeleDeger = Random.Range(1, 13);
            bolumDegerleriListesi.Add(rastgeleDeger);

            kare.transform.GetChild(0).GetComponent<Text>().text = rastgeleDeger.ToString();

        }
    }
    void SoruPaneliAc()
    {
        SoruyuSor();
        butonaBasilsinMi = true;
        soruPaneli.GetComponent<RectTransform>().DOScale(1, 0.3f).SetEase(Ease.OutBack);
    }
    void SoruyuSor()
    {
        bolenSayi = Random.Range(2, 11);

        kacinciSoru = Random.Range(0, bolumDegerleriListesi.Count);

        dogruSonuc = bolumDegerleriListesi[kacinciSoru];

        bolunenSayi = bolenSayi * dogruSonuc;

        if (bolunenSayi <= 40)
        {
            sorununZorlukDerecesi = "kolay";
        }
        else if(bolunenSayi>40 && bolunenSayi <= 80)
        {
            sorununZorlukDerecesi = "orta";
        }
        else
        {
            sorununZorlukDerecesi = "zor";
        }


        soruText.text = bolunenSayi.ToString() + " : " + bolenSayi.ToString();
    }
}
