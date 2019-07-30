using System;

namespace Acqusys.Grace.Extensions
{
  [AttributeUsage(AttributeTargets.Class)]
	public class ModuleConfigurationAttribute : Attribute
	{
		public string SectionName { get; }

		public ModuleConfigurationAttribute(string sectionName)
			=> SectionName = sectionName;
	}

}

// Acqusys.Grace.Extensions
// Copyright (C) 2019 Richard A. Fleming (rfleming@acqusys.com)
// This library is free software; you can redistribute it and/or modify it under the terms of the GNU Lesser General Public
// License version 2.1 as published by the Free Software Foundation.  A full copy of the license can be found in the file LICENSE.