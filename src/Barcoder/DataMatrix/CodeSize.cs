namespace Barcoder.DataMatrix
{
    internal sealed class CodeSize
    {
        public CodeSize(int rows, int columns, int regionCountHorizontal, int regionCountVertical, int eccCount, int blockCount)
        {
            Rows = rows;
            Columns = columns;
            RegionCountHorizontal = regionCountHorizontal;
            RegionCountVertical = regionCountVertical;
            EccCount = eccCount;
            BlockCount = blockCount;
        }

        public int Rows { get; }
        public int Columns { get; }
        public int RegionCountHorizontal { get; }
        public int RegionCountVertical { get; }
        public int EccCount { get; }
        public int BlockCount { get; }
        public int RegionRows => (Rows - (RegionCountVertical * 2)) / RegionCountVertical;
        public int RegionColumns => (Columns - (RegionCountHorizontal * 2)) / RegionCountHorizontal;
        public int MatrixRows => RegionRows * RegionCountVertical;
        public int MatrixColumns => RegionColumns * RegionCountHorizontal;
        public int DataCodewords => ((MatrixColumns * MatrixRows) / 8) - EccCount;

        public int DataCodewordsForBlock(int index)
        {
            if (Rows == 144 && Columns == 144)
            {
                // Special case...
                if (index < 8)
                    return 156;

                return 155;
            }

            return DataCodewords / BlockCount;
        }

        public int ErrorCorrectionCodewordsPerBlock => EccCount / BlockCount;
    }

    //internal static class CodeSizes
    //{
    //    public static readonly CodeSize[] All = new[]
    //    {
    //        new CodeSize(96, 96, 4, 4, 272, 4),
    //        new CodeSize(104, 104, 4, 4, 336, 6),
    //        new CodeSize(120, 120, 6, 6, 408, 6),
    //        new CodeSize(132, 132, 6, 6, 496, 8),
    //        new CodeSize(144, 144, 6, 6, 620, 10),

    //        new CodeSize(8, 18, 1, 1, 7, 1),
    //        new CodeSize(8, 32, 2, 1, 11, 1),
    //        new CodeSize(12, 26, 1, 1, 14, 1),
    //        new CodeSize(12, 36, 2, 1, 18, 1),
    //        new CodeSize(16, 36, 2, 1, 24, 1),
    //        new CodeSize(16, 48, 2, 1, 28, 1)
    //    };
    //}
}
