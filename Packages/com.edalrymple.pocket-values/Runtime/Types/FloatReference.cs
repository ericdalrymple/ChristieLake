﻿using System;

namespace PocketValues.Types
{
    [Serializable]
    public sealed class FloatReference
    : VariableReference<FloatVariable, float>
    {
        public FloatReference() : base() { }
        public FloatReference(float initialValue) : base(initialValue) { }
    }
}
