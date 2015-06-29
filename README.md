Xamarin.GradleBindings
=========

How java developers add dependencies to their projects? Yes that's right, via gradle (something like [this](https://github.com/WhisperSystems/TextSecure/blob/master/build.gradle#L37) or [that](https://github.com/popcorn-official/popcorn-android/blob/development/mobile/build.gradle#L92)). As you can see some java-projects use those dependencies a lot (all you want to write is already written) so it'd be nice to use those huge amount of 3rd party libraries in your Xamarin project, huh? I believe this Add-in for the Visul Studio 2013 (and lately for Xamarin Studio) will help you with it:

Step 1: Execute the command over "References" folder

![Alt text](https://habrastorage.org/files/2af/737/330/2af7373308564b70bf3c12b589ac20f9.png)

Step 2: Set an external dependency id and a name for Xamarin Android Binding Project (will be generated). This dialog will allow you to specify custom repositories as well soon.

![Alt text](https://habrastorage.org/files/cf1/414/c6a/cf1414c6a55448efbd65874c6311e719.png)

The Plugin executes gradle scripts and receives dependencies list (including transitive ones). On this step you can select or deselect needed binaries (transitive dependencies are deselected by default).
**NOTE: you'd better use "Xamarin Components" for Support dependencies** (v4, RecyclerView, AppCompact, etc..).

![Alt text](https://habrastorage.org/files/0bb/bae/e3c/0bbbaee3cefa406981aa825710eb245e.png)

Step 3: The binding project will be generated but you still may have to fix some issues via Metadata.xml because the Add-in is not smart enough.

Step 4: Now you are ready to use them! i.e. the Material Dialogs:

![Alt text](https://habrastorage.org/files/273/712/364/2737123640984b55b37d6a286b0c741f.png)

Enjoy! 
