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

    #region Kind is SlangTypeKind.Resource
    public SlangResourceShape BaseShape { get; }

    public bool Array { get; }

    public bool Multisample { get; }

    public bool Feedback { get; }

    public SlangResourceAccess Access { get; }
    #endregion
}
