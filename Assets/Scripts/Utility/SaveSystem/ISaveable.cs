﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace bowen.Saving
{
    public interface ISaveable
    {
        object CaptureState();
        void RestoreState(object state);
    }
}

