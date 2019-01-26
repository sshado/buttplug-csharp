using System;

using Buttplug.Client.Platforms.Bluetooth.Composition ;

using PostSharp.Patterns.Model ;

namespace Buttplug.Client.Platforms.Bluetooth
{
    internal sealed class Controller : IDisposable
    {
        [ Reference ]
        public ControllerState State { get ; set ; }

        public void Dispose ()
        {
            if ( Activated () )
            {
                Terminate () ;
            }
        }

        private bool Activated () => this.State == ControllerState.Activated ;

        private void Terminate ()
        {
            this.ReleaseUnmanagedMemory();
            GC.SuppressFinalize(this);
        }

        private void ReleaseUnmanagedMemory ()
        {
            // Currently none.
        }

        /// <inheritdoc />
        ~Controller()
        {
            this.ReleaseUnmanagedMemory();
        }
    }
}
