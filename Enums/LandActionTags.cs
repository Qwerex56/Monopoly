using System;

namespace Monopoly.Enums;

[Flags]
public enum LandActionTags {
    None = 0,
    FreeLand = 1 << 0,
    PropertyLand = 1 << 1,
    EventLand = 1 << 2,
}