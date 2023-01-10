# StreamDeckSharp

## Overview

This project is intended to provide quick and easy scaffolding of a basic StreamDeck Plugin.  It is not a full featured plugin, and has limitations to what it can do.  However, it is a nice easy way to get started, and provides a relatively decent stepping off point for more full featured capabilities.

## QuickStart

* Create a new console project
  * The name of the project is important: [ProjectName]
* Add a reference to StreamDeckSharp
* Set the post-build event to: `$(TargetDir)$(TargetName).exe -checkConfiguration -developmentDeploy -releaseBundle`
* Setup your project configuration in Program.cs:
    ``` 
    using StreamDeckSharp;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    namespace [ProjectName]
    {
        public class Program
        {
            protected Program()
            {
            }

            public static async Task Main(string[] args)
            {
                var plugin = new StreamDeckPlugin<Program>(args);
                await plugin.Run(new StreamDeckConfig
                {
                    PluginAuthor = "...",
                    PluginDescription = "...",
                    PluginName = "...",
                    PluginIcon = "images/Plugin",
                    PluginUrl = "...",
                    PluginVersion = "1.0.0",
                    CategoryName = "StreamDeckSharp Tools",
                    CategoryIcon = "images/Category",
                    ActionId = "com.StreamDeckSharp.streamdeck.[ProjectName]",
                    ActionIcon = "images/Action",
                    ActionName = "...",
                    ActionDescription = "...",
                    UpdateFrequencySeconds = 30,
                    StateIcons = new Dictionary<string, string> {
                        { "Initializing" , "images/State" },
                        { "Down" , "images/StateDown"},
                        { "Up" , "images/StateUp"},
                        { "Unknown" , "images/StateUnknown"}
                    },
                    Configurations = new Dictionary<string, string> {
                        { "Access Key Id", "AwsAccessKeyId" },
                        { "Access Key Secret", "AwsAccessKeySecret"},
                        { "Region", "AwsRegion"},
                        { "Alarm Name", "AwsAlarmName"}
                    },
                    DistributionTool = "..\\DistributionTool.exe"
                });
            }
        }
    }
    ```
* Add an Images folder, and add all necessary images (referenced in the project configuration)
  * Be sure all image properties are set to `Build Action = "Content"` and `Copy to Output Directory = "Copy if newer"`
* Build.   This initial build will fail with an error stating that you need to rebuild.   This build will auto-generate several assets needed for deployment.
* Build again.   This build will build and deploy your new StreamDeck Plugin.  It will do nothing at this time.
* Implement the generated `[ProjectName]Action.cs` and `[ProjectName]Service.cs` files to provide the behaviors you want to see in StreamDeck.
* Build, run and enjoy!

## Details

* State Icons should be 72x72 pixel png's, with an @2x version at 144x144px.
* Category and Action Icons should be 28x28px png's with an @2x version at 56x56px.
