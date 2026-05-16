namespace No1.EnvBasedEndpoints;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public sealed class StagingOnlyAttribute : Attribute;