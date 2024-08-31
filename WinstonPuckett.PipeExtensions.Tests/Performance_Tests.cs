using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using Xunit;
using Xunit.Abstractions;

namespace WinstonPuckett.PipeExtensions.Tests
{
    public class PerformanceTests(ITestOutputHelper output)
    {
        private readonly ITestOutputHelper output = output;

        [Fact]
        public void IncrementDirectVsFuncVsPipe()
        {
            var iterations = 1_000_000;
            var number = 0;

            static void Increment(int n)
                => n += 1;

            // Increment directly
            var directPerf = Time(() => {
                for (int i = 0; i < iterations; i++) {
                    number += 1;
                }
            });

            number = 0;

            // Increment with function
            var fnPerf = Time(() => {
                for (int i = 0; i < iterations; i++) {
                    Increment(number);
                }
            });

            number = 0;

            // Increment with pipe
            var pipePerf = Time(() => {
                for (int i = 0; i < iterations; i++) {
                    number.Pipe(Increment);
                }
            });

            var libraryPerf = pipePerf - fnPerf - directPerf;
            var vsRawPerf = pipePerf - directPerf;

            Assert.True(libraryPerf <= 5);
            Assert.True(vsRawPerf <= 6);
        }

        private static long Time(Action action)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();
            action();
            stopwatch.Stop();

            return stopwatch.ElapsedMilliseconds;
        }
    }
}
