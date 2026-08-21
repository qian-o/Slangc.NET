using System.Text.Json.Nodes;

namespace Slangc.NET;

/// <summary>
/// Represents a shader entry point (such as vertex, fragment, or compute shader stages) with its associated bindings and metadata.
/// </summary>
public class SlangEntryPoint
{
    /// <summary>
    /// Initializes a new instance of the SlangEntryPoint class from JSON reflection data.
    /// </summary>
    /// <param name="reader">JSON object containing entry point information</param>
    internal SlangEntryPoint(JsonObject reader)
    {
        Name = reader["name"].Deserialize<string>();
        Stage = reader["stage"].Deserialize<SlangStage>();
        Scope = reader.ContainsKey("scope") ? new(reader["scope"]!.AsObject()) : null;
        Parameters = reader.ContainsKey("parameters") ? [.. reader["parameters"]!.AsArray().Select(static reader => new SlangParameter(reader!.AsObject()))] : [];
        Result = reader.ContainsKey("result") ? new(reader["result"]!.AsObject()) : null;
        ThreadGroupSize = reader.ContainsKey("threadGroupSize") ? [.. reader["threadGroupSize"]!.AsArray().Select(static reader => reader.Deserialize<uint>())] : [];
        Bindings = reader.ContainsKey("bindings") ? [.. reader["bindings"]!.AsArray().Select(static reader => new SlangVariableLayout(reader!.AsObject()))] : [];
        UsesAnySampleRateInput = reader["usesAnySampleRateInput"].Deserialize<bool>();
        UserAttributes = reader.ContainsKey("userAttribs") ? [.. reader["userAttribs"]!.AsArray().Select(static reader => new SlangUserAttribute(reader!.AsObject()))] : [];
    }

    /// <summary>
    /// Gets the name of the entry point function.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the shader stage this entry point represents (vertex, fragment, compute, etc.).
    /// </summary>
    public SlangStage Stage { get; }

    /// <summary>
    /// Gets the complete parameter scope for this entry point, when the JSON contains one.
    /// </summary>
    public SlangScope? Scope { get; }

    /// <summary>
    /// Gets the parameters associated with this entry point.
    /// </summary>
    public SlangParameter[] Parameters { get; }

    /// <summary>
    /// Gets the entry-point result, when one is reflected.
    /// </summary>
    public SlangParameter? Result { get; }

    /// <summary>
    /// Gets the thread group size for compute, task, and mesh shaders (if applicable).
    /// This array contains the X, Y, Z dimensions of the thread group.
    /// </summary>
    public uint[] ThreadGroupSize { get; internal set; }

    /// <summary>
    /// Gets the program parameter layouts as used by this entry point.
    /// </summary>
    public SlangVariableLayout[] Bindings { get; }

    /// <summary>
    /// Gets a value indicating whether the entry point uses any sample-rate input.
    /// </summary>
    public bool UsesAnySampleRateInput { get; }

    /// <summary>
    /// Gets the user-defined attributes associated with the entry point.
    /// </summary>
    public SlangUserAttribute[] UserAttributes { get; }
}
