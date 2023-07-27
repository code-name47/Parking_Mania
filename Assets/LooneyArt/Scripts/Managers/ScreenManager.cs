using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
namespace LooneyDog
{
    public class ScreenManager : MonoBehaviour
    {
        public HomeScreen Home { get { return _home; } set { _home = value; } }
        public GameScreen GameScreen { get { return _gameScreen; } set { _gameScreen = value; } }
        //public ReviveScreen Revive { get { return _revive; } set { _revive = value; } }
        public GameOverScreen GameOver { get { return _gameOver; } set { _gameOver = value; } }
        public SplashScreen Splsh { get { return _splash; } set { _splash = value; } }
        /*       public ResultScreen Result { get { return _result; } set { _result = value; } }
               public SplashScreen Splash { get { return _splash; } set { _splash = value; } }
               public ReviveScreen Revive { get { return _revive; } set { _revive = value; } }
               public SettingScreen Setting { get { return _setting; } set { _setting = value; } }

               public LoadingScreen Load { get { return _load; } set { _load = value; } }
               public TopMenu Top { get { return _topMenu; } set { _topMenu = value; } }*/

        [SerializeField] private GameOverScreen _gameOver;
        //[SerializeField] private ReviveScreen _revive;
        [SerializeField] private HomeScreen _home;
        [SerializeField] private GameScreen _gameScreen;
        [SerializeField] private SplashScreen _splash;

        [SerializeField] private float _fadeScreenDuration;
        /* [SerializeField] private ResultScreen _result;
         [SerializeField] private SplashScreen _splash;
         [SerializeField] private ReviveScreen _revive;
         [SerializeField] private SettingScreen _setting;

         [SerializeField] private LoadingScreen _load;
         [SerializeField] private TopMenu _topMenu;
 */
        private void Awake()
        {

            /* if (_gameScreen == null)
             {
                 _gameScreen = FindObjectOfType<GameScreen>();

             }*/
        }

        public void LoadScreen(ScreenId Id)
        {
            switch (Id)
            {
                /*  case ScreenId.SplashScreen:
                      _splash.gameObject.SetActive(true);
                      break;*/
                case ScreenId.HomeScreen:
                    _home.gameObject.SetActive(true);
                    break;
                case ScreenId.GameScreen:
                    _gameScreen.gameObject.SetActive(true);
                    break;
                    /*case ScreenId.ResultScreen:
                        _result.gameObject.SetActive(true);
                        break;
                    case ScreenId.LoadingScreen:
                        _load.gameObject.SetActive(true);
                        break;*/
            }
        }

        public void DisableScreen(ScreenId id)
        {
            switch (id)
            {
                /* case ScreenId.SplashScreen:
                     _splash.gameObject.SetActive(false);
                     break;*/
                case ScreenId.HomeScreen:
                    _home.gameObject.SetActive(false);
                    break;
                case ScreenId.GameScreen:
                    _gameScreen.gameObject.SetActive(false);
                    break;
                    /*case ScreenId.ResultScreen:
                        _result.gameObject.SetActive(false);
                        break;
                    case ScreenId.LoadingScreen:
                        _load.gameObject.SetActive(false);
                        break;*/
            }
        }


        public void LoadFadeScreen(GameObject FromScreen, GameObject ToScreen)
        {
            Image[] FromScreenImages = FromScreen.GetComponentsInChildren<Image>();
            TextMeshProUGUI[] FromScreenText = FromScreen.GetComponentsInChildren<TextMeshProUGUI>();
            float[] FromImageAlphaRef = new float[FromScreenImages.Length];
            float[] FromTextAplhaRef = new float[FromScreenText.Length];

            Image[] ToScreenImages = ToScreen.GetComponentsInChildren<Image>();
            TextMeshProUGUI[] ToScreenText = ToScreen.GetComponentsInChildren<TextMeshProUGUI>();
            float[] ImageAplhaRef = new float[ToScreenImages.Length];
            float[] TextAplhaRef = new float[ToScreenText.Length];

            //-----------  FromScreen

            for (int i = 0; i < FromScreenImages.Length; i++)
            {
                FromImageAlphaRef[i] = FromScreenImages[i].color.a;
            }

            for (int i = 0; i < FromScreenText.Length; i++)
            {
                FromTextAplhaRef[i] = FromScreenText[i].color.a;
            }

            //------------ ToScreen
            for (int i = 0; i < ToScreenImages.Length; i++)
            {
                ImageAplhaRef[i] = ToScreenImages[i].color.a;
            }

            for (int i = 0; i < ToScreenText.Length; i++)
            {
                TextAplhaRef[i] = ToScreenText[i].color.a;
            }

            fadeScreen(FromScreenImages, FromScreenText);
            setAlpha(ToScreenImages, ToScreenText, 0);
            ToScreen.SetActive(true);
            UnfadeScreen(ToScreenImages, ToScreenText, ImageAplhaRef, TextAplhaRef);
            //StartCoroutine(DisableScreenAfter(2, FromScreen));
            StartCoroutine(DisableScreenAfter(_fadeScreenDuration, FromScreen, FromScreenImages, FromScreenText, FromImageAlphaRef, FromTextAplhaRef));
        }

