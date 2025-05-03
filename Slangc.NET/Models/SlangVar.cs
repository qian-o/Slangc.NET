using System.Text.Json.Nodes;

namespace Slangc.NET.Models;

public class SlangVar
{
    internal SlangVar(JsonObject reader)
    {
        Name = reader["name"].Deserialize<string>();
    }

    public string Name { get; }
}
