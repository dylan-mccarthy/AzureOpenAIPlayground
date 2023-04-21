public class Room
{
    public int X;
    public int Y;

    public bool HasKey;

    public bool HasTreasure;

    public List<RoomLink> RoomLinks = new List<RoomLink>();
}

public class RoomLink
{
    public Room Source;
    public Room Destination;
    public string Direction;
}