
public class DestinationMapData
{
    public required double Destination { get; set; }
    public double Range { get; set; }
}

class Program
{
    private static string[] _lines = File.ReadAllLines("input.txt");
    private static Dictionary<string, Dictionary<double, DestinationMapData>> _maps = new()
    {
        { "seed-to-soil map:",new Dictionary<double,DestinationMapData>() },
        { "soil-to-fertilizer map:", new Dictionary<double,DestinationMapData>() },
        { "fertilizer-to-water map:", new Dictionary<double,DestinationMapData>() },
        { "water-to-light map:", new Dictionary<double,DestinationMapData>() },
        { "light-to-temperature map:", new Dictionary<double,DestinationMapData>() },
        { "temperature-to-humidity map:", new Dictionary<double,DestinationMapData>() },
        { "humidity-to-location map:", new Dictionary<double,DestinationMapData>() }

    };
    public static void Main(string[] args)
    {
        var seeds = _lines[0].Split(':')[1].Trim().Split(" ");
        for (var i = 0; i < _lines.Length; i++)
        {
            var line = _lines[i];
            if (_maps.ContainsKey(line))
            {
                GetMap(i, line);
            }
        }
        double lowestLocation = double.MaxValue;
        foreach(var strSeed in seeds)
        {
            double nextMapInput = double.Parse(strSeed);
            foreach (var mapKeyValuePair in _maps)
            {
                var map = mapKeyValuePair.Value;

                var mapping = map.FirstOrDefault(sourceKeyValuePair => sourceKeyValuePair.Key <= nextMapInput && nextMapInput <= sourceKeyValuePair.Key + sourceKeyValuePair.Value.Range - 1);

                if (!mapping.Equals(default(KeyValuePair<double,DestinationMapData>)))
                {
                    var differenceFromInitial = mapping.Value.Destination - mapping.Key;
                    nextMapInput = nextMapInput + differenceFromInitial;
                }
            }
            if(nextMapInput < lowestLocation)
            {
                lowestLocation = nextMapInput;
            }
        }
        Console.Write(lowestLocation);
        Console.Read();
    }

    private static void GetMap(int i, string mapName)
    {
        while (i+1 < _lines.Length && _lines[i + 1].ToString() != "")
        {
            var data = _lines[i + 1].Split(" ");
            var originStart = double.Parse(data[1]);
            var destinationStart = double.Parse(data[0]);
            var range = double.Parse(data[2]);
            _maps[mapName].Add(originStart, new DestinationMapData { Destination = destinationStart, Range = range });
            i++;
        }
    }
}

