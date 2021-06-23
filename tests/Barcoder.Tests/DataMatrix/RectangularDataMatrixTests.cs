using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Barcoder.DataMatrix;
using FluentAssertions;
using Xunit;

namespace Barcoder.Tests.DataMatrix
{
    public class RectangularDataMatrixTests
    {
        [Fact]
        public void Encode_8x18()
        {
            var content = "123456";
            var expectedDataBits = ImageStringToBools(@"
                #.#.#.#.#.#.#.#.#.
                ##..#.....##.....#
                ##...#..##.####.#.
                ##..##...#...###.#
                ####.##..###..#...
                #.####...#...#.###
                #....####.##.##.#.
                ##################
            ");

            var dataMatrix = DataMatrixEncoder.Encode(content, CodeSizes.DM_8x18) as DataMatrixCode;
            dataMatrix.Should().NotBeNull();
            dataMatrix.Bounds.X.Should().Be(18);
            dataMatrix.Bounds.Y.Should().Be(8);

            for (int i = 0; i < expectedDataBits.Length; i++)
            {
                int x = i % dataMatrix.Bounds.X;
                int y = i / dataMatrix.Bounds.X;
                dataMatrix.Get(x, y).Should().Be(expectedDataBits[i], $"of expected bit on index {i}");
            }
        }

        [Fact]
        public void Encode_8x32()
        {
            var content = "Abc12345";
            var expectedDataBits = ImageStringToBools(@"
                #.#.#.#.#.#.#.#.#.#.#.#.#.#.#.#.
                #.##....#..##.###..##..#..###..#
                ##..#.##.####.#.#.#####..#####..
                #.###..#.......#####....#.###.##
                #...#.....#####.#.###..##..###..
                ####..#..#.#.####.###.####.#...#
                #.##.#..#..#....##.###.#...##...
                ################################
            ");

            var dataMatrix = DataMatrixEncoder.Encode(content, CodeSizes.DM_8x32) as DataMatrixCode;
            dataMatrix.Should().NotBeNull();
            dataMatrix.Bounds.X.Should().Be(32);
            dataMatrix.Bounds.Y.Should().Be(8);

            for (int i = 0; i < expectedDataBits.Length; i++)
            {
                int x = i % dataMatrix.Bounds.X;
                int y = i / dataMatrix.Bounds.X;
                dataMatrix.Get(x, y).Should().Be(expectedDataBits[i], $"of expected bit on index {i}");
            }
        }

        [Fact]
        public void Encode_12x26()
        {
            var content = "Abc12345Gtzw";
            var expectedDataBits = ImageStringToBools(@"
                #.#.#.#.#.#.#.#.#.#.#.#.#.
                #.##....##..###.###.###.##
                ##..#.##....##.#.#.#.##...
                #.###...#..#...#.#.#.##.##
                #...#..###.#...#.#.#..#...
                #.##..#....#..###.#.....##
                ####.########.#.##.###....
                #.#.#.###.#.###.#.#...##.#
                #..##.###.##.###.##..#....
                #.##.#####.##..#.#..###.##
                ##..#...#.#...#.#......#..
                ##########################
                
            ");

            var dataMatrix = DataMatrixEncoder.Encode(content, CodeSizes.DM_12x26) as DataMatrixCode;
            dataMatrix.Should().NotBeNull();
            dataMatrix.Bounds.X.Should().Be(26);
            dataMatrix.Bounds.Y.Should().Be(12);

            for (int i = 0; i < expectedDataBits.Length; i++)
            {
                int x = i % dataMatrix.Bounds.X;
                int y = i / dataMatrix.Bounds.X;
                dataMatrix.Get(x, y).Should().Be(expectedDataBits[i], $"of expected bit on index {i}");
            }

        }

        private static bool[] ImageStringToBools(string imageString)
        {
            var lines = imageString?
                .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrEmpty(x))
                .ToArray();

            if (lines.Length <= 0)
                throw new InvalidOperationException($"No data in {nameof(imageString)}");
            var cols = lines.First().Length;
            var rows = lines.Length;
            foreach (var line in lines)
                if (line.Length != cols)
                    throw new InvalidOperationException("Not all lines have the same length");
            return lines.SelectMany(x => x).Select(x => x != '.').ToArray();
        }
    }
}
