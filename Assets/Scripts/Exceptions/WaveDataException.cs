using System;
using UnityEngine;

namespace Exceptions
{
    /// <summary>
    /// Custom exception for wave configuration errors
    /// </summary>
    public class WaveDataException : Exception
    {
        public WaveDataException(string message, Exception inner)
            : base(message, inner) { }

        public WaveDataException(string message)
            : base(message) { }
    }
}