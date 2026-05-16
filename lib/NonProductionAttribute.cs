namespace No1.EnvBasedApi;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public sealed class NonProductionAttribute : Attribute;