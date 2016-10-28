using System;

public class EasyAI : IGameAI
{
    public bool SideX { get; set; }
    public GameSrc GameObj { get; set; }
    private Random Rand = new Random();

    public void InitAI(bool _side, GameSrc _game)
    {
        SideX = _side;
        GameObj = _game;
    }

    public void Action()
    {
        byte _empty = 9;
        while (true)
        {
            _empty = 9;
            for (int _node = 0; _node < GameObj.PlaygroundGrid.Length; _node++)
                if (GameObj.PlaygroundGrid[_node] == 0)
                {
                    if (Rand.Next(100) > 75)
                    {
                        GameObj.PlaygroundGrid[_node] = SideX ? (byte)1 : (byte)2;
                        for (int _wins = 0; _wins < GameObj.WinLink[_node].Length; _wins++)
                            GameObj.WinStateAI[GameObj.WinLink[_node][_wins]]++;
                        return;
                    }
                }
                else _empty--;
            if (_empty == 0)
            {
                GameObj.State = GameSrc.GameState.GAME_NEW_ROUND;
                return;
            }
        }
    }
}