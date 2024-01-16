using VideoOS.Platform.Client;

namespace PrefetchVideo.Client
{
    public class PrefetchVideoWorkSpaceViewItemManager : ViewItemManager
    {
        public PrefetchVideoWorkSpaceViewItemManager() : base("PrefetchVideoWorkSpaceViewItemManager")
        {
        }

        public override ViewItemWpfUserControl GenerateViewItemWpfUserControl()
        {
            return new PrefetchVideoWorkSpaceViewItemWpfUserControl();
        }
    }
}
