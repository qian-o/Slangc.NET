using System.Text.Json.Nodes;

namespace Slangc.NET;

/// <summary>
/// Represents the allocation scope for global or entry-point parameters.
/// </summary>
public class SlangScope
{
    /// <summary>
    /// Initializes a scope from Slang JSON reflection data.
    /// </summary>
    /// <param name="reader">JSON object containing scope information.</param>
    internal SlangScope(JsonObject reader)
    {
        Kind = reader["kind"].Deserialize<SlangScopeKind>();
        Binding = reader.ContainsKey("binding") ? new(reader["binding"]!.AsObject()) : null;
        Parameters = reader.ContainsKey("parameters") ? [.. reader["parameters"]!.AsArray().Select(static reader => new SlangParameter(reader!.AsObject()))] : [];
    }

    /// <summary>
    /// Gets how the scope's parameters are allocated.
    /// </summary>
    public SlangScopeKind Kind { get; }

    /// <summary>
    /// Gets the binding owned by an implicitly introduced container, if any.
    /// </summary>
    public SlangBinding? Binding { get; }

    /// <summary>
    /// Gets the parameters contained by this scope.
    /// </summary>
    public SlangParameter[] Parameters { get; }
}