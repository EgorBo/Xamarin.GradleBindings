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

        public IEnumerable<CompiledDependencyInfo> ExtractDependencies(string gradleFile)
        {

            //just execute the gradle with getDeps task 

            /*
             
apply plugin: 'java'

dependencies {
  compile 'com.afollestad:material-dialogs:0.7.6.0@aar'
  compile 'com.makeramen:roundedimageview:2.1.0@aar'
  //list of dependencies...
}

repositories { jcenter() }

task getDeps(type: Copy) {
  from sourceSets.main.runtimeClasspath
  into 'runtime/' 
}
             
             */


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