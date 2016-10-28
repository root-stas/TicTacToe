//Класс игры крестики-нолики:
public class GameSrc
{
    /// <summary>
    /// Игровые состояния
    /// </summary>
    public enum GameState
    {
        GAME_MENU = 0,
        GAME_START = 1,
        GAME_PLAYER_TURN = 2,
        GAME_AI_TURN = 3,
        GAME_WIN_CHECK = 4,
        GAME_NEW_ROUND = 5,
    }

    public GameState State = GameState.GAME_MENU;
    public byte[] PlaygroundGrid; //0 - empty, 1 - X, 2 - O
    public byte GameType = 0;
    public bool PlayerSideX = true;
    public int PlayerScore = 0;
    public byte[][] WinLink;
    public byte[] WinStateAI;

    private byte[] WinStatePlayer;
    private GameState PrevState;
    private IGameAI GameAI;
    /// <summary>
    /// Инициализация игры крестики-нолики
    /// </summary>
    public void InitGame()
    {
        WinStateAI = new byte[8]; // вхождений в каждую из победных комбинаций для ИИ
        WinStatePlayer = new byte[8]; // вхождений в каждую из победных комбинаций для игрока
        //Массив хранящий номера победных комбинаций для каждой клетки игрового поля:
        WinLink = new byte[9][]; //[PlaygroundGrid ID][Wins] = WinState ID
        WinLink[0] = new byte[3] { 0, 1, 2 };
        WinLink[1] = new byte[2] { 0, 3 };
        WinLink[2] = new byte[3] { 0, 4, 5 };

        WinLink[3] = new byte[2] { 1, 6 };
        WinLink[4] = new byte[4] { 2, 3, 5, 6 };
        WinLink[5] = new byte[2] { 4, 6 };

        WinLink[6] = new byte[3] { 1, 5, 7 };
        WinLink[7] = new byte[2] { 3, 7 };
        WinLink[8] = new byte[3] { 2, 4, 7 };

        PlaygroundGrid = new byte[9];
    }
    /// <summary>
    /// Запуск новой игры
    /// </summary>
    public void NewGame()
    {
        PlayerScore = 0;
        if (GameAI != null)
            GameAI = null;

        switch (GameType)
        {
            case 0:
                GameAI = new EasyAI();
                break;
            case 1:
                GameAI = new HardAI();
                break;
        }
        GameAI.InitAI(!PlayerSideX, this);
        InitPlayground();
    }
    /// <summary>
    /// Перезапуск игрового поля
    /// </summary>
    public void InitPlayground()
    {
        for (int _node = 0; _node < PlaygroundGrid.Length; _node++)
            PlaygroundGrid[_node] = 0;

        for (int _wins = 0; _wins < WinStateAI.Length; _wins++)
        {
            WinStateAI[_wins] = 0;
            WinStatePlayer[_wins] = 0;
        }

        State = GameState.GAME_NEW_ROUND;
    }
    /// <summary>
    /// Ход противника
    /// </summary>
    public void AITurn()
    {
        GameAI.Action();
        if (State != GameState.GAME_NEW_ROUND)
        {
            PrevState = GameState.GAME_AI_TURN;
            State = GameState.GAME_WIN_CHECK;
        }
        else NewGame();
    }
    /// <summary>
    /// Ход игрока
    /// </summary>
    /// <param name="_value">номер ячейки</param>
    public void PlayerTurn(byte _value)
    {
        PlaygroundGrid[_value] = PlayerSideX ? (byte)1 : (byte)2;
        for (int _wins = 0; _wins < WinLink[_value].Length; _wins++)
            WinStatePlayer[WinLink[_value][_wins]]++;
        PrevState = GameState.GAME_PLAYER_TURN;
        State = GameState.GAME_WIN_CHECK;
    }
    /// <summary>
    /// Проверка на победу и передача хода
    /// </summary>
    public void CheckState()
    {
        for (int _wins = 0; _wins < WinStateAI.Length; _wins++)
        {
            if (WinStateAI[_wins] >= 3)
            {
                PlayerScore = 0;
                NewGame();
                return;
            }
            else
            if (WinStatePlayer[_wins] >= 3)
            {
                PlayerScore++;
                InitPlayground();
                return;
            }
        }
        State = (PrevState == GameState.GAME_PLAYER_TURN ? GameState.GAME_AI_TURN : GameState.GAME_PLAYER_TURN);
    }
}