using System;
using System.Diagnostics;
namespace SE456
{
    public abstract class UFOMoveStrategy
    {
        abstract public void Move(UFO pUFO, float delta);
    }
}
