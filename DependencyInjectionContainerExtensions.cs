using System;
using System.Collections.Generic;
using Grace.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Acqusys.Grace.Extensions
{
  public static class DependencyInjectionContainerExtensions
	{
		private static readonly IList<Type> _modules = new List<Type>();
		private static IConfiguration _config;

		public static DependencyInjectionContainer WithConfiguration(this DependencyInjectionContainer target, IConfiguration config)
		{
			_config = config;
			return target;
		}

		public static IInjectionScope WithConfiguration(this IInjectionScope target, IConfiguration config)
		{
			_config = config;
			return target;
		}

		public static void AddModuleOnce<T>(this IExportRegistrationBlock target) where T: IConfigurationModule, new()
			=> target.AddModuleOnce(new T());

		public static void AddModuleOnce(this IExportRegistrationBlock target, IConfigurationModule module)
		{
			lock (_modules)
			{
				var type = module.GetType();

				if (_modules.Contains(type))
					return;

				if (Attribute.IsDefined(type, typeof(ModuleConfigurationAttribute)) && _config != null)
				{
					var attribute = (ModuleConfigurationAttribute)Attribute.GetCustomAttribute(type, typeof(ModuleConfigurationAttribute));
					var section = _config.GetSection(attribute.SectionName);

					section.Bind(module);
				}

				_modules.Add(type);
				target.AddModule(module);
			}
		}
	}

}

// Acqusys.Grace.Extensions
// Copyright (C) 2019 Richard A. Fleming (rfleming@acqusys.com)
// This library is free software; you can redistribute it and/or modify it under the terms of the GNU Lesser General Public
// License version 2.1 as published by the Free Software Foundation.  A full copy of the license can be found in the file LICENSE.