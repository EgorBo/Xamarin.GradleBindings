using System;
using System.IO;
using System.Linq;

namespace GradleBindings.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var androidSdk = Environment.GetEnvironmentVariable("ANDROID_HOME") ?? 
                Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE"), @"AppData\Local\Android\sdk");

            string repositoriesDir;
            if (!Gradle.HasLocalRepositories(androidSdk, out repositoriesDir))
                throw new Exception("Android SDK path is invalid or repositories are not installed via Android SDK Manager.");

            foreach (var dependencyId in new[] {
                                                //+ means 'latest'
                                                "com.squareup.retrofit:retrofit:1.9.0",
                                                "com.afollestad:material-dialogs:+",
                                                "com.squareup.picasso:picasso:+",
                                                "org.parceler:parceler-api:+",
                                                "de.keyboardsurfer.android.widget:crouton:+",
                                                "com.facebook.android:facebook-android-sdk:+",
                                                "se.emilsjolander:stickylistheaders:+",
                                                "com.jpardogo.materialtabstrip:library:+",
                                                "com.github.chrisbanes.photoview:library:+",
                                                "com.github.bumptech.glide:glide:+",
                                                "com.makeramen:roundedimageview:+",
                                                "com.afollestad:material-dialogs:+",
                                                "com.melnykov:floatingactionbutton:+",
                                                "de.hdodenhof:circleimageview:+",
                                                "com.github.nirhart:parallaxscroll:+",
                                                "com.larswerkman:HoloColorPicker:+",
                                                "net.rdrei.android.dirchooser:library:+",
                                                "com.github.johnkil.android-robototextview:robototextview:+",
                                                "com.sothree.slidinguppanel:library:+",
                                            }){
                ResolveAndPrintDependencies(dependencyId, androidSdk);
            }

            System.Console.WriteLine("Done.");
            System.Console.ReadKey();
        }

        public static void ResolveAndPrintDependencies(string dependencyId, string androidSdk)
        {
            System.Console.WriteLine("\nResolving '{0}'...", dependencyId);
            string workingDir;
            var result = Gradle.ExtractDependencies(dependencyId, androidSdk, out workingDir)
                .OrderBy(f => f.IsTransitive)
                .ToList();

            foreach (var file in result.OrderBy(f => f.IsTransitive))
            {
                System.Console.WriteLine("{1}{0}", Path.GetFileName(file.File), file.IsTransitive ? "\t" : "");
            }
        }
    }
}