        private void fadeScreen(Image[] images, TextMeshProUGUI[] texts)
        {
            for (int i = 0; i < images.Length; i++)
            {
                images[i].DOFade(0, 1f).SetUpdate(true);
            }
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i].DOFade(0, 1f).SetUpdate(true);
            }
        }

        private void setAlpha(Image[] images, TextMeshProUGUI[] texts, float alphaValue)
        {
            for (int i = 0; i < images.Length; i++)
            {
                images[i].color =
                    new Color((byte)images[i].color.r
                    , (byte)images[i].color.g
                    , (byte)images[i].color.b
                    , alphaValue);
            }
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i].color =
                    new Color((byte)texts[i].color.r
                    , (byte)texts[i].color.g
                    , (byte)texts[i].color.b
                    , alphaValue);
            }
        }

        private void setAlpha(Image[] images, TextMeshProUGUI[] texts, float[] alphaValueImages, float[] alphaValueText)
        {
            for (int i = 0; i < images.Length; i++)
            {
                images[i].color =
                    new Color((byte)images[i].color.r
                    , (byte)images[i].color.g
                    , (byte)images[i].color.b
                    , alphaValueImages[i]);
            }
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i].color =
                    new Color((byte)texts[i].color.r
                    , (byte)texts[i].color.g
                    , (byte)texts[i].color.b
                    , alphaValueText[i]);
            }


        }

        private void UnfadeScreen(Image[] images, TextMeshProUGUI[] texts, float[] imageAplhaRef, float[] textAplhaRef)
        {
            for (int i = 0; i < images.Length; i++)
            {
                images[i].DOFade(imageAplhaRef[i], 2f).SetUpdate(true);
            }
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i].DOFade(textAplhaRef[i], 2f).SetUpdate(true);
            }
        }

        private IEnumerator DisableScreenAfter(float seconds, GameObject Screen)
        {
            yield return new WaitForSecondsRealtime(seconds);
            Screen.SetActive(false);
            setAlpha(Screen.GetComponentsInChildren<Image>(), Screen.GetComponentsInChildren<TextMeshProUGUI>(), 255);
            //setAlpha()
        }

        private IEnumerator DisableScreenAfter(float seconds, GameObject Screen, Image[] images, TextMeshProUGUI[] texts, float[] alphaValueImages, float[] alphaValueText)
        {
            yield return new WaitForSecondsRealtime(seconds);
            Screen.SetActive(false);
            setAlpha(images, texts, alphaValueImages, alphaValueText);
            //setAlpha()
        }

        public void OpenPopUpScreen(Transform PopUpScreen, ScreenLocation startlocation, float Duration)
        {
            switch (startlocation)
            {
                case ScreenLocation.left:
                    PopUpScreen.transform.position = new Vector3(PopUpScreen.transform.position.x - Screen.currentResolution.width,
                        PopUpScreen.transform.position.y, PopUpScreen.transform.position.z);
                    PopUpScreen.DOMoveX(PopUpScreen.transform.position.x + Screen.currentResolution.width, Duration).OnStart(() =>
                    {
                        PopUpScreen.gameObject.SetActive(true);
                    }).SetEase(Ease.OutBounce).SetUpdate(true);
                    break;
                case ScreenLocation.right:
                    PopUpScreen.transform.position = new Vector3(PopUpScreen.transform.position.x + Screen.currentResolution.width,
                        PopUpScreen.transform.position.y, PopUpScreen.transform.position.z);
                    PopUpScreen.DOMoveX(PopUpScreen.transform.position.x - Screen.currentResolution.width, Duration).OnStart(() =>
                    {
                        PopUpScreen.gameObject.SetActive(true);
                    }).SetEase(Ease.OutBounce).SetUpdate(true);
                    break;
                case ScreenLocation.up:
                    PopUpScreen.transform.position = new Vector3(PopUpScreen.transform.position.x,
                      PopUpScreen.transform.position.y + Screen.currentResolution.height, PopUpScreen.transform.position.z);
                    PopUpScreen.DOMoveY(PopUpScreen.transform.position.y - Screen.currentResolution.height, Duration).OnStart(() =>
                    {
                        PopUpScreen.gameObject.SetActive(true);
                    }).SetEase(Ease.OutBounce).SetUpdate(true);
                    break;
                case ScreenLocation.down:
                    PopUpScreen.transform.position = new Vector3(PopUpScreen.transform.position.x,
                     PopUpScreen.transform.position.y - Screen.currentResolution.height, PopUpScreen.transform.position.z);
                    PopUpScreen.DOMoveY(PopUpScreen.transform.position.y + Screen.currentResolution.height, Duration).OnStart(() =>
                    {
                        PopUpScreen.gameObject.SetActive(true);
                    }).SetEase(Ease.OutBounce).SetUpdate(true);
                    break;
            }


        }

        public void ClosePopUpScreen(Transform PopUpScreen, ScreenLocation startlocation, float Duration)
        {
            Vector3 initialposition = PopUpScreen.position;
            switch (startlocation)
            {
                case ScreenLocation.left:
                    //PopUpScreen.transform.position = Vector3.zero;
                    PopUpScreen.DOMoveX(PopUpScreen.transform.position.x - Screen.currentResolution.width, Duration).OnComplete(() =>
                    {
                        PopUpScreen.gameObject.SetActive(false);
                        PopUpScreen.transform.position = initialposition;
                    }).SetEase(Ease.OutBounce).SetUpdate(true);
                    break;
                case ScreenLocation.right:
                    //PopUpScreen.transform.position = Vector3.zero;
                    PopUpScreen.DOMoveX(PopUpScreen.transform.position.x + Screen.currentResolution.width, Duration).OnComplete(() =>
                    {
                        PopUpScreen.gameObject.SetActive(false);
                        PopUpScreen.transform.position = initialposition;
                    }).SetEase(Ease.OutBounce).SetUpdate(true);
                    break;
                case ScreenLocation.up:
                    //PopUpScreen.transform.position = Vector3.zero;
                    PopUpScreen.DOMoveY(PopUpScreen.transform.position.y + Screen.currentResolution.height, Duration).OnComplete(() =>
                    {
                        PopUpScreen.gameObject.SetActive(false);
                        PopUpScreen.transform.position = initialposition;
                    }).SetEase(Ease.OutBounce).SetUpdate(true);
                    break;
                case ScreenLocation.down:
                    //PopUpScreen.transform.position = Vector3.zero;
                    PopUpScreen.DOMoveY(PopUpScreen.transform.position.y - Screen.currentResolution.height, Duration).OnComplete(() =>
                    {
                        PopUpScreen.gameObject.SetActive(false);
                        PopUpScreen.transform.position = initialposition;
                    }).SetEase(Ease.OutBounce).SetUpdate(true);
                    break;
            }
        }

        public void ClosePopUpScreen(Transform PopUpScreen, ScreenLocation startlocation, float Duration, Button DisableButton)
        {
            Vector3 initialposition = PopUpScreen.position;
            DisableButton.interactable = false;
            switch (startlocation)
            {
                case ScreenLocation.left:
                    //PopUpScreen.transform.position = Vector3.zero;
                    PopUpScreen.DOMoveX(PopUpScreen.transform.position.x - Screen.currentResolution.width, Duration).OnComplete(() =>
                    {
                        PopUpScreen.gameObject.SetActive(false);
                        PopUpScreen.transform.position = initialposition;
                        DisableButton.interactable = true;
                    }).SetEase(Ease.OutBounce).SetUpdate(true);
                    break;
                case ScreenLocation.right:
                    //PopUpScreen.transform.position = Vector3.zero;
                    PopUpScreen.DOMoveX(PopUpScreen.transform.position.x + Screen.currentResolution.width, Duration).OnComplete(() =>
                    {
                        PopUpScreen.gameObject.SetActive(false);
                        PopUpScreen.transform.position = initialposition;
                        DisableButton.interactable = true;
                    }).SetEase(Ease.OutBounce).SetUpdate(true);
                    break;
                case ScreenLocation.up:
                    //PopUpScreen.transform.position = Vector3.zero;
                    PopUpScreen.DOMoveY(PopUpScreen.transform.position.y + Screen.currentResolution.height, Duration).OnComplete(() =>
                    {
                        PopUpScreen.gameObject.SetActive(false);
                        PopUpScreen.transform.position = initialposition;
                        DisableButton.interactable = true;
                    }).SetEase(Ease.OutBounce).SetUpdate(true);
                    break;
                case ScreenLocation.down:
                    //PopUpScreen.transform.position = Vector3.zero;
                    PopUpScreen.DOMoveY(PopUpScreen.transform.position.y - Screen.currentResolution.height, Duration).OnComplete(() =>
                    {
                        PopUpScreen.gameObject.SetActive(false);
                        PopUpScreen.transform.position = initialposition;
                        DisableButton.interactable = true;
                    }).SetEase(Ease.OutBounce).SetUpdate(true);
                    break;
            }
        }
    }

    public enum ScreenLocation
    {
        left = 1,
        right = 2,
        up = 3,
        down = 4
    }
    public enum ScreenId
    {
        SplashScreen = 1,
        HomeScreen = 2,
        GameScreen = 3,
        ResultScreen = 4,
        LoadingScreen = 5
    }
}