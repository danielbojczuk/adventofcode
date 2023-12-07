

using System;

class Range
{
    public double Start { get; private set; }
    public double End { get; private set; }

    public Range (double start, double end) 
    {
        Start = start;
        End = end;
    }   
}

class Rule
{
    public Range Range { get; private set; }

    public double TransformationFactor { get; private set; }

    public Rule(Range range, double transformationFactor)
    {
        Range = range;
        TransformationFactor = transformationFactor;
    }
}

        
class Map
{
    private readonly Rule[] _rules;
    public Map(List<Rule> rules)
    {
        _rules = rules.OrderBy(rule => rule.Range.Start).ToArray();
    }

    public List<Range> ApplyMapping(List<Range> inputRanges)
    {
        var processedRanges = new List<Range>();
        var orderedInputRanges = inputRanges.OrderBy(o => o.Start).ToArray();

        var lastInputValueFromAllRanges = orderedInputRanges[orderedInputRanges.Length-1].End;

        var firstValueFromAllRules = _rules[0].Range.Start;
        var lastValueFromAllRules = _rules[_rules.Length-1].Range.End;

        

        foreach (var inputRange in orderedInputRanges)
        {
            //Check if the input range has values before first map rulling. If there is, add them without any transofrmation
            if (inputRange.Start < firstValueFromAllRules)
            {
                processedRanges.Add(new Range(inputRange.Start, Math.Min(lastInputValueFromAllRanges, firstValueFromAllRules - 1)));
            }

            for (int r =  0; r < _rules.Length; r++) 
            {
                var rule = _rules[r];
                var isThereNextRule = r + 1 < _rules.Length;

                if (inputRange.Start >= rule.Range.Start && inputRange.Start <= rule.Range.End)
                {
                    var rangeInit = inputRange.Start;
                    var rangeEnd = Math.Min(inputRange.End, rule.Range.End);

                    processedRanges.Add(new Range(rangeInit + rule.TransformationFactor, rangeEnd + rule.TransformationFactor));
                } else if(inputRange.Start < rule.Range.Start && inputRange.End >= rule.Range.Start)
                {
                    var rangeInit = rule.Range.Start;
                    var rangeEnd = Math.Min(inputRange.End, rule.Range.End);
                    processedRanges.Add(new Range(rangeInit + rule.TransformationFactor, rangeEnd + rule.TransformationFactor));
                }

                if(inputRange.Start <= rule.Range.End && inputRange.End > rule.Range.End)
                {
                    var initialValue = rule.Range.End + 1;
                    var endValue = (isThereNextRule) ? Math.Min(_rules[r + 1].Range.Start - 1, inputRange.End) : inputRange.End;
                    processedRanges.Add(new Range(initialValue, endValue));
                }
            }
            //Check if the input range has values after last map rulling. If there is, add them without any transofrmation
            if (inputRange.End > lastValueFromAllRules)
            {
                processedRanges.Add(new Range(Math.Max(lastValueFromAllRules + 1, inputRange.Start), inputRange.End));
            }
        }

        processedRanges = processedRanges.OrderBy(o => o.Start).ToList();

        
        return processedRanges;
    }
}


class Program
{
    private static string[] _lines = File.ReadAllLines("input.txt");
    private static Dictionary<string, Map> _maps = new()
    {
        { "seed-to-soil map:",default },
        { "soil-to-fertilizer map:", default },
        { "fertilizer-to-water map:",default },
        { "water-to-light map:", default },
        { "light-to-temperature map:", default },
        { "temperature-to-humidity map:", default },
        { "humidity-to-location map:", default }

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


        var ranges = new List<Range>();
        for(var s = 0; s < seeds.Length; s +=2)
        {
            ranges.Add(new Range(double.Parse(seeds[s]), double.Parse(seeds[s]) + double.Parse(seeds[s+1])-1));
        }

        foreach(var map in _maps)
        {
            ranges = map.Value.ApplyMapping(ranges);
        }

        Console.WriteLine(ranges[0].Start);

        
        Console.Read();
    }

    private static void GetMap(int i, string mapName)
    {
        var rules = new List<Rule>();
        while (i+1 < _lines.Length && _lines[i + 1].ToString() != "")
        {
            var data = _lines[i + 1].Split(" ");
            var originStart = double.Parse(data[1]);
            var destinationStart = double.Parse(data[0]);
            var range = double.Parse(data[2]);

            var rangeObject = new Range(originStart, originStart + range - 1);
            rules.Add(new Rule(rangeObject, destinationStart - originStart));
            i++;
        }
        _maps[mapName] = new Map(rules);
    }
}

