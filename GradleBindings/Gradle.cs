using System.Collections.Generic;

namespace GradleBindings
{
    public class Gradle
    {
        private readonly string _androidSdk;

        /// <summary>
        /// We need it for the gradle 
        /// (or we can hope that user has ANDROID_HOME set)
        /// </summary>
        public Gradle(string androidSdk)
        {
            _androidSdk = androidSdk;
        }

        public IEnumerable<CompiledDependencyInfo> ExtractAndCompileDependencies(string gradleFile)
        {

            //Algorithm:
            //1) create a simple Android gradle project (extract from embedded template)
            //2) set Android.Sdk path to properties.local file (or we can check if ANDROID_HOME exists)
            //3) replace gradle.build with a given one
            //4) build the project via "gradlew build" (BTW, we can distribute the plugin with gradle binaries - https://gradle.org/downloads/ - 41 mb)
            //5) if build failed - show errors
            //6) open "build\intermediates\exploded-aar" and zip each directories (rename zip to aar and skip android support libraries)
            //7) not sure yet about dependencies which compiles to "jar" rather than "aar" (i.e. Gson library) but they also should be somewhere here in the "build" folder


            //for the demo:
            //NOTE: Material Dialogs has some class that should be public so there should be a fix applied via Metadata.xml
            yield return new CompiledDependencyInfo(name: "com.afollestad:material-dialogs:0.7.6.0",
                shortName: "Binding_MaterialDialogs", 
                files: new List<DependencyFile> { new DependencyFile(@"C:\Users\Egorbo\Downloads\material-dialogs-master\material-dialogs-master\library\build\outputs\aar\library-release.aar", DependencyFileType.Aar) });

            yield return new CompiledDependencyInfo(name: "com.makeramen:roundedimageview:2.1.0", 
                shortName: "Binding_RoundedImageView", 
                files: new List<DependencyFile> { new DependencyFile(@"C:\Users\Egorbo\Downloads\RoundedImageView-master\RoundedImageView-master\roundedimageview\build\outputs\aar\roundedimageview-release.aar", DependencyFileType.Aar) });
        }
    }
}