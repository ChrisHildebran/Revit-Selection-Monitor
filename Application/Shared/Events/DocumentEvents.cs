﻿// /////////////////////////////////////////////////////////////
// Solution:............ SelectionMonitorCompilation
// Project:............. Core
// File:................ DocumentEvents.cs
// Last Code Cleanup:... 01/17/2020 @ 8:16 AM Using ReSharper ✓
// /////////////////////////////////////////////////////////////
namespace SelectionMonitorCompilationCore.Shared.Events
{

	using Autodesk.Revit.DB.Events;

	public static class DocumentEvents
	{

		#region Methods (SC)

		public static void OnClosing(object sender, DocumentClosingEventArgs e)
		{
		}


		public static void OnOpening(object sender, DocumentOpeningEventArgs e)
		{
		}

		#endregion

	}

}