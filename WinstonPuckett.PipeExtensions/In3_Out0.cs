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
        /// Destructure input tuple and pass it to func.
        /// </summary>
        /// <typeparam name="TParam">First parameter type.</typeparam>
        /// <typeparam name="TParam2">Second parameter type.</typeparam>
        /// <typeparam name="TParam3">Third parameter type.</typeparam>
        /// <param name="input">The tuple desctructured and passed to func.</param>
        /// <param name="func">The function to call which operates on input.</param>
        public static void Pipe<TParam, TParam2, TParam3>(this (TParam, TParam2, TParam3) input, Action<TParam, TParam2, TParam3> func)
            => func(input.Item1, input.Item2, input.Item3);

        /// <summary>
        /// Destructure input tuple, pass it to func, and return a Task.
        /// </summary>
        /// <typeparam name="TParam">First parameter type.</typeparam>
        /// <typeparam name="TParam2">Second parameter type.</typeparam>
        /// <typeparam name="TParam3">Third parameter type.</typeparam>
        /// <param name="input">The tuple desctructured and passed to func.</param>
        /// <param name="asyncFunc">The async function to call/await</param>
        public static async Task PipeAsync<TParam, TParam2, TParam3>(
            this (TParam, TParam2, TParam3) input,
            Func<TParam, TParam2, TParam3, Task> asyncFunc)
            => await asyncFunc(input.Item1, input.Item2, input.Item3);

        /// <summary>
        /// Await inputTask tuple, destructure it, and pass it to func.
        /// </summary>
        /// <typeparam name="TParam">First parameter type.</typeparam>
        /// <typeparam name="TParam2">Second parameter type.</typeparam>
        /// <typeparam name="TParam3">Third parameter type.</typeparam>
        /// <param name="inputTask">The tuple awaited, desctructured, and passed to func.</param>
        /// <param name="func">The function to call which operates on input.</param>
        public static async Task PipeAsync<TParam, TParam2, TParam3>(
            this Task<(TParam, TParam2, TParam3)> inputTask,
            Action<TParam, TParam2, TParam3> func)
            => (await inputTask).Pipe(func);

        /// <summary>
        /// Await inputTask tuple, destructure it, pass it to func, return a Task.
        /// </summary>
        /// <typeparam name="TParam">First parameter type.</typeparam>
        /// <typeparam name="TParam2">Second parameter type.</typeparam>
        /// <typeparam name="TParam3">Third parameter type.</typeparam>
        /// <param name="inputTask">The tuple awaited, desctructured, and passed to func.</param>
        /// <param name="asyncFunc">The function to call which operates on input.</param>
        public static async Task PipeAsync<TParam, TParam2, TParam3>(
            this Task<(TParam, TParam2, TParam3)> inputTask,
            Func<TParam, TParam2, TParam3, Task> asyncFunc)
            => await (await inputTask).PipeAsync(asyncFunc);

        /// <summary>
        /// Destructure input tuple, and pass it and cancellationToken to func, return a Task.
        /// </summary>
        /// <typeparam name="TParam">First parameter type.</typeparam>
        /// <typeparam name="TParam2">Second parameter type.</typeparam>
        /// <typeparam name="TParam3">Third parameter type.</typeparam>
        /// <param name="input">The object passed to func.</param>
        /// <param name="asyncFunc">The async function to call/await</param>
        /// <param name="cancellationToken">The cancellation token to pass to func</param>
        public static async Task PipeAsync<TParam, TParam2, TParam3>(
            this (TParam, TParam2, TParam3) input,
            Func<TParam, TParam2, TParam3, CancellationToken, Task> asyncFunc,
            CancellationToken cancellationToken = default)
            => await asyncFunc(input.Item1, input.Item2, input.Item3, cancellationToken);

        /// <summary>
        /// Await input tuple, destructure it, and pass it and cancellationToken to func, return a Task.
        /// </summary>
        /// <typeparam name="TParam">First parameter type.</typeparam>
        /// <typeparam name="TParam2">Second parameter type.</typeparam>
        /// <typeparam name="TParam3">Third parameter type.</typeparam>
        /// <param name="inputTask">The tuple awaited, desctructured, and passed to func.</param>
        /// <param name="asyncFunc">The async function to call/await</param>
        /// <param name="cancellationToken">The cancellation token to pass to func</param>
        public static async Task PipeAsync<TParam, TParam2, TParam3>(
            this Task<(TParam, TParam2, TParam3)> inputTask,
            Func<TParam, TParam2, TParam3, CancellationToken, Task> asyncFunc,
            CancellationToken cancellationToken = default)
            => await (await inputTask).PipeAsync(asyncFunc, cancellationToken);
    }
}
