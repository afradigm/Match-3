using System;

public class LevelData
{
    public Level[] levels;
}

[Serializable]
public class Level
{
    public int boardSize;
    public int moves;
}
