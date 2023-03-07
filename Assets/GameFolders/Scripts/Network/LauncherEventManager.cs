using System;
using GameFolders.Scripts.Core;

namespace GameFolders.Scripts.Network
{
    public class LauncherEventManager : MonoSingleton<LauncherEventManager>
    {
        public Action OnJoinedLobby { get; set; }
        public Action OnCreateRoomFailed {get; set; }
        public Action OnSearchMatch { get; set; }

        private void Awake()
        {
            Singleton();
        }

    }
}
