using System.Text.Json.Nodes;
using Slangc.NET.Enums;

namespace Slangc.NET.Models;

public class SlangType
{
    internal SlangType(JsonObject reader)
    {
    }

    public SlangTypeKind Kind { get; }
}
