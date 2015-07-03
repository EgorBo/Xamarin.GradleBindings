using MonoDevelop.Components.Commands;
using MonoDevelop.Ide;
using MonoDevelop.Ide.Gui;   
using Mono.TextEditor;
using System;
using System.Linq;

namespace GradleBindings.XamarinStudio
{
	public class InsertDateHandler : CommandHandler
	{
		protected override void Run ()
		{
			var templates = IdeApp.Services.TemplatingService.GetProjectTemplateCategories ();
			var temps = templates.First (t => t.Id == "android").Categories;
			var pci = new MonoDevelop.Projects.ProjectCreateInformation ();
			pci.ParentFolder = IdeApp.ProjectOperations.CurrentSelectedSolution.RootFolder;
			//pci.ProjectBasePath =
			//pci.ProjectName = 

			IdeApp.Services.ProjectService.CreateProject ("MonoDroidBindingProject", pci, null);
		}

		protected override void Update (CommandInfo info)
		{

		}   
	}    

	public enum DateInserterCommands
	{
		InsertDate,
	}
}

