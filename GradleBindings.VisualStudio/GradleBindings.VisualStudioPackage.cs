using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using EnvDTE;
using EnvDTE80;
using GradleBindings;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using VSLangProj;

namespace EgorBo.GradleBindings_VisualStudio
{

    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    [ProvideAutoLoad("f1536ef8-92ec-443c-9ed7-fdadf150da82")] //UICONTEXT_SolutionExists -- force it to autoload
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(GuidList.guidGradleBindings_VisualStudioPkgString)]
    public sealed class GradleBindings_VisualStudioPackage : Package
    {
        private const string GradleFileExtension = ".gradle";

        protected override void Initialize()
        {
            base.Initialize();
            OleMenuCommandService mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (mcs != null)
            {
                CommandID generateBindingCmdId = new CommandID(GuidList.guidGradleBindings_VisualStudioCmdSet, (int)PkgCmdIDList.cmdidGenerateBinding);
                OleMenuCommand menuItem = new OleMenuCommand(GenerateBindingMenuClickedCallback, generateBindingCmdId);
                menuItem.BeforeQueryStatus += GenerateBindingsMenu_OnBeforeQueryStatus;
                mcs.AddCommand( menuItem );
            }
        }

        /// <summary>
        /// Let's show our "Generate bindings" menu only for ".gradle" files
        /// </summary>
        private void GenerateBindingsMenu_OnBeforeQueryStatus(object sender, EventArgs e)
        {
            bool enable = false;
            var menu = sender as OleMenuCommand;
            var envDte = GetService(typeof(DTE)) as DTE;
            var selectedItems = envDte.SelectedItems.OfType<SelectedItem>().ToArray();
            if (selectedItems.Length == 1)
            {
                var selectedItem = selectedItems[0].ProjectItem;
                if (selectedItem == null)
                    return;

                var kind = selectedItem.Kind;
                Guid kindId;
                if (Guid.TryParse(kind, out kindId))
                {
                    var isFile = VSConstants.GUID_ItemType_PhysicalFile == kindId;
                    if (isFile)
                    {
                        var extension = Path.GetExtension(selectedItem.Name);
                        if (GradleFileExtension.Equals(extension, StringComparison.InvariantCultureIgnoreCase))
                        {
                            enable = true;
                        }
                    }
                }
            }
            menu.Visible = enable;
        }

        private void GenerateBindingMenuClickedCallback(object sender, EventArgs e)
        {
            var envDte = GetService(typeof(DTE)) as DTE2;

            var gradleFileItem = envDte.SelectedItems.OfType<SelectedItem>().First().ProjectItem;
            var project = gradleFileItem.ContainingProject.Object as VSProject; //it should have at least one item
            
            var solution = envDte.Solution as Solution2;

            //NOTE: we should somehow get current Android SDK home path, Xamarin VS add-in definitely knows it
            var dependencies = new Gradle(string.Empty)
                .ExtractAndCompileDependencies(gradleFileItem.Name);

            foreach (var libraryItem in dependencies)
            {
                CreateBindingProject(project, solution,
                    libraryItem.ShortName,
                    libraryItem.Files.Where(f => f.Type == DependencyFileType.Aar).Select(f => f.Path),
                    libraryItem.Files.Where(f => f.Type == DependencyFileType.Jar).Select(f => f.Path),
                    libraryItem.Files.Where(f => f.Type == DependencyFileType.ReferencedJar).Select(f => f.Path));
            }
        }

        /// <summary>
        /// Creates an Android Binding Project
        /// adds a reference to the source project
        /// adds jars, referenced jars and aar files
        /// </summary>
        private static void CreateBindingProject(VSProject sourceProject, Solution2 solution, string bindingProjectName,
            IEnumerable<string> aarFiles,//multiple aar in a single project doesn't seem like a good idea: https://forums.xamarin.com/discussion/23766/error-no-resource-found-that-matches-the-given-name-when-linking-android-binding-project#latest
            IEnumerable<string> jarFiles,
            IEnumerable<string> referencedJarFiles)
        {
            var newProject = CreateProjectAndAddReferenceToIt(sourceProject, solution, "BindingsProject.zip", bindingProjectName, "AndroidBindings");
            //BindingsProject.zip stands for   //C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\IDE\Extensions\Xamarin\Xamarin\3.11.590.0\T\~PC\PT\Android\BindingsProject.zip\BindingsProject.vstemplate


            if (aarFiles != null)
            {
                foreach (var file in aarFiles)
                {
                    var addedAar = newProject.ProjectItems.AddFromFileCopy(file);
                    addedAar.Properties.Item("ItemType").Value = "LibraryProjectZip";
                }
            }

            if (jarFiles != null)
            {
                foreach (var file in jarFiles)
                {
                    //should be put to "Jars" dir?
                    var addedAar = newProject.ProjectItems.AddFromFileCopy(file);
                    addedAar.Properties.Item("ItemType").Value = "InputJar";
                }
            }

            if (referencedJarFiles != null)
            {
                foreach (var file in referencedJarFiles)
                {
                    //should be put to "Jars" dir?
                    var addedAar = newProject.ProjectItems.AddFromFileCopy(file);
                    addedAar.Properties.Item("ItemType").Value = "ReferenceJar";
                }
            }

            newProject.Save();
        }


        /// <summary>
        /// Creates a project of given template and adds a reference to it to the sourceProject
        /// </summary>
        /// <param name="sourceProject">can be null, otherwise a reference will be added</param>
        private static Project CreateProjectAndAddReferenceToIt(VSProject sourceProject, Solution2 solution, 
            string newProjectTemplate, string newProjectName, string newProjectDirectory)
        {
            var bindingProjectDir = Path.Combine(Path.GetDirectoryName(solution.FileName), newProjectDirectory ?? "", newProjectName);
            if (Directory.Exists(bindingProjectDir))
                Directory.Delete(bindingProjectDir, true);

            string csTemplatePath = solution.GetProjectTemplate(newProjectTemplate, "CSharp");
            solution.AddFromTemplate(csTemplatePath, bindingProjectDir, newProjectName);

            //let's find our new project (AddFromTemplate returns null all the time)
            var newProject = solution.Projects.OfType<Project>().First(p => p.Name == newProjectName);
            newProject.Save();

            //add reference for the target project
            if (sourceProject != null)
            {
                sourceProject.References.AddProject(newProject);
            }

            return newProject;
        }
    }
}
