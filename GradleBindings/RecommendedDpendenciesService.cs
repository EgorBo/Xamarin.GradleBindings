using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradleBindings
{
    public class RecommendedDpendenciesService
    {
        public async Task<IEnumerable<RecommendedDpendencyInfo>> GetAsync()
        {
            return new[]
                {
                    //from https://github.com/wasabeef/awesome-android-ui
                    //and  https://github.com/wasabeef/awesome-android-libraries
                    new RecommendedDpendencyInfo("Material Dialog", "com.afollestad:material-dialogs:0.9.4.5", "https://github.com/afollestad/material-dialogs"),
                    new RecommendedDpendencyInfo("CircularImageView", "com.pkmmte.view:circularimageview:+", "https://github.com/Pkmmte/CircularImageView"),
                    new RecommendedDpendencyInfo("RoundedImageView", "com.makeramen:roundedimageview:+", "https://github.com/vinc3m1/RoundedImageView"),
                    new RecommendedDpendencyInfo("Facebook SDK", "com.facebook.android:facebook-android-sdk:+", "https://github.com/facebook/facebook-android-sdk"),
                    new RecommendedDpendencyInfo("Picasso", "com.squareup.picasso:picasso:+", "https://github.com/square/picasso"),
                    new RecommendedDpendencyInfo("Glide", "com.github.bumptech.glide:glide:+", "https://github.com/bumptech/glide"),
                    new RecommendedDpendencyInfo("Universal Image Loader UID", "com.nostra13.universalimageloader:universal-image-loader:+", "https://github.com/nostra13/Android-Universal-Image-Loader"),
                    new RecommendedDpendencyInfo("Crouton", "de.keyboardsurfer.android.widget:crouton:+", "https://github.com/keyboardsurfer/Crouton"),
                    new RecommendedDpendencyInfo("StickyListHeaders", "se.emilsjolander:stickylistheaders:+", "https://github.com/emilsjolander/StickyListHeaders"),
                    new RecommendedDpendencyInfo("MaterialTabStrip", "com.jpardogo.materialtabstrip:library:+", "https://github.com/jpardogo/PagerSlidingTabStrip"),
                    new RecommendedDpendencyInfo("Photoview", "com.github.chrisbanes.photoview:library:+", "https://github.com/chrisbanes/PhotoView"),
                    new RecommendedDpendencyInfo("Floating Action Button FAB", "com.melnykov:floatingactionbutton:+", "https://github.com/makovkastar/FloatingActionButton"),
                    new RecommendedDpendencyInfo("ParallaxScroll", "com.github.nirhart:parallaxscroll:+", "https://github.com/nirhart/ParallaxScroll"),
                    new RecommendedDpendencyInfo("HoloColorPicker", "com.larswerkman:HoloColorPicker:+", "https://github.com/LarsWerkman/HoloColorPicker"),
                    new RecommendedDpendencyInfo("DirChooser", "net.rdrei.android.dirchooser:library:3.2@aar", "https://github.com/passy/Android-DirectoryChooser/blob/master/README.md"),
                    new RecommendedDpendencyInfo("RobotoTextView", "com.github.johnkil.android-robototextview:robototextview:+", "https://github.com/johnkil/Android-RobotoTextView"),
                    new RecommendedDpendencyInfo("SlidingUpPanel", "com.sothree.slidinguppanel:library:+", "https://github.com/umano/AndroidSlidingUpPanel"),
                    new RecommendedDpendencyInfo("Joda Time", "net.danlew:android.joda:+", "https://github.com/dlew/joda-time-android"),
                    new RecommendedDpendencyInfo("Grab-n-Run", "it.necst.grabnrun:grabnrun:+", "https://github.com/lukeFalsina/Grab-n-Run"),
                    new RecommendedDpendencyInfo("LandscapeVideoCamera", "com.github.jmolsmobile:landscapevideocamera:+", "https://github.com/JeroenMols/LandscapeVideoCamera"),
                    new RecommendedDpendencyInfo("GPUImage for Android", "jp.co.cyberagent.android.gpuimage:gpuimage-library:+", "https://github.com/CyberAgent/android-gpuimage"),
                    new RecommendedDpendencyInfo("ExoPlayer", "com.google.android.exoplayer:exoplayer:+", "https://github.com/google/ExoPlayer"),
                    new RecommendedDpendencyInfo("Calligraphy", "uk.co.chrisjenx:calligraphy:+", "https://github.com/chrisjenx/Calligraphy"),
                    new RecommendedDpendencyInfo("Tape", "com.squareup:tape:+", "https://github.com/square/tape"),
                    new RecommendedDpendencyInfo("Android Priority Job Queue", "com.path:android-priority-jobqueue:+", "https://github.com/path/android-priority-jobqueue"),
                    new RecommendedDpendencyInfo("Timber", "com.jakewharton.timber:timber:+", "https://github.com/JakeWharton/timber"),
                    new RecommendedDpendencyInfo("Otto", "com.squareup:otto:+", "https://github.com/square/otto"),
                    new RecommendedDpendencyInfo("EventBus", "de.greenrobot:eventbus:+", "https://github.com/greenrobot/EventBus"),
                    new RecommendedDpendencyInfo("SimpleNoSQL", "com.colintmiller:simplenosql:+", "https://github.com/Jearil/SimpleNoSQL"),
                    new RecommendedDpendencyInfo("Couchbase lite", "com.couchbase.lite:couchbase-lite-android:+", "https://github.com/couchbase/couchbase-lite-android"),
                    new RecommendedDpendencyInfo("SQLBrite", "com.squareup.sqlbrite:sqlbrite:+", "https://github.com/square/sqlbrite"),
                    new RecommendedDpendencyInfo("Sugar ORM", "com.github.satyan:sugar:+", "https://github.com/satyan/sugar"),
                    new RecommendedDpendencyInfo("Fresco", "com.facebook.fresco:fresco:+", "https://github.com/facebook/fresco"),
                    new RecommendedDpendencyInfo("Ion", "com.koushikdutta.ion:ion:+", "https://github.com/koush/ion#get-ion"),
                    new RecommendedDpendencyInfo("Basic HTTP Client", "com.turbomanage.basic-http-client:http-client-android:+", "https://github.com/turbomanage/basic-http-client"),
                    new RecommendedDpendencyInfo("Retrofit", "com.squareup.retrofit:retrofit:+", "https://github.com/square/retrofit"),
                    new RecommendedDpendencyInfo("OkHttp", "com.squareup.okhttp:okhttp:+", "https://github.com/square/okhttp"),
                    new RecommendedDpendencyInfo("Material Design Android Library", "com.github.navasmdc:MaterialDesign:+@aar", "https://github.com/navasmdc/MaterialDesignLibrary"),
                    new RecommendedDpendencyInfo("MaterialTabs", "it.neokree:MaterialTabs:+", "https://github.com/neokree/MaterialTabs"),
                    new RecommendedDpendencyInfo("Android PagerSlidingTabStrip ", "com.jpardogo.materialtabstrip:library:+", "https://github.com/jpardogo/PagerSlidingTabStrip"),
                    new RecommendedDpendencyInfo("Material Ripple Layout", "com.balysv:material-ripple:+", "https://github.com/balysv/material-ripple"),
                    new RecommendedDpendencyInfo("RippleEffect", "com.github.traex.rippleeffect:library:+", "https://github.com/traex/RippleEffect"),
                    new RecommendedDpendencyInfo("LDrawer", "com.ikimuhendis:ldrawer:+", "https://github.com/ikimuhendis/LDrawer"),
                    new RecommendedDpendencyInfo("AlertDialogPro", "com.github.fengdai:alertdialogpro-theme-material:+", "https://github.com/fengdai/AlertDialogPro"),
                    new RecommendedDpendencyInfo("MaterialNavigationDrawer", "it.neokree:MaterialNavigationDrawer:+", "https://github.com/neokree/MaterialNavigationDrawer"),
                    new RecommendedDpendencyInfo("Material-ish Progress", "com.pnikosis:materialish-progress:+", "https://github.com/pnikosis/materialish-progress"),
                    new RecommendedDpendencyInfo("CircularReveal", "com.github.ozodrukh:CircularReveal:2.0.1@aar", "https://github.com/ozodrukh/CircularReveal"),
                    new RecommendedDpendencyInfo("MaterialRangeBar", "com.appyvet:materialrangebar:+", "https://github.com/oli107/material-range-bar"),
                    new RecommendedDpendencyInfo("Carbon", "tk.zielony:carbon:+", "https://github.com/ZieIony/Carbon"),
                    new RecommendedDpendencyInfo("Material Calendar View", "com.prolificinteractive:material-calendarview:+", "https://github.com/prolificinteractive/material-calendarview"),
                    new RecommendedDpendencyInfo("rey5137 material", "com.github.rey5137:material:+", "https://github.com/rey5137/material"),
                    new RecommendedDpendencyInfo("Android Swipe Layout", "com.daimajia.swipelayout:library:+@aar", "https://github.com/daimajia/AndroidSwipeLayout"),
                    new RecommendedDpendencyInfo("ExpandableLayout", "com.github.traex.expandablelayout:library:+", "https://github.com/traex/ExpandableLayout"),
                    new RecommendedDpendencyInfo("PullRefreshLayout", "com.baoyz.pullrefreshlayout:library:+", "https://github.com/baoyongzhang/android-PullRefreshLayout"),
                    new RecommendedDpendencyInfo("TileView", "com.qozix:TileView:+", "https://github.com/moagrius/TileView"),
                    new RecommendedDpendencyInfo("Draggable Panel", "com.github.pedrovgs:draggablepanel:+", "https://github.com/pedrovgs/DraggablePanel"),
                    new RecommendedDpendencyInfo("Slidr", "com.r0adkll:slidableactivity:+", "https://github.com/r0adkll/Slidr"),
                    new RecommendedDpendencyInfo("Phoenix Pull-to-Refresh", "com.yalantis:phoenix:+", "https://github.com/Yalantis/Phoenix"),
                    new RecommendedDpendencyInfo("ArcLayout", "com.ogaclejapan.arclayout:library:+@aar", "https://github.com/ogaclejapan/ArcLayout"),
                    new RecommendedDpendencyInfo("Dragger", "com.github.ppamorim:dragger:+", "https://github.com/ppamorim/Dragger"),
                    new RecommendedDpendencyInfo("PhysicsLayout", "com.jawnnypoo:physicslayout:+", "https://github.com/Jawnnypoo/PhysicsLayout"),
                    new RecommendedDpendencyInfo("Bubbles for Android", "com.txusballesteros:bubbles:+", "https://github.com/txusballesteros/bubbles-for-android"),
                    new RecommendedDpendencyInfo("AndroidSlidingUpPanel", "com.sothree.slidinguppanel:library:+", "https://github.com/umano/AndroidSlidingUpPanel"),
                    new RecommendedDpendencyInfo("circular-progress-button", "com.github.dmytrodanylyk.circular-progress-button:library:+", "https://github.com/dmytrodanylyk/circular-progress-button"),
                    new RecommendedDpendencyInfo("android-process-button", "com.github.dmytrodanylyk.android-process-button:library:+", "https://github.com/dmytrodanylyk/android-process-button"),
                    new RecommendedDpendencyInfo("android-circlebutton", "com.github.markushi:circlebutton:+", "https://github.com/markushi/android-circlebutton"),
                    new RecommendedDpendencyInfo("Moving Button", "com.thefinestartist:movingbutton:+", "https://github.com/TheFinestArtist/MovingButton"),
                    //...
                }.OrderBy(i => i.Name);
        }
    }

    public class RecommendedDpendencyInfo
    {
        public string Name { get; set; }

        public string DependencyId { get; set; }

        public string Url { get; set; }

        public RecommendedDpendencyInfo(string name, string dependencyId, string url)
        {
            Name = name;
            DependencyId = dependencyId;
            Url = url;
        }

        public override string ToString()
        {
            return Name + " " + DependencyId;
        }
    }
}
