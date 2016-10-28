using System;
//Соперник с расширенной логикой, есть ошибка когда сделанно ходов больше 4, и стоит заглушка если игрок играет за Х.
public class HardAI : IGameAI
{
    public bool SideX { get; set; }
    public GameSrc GameObj { get; set; }
    private Random Rand = new Random();
    private int Turns = 0;
    private byte[] Nodes;

    public void InitAI(bool _side, GameSrc _game)
    {
        SideX = _side;
        GameObj = _game;
        Nodes = new byte[9];
    }

    public void Action()
    {
        Turns++;
        byte _empty = 9;
        while (true)
        {
            _empty = 9;
            if (SideX)
            {
                if (Turns == 1)
                {
                    byte _id = (byte)Rand.Next(4);
                    if (_id == 1) _id = 2;
                    else if (_id == 2) _id = 6;
                    else if (_id == 3) _id = 8;
                    Nodes[Turns - 1] = _id;
                }
                else if (Turns == 2)
                {
                    if (Nodes[Turns - 2] == 0)
                    {
                        if (GameObj.PlaygroundGrid[8] == 0)
                            Nodes[Turns - 1] = 8;
                        else
                        {
                            byte _id = (byte)Rand.Next(2);
                            if (_id == 0) _id = 2;
                            else _id = 6;
                            Nodes[Turns - 1] = _id;
                        }
                    }
                    else
                    if (Nodes[Turns - 2] == 8)
                    {
                        if (GameObj.PlaygroundGrid[0] == 0)
                            Nodes[Turns - 1] = 0;
                        else
                        {
                            byte _id = (byte)Rand.Next(2);
                            if (_id == 0) _id = 2;
                            else _id = 6;
                            Nodes[Turns - 1] = _id;
                        }
                    }
                    else
                    if (Nodes[Turns - 2] == 2)
                    {
                        if (GameObj.PlaygroundGrid[6] == 0)
                            Nodes[Turns - 1] = 6;
                        else
                        {
                            byte _id = (byte)Rand.Next(2);
                            if (_id == 0) _id = 0;
                            else _id = 8;
                            Nodes[Turns - 1] = _id;
                        }
                    }
                    else
                    if (Nodes[Turns - 2] == 6)
                    {
                        if (GameObj.PlaygroundGrid[2] == 0)
                            Nodes[Turns - 1] = 2;
                        else
                        {
                            byte _id = (byte)Rand.Next(2);
                            if (_id == 0) _id = 0;
                            else _id = 8;
                            Nodes[Turns - 1] = _id;
                        }
                    }
                }
                else if (Turns == 3)
                {
                    if ((Nodes[0] == 0 && Nodes[1] == 8) || (Nodes[0] == 8 && Nodes[1] == 0))
                    {
                        if (GameObj.PlaygroundGrid[4] == 0)
                            Nodes[Turns - 1] = 4;
                        else
                        if (GameObj.PlaygroundGrid[3] == 2 || GameObj.PlaygroundGrid[6] == 2 || GameObj.PlaygroundGrid[7] == 2)
                            Nodes[Turns - 1] = 2;
                        else Nodes[Turns - 1] = 6;
                    }
                    else if ((Nodes[0] == 2 && Nodes[1] == 6) || (Nodes[0] == 6 && Nodes[1] == 2))
                    {
                        if (GameObj.PlaygroundGrid[4] == 0)
                            Nodes[Turns - 1] = 4;
                        else
                        if (GameObj.PlaygroundGrid[0] == 2 || GameObj.PlaygroundGrid[1] == 2 || GameObj.PlaygroundGrid[3] == 2)
                            Nodes[Turns - 1] = 8;
                        else Nodes[Turns - 1] = 0;
                    }
                    else if ((Nodes[0] == 0 && Nodes[1] == 2) || (Nodes[0] == 2 && Nodes[1] == 0))
                    {
                        if (GameObj.PlaygroundGrid[1] == 0)
                            Nodes[Turns - 1] = 1;
                        else if (GameObj.PlaygroundGrid[4] == 0)
                            Nodes[Turns - 1] = 4;
                        else Nodes[Turns - 1] = 7;
                    }
                    else if ((Nodes[0] == 6 && Nodes[1] == 8) || (Nodes[0] == 8 && Nodes[1] == 6))
                    {
                        if (GameObj.PlaygroundGrid[7] == 0)
                            Nodes[Turns - 1] = 7;
                        else if (GameObj.PlaygroundGrid[4] == 0)
                            Nodes[Turns - 1] = 4;
                        else Nodes[Turns - 1] = 1;
                    }
                    else if ((Nodes[0] == 0 && Nodes[1] == 6) || (Nodes[0] == 6 && Nodes[1] == 0))
                    {
                        if (GameObj.PlaygroundGrid[3] == 0)
                            Nodes[Turns - 1] = 3;
                        else if (GameObj.PlaygroundGrid[4] == 0)
                            Nodes[Turns - 1] = 4;
                        else Nodes[Turns - 1] = 5;
                    }
                    else if ((Nodes[0] == 8 && Nodes[1] == 2) || (Nodes[0] == 2 && Nodes[1] == 8))
                    {
                        if (GameObj.PlaygroundGrid[5] == 0)
                            Nodes[Turns - 1] = 5;
                        else if (GameObj.PlaygroundGrid[4] == 0)
                            Nodes[Turns - 1] = 4;
                        else Nodes[Turns - 1] = 3;
                    }
                }
                else if (Turns == 4)
                {
                    if (Nodes[Turns - 2] == 4)
                    {
                        if (GameObj.PlaygroundGrid[0] == 1 && GameObj.PlaygroundGrid[2] == 1)
                        {
                            if (GameObj.PlaygroundGrid[6] == 2) Nodes[Turns - 1] = 8;
                            else Nodes[Turns - 1] = 6;
                        }
                        else if (GameObj.PlaygroundGrid[6] == 1 && GameObj.PlaygroundGrid[8] == 1)
                        {
                            if (GameObj.PlaygroundGrid[0] == 2) Nodes[Turns - 1] = 2;
                            else Nodes[Turns - 1] = 0;
                        }
                        else
                        if (GameObj.PlaygroundGrid[0] == 1 && GameObj.PlaygroundGrid[6] == 1)
                        {
                            if (GameObj.PlaygroundGrid[2] == 2) Nodes[Turns - 1] = 8;
                            else Nodes[Turns - 1] = 2;
                        }
                        else if (GameObj.PlaygroundGrid[2] == 1 && GameObj.PlaygroundGrid[8] == 1)
                        {
                            if (GameObj.PlaygroundGrid[0] == 2) Nodes[Turns - 1] = 6;
                            else Nodes[Turns - 1] = 0;
                        }
                    }
                    else
                    {
                        if (GameObj.PlaygroundGrid[0] == 1 && GameObj.PlaygroundGrid[2] == 1)
                        {
                            if (GameObj.PlaygroundGrid[1] == 2)
                            {
                                if (GameObj.PlaygroundGrid[6] == 2)
                                    Nodes[Turns - 1] = 5;
                                else Nodes[Turns - 1] = 3;
                            }
                            else Nodes[Turns - 1] = 1;
                        }
                        else if (GameObj.PlaygroundGrid[6] == 1 && GameObj.PlaygroundGrid[8] == 1)
                        {
                            if (GameObj.PlaygroundGrid[7] == 2)
                            {
                                if (GameObj.PlaygroundGrid[0] == 2)
                                    Nodes[Turns - 1] = 5;
                                else Nodes[Turns - 1] = 3;
                            }
                            else Nodes[Turns - 1] = 7;
                        }
                        else
                        if (GameObj.PlaygroundGrid[0] == 1 && GameObj.PlaygroundGrid[6] == 1)
                        {
                            if (GameObj.PlaygroundGrid[3] == 2)
                            {
                                if (GameObj.PlaygroundGrid[8] == 2)
                                    Nodes[Turns - 1] = 1;
                                else Nodes[Turns - 1] = 7;
                            }
                            else Nodes[Turns - 1] = 3;
                        }
                        else if (GameObj.PlaygroundGrid[2] == 1 && GameObj.PlaygroundGrid[8] == 1)
                        {
                            if (GameObj.PlaygroundGrid[5] == 2)
                            {
                                if (GameObj.PlaygroundGrid[6] == 2)
                                    Nodes[Turns - 1] = 1;
                                else Nodes[Turns - 1] = 7;
                            }
                            else Nodes[Turns - 1] = 5;
                        }
                    }
                }
                else
                {
                    for (int _node = 0; _node < GameObj.PlaygroundGrid.Length; _node++)
                        if (GameObj.PlaygroundGrid[_node] == 0)
                        {
                            Nodes[Turns - 1] = (byte)_node;
                            break;
                        }
                }
                GameObj.PlaygroundGrid[Nodes[Turns - 1]] = 1;
                for (int _wins = 0; _wins < GameObj.WinLink[Nodes[Turns - 1]].Length; _wins++)
                    GameObj.WinStateAI[GameObj.WinLink[Nodes[Turns - 1]][_wins]]++;
                if (Turns > 6)
                    GameObj.State = GameSrc.GameState.GAME_NEW_ROUND;
                return;
            }
            else
            {
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
            }
            if (_empty == 0)
            {
                GameObj.State = GameSrc.GameState.GAME_NEW_ROUND;
                return;
            }
        }
    }
}