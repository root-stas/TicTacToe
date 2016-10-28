using UnityEngine;
using UnityEngine.UI;

public class MainSrc : MonoBehaviour
{

    public GameObject MenuUI;
    public GameObject GameUI;
    public Text EasyTxT;
    public Text HardTxT;
    public Text SideXTxT;
    public Text SideOTxT;
    public Text ScoreTxT;
    public Text[] PlaygroundTxT;

    private bool InGame = false;
    private GameSrc TTEGame;

    // Use this for initialization
    void Start()
    {
        TTEGame = new GameSrc();
        TTEGame.InitGame();
        InitRound();
        MenuUI.SetActive(!InGame);
        GameUI.SetActive(InGame);

        EasyTxT.color = TTEGame.GameType == 1 ? Color.white : Color.green;
        HardTxT.color = TTEGame.GameType == 0 ? Color.white : Color.green;
        SideOTxT.color = TTEGame.PlayerSideX == true ? Color.white : Color.green;
        SideXTxT.color = TTEGame.PlayerSideX == false ? Color.white : Color.green;
    }

    void InitRound()
    {
        for (int _node = 0; _node < PlaygroundTxT.Length; _node++)
        {
            PlaygroundTxT[_node].color = Color.black;
            PlaygroundTxT[_node].text = "";
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (TTEGame.State)
        {
            case GameSrc.GameState.GAME_START:
                TTEGame.NewGame();
                break;
            case GameSrc.GameState.GAME_WIN_CHECK:
                TTEGame.CheckState();
                for (int _node = 0; _node < TTEGame.PlaygroundGrid.Length; _node++)
                    if (TTEGame.PlaygroundGrid[_node] > 0)
                    {
                        PlaygroundTxT[_node].text = TTEGame.PlaygroundGrid[_node] == 1 ? "X" : "O";
                        PlaygroundTxT[_node].color = TTEGame.PlaygroundGrid[_node] == 1 ? Color.green : Color.red;
                    }
                break;
            case GameSrc.GameState.GAME_AI_TURN:
                TTEGame.AITurn();
                break;
            case GameSrc.GameState.GAME_NEW_ROUND:
                InitRound();
                ScoreTxT.text = "Score: " + TTEGame.PlayerScore;
                TTEGame.State = TTEGame.PlayerSideX ? GameSrc.GameState.GAME_PLAYER_TURN : GameSrc.GameState.GAME_AI_TURN;
                break;
        }
    }

    public void DifficultyBtn(int _value)
    {
        TTEGame.GameType = (byte)_value;
        EasyTxT.color = _value == 1 ? Color.white : Color.green;
        HardTxT.color = _value == 0 ? Color.white : Color.green;
    }

    public void SideBtn(bool _value)
    {
        TTEGame.PlayerSideX = _value;
        SideOTxT.color = _value == true ? Color.white : Color.green;
        SideXTxT.color = _value == false ? Color.white : Color.green;
    }

    public void GameStateBtn(int _value)
    {
        if (_value > -1)
        {
            Debug.Log(_value);
            if (TTEGame.State == GameSrc.GameState.GAME_PLAYER_TURN && TTEGame.PlaygroundGrid[_value] == 0)
                TTEGame.PlayerTurn((byte)_value);
        }
        else
        {
            MenuUI.SetActive(InGame);
            GameUI.SetActive(!InGame);
            TTEGame.State = InGame ? GameSrc.GameState.GAME_MENU : GameSrc.GameState.GAME_START;
            InGame = !InGame;
        }
    }
}