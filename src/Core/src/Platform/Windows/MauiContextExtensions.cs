﻿using System;
using System.Collections.Generic;
using System.Text;
using Windows.ApplicationModel.Resources.Core;

namespace Microsoft.Maui
{
	internal static class MauiContextExtensions
	{
		public static FlowDirection GetFlowDirection(this IMauiContext mauiContext)
		{
			string resourceFlowDirection = ResourceManager.Current.DefaultContext.QualifierValues["LayoutDirection"];
			if (resourceFlowDirection == "LTR")
				return FlowDirection.LeftToRight;
			else if (resourceFlowDirection == "RTL")
				return FlowDirection.RightToLeft;

			return FlowDirection.MatchParent;
		}

		public static WindowManager GetWindowManager(this IMauiContext mauiContext)
		{
			WindowManager? windowManager = null;
			if (mauiContext is IScopedMauiContext smc)
				windowManager = smc.WindowManager;

			return windowManager ?? throw new InvalidOperationException("WindowManager Not Found");
		}
	}
}
