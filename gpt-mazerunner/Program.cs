int x = 32;
int y = 32;

int maxRooms = 100;

Random random = new Random();

List<Room> rooms = new List<Room>();

for(int i = 0; i < maxRooms; i++)
{
    Room newRoom = new Room();
    int r_x = random.Next(0,x);
    int r_y = random.Next(0,y);

    bool overlap = false;
    foreach(Room r in rooms){
        if(r.X == r_x && r.Y == r_y)
            overlap = true;
    }

    if(overlap)
    {
        i--;
    }
    else{
        newRoom.X = r_x;
        newRoom.Y = r_y;
        rooms.Add(newRoom);
    }
}

for(int i = 0; i < x; i++)
{
    List<Room> selectedRooms = new List<Room>();
    foreach(Room r in rooms)
    {
        if(r.X == i)
        {
            selectedRooms.Add(r);
        }
    }
    selectedRooms.OrderBy(c => c.Y);

    // Create West to East connections
    for(int j = 0; j < selectedRooms.Count-2; j++)
    {
        Room room1 = selectedRooms[j];
        Room room2 = selectedRooms[j+1];
        RoomLink link_source_to_dest = new RoomLink();
        link_source_to_dest.Source = room1;
        link_source_to_dest.Destination = room2;
        link_source_to_dest.Direction = "east";
        room1.RoomLinks.Add(link_source_to_dest);

        RoomLink link_dest_to_source = new RoomLink();
        link_dest_to_source.Source = room2;
        link_dest_to_source.Destination = room1;
        link_dest_to_source.Direction = "west";
    }
}

for(int i = 0; i < y; i++)
{
    List<Room> selectedRooms = new List<Room>();
    foreach(Room r in rooms)
    {
        if(r.Y == i)
        {
            selectedRooms.Add(r);
        }
    }
    selectedRooms.OrderBy(c => c.X);

    // Create South to North connections
    for(int j = 0; j < selectedRooms.Count-2; j++)
    {
        Room room1 = selectedRooms[j];
        Room room2 = selectedRooms[j+1];
        RoomLink link_source_to_dest = new RoomLink();
        link_source_to_dest.Source = room1;
        link_source_to_dest.Destination = room2;
        link_source_to_dest.Direction = "north";
        room1.RoomLinks.Add(link_source_to_dest);

        RoomLink link_dest_to_source = new RoomLink();
        link_dest_to_source.Source = room2;
        link_dest_to_source.Destination = room1;
        link_dest_to_source.Direction = "south";
    }
}

List<Room> connectedRooms = new List<Room>();

foreach(Room room in rooms){
    if(room.RoomLinks.Count >= 1)
    {
        Console.WriteLine($"X: {room.X} Y: {room.Y}");
        Console.WriteLine($"Links: ");
        foreach(RoomLink link in room.RoomLinks){
            Console.WriteLine($"\t {link.Direction}");
        }
        connectedRooms.Add(room);
    }
}

int keyRoom = random.Next(0,connectedRooms.Count);
int treasureRoom = random.Next(0,connectedRooms.Count);

connectedRooms[keyRoom].HasKey = true;
connectedRooms[treasureRoom].HasTreasure = true;

bool foundKey = false;
bool foundTreasure = false;
int startingRoom = random.Next(0, connectedRooms.Count);

Room currentRoom = connectedRooms[startingRoom];

while(foundTreasure == false)
{
    Console.WriteLine($"Current Coordiates: {currentRoom.X} , {currentRoom.Y}");
    string exits = "";
    foreach(var link in currentRoom.RoomLinks){
        exits += link.Direction + " ";
    }
    Console.WriteLine($"Exits: {exits}");
    Console.WriteLine("Choose a direction >");
    var input = Console.ReadLine();

    if(input == "east")
    {
        var exitlink = currentRoom.RoomLinks.Where(x => x.Direction == "east").FirstOrDefault();
        if(exitlink != null)
        {
            currentRoom = currentRoom = connectedRooms.Where(r => (r.X == exitlink.Destination.X && r.Y == exitlink.Destination.Y)).FirstOrDefault();
        }
        else{
            Console.WriteLine("No exit that direction");
        }
    }

    if(input == "west")
    {
        var exitlink = currentRoom.RoomLinks.Where(x => x.Direction == "west").FirstOrDefault();
        if(exitlink != null)
        {
            currentRoom = connectedRooms.Where(r => (r.X == exitlink.Destination.X && r.Y == exitlink.Destination.Y)).FirstOrDefault();
        }
        else{
            Console.WriteLine("No exit that direction");
        }
    }

    if(input == "south")
    {
        var exitlink = currentRoom.RoomLinks.Where(x => x.Direction == "south").FirstOrDefault();
        if(exitlink != null)
        {
            currentRoom = connectedRooms.Where(r => (r.X == exitlink.Destination.X && r.Y == exitlink.Destination.Y)).FirstOrDefault();
        }
        else{
            Console.WriteLine("No exit that direction");
        }
    }

    if(input == "north")
    {
        var exitlink = currentRoom.RoomLinks.Where(x => x.Direction == "north").FirstOrDefault();
        if(exitlink != null)
        {
            currentRoom = connectedRooms.Where(r => (r.X == exitlink.Destination.X && r.Y == exitlink.Destination.Y)).FirstOrDefault();
        }
        else{
            Console.WriteLine("No exit that direction");
        }
    }
}