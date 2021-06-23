using System;

namespace Barcoder.DataMatrix
{
    public enum CodeSizes
    {
        DM_10x10, DM_12x12, DM_14x14, DM_16x16, DM_18x18, DM_20x20, DM_22x22, DM_24x24, DM_26x26, DM_32x32, DM_36x36,
        DM_40x40, DM_44x44, DM_48x48, DM_52x52, DM_64x64, DM_72x72, DM_80x80, DM_88x88, DM_96x96, DM_104x104, DM_120x120,
        DM_132x132, DM_144x144,

        DM_8x18, DM_8x32, DM_12x26
    }

    internal static class CodeSizesExtensions
    {
        public static CodeSize GetCodeSize(this CodeSizes codeSize)
        {
            return codeSize switch
            {
                CodeSizes.DM_10x10 => new CodeSize(10, 10, 1, 1, 5, 1),
                CodeSizes.DM_12x12 => new CodeSize(12, 12, 1, 1, 7, 1),
                CodeSizes.DM_14x14 => new CodeSize(14, 14, 1, 1, 10, 1),
                CodeSizes.DM_16x16 => new CodeSize(16, 16, 1, 1, 12, 1),
                CodeSizes.DM_18x18 => new CodeSize(18, 18, 1, 1, 14, 1),
                CodeSizes.DM_20x20 => new CodeSize(20, 20, 1, 1, 18, 1),
                CodeSizes.DM_22x22 => new CodeSize(22, 22, 1, 1, 20, 1),
                CodeSizes.DM_24x24 => new CodeSize(24, 24, 1, 1, 24, 1),
                CodeSizes.DM_26x26 => new CodeSize(26, 26, 1, 1, 28, 1),
                CodeSizes.DM_32x32 => new CodeSize(32, 32, 2, 2, 36, 1),
                CodeSizes.DM_36x36 => new CodeSize(36, 36, 2, 2, 42, 1),
                CodeSizes.DM_40x40 => new CodeSize(40, 40, 2, 2, 48, 1),
                CodeSizes.DM_44x44 => new CodeSize(44, 44, 2, 2, 56, 1),
                CodeSizes.DM_48x48 => new CodeSize(48, 48, 2, 2, 68, 1),
                CodeSizes.DM_52x52 => new CodeSize(52, 52, 2, 2, 84, 2),
                CodeSizes.DM_64x64 => new CodeSize(64, 64, 4, 4, 112, 2),
                CodeSizes.DM_72x72 => new CodeSize(72, 72, 4, 4, 144, 4),
                CodeSizes.DM_80x80 => new CodeSize(80, 80, 4, 4, 192, 4),
                CodeSizes.DM_88x88 => new CodeSize(88, 88, 4, 4, 224, 4),
                CodeSizes.DM_96x96 => new CodeSize(96, 96, 4, 4, 272, 4),
                CodeSizes.DM_104x104 => new CodeSize(104, 104, 4, 4, 336, 6),
                CodeSizes.DM_120x120 => new CodeSize(120, 120, 6, 6, 408, 6),
                CodeSizes.DM_132x132 => new CodeSize(132, 132, 6, 6, 496, 8),
                CodeSizes.DM_144x144 => new CodeSize(144, 144, 6, 6, 620, 10),

                CodeSizes.DM_8x18 => new CodeSize(8, 18, 1, 1, 7, 1),
                CodeSizes.DM_8x32 => new CodeSize(8, 32, 2, 1, 11, 1),
                CodeSizes.DM_12x26 => new CodeSize(12, 26, 1, 1, 14, 1),

                _ => throw new NotSupportedException()
            };
        }
    }
}
