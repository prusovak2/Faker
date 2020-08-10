using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("FakerTests")]

namespace Faker
{
    //example by Mianen
    /*public class RandomGenerator 
    {
        public int Int(int min = int.MinValue, int max = int.MaxValue)
        {
            return 0;
        }
    }*/
    internal interface IRandomGeneratorAlg
    {
        public ulong Seed { get; }
        public ulong Next();
        public static ulong getCurrentUnixTime()
        {
            return (ulong)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
    }
    /// <summary>
    /// based on http://prng.di.unimi.it/xoshiro256starstar.c
    /// </summary>
    internal class Xoshiro256starstar : IRandomGeneratorAlg
    {
        public ulong Seed { get; }
        private ulong[] State = new ulong[4];
        private Splitmix64 SeedRandomGenerator;
        public Xoshiro256starstar()
        {
            ulong seed = IRandomGeneratorAlg.getCurrentUnixTime();
            this.Seed = seed;
            this.SeedRandomGenerator = new Splitmix64(seed);
            for (int i = 0; i < 4; i++)
            {
                this.State[i] = this.SeedRandomGenerator.Next();
            }
        }
        public Xoshiro256starstar(ulong seed)
        {
            this.Seed = seed;
            this.SeedRandomGenerator = new Splitmix64(seed);
            for (int i = 0; i < 4; i++)
            {
                this.State[i] = this.SeedRandomGenerator.Next();
            }
        }

        private static ulong Roll64(ulong toRoll, int rollAmount)
        {
            return (toRoll << rollAmount) | (toRoll >> (64 - rollAmount));
        }

        public ulong Next()
        {
            ulong result = Roll64(this.State[1] * 5, 7) * 9;
            ulong t = this.State[1] << 17;

            this.State[2] ^= this.State[0];
            this.State[3] ^= this.State[1];
            this.State[1] ^= this.State[2];
            this.State[0] ^= this.State[3];

            this.State[2] ^= t;

            this.State[3] = Roll64(this.State[3], 45);

            return result;
        }

    }
    /// <summary>
    /// to generate seed for a Xoshiro256starstar
    /// Based on http://prng.di.unimi.it/splitmix64.c
    /// </summary>
    internal class Splitmix64 : IRandomGeneratorAlg
    {
        public ulong Seed { get; }
        private ulong State { get; set; }
        /// <summary>
        /// Initialize seed by given value
        /// </summary>
        /// <param name="seed"></param>
        public Splitmix64(ulong seed)
        {
            this.Seed = seed;
            this.State = seed;
        }
        /// <summary>
        /// Initialize seed by current time
        /// </summary>
        public Splitmix64()
        {
            ulong unixDate = IRandomGeneratorAlg.getCurrentUnixTime();
            this.Seed = unixDate;
            this.State = unixDate;
        }
        public ulong Next()
        {
            ulong next = (this.State += 0x9e3779b97f4a7c15);
            next = (next ^ (next >> 30)) * 0xbf58476d1ce4e5b9;
            next = (next ^ (next >> 27)) * 0x94d049bb133111eb;
            return next ^ (next >> 31);
        }
    }

}

