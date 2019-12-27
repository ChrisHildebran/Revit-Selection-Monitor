﻿// /////////////////////////////////////////////////////////////
// Solution:............ Kelly Development
// Project:............. BaseRevitModeless
// File:................ SelectionChangedCommand.cs
// Last Code Cleanup:... 12/27/2019 @ 12:12 PM Using ReSharper ✓
// /////////////////////////////////////////////////////////////
namespace BaseRevitModeless.Commands
{

	// https://thebuildingcoder.typepad.com/blog/2015/03/element-selection-changed-event.html
	// https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdSelectionChanged.cs
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Diagnostics;
	using System.Linq;

	using Autodesk.Revit.Attributes;
	using Autodesk.Revit.DB;
	using Autodesk.Revit.UI;
	using Autodesk.Windows;

	[Transaction(TransactionMode.ReadOnly)]
	internal class SelectionChangedCommand : IExternalCommand
	{

		#region Fields (SC)

		private static bool _subscribed;

		private static UIApplication _uiapp;

		#endregion

		#region Methods (SC)

		public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
		{
			_uiapp = commandData.Application;

			foreach(var tab in ComponentManager.Ribbon.Tabs)
			{
				if(tab.Id == "Modify")
				{
					if(_subscribed)
					{
						tab.PropertyChanged -= PanelEvent;
						_subscribed         =  false;
					}
					else
					{
						//tab.PropertyChanged -= PanelEventOrigFromJT; // Added based on reading https://forums.xamarin.com/discussion/112676/eventhandler-not-being-removed-despite-being-unsubscribed
						tab.PropertyChanged += PanelEvent;
						_subscribed         =  true;
					}

					break;
				}
			}

			Debug.Print("CmdSelectionChanged: _subscribed = {0}", _subscribed);

			return Result.Succeeded;
		}


		private void PanelEvent(object sender, PropertyChangedEventArgs e)
		{
			Debug.Print("Sender: " + sender);

			Debug.Assert(sender is RibbonTab, "expected sender to be a ribbon tab");

			if(e.PropertyName == "Title" && _subscribed)
			{
				ICollection<ElementId> ids = _uiapp.ActiveUIDocument.Selection.GetElementIds();

				var n = ids.Count;

				string s;

				if(0 == n)
				{
					s = "<nil>";
				}
				else
				{
					s = string.Join(", ", ids.Select(id => id.IntegerValue.ToString()));
				}

				Debug.Print("CmdSelectionChanged: selection changed: " + s);
			}
		}

		#endregion

	}

}