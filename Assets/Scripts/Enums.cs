// ��Ʈ�� ������Ʈ�� ��Ʈ�� ��ü
public enum Player
{
    None,       // ��Ʈ�� X
    Player1,    // 1P ��Ʈ�� (wasd)
    Player2,    // 2P ��Ʈ�� (����Ű)
    All,
}

// HP���� ����
public enum HpUIType
{
    Counter,
    Fixed,
}

//���� ����
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
    Shield = 1,
    Faint = 2,
    Heal = 3,
    MaxEnergy = 4,
    ChangeEnergy = 5,
    Bang = 6,
}