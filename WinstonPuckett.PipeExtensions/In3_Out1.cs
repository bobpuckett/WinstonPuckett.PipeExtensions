using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WinstonPuckett.PipeExtensions
{
    public static partial class PipeExtensions
    {
        /// <summary>
        /// Destructure input tuple, pass it to func, and return the result.
        /// </summary>
        /// <typeparam name="TParam">First parameter type.</typeparam>
        /// <typeparam name="TParam2">Second parameter type.</typeparam>
        /// <typeparam name="TParam3">Third parameter type.</typeparam>
        /// <param name="input">The object passed to func.</param>
        /// <param name="func">The function to call which operates on input.</param>
        public static TOutput Pipe<TParam, TParam2, TParam3, TOutput>(
            this (TParam, TParam2, TParam3) input,
            Func<TParam, TParam2, TParam3, TOutput> func)
            => func(input.Item1, input.Item2, input.Item3);

        /// <summary>
        /// Destructure input tuple, pass it to func, and return the result.
        /// </summary>
        /// <typeparam name="TParam">First parameter type.</typeparam>
        /// <typeparam name="TParam2">Second parameter type.</typeparam>
        /// <typeparam name="TParam3">Third parameter type.</typeparam>
        /// <param name="input">The object passed to func.</param>
        /// <param name="asyncFunc">The function to call which operates on input.</param>
        public static async Task<TOutput> PipeAsync<TParam, TParam2, TParam3, TOutput>(
            this (TParam, TParam2, TParam3) input,
            Func<TParam, TParam2, TParam3, Task<TOutput>> asyncFunc)
            => await asyncFunc(input.Item1, input.Item2, input.Item3);

        /// <summary>
        /// Await input tuple, destructure it, pass it to func, and return the result.
        /// </summary>
        /// <typeparam name="TParam">First parameter type.</typeparam>
        /// <typeparam name="TParam2">Second parameter type.</typeparam>
        /// <typeparam name="TParam3">Third parameter type.</typeparam>
        public static async Task<TOutput> PipeAsync<TParam, TParam2, TParam3, TOutput>(
            this Task<(TParam, TParam2, TParam3)> inputTask,
            Func<TParam, TParam2, TParam3, TOutput> func)
            => (await inputTask).Pipe(func);

        /// <summary>
        /// Await input tuple, destructure it, pass it to asyncFunc, and return the result.
        /// </summary>
        /// <typeparam name="TParam">First parameter type.</typeparam>
        /// <typeparam name="TParam2">Second parameter type.</typeparam>
        /// <typeparam name="TParam3">Third parameter type.</typeparam>
        public static async Task<TOutput> PipeAsync<TParam, TParam2, TParam3, TOutput>(
            this Task<(TParam, TParam2, TParam3)> inputTask,
            Func<TParam, TParam2, TParam3, Task<TOutput>> func)
            => await (await inputTask).PipeAsync(func);

        /// <summary>
        /// Destructure input tuple, pass it and cancellationToken to asyncFunc, and return the result.
        /// </summary>
        /// <typeparam name="TParam">First parameter type.</typeparam>
        /// <typeparam name="TParam2">Second parameter type.</typeparam>
        /// <typeparam name="TParam3">Third parameter type.</typeparam>
        public static async Task<TOutput> PipeAsync<TParam, TParam2, TParam3, TOutput>(
            this (TParam, TParam2, TParam3) input,
            Func<TParam, TParam2, TParam3, CancellationToken, Task<TOutput>> asyncFunc,
            CancellationToken cancellationToken = default)
            => await asyncFunc(input.Item1, input.Item2, input.Item3, cancellationToken);

        /// <summary>
        /// Await input tuple, destructure it, pass it and cancellationToken to asyncFunc, and return the result.
        /// </summary>
        /// <typeparam name="TParam">First parameter type.</typeparam>
        /// <typeparam name="TParam2">Second parameter type.</typeparam>
        /// <typeparam name="TParam3">Third parameter type.</typeparam>
        public static async Task<TOutput> PipeAsync<TParam, TParam2, TParam3, TOutput>(
            this Task<(TParam, TParam2, TParam3)> inputTask,
            Func<TParam, TParam2, TParam3, CancellationToken, Task<TOutput>> asyncFunc,
            CancellationToken cancellationToken = default)
            => await (await inputTask).PipeAsync(asyncFunc, cancellationToken);
    }
}
