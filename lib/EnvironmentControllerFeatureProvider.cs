using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace No1.EnvBasedApi;

public class EnvironmentControllerFeatureProvider(IWebHostEnvironment environment) : ControllerFeatureProvider
{
	public static void Register(ApplicationPartManager manager, IWebHostEnvironment environment) {
		ArgumentNullException.ThrowIfNull(manager);
		ArgumentNullException.ThrowIfNull(environment);

		manager.FeatureProviders.Clear();
		manager.FeatureProviders.Add(
			new EnvironmentControllerFeatureProvider(environment)
		);
	}

	protected override bool IsController(TypeInfo typeInfo) {
		if (!base.IsController(typeInfo)) {
			return false;
		}

		if (typeInfo.GetCustomAttribute<NonProductionAttribute>() is not null && environment.IsProduction()) {
			return false;
		}

		if (typeInfo.GetCustomAttribute<ProductionOnlyAttribute>() is not null && !environment.IsProduction()) {
			return false;
		}

		if (typeInfo.GetCustomAttribute<DevelopmentOnlyAttribute>() is not null && !environment.IsDevelopment()) {
			return false;
		}

		if (typeInfo.GetCustomAttribute<StagingOnlyAttribute>() is not null && !environment.IsStaging()) {
			return false;
		}

		if (typeInfo.GetCustomAttribute<EnvironmentAttribute>() is EnvironmentAttribute environmentAttribute) {
			bool? includes = environmentAttribute.IncludedEnvironments?.Length > 0 ? environmentAttribute.IncludedEnvironments.Contains(environment.EnvironmentName) : null;
			bool? excludes = environmentAttribute.ExcludedEnvironments?.Length > 0 ? environmentAttribute.ExcludedEnvironments.Contains(environment.EnvironmentName) : null;

			if ((includes == true) && (excludes == true)) {
				throw new ArgumentException($"envioronment {environment.EnvironmentName} is in both including ({environmentAttribute.IncludedEnvironments}) and exluding ({environmentAttribute.ExcludedEnvironments})environments of EnvironmentAttribute");
			}

			return includes == true || excludes == false;
		}

		return true;
	}
}