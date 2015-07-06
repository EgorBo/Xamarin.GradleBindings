Xamarin.GradleBindings
=========

Link to extensions gallery: https://visualstudiogallery.msdn.microsoft.com/3a3257c7-473a-4790-9610-9a561eed0b8c

How do java developers add dependencies to their projects? Yes that's right, via gradle (something like [this](https://github.com/WhisperSystems/TextSecure/blob/master/build.gradle#L37) or [that](https://github.com/popcorn-official/popcorn-android/blob/development/mobile/build.gradle#L92)). As you can see some java-projects use those dependencies a lot (all you want to write is already written) so it'd be nice to use [those huge amount](https://github.com/wasabeef/awesome-android-ui) of 3rd party libraries in your Xamarin project, right? I believe this Add-in for Visual Studio 2013 (and lately for Xamarin Studio) will help you with it:

![Alt text](https://habrastorage.org/files/977/26a/f97/97726af97cac492eb591541ab5e2f4b0.gif)


**Step 1**: Execute the command over "References" folder

![Alt text](https://habrastorage.org/files/f19/c62/8e1/f19c628e122349129eaa0436f891b49a.png)

**Step 2**: Set an external dependency id and a name for Xamarin Android Binding Project (will be generated). This dialog will allow you to specify custom repositories as well soon.

![Alt text](https://habrastorage.org/files/2d5/006/e93/2d5006e93bfd4f66959c327236fd6d5d.png)

The Plugin executes gradle scripts and receives dependencies list (including transitive ones). At this step you can select or deselect needed binaries (transitive dependencies are deselected by default).
**NOTE: you'd better use "Xamarin Components" or directly NuGet for Support dependencies** (v4, RecyclerView, AppCompact, etc..).

![Alt text](https://habrastorage.org/files/d33/b60/0ae/d33b600ae87e41888c59fda2971861a0.png)

**Step 3**: The binding project will be generated but you still may have to fix some issues via Metadata.xml because the Add-in is not smart enough. For our example you will have to change visibility of [DialogBase](https://github.com/afollestad/material-dialogs/blob/master/library/src/main/java/com/afollestad/materialdialogs/DialogBase.java#L14) class because it's private in Java and C# doesn't support deriving public classes from private ones. So just add it to the Transforms/Metadata.xml (more info [here](http://developer.xamarin.com/guides/android/advanced_topics/java_integration_overview/binding_a_java_library_(.jar)/#Resolving_API_Differences)):

```Xml
<attr path="/api/package[@name='com.afollestad.materialdialogs']/class[@name='DialogBase']" name="visibility">public</attr>
```


**Step 4**: Now you are ready to use them! i.e. the Material Dialogs:

![Alt text](https://habrastorage.org/files/273/712/364/2737123640984b55b37d6a286b0c741f.png)

Enjoy! 
