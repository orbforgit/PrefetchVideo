using System;
using System.Collections.Generic;
using VideoOS.Platform;
using VideoOS.Platform.Client;

namespace PrefetchVideo.Client
{
    class PrefetchVideoWorkSpaceToolbarPluginInstance : WorkSpaceToolbarPluginInstance
    {
        private Item _window;

        public PrefetchVideoWorkSpaceToolbarPluginInstance()
        {
        }

        public override void Init(Item window)
        {
            _window = window;

            Title = "TapePrefetch(Orbisis)";
        }

        public override void Activate()
        {
            // Here you should put whatever action that should be executed when the toolbar button is pressed
        }

        public override void Close()
        {
        }

    }

    class PrefetchVideoWorkSpaceToolbarPlugin : WorkSpaceToolbarPlugin
    {
        public PrefetchVideoWorkSpaceToolbarPlugin()
        {
        }

        public override Guid Id
        {
            get { return PrefetchVideoDefinition.PrefetchVideoWorkSpaceToolbarPluginId; }
        }

        public override string Name
        {
            get { return "TapePrefetch(Orbisis)"; }
        }

        public override void Init()
        {
            // TODO: remove below check when PrefetchVideoDefinition.PrefetchVideoWorkSpaceToolbarPluginId has been replaced with proper GUID
            if (Id == new Guid("22222222-2222-2222-2222-222222222222"))
            {
                System.Windows.MessageBox.Show("Default GUID has not been replaced for PrefetchVideoWorkSpaceToolbarPluginId!");
            }

            WorkSpaceToolbarPlaceDefinition.WorkSpaceIds = new List<Guid>() { ClientControl.LiveBuildInWorkSpaceId, ClientControl.PlaybackBuildInWorkSpaceId, PrefetchVideoDefinition.PrefetchVideoWorkSpacePluginId };
            WorkSpaceToolbarPlaceDefinition.WorkSpaceStates = new List<WorkSpaceState>() { WorkSpaceState.Normal };
        }

        public override void Close()
        {
        }

        public override WorkSpaceToolbarPluginInstance GenerateWorkSpaceToolbarPluginInstance()
        {
            return new PrefetchVideoWorkSpaceToolbarPluginInstance();
        }
    }
}
