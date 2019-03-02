using GlassfullPlugin.Libary;
using GlassfullPlugin.UI;
using Kompas6API5;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Environment = System.Environment;

namespace GlassFull.UnitTest
{
    [TestFixture]
    public class GlassfulStressTest
    {
        [Test]
        public void Start()
        {
            var writer = new StreamWriter($@"{AppDomain.CurrentDomain.BaseDirectory}\StressTest.txt");//заменено на относительный путь

            var kompas = StartKompas();
            var builder = new DetailBuilder(kompas);
            var parameters = new GlassfulParametrs(1.0d, 10.0d, 10.0d, 1.0d, 8.0d);
            var count = 500;

            var processes = Process.GetProcessesByName("KOMPAS");
            var process = processes.First();

            var ramCounter = new PerformanceCounter("Process", "Working Set", process.ProcessName);
            var cpuCounter = new PerformanceCounter("Process", "% Processor Time", process.ProcessName);

            for (int i = 0; i < count; i++)
            {
                cpuCounter.NextValue();

                builder.CreateDetail(parameters, false);

                var ram = ramCounter.NextValue();
                var cpu = cpuCounter.NextValue();

                writer.Write($"{i}. ");
                writer.Write($"RAM: {Math.Round(ram / 1024 / 1024)} MB");
                writer.Write($"\tCPU: {cpu} %");
                writer.Write(Environment.NewLine);
                writer.Flush();
            }
        }

        public KompasObject StartKompas()
        {
            var kompas = new KompasConnector();
            kompas.OpenKompas();
            return kompas.Kompas;
        }

    }
}
