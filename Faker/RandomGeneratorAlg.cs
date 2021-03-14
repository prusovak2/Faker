using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("FakerTests")]

namespace Faker
{
    internal interface IRandomGeneratorAlg
    {
        public ulong Seed { get; }
        public ulong Next();
        public double Next01Double();
        public double Next01NonZeroDouble();
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
        private const double oneStep = 1.0 / (1UL << 53);
        /// <summary>
        /// seed of this RNG, any instance created with the same value of seed is going to produce the same sequence of pseudo-random numbers
        /// </summary>
        public ulong Seed { get => this.StateRandomGenerator.Seed; }
        /// <summary>
        /// internal state necessary for generating pseudo-random numbers 
        /// </summary>
        internal ulong[] State = new ulong[4];
        /// <summary>
        /// uses given seed or new seed based on the current time and the Weyl's sequence to generate a pseudo-random initial state for xorshift algorithm
        /// </summary>
        internal Splitmix64 StateRandomGenerator;

        private readonly object XoshiroLock = new object();
        /// <summary>
        /// Initialize seed by a current time <br/>
        /// due to use of Weyl's sequence, seeds of two RandomGenerators created soon after each other should differ
        /// </summary>
        public Xoshiro256starstar()
        {
            this.StateRandomGenerator = new Splitmix64();
            //use inner simple random generator to initialize the state
            for (int i = 0; i < 4; i++)
            {
                this.State[i] = this.StateRandomGenerator.Next();
            }
        }
        /// <summary>
        /// Initialize seed by a given value
        /// </summary>
        /// <param name="seed"></param>
        public Xoshiro256starstar(ulong seed)
        {
            this.StateRandomGenerator = new Splitmix64(seed);
            //use inner simple random generator to initialize the state
            for (int i = 0; i < 4; i++)
            {
                this.State[i] = this.StateRandomGenerator.Next();
            }
        }

        internal static ulong Roll64(ulong toRoll, int rollAmount)
        {
            return (toRoll << rollAmount) | (toRoll >> (64 - rollAmount));
        }
        /// <summary>
        /// returns another random 64 bit number <br/>
        /// only upper 53 bits are of a sufficient quality of randomness to be used
        /// </summary>
        /// <returns></returns>
        public ulong Next()
        {
            ulong result;
            lock (XoshiroLock)
            {
                result = Roll64(this.State[1] * 5, 7) * 9;
                ulong t = this.State[1] << 17;

                this.State[2] ^= this.State[0];
                this.State[3] ^= this.State[1];
                this.State[1] ^= this.State[2];
                this.State[0] ^= this.State[3];

                this.State[2] ^= t;

                this.State[3] = Roll64(this.State[3], 45);
            }
            return result;
        }
        /// <summary>
        /// random double from interval [0, 1 - 1/2^53]
        /// </summary>
        /// <returns></returns>
        public double Next01Double()
        {
            ulong next = this.Next();
            double result = (next >> 11) * oneStep;
            return result;
        }
        /// <summary>
        /// random double from interval [1/2^53, 1]
        /// </summary>
        /// <returns></returns>
        public double Next01NonZeroDouble()
        {
            double res = Next01Double() + oneStep;
            return res;
        }

    }
    /// <summary>
    /// to generate seed for a Xoshiro256starstar
    /// Based on http://prng.di.unimi.it/splitmix64.c
    /// </summary>
    internal class Splitmix64 : IRandomGeneratorAlg
    {
        internal static ulong WeylSequenceSeedCounter { get; private set; } = 0;

        internal static readonly object WeylsCounterLock = new object();
        public ulong Seed { get; }
        internal ulong State { get; set; }

        private readonly object SplitmixLock = new object();
        /// <summary>
        /// Initialize seed by a given value
        /// </summary>
        /// <param name="seed"></param>
        public Splitmix64(ulong seed)
        {
            this.Seed = seed;
            this.State = seed;
        }
        /// <summary>
        /// Initialize seed by current time <br/>
        /// due to use of Weyl's sequence, seeds of two RandomGenerators created soon after each other should differ
        /// </summary>
        public Splitmix64()
        {
            //this constant should be relatively prime to 2^64 and thus adding it each time ctor is called to a counter
            //should produce a Weyl's sequence in WeylSequenceSeedCounter and so values of a counter should resemble a uniform distribution
            //to avoid producing the same seed for a two PRNGs seeded soon after each other
            //TODO: figure out, whether it is a good idea
            ulong ticks = IRandomGeneratorAlg.getCurrentUnixTime();
            ulong counter;
            lock (WeylsCounterLock)
            {
                Splitmix64.WeylSequenceSeedCounter += 0xb5ad4eceda1ce2a9;
                counter = Splitmix64.WeylSequenceSeedCounter;
            }
            ulong seed = ticks ^ counter;
            this.Seed = seed;
            this.State = seed;
        }
        /// <summary>
        /// returns another 64 bit random number
        /// </summary>
        /// <returns></returns>
        public ulong Next()
        {
            ulong next;
            lock (SplitmixLock)
            {
                next = (this.State += 0x9e3779b97f4a7c15);
            }
            next = (next ^ (next >> 30)) * 0xbf58476d1ce4e5b9;
            next = (next ^ (next >> 27)) * 0x94d049bb133111eb;
            return next ^ (next >> 31);
        }

        double IRandomGeneratorAlg.Next01Double()
        {
            throw new NotImplementedException();
        }

        double IRandomGeneratorAlg.Next01NonZeroDouble()
        {
            throw new NotImplementedException();
        }
    }
}

