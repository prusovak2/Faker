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
            return (ulong)Environment.TickCount64;
        }
    }
    /// <summary>
    /// based on http://prng.di.unimi.it/xoshiro256starstar.c
    /// </summary>
    internal class Xoshiro256starstar : IRandomGeneratorAlg
    {
        public ulong Seed { get => this.StateRandomGenerator.Seed; }
        private ulong[] State = new ulong[4];
        /// <summary>
        /// uses given seed or new sedd based on the current time and the Weyl's sequence to generate a pseudorandom initial state for xorshift algorithm
        /// </summary>
        private Splitmix64 StateRandomGenerator;
        public Xoshiro256starstar()
        {
            this.StateRandomGenerator = new Splitmix64();
            for (int i = 0; i < 4; i++)
            {
                this.State[i] = this.StateRandomGenerator.Next();
            }
        }
        public Xoshiro256starstar(ulong seed)
        {
            this.StateRandomGenerator = new Splitmix64(seed);
            for (int i = 0; i < 4; i++)
            {
                this.State[i] = this.StateRandomGenerator.Next();
            }
        }

        private static ulong Roll64(ulong toRoll, int rollAmount)
        {
            return (toRoll << rollAmount) | (toRoll >> (64 - rollAmount));
        }
        /// <summary>
        /// returns another random number
        /// </summary>
        /// <returns></returns>
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
        private static ulong WeylSequenceSeedCounter = 0;
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
            //this constant should be relatively prime to 2^64 and thus adding it each time ctor is called to a counter
            //should produce a Weyl's sequence in WeylSequenceSeedCounter and so values of a counter should resamble a uniform distribution
            //to avoid producing the same seed for a two PRNGs seeded soon after each other
            //TODO: figure out, wheather it is a goood idea
            Splitmix64.WeylSequenceSeedCounter += 0xb5ad4eceda1ce2a9; 
            ulong ticks = IRandomGeneratorAlg.getCurrentUnixTime();
            ulong counter = Splitmix64.WeylSequenceSeedCounter;
            ulong seed = ticks ^ counter;
            this.Seed = seed;
            this.State = seed;
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

