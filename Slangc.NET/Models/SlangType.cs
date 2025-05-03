using System.Text.Json.Nodes;
using Slangc.NET.Enums;

namespace Slangc.NET.Models;

public class SlangType
{
    internal SlangType(JsonObject reader)
    {
        Kind = reader["kind"].Deserialize<SlangTypeKind>();
        BaseShape = reader["baseShape"].Deserialize<SlangResourceShape>();
    }

    public SlangTypeKind Kind { get; }

    public SlangResourceShape BaseShape { get; }
}
