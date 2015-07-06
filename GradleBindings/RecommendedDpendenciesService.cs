using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradleBindings
{
    public class RecommendedDpendenciesService
    {
        public async Task<IEnumerable<RecommendedDpendencyInfo>> GetAsync()
        {
            //TODO: create a web service

            return new[]
                {
                    //from https://github.com/wasabeef/awesome-android-ui
                    //and  https://github.com/wasabeef/awesome-android-libraries
                    new RecommendedDpendencyInfo("Material Dialog", "com.afollestad:material-dialogs:0.7.6.0", "https://github.com/afollestad/material-dialogs"),
                    new RecommendedDpendencyInfo("CircularImageView", "com.pkmmte.view:circularimageview:1.1", "https://github.com/Pkmmte/CircularImageView"),
                    new RecommendedDpendencyInfo("RoundedImageView", "com.makeramen:roundedimageview:2.1.0", "https://github.com/vinc3m1/RoundedImageView"),
                    new RecommendedDpendencyInfo("Facebook SDK", "com.facebook.android:facebook-android-sdk:4.1.0", "https://github.com/facebook/facebook-android-sdk"),
                    new RecommendedDpendencyInfo("Picasso", "com.squareup.picasso:picasso:2.5.2", "https://github.com/square/picasso"),
                    new RecommendedDpendencyInfo("Glide", "com.github.bumptech.glide:glide", "https://github.com/bumptech/glide"),
                    new RecommendedDpendencyInfo("Universal Image Loader UID", "com.nostra13.universalimageloader:universal-image-loader:1.9.4", "https://github.com/nostra13/Android-Universal-Image-Loader"),
                    new RecommendedDpendencyInfo("Crouton", "de.keyboardsurfer.android.widget:crouton:1.8.5", "https://github.com/keyboardsurfer/Crouton"),
                    new RecommendedDpendencyInfo("StickyListHeaders", "se.emilsjolander:stickylistheaders:+", "https://github.com/emilsjolander/StickyListHeaders"),
                    new RecommendedDpendencyInfo("MaterialTabStrip", "com.jpardogo.materialtabstrip:library:1.1.0", "https://github.com/jpardogo/PagerSlidingTabStrip"),
                    new RecommendedDpendencyInfo("Photoview", "com.github.chrisbanes.photoview:library:1.2.4", "https://github.com/chrisbanes/PhotoView"),
                    new RecommendedDpendencyInfo("Floating Action Button FAB", "com.melnykov:floatingactionbutton:1.3.0", "https://github.com/makovkastar/FloatingActionButton"),
                    new RecommendedDpendencyInfo("ParallaxScroll", "com.github.nirhart:parallaxscroll:1.0", "https://github.com/nirhart/ParallaxScroll"),
                    new RecommendedDpendencyInfo("HoloColorPicker", "com.larswerkman:HoloColorPicker:1.5", "https://github.com/LarsWerkman/HoloColorPicker"),
                    new RecommendedDpendencyInfo("DirChooser", "net.rdrei.android.dirchooser:library:2.1", "https://github.com/passy/Android-DirectoryChooser/blob/master/README.md"),
                    new RecommendedDpendencyInfo("RobotoTextView", "com.github.johnkil.android-robototextview:robototextview:2.4.0", "https://github.com/johnkil/Android-RobotoTextView"),
                    new RecommendedDpendencyInfo("SlidingUpPanel", "com.sothree.slidinguppanel:library:3.0.0", "https://github.com/umano/AndroidSlidingUpPanel"),
                    new RecommendedDpendencyInfo("Joda Time", "net.danlew:android.joda:2.8.1", "https://github.com/dlew/joda-time-android"),
                    new RecommendedDpendencyInfo("Grab-n-Run", "it.necst.grabnrun:grabnrun:1.0.2", "https://github.com/lukeFalsina/Grab-n-Run"),
                    new RecommendedDpendencyInfo("EasyCamera", "net.bozho.easycamera:easycamera:0.0.1:aar@aar", "https://github.com/Glamdring/EasyCamera"),
                    new RecommendedDpendencyInfo("LandscapeVideoCamera", "com.github.jmolsmobile:landscapevideocamera:1.0.7", "https://github.com/JeroenMols/LandscapeVideoCamera"),
                    new RecommendedDpendencyInfo("GPUImage for Android", "jp.co.cyberagent.android.gpuimage:gpuimage-library:1.2.3", "https://github.com/CyberAgent/android-gpuimage"),
                    new RecommendedDpendencyInfo("ExoPlayer", "com.google.android.exoplayer:exoplayer:+", "https://github.com/google/ExoPlayer"),
                    new RecommendedDpendencyInfo("Calligraphy", "uk.co.chrisjenx:calligraphy:2.1.0", "https://github.com/chrisjenx/Calligraphy"),
                    new RecommendedDpendencyInfo("Tape", "com.squareup:tape:1.2.3", "https://github.com/square/tape"),
                    new RecommendedDpendencyInfo("Android Priority Job Queue", "com.path:android-priority-jobqueue:1.1.2", "https://github.com/path/android-priority-jobqueue"),
                    new RecommendedDpendencyInfo("Timber", "com.jakewharton.timber:timber:3.1.0", "https://github.com/JakeWharton/timber"),
                    new RecommendedDpendencyInfo("Otto", "com.squareup:otto:1.3.8", "https://github.com/square/otto"),
                    new RecommendedDpendencyInfo("EventBus", "de.greenrobot:eventbus:2.4.0", "https://github.com/greenrobot/EventBus"),
                    new RecommendedDpendencyInfo("SimpleNoSQL", "com.colintmiller:simplenosql:0.5", "https://github.com/Jearil/SimpleNoSQL"),
                    new RecommendedDpendencyInfo("Realm", "io.realm:realm-android:0.82.0-SNAPSHOT", "https://github.com/realm/realm-java"),
                    new RecommendedDpendencyInfo("Couchbase lite", "com.couchbase.lite:couchbase-lite-android:+", "https://github.com/couchbase/couchbase-lite-android"),
                    new RecommendedDpendencyInfo("SQLBrite", "com.squareup.sqlbrite:sqlbrite:0.2.0", "https://github.com/square/sqlbrite"),
                    new RecommendedDpendencyInfo("Sugar ORM", "com.github.satyan:sugar:1.3.1", "https://github.com/satyan/sugar"),
                    new RecommendedDpendencyInfo("Fresco", "com.facebook.fresco:fresco:0.5.3+", "https://github.com/facebook/fresco"),
                    new RecommendedDpendencyInfo("Ion", "com.koushikdutta.ion:ion:2.+", "https://github.com/koush/ion#get-ion"),
                    new RecommendedDpendencyInfo("Basic HTTP Client", "com.turbomanage.basic-http-client:http-client-android:0.89", "https://github.com/turbomanage/basic-http-client"),
                    new RecommendedDpendencyInfo("Retrofit", "com.squareup.retrofit:retrofit:1.9.0", "https://github.com/square/retrofit"),
                    new RecommendedDpendencyInfo("OkHttp", "com.squareup.okhttp:okhttp:2.4.0", "https://github.com/square/okhttp"),
                    new RecommendedDpendencyInfo("Material Design Android Library", "com.github.navasmdc:MaterialDesign:1.5@aar", "https://github.com/navasmdc/MaterialDesignLibrary"),
                    new RecommendedDpendencyInfo("MaterialTabs", "it.neokree:MaterialTabs:0.11", "https://github.com/neokree/MaterialTabs"),
                    new RecommendedDpendencyInfo("Android PagerSlidingTabStrip ", "com.jpardogo.materialtabstrip:library:1.1.0", "https://github.com/jpardogo/PagerSlidingTabStrip"),
                    new RecommendedDpendencyInfo("Material Ripple Layout", "com.balysv:material-ripple:1.0.2", "https://github.com/balysv/material-ripple"),
                    new RecommendedDpendencyInfo("RippleEffect", "com.github.traex.rippleeffect:library:1.3", "https://github.com/traex/RippleEffect"),
                    new RecommendedDpendencyInfo("LDrawer", "com.ikimuhendis:ldrawer:0.1", "https://github.com/ikimuhendis/LDrawer"),
                    new RecommendedDpendencyInfo("AlertDialogPro", "com.github.fengdai:alertdialogpro-theme-material:0.2.3", "https://github.com/fengdai/AlertDialogPro"),
                    new RecommendedDpendencyInfo("MaterialNavigationDrawer", "it.neokree:MaterialNavigationDrawer:1.3.3", "https://github.com/neokree/MaterialNavigationDrawer"),
                    new RecommendedDpendencyInfo("Material-ish Progress", "com.pnikosis:materialish-progress:1.5", "https://github.com/pnikosis/materialish-progress"),
                    new RecommendedDpendencyInfo("CircularReveal", "com.github.ozodrukh:CircularReveal:1.1.0@aar", "https://github.com/ozodrukh/CircularReveal"),
                    new RecommendedDpendencyInfo("MaterialRangeBar", "com.appyvet:materialrangebar:1.0", "https://github.com/oli107/material-range-bar"),
                    new RecommendedDpendencyInfo("Carbon", "tk.zielony:carbon:0.8.0", "https://github.com/ZieIony/Carbon"),
                    new RecommendedDpendencyInfo("Material Calendar View", "com.prolificinteractive:material-calendarview:0.6.1", "https://github.com/prolificinteractive/material-calendarview"),
                    new RecommendedDpendencyInfo("rey5137 material", "com.github.rey5137:material:1.1.1", "https://github.com/rey5137/material"),
                    new RecommendedDpendencyInfo("Android Swipe Layout", "com.daimajia.swipelayout:library:1.2.0@aar", "https://github.com/daimajia/AndroidSwipeLayout"),
                    new RecommendedDpendencyInfo("ExpandableLayout", "com.github.traex.expandablelayout:library:1.2.2", "https://github.com/traex/ExpandableLayout"),
                    new RecommendedDpendencyInfo("PullRefreshLayout", "com.baoyz.pullrefreshlayout:library:1.0.1", "https://github.com/baoyongzhang/android-PullRefreshLayout"),
                    new RecommendedDpendencyInfo("TileView", "com.qozix:TileView:1.0.13", "https://github.com/moagrius/TileView"),
                    new RecommendedDpendencyInfo("Draggable Panel", "com.github.pedrovgs:draggablepanel:1.8", "https://github.com/pedrovgs/DraggablePanel"),
                    new RecommendedDpendencyInfo("Slidr", "com.r0adkll:slidableactivity:2.0.3", "https://github.com/r0adkll/Slidr"),
                    new RecommendedDpendencyInfo("Phoenix Pull-to-Refresh", "com.yalantis:phoenix:1.2.1", "https://github.com/Yalantis/Phoenix"),
                    new RecommendedDpendencyInfo("ArcLayout", "com.ogaclejapan.arclayout:library:1.0.1@aar", "https://github.com/ogaclejapan/ArcLayout"),
                    new RecommendedDpendencyInfo("Dragger", "com.github.ppamorim:dragger:1.1", "https://github.com/ppamorim/Dragger"),
                    new RecommendedDpendencyInfo("PhysicsLayout", "com.jawnnypoo:physicslayout:1.0.0", "https://github.com/Jawnnypoo/PhysicsLayout"),
                    new RecommendedDpendencyInfo("Bubbles for Android", "com.txusballesteros:bubbles:1.0", "https://github.com/txusballesteros/bubbles-for-android"),
                    new RecommendedDpendencyInfo("AndroidSlidingUpPanel", "com.sothree.slidinguppanel:library:3.0.0", "https://github.com/umano/AndroidSlidingUpPanel"),
                    new RecommendedDpendencyInfo("circular-progress-button", "com.github.dmytrodanylyk.circular-progress-button:library:1.1.3", "https://github.com/dmytrodanylyk/circular-progress-button"),
                    new RecommendedDpendencyInfo("android-process-button", "com.github.dmytrodanylyk.android-process-button:library:1.0.4", "https://github.com/dmytrodanylyk/android-process-button"),
                    new RecommendedDpendencyInfo("android-circlebutton", "com.github.markushi:circlebutton:1.1", "https://github.com/markushi/android-circlebutton"),
                    new RecommendedDpendencyInfo("Moving Button", "com.thefinestartist:movingbutton:1.0.0", "https://github.com/TheFinestArtist/MovingButton"),
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
