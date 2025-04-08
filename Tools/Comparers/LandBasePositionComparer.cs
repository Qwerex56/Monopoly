using System.Collections.Generic;
using Monopoly.ResourcesModels;
using Monopoly.Scenes.Lands;

namespace Monopoly.Tools.Comparers;

public class LandBasePositionComparer : IComparer<LandBase> {
    public int Compare(LandBase? x, LandBase? y) {
        return x.Resource.Position.CompareTo(y.Resource.Position);
    }
}