Xamarin.GradleBindings
=========

How java developers add dependencies to their projects? Yes that's right, via gradle (something like [this](https://github.com/WhisperSystems/TextSecure/blob/master/build.gradle#L37) or [that](https://github.com/popcorn-official/popcorn-android/blob/development/mobile/build.gradle#L92)). As you can see some java-projects use those dependencies a lot (all you want to write is already written) so it'd be nice to use those huge amount of 3rd party libraries in your Xamarin project, huh? I believe my Add-in for the Visul Studio 2013 (and lately for Xamarin Studio) will help you with it:

Step 1: Add a "build.gradle" to your xamarin project with the list of native dependencies you want to generate bindings for

![Alt text](https://habrastorage.org/files/c93/dcf/acc/c93dcfacca754948823facecaa786ef0.png)

Step 2: Just execute the command:

![Alt text](https://habrastorage.org/files/5af/899/736/5af8997367f94628acbd049fe484e042.png)

and you will get something like this in your solution:

![Alt text](https://habrastorage.org/files/c93/dcf/acc/c93dcfacca754948823facecaa786ef0.png)

Step 3: You still may have to fix some issues via Metadata.xml because my Add-in is not smart enough.

Step 4: Now you are ready to use them! i.e. the Material Dialogs:

```CSharp
	button.Click += delegate {
		new MaterialDialog.Builder(this)
			.Title("Some title")
			.Content("Some Content")
			.PositiveText("Yes")
			.NegativeText("No")
			.Show();
	};
```

Enjoy! By the way, it's not working yet. Oops. :(