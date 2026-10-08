namespace FirstAgent.Tools;

public class XTool : IXTool
{
    public string GetY(string city)
    {
        Console.WriteLine("TOOL CALL GetY");
        return $"The weather is cold in {city}";
    }

    public string GetZ(string city)
    {
        Console.WriteLine("TOOL CALL GetZ");
        return $"{city} is a beautiful city. You can enjoy the architecture in {city}";
    }
    
}