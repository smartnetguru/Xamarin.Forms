﻿using System.Reflection;
using System.Runtime.CompilerServices;
using Xamarin.Forms.Platform.MacOS;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

// Information about this assembly is defined by the following attributes. 
// Change them to the values specific to your project.

[assembly: AssemblyTitle("Xamarin.Forms.Platform.MacOS")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("")]
[assembly: AssemblyCopyright("rmarinho")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// The assembly version has the format "{Major}.{Minor}.{Build}.{Revision}".
// The form "{Major}.{Minor}.*" will automatically update the build and revision,
// and "{Major}.{Minor}.{Build}.*" will update just the revision.

[assembly: AssemblyVersion("1.0.*")]

// The following attributes are used to specify the signing key for the assembly, 
// if desired. See the Mono documentation for more information about signing.

//[assembly: AssemblyDelaySign(false)]
//[assembly: AssemblyKeyFile("")]
[assembly: Xamarin.Forms.Dependency(typeof(Deserializer))]
[assembly: Xamarin.Forms.Dependency(typeof(ResourcesProvider))]

[assembly: ExportImageSourceHandler(typeof(FileImageSource), typeof(FileImageSourceHandler))]
[assembly: ExportImageSourceHandler(typeof(StreamImageSource), typeof(StreamImagesourceHandler))]
[assembly: ExportImageSourceHandler(typeof(UriImageSource), typeof(ImageLoaderSourceHandler))]


[assembly: ExportRenderer(typeof(Page), typeof(PageRenderer))]
[assembly: ExportRenderer(typeof(Label), typeof(LabelRenderer))]
[assembly: ExportRenderer(typeof(Button), typeof(ButtonRenderer))]
[assembly: ExportRenderer(typeof(BoxView), typeof(BoxViewRenderer))]
[assembly: ExportRenderer(typeof(ScrollView), typeof(ScrollViewRenderer))]
[assembly: ExportRenderer(typeof(ActivityIndicator), typeof(ActivityIndicatorRenderer))]
[assembly: ExportRenderer(typeof(DatePicker), typeof(DatePickerRenderer))]
[assembly: ExportRenderer(typeof(Editor), typeof(EditorRenderer))]
[assembly: ExportRenderer(typeof(Entry), typeof(EntryRenderer))]
[assembly: ExportRenderer(typeof(Frame), typeof(FrameRenderer))]
[assembly: ExportRenderer(typeof(Image), typeof(ImageRenderer))]
[assembly: ExportRenderer(typeof(OpenGLView), typeof(OpenGLViewRenderer))]
[assembly: ExportRenderer(typeof(Picker), typeof(PickerRenderer))]
[assembly: ExportRenderer(typeof(ProgressBar), typeof(ProgressBarRenderer))]
[assembly: ExportRenderer(typeof(SearchBar), typeof(SearchBarRenderer))]
[assembly: ExportRenderer(typeof(Slider), typeof(SliderRenderer))]
[assembly: ExportRenderer(typeof(Stepper), typeof(StepperRenderer))]
[assembly: ExportRenderer(typeof(Switch), typeof(SwitchRenderer))]
[assembly: ExportRenderer(typeof(TimePicker), typeof(TimePickerRenderer))]
