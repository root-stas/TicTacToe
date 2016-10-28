public interface IGameAI {
    bool SideX { get; set; }
    GameSrc GameObj { get; set; }
    void InitAI(bool _side, GameSrc _game);
    void Action();
}