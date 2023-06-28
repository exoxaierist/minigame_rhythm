// 컨트롤 오브젝트의 컨트롤 주체
public enum Player
{
    None,       // 컨트롤 X
    Player1,    // 1P 컨트롤 (wasd)
    Player2,    // 2P 컨트롤 (방향키)
    All,
}

// HP바의 종류
public enum HpUIType
{
    Counter,
    Fixed,
}

//리듬 레벨
public enum RhythmLevel
{
    Zero = 0,
    One = 1,
    Two = 2,
    Three = 3,
}

public enum Wtype
{
    None,
    Bullet,
    Laser,
    Slug,
    Shield,
}

public enum ItemType
{
    //itemName
    None = 0,
    item1 = 1,
    item2 = 2,
    item3 = 3,
    Shield = 4,
}