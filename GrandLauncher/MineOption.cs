using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrandLauncher
{
    public class MineOption
    {
        string log_path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland\\journal.log";

        Process Minecraft;

        public void StartMinecraft(int memory, string username, string modpackpath)
        {
            string java = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland\\java_x64\\jre1.8.0_331\\bin\\javaw.exe";

            string dot_minecraft = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland\\minecraft\\Modern_War";
            string lib_path = dot_minecraft + "\\libraries\\";

            string JavaParameters = $"-Xmx{memory * 1024}M";
            string Addition = "-XX:HeapDumpPath=MojangTricksIntelDriversForPerformance_javaw.exe_minecraft.exe.heapdump";
            string Java_Natives = $"-Djava.library.path=\"{dot_minecraft}/natives/\"";
            string Org_Natives = $"-Dorg.lwjgl.librarypath=\"{dot_minecraft}/natives/\"";
            string Libraries = $"-cp \"{lib_path}net/minecraftforge/forge/1.12.2-14.23.5.2860/forge-1.12.2-14.23.5.2860.jar;{lib_path}org/ow2/asm/asm-debug-all/5.2/asm-debug-all-5.2.jar;{lib_path}net/minecraft/launchwrapper/1.12/launchwrapper-1.12.jar;{lib_path}org/jline/jline/3.5.1/jline-3.5.1.jar;{lib_path}com/typesafe/akka/akka-actor_2.11/2.3.3/akka-actor_2.11-2.3.3.jar;{lib_path}com/typesafe/config/1.2.1/config-1.2.1.jar;{lib_path}org/scala-lang/scala-actors-migration_2.11/1.1.0/scala-actors-migration_2.11-1.1.0.jar;{lib_path}org/scala-lang/scala-compiler/2.11.1/scala-compiler-2.11.1.jar;{lib_path}org/scala-lang/plugins/scala-continuations-library_2.11/1.0.2_mc/scala-continuations-library_2.11-1.0.2_mc.jar;{lib_path}org/scala-lang/plugins/scala-continuations-plugin_2.11.1/1.0.2_mc/scala-continuations-plugin_2.11.1-1.0.2_mc.jar;{lib_path}org/scala-lang/scala-library/2.11.1/scala-library-2.11.1.jar;{lib_path}org/scala-lang/scala-parser-combinators_2.11/1.0.1/scala-parser-combinators_2.11-1.0.1.jar;{lib_path}org/scala-lang/scala-reflect/2.11.1/scala-reflect-2.11.1.jar;{lib_path}org/scala-lang/scala-swing_2.11/1.0.1/scala-swing_2.11-1.0.1.jar;{lib_path}org/scala-lang/scala-xml_2.11/1.0.2/scala-xml_2.11-1.0.2.jar;{lib_path}lzma/lzma/0.0.1/lzma-0.0.1.jar;{lib_path}java3d/vecmath/1.5.2/vecmath-1.5.2.jar;{lib_path}net/sf/trove4j/trove4j/3.0.3/trove4j-3.0.3.jar;{lib_path}org/apache/maven/maven-artifact/3.5.3/maven-artifact-3.5.3.jar;{lib_path}net/sf/jopt-simple/jopt-simple/5.0.3/jopt-simple-5.0.3.jar;{lib_path}org/apache/logging/log4j/log4j-api/2.15.0/log4j-api-2.15.0.jar;{lib_path}org/apache/logging/log4j/log4j-core/2.15.0/log4j-core-2.15.0.jar;{lib_path}com/mojang/patchy/1.3.9/patchy-1.3.9.jar;{lib_path}oshi-project/oshi-core/1.1/oshi-core-1.1.jar;{lib_path}net/java/dev/jna/jna/4.4.0/jna-4.4.0.jar;{lib_path}net/java/dev/jna/platform/3.4.0/platform-3.4.0.jar;{lib_path}com/ibm/icu/icu4j-core-mojang/51.2/icu4j-core-mojang-51.2.jar;{lib_path}net/sf/jopt-simple/jopt-simple/5.0.3/jopt-simple-5.0.3.jar;{lib_path}com/paulscode/codecjorbis/20101023/codecjorbis-20101023.jar;{lib_path}com/paulscode/codecwav/20101023/codecwav-20101023.jar;{lib_path}com/paulscode/libraryjavasound/20101123/libraryjavasound-20101123.jar;{lib_path}com/paulscode/librarylwjglopenal/20100824/librarylwjglopenal-20100824.jar;{lib_path}com/paulscode/soundsystem/20120107/soundsystem-20120107.jar;{lib_path}io/netty/netty-all/4.1.9.Final/netty-all-4.1.9.Final.jar;{lib_path}com/google/guava/guava/21.0/guava-21.0.jar;{lib_path}org/apache/commons/commons-lang3/3.5/commons-lang3-3.5.jar;{lib_path}commons-io/commons-io/2.5/commons-io-2.5.jar;{lib_path}commons-codec/commons-codec/1.10/commons-codec-1.10.jar;{lib_path}net/java/jinput/jinput/2.0.5/jinput-2.0.5.jar;{lib_path}net/java/jutils/jutils/1.0.0/jutils-1.0.0.jar;{lib_path}com/google/code/gson/gson/2.8.0/gson-2.8.0.jar;{lib_path}com/mojang/authlib/1.5.25/authlib-1.5.25.jar;{lib_path}com/mojang/realms/1.10.22/realms-1.10.22.jar;{lib_path}org/apache/commons/commons-compress/1.8.1/commons-compress-1.8.1.jar;{lib_path}org/apache/httpcomponents/httpclient/4.3.3/httpclient-4.3.3.jar;{lib_path}commons-logging/commons-logging/1.1.3/commons-logging-1.1.3.jar;{lib_path}org/apache/httpcomponents/httpcore/4.3.2/httpcore-4.3.2.jar;{lib_path}it/unimi/dsi/fastutil/7.1.0/fastutil-7.1.0.jar;{lib_path}org/apache/logging/log4j/log4j-api/2.8.1/log4j-api-2.8.1.jar;{lib_path}org/apache/logging/log4j/log4j-core/2.8.1/log4j-core-2.8.1.jar;{lib_path}org/lwjgl/lwjgl/lwjgl/2.9.4-nightly-20150209/lwjgl-2.9.4-nightly-20150209.jar;{lib_path}org/lwjgl/lwjgl/lwjgl_util/2.9.4-nightly-20150209/lwjgl_util-2.9.4-nightly-20150209.jar;{lib_path}org/lwjgl/lwjgl/lwjgl-platform/2.9.4-nightly-20150209/lwjgl-platform-2.9.4-nightly-20150209.jar;{lib_path}org/lwjgl/lwjgl/lwjgl-platform/2.9.4-nightly-20150209/lwjgl-platform-2.9.4-nightly-20150209-natives-windows.jar;{lib_path}org/lwjgl/lwjgl/lwjgl/2.9.2-nightly-20140822/lwjgl-2.9.2-nightly-20140822.jar;{lib_path}org/lwjgl/lwjgl/lwjgl_util/2.9.2-nightly-20140822/lwjgl_util-2.9.2-nightly-20140822.jar;{lib_path}org/lwjgl/lwjgl/lwjgl-platform/2.9.2-nightly-20140822/lwjgl-platform-2.9.2-nightly-20140822-natives-windows.jar;{lib_path}net/java/jinput/jinput-platform/2.0.5/jinput-platform-2.0.5-natives-windows.jar;{lib_path}com/mojang/text2speech/1.10.3/text2speech-1.10.3.jar;{lib_path}com/mojang/text2speech/1.10.3/text2speech-1.10.3.jar;{lib_path}com/mojang/text2speech/1.10.3/text2speech-1.10.3-natives-windows.jar;{lib_path}ca/weblite/java-objc-bridge/1.0.0/java-objc-bridge-1.0.0.jar;{dot_minecraft}/versions/Forge 1.12.2/Forge 1.12.2.jar;\"";
            //com/mojang/text2speech/1.10.3/text2speech-1.10.3-sources.jar
            string JVM = "";
            string ClassName = $"net.minecraft.launchwrapper.Launch"; //net.minecraft.launchwrapper.Launch
            string WindowSize = "--width 1200 --height 600";
            string User = $"--username {username}";
            string Version = $"--version Forge 1.12.2"; //1.12.2-forge1.12.2-14.23.5.2768
            string GameDir = $"--gameDir \"{dot_minecraft}\"";
            string AssetsDir = $"--assetsDir \"{dot_minecraft}/assets/\"";
            string AssetIndex = "--assetIndex 1.12";
            string UUID = $"--uuid uuu";
            string AccessToken = $"--accessToken uuu";
            string Other = $"--userType mojang --tweakClass net.minecraftforge.fml.common.launcher.FMLTweaker --versionType Forge";

            string args = $"{JavaParameters} {Addition} {Java_Natives} {Org_Natives} " +
                $"{Libraries} {JVM} {ClassName} {WindowSize} {User} {Version} {GameDir} {AssetsDir} " +
                $"{AssetIndex} {UUID} {AccessToken} {Other}";


            ProcessStartInfo info = new ProcessStartInfo()
            {
                FileName = java,
                Arguments = args,
                CreateNoWindow = true,
                RedirectStandardError = true,
                UseShellExecute = false
            };

            Minecraft = Process.Start(info);
            Minecraft.BeginErrorReadLine();
            Minecraft.ErrorDataReceived += Minecraft_ErrorDataReceived;
        }

        void Minecraft_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!File.Exists(log_path)) File.Create(log_path).Dispose();
            File.AppendAllText(log_path, e.Data + Environment.NewLine);
        }
    }
}
