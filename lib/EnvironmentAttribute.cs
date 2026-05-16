namespace No1.EnvBasedEndpoints;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public sealed class EnvironmentAttribute(string[] includedEnvironments, string[] excludedEnvironments) : Attribute()
{
	public string[] IncludedEnvironments { get; } = includedEnvironments;

	public string[] ExcludedEnvironments { get; } = excludedEnvironments;
}