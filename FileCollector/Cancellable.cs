using System;

namespace FileCollector
{
    public abstract class Cancellable
    {
        #region Public Classes

        public class CancelledException : Exception
        {
        }

        #endregion

        #region Private Fields

        private bool cancellationRequired = false;
        private readonly object locker = new object();

        #endregion

        #region Public Properties

        public bool CancellationRequired
        {
            get
            {
                bool result;

                lock (locker)
                {
                    result = cancellationRequired;
                }

                return result;
            }

            set
            {
                lock (locker)
                {
                    cancellationRequired = value;
                }
            }
        }

        #endregion
    }
}
