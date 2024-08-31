using System;
using System.Threading;
using System.Threading.Tasks;

namespace WinstonPuckett.PipeExtensions
{
    public static partial class PipeExtensions
    {
        /// <summary>
        /// Pass input to func.
        /// </summary>
        /// <typeparam name="TParam">Parameter type.</typeparam>
        /// <param name="input">The object passed to func.</param>
        /// <param name="func">The function to call which operates on input.</param>
        public static void Pipe<TParam>(this TParam input, Action<TParam> func)
            => func(input);

        /// <summary>
        /// Await inputTask and pass it to func.
        /// </summary>
        /// <typeparam name="TParam">Parameter type.</typeparam>
        /// <param name="inputTask">The object you're operating on wrapped in a Task.</param>
        /// <param name="func">The function to call which operates on input.</param>
        public static async Task PipeAsync<TParam>(this Task<TParam> inputTask, Action<TParam> func)
            => func(await inputTask);

        /// <summary>
        /// Pass input to asyncFunc and return a Task.
        /// </summary>
        /// <typeparam name="TParam">Parameter type.</typeparam>
        /// <param name="input">The object passed to func.</param>
        /// <param name="asyncFunc">The function to call which operates on T.</param>
        /// <returns>A Task</returns>
        public static async Task PipeAsync<TParam>(this TParam input, Func<TParam, Task> asyncFunc)
            => await asyncFunc(input);

        /// <summary>
        /// Pass input and cancellationToken to asyncFunc and return a Task.
        /// </summary>
        /// <typeparam name="TParam">Parameter type.</typeparam>
        /// <param name="input">The object passed to func.</param>
        /// <param name="asyncFunc">The function to call which operates on T.</param>
        /// <param name="cancellationToken">The cancellationToken to pass to asyncFunc</param>
        /// <returns>A Task</returns>
        public static async Task PipeAsync<TParam>(this TParam input,
            Func<TParam, CancellationToken, Task> asyncFunc,
            CancellationToken cancellationToken = default)
            => await asyncFunc(input, cancellationToken);


        /// <summary>
        /// Pass input and cancellationToken to asyncFunc and return a Task.
        /// </summary>
        /// <typeparam name="TParam">Parameter type.</typeparam>
        /// <param name="input">The object passed to func.</param>
        /// <param name="asyncFunc">The function to call which operates on T.</param>
        /// <param name="cancellationToken">The cancellationToken to pass to asyncFunc</param>
        /// <returns>A Task</returns>
        public static async Task PipeAsync<TParam>(
            this Task<TParam> input,
            Func<TParam, Task> asyncFunc)
            => await asyncFunc(await input);

        /// <summary>
        /// Pass input and cancellationToken to asyncFunc
        /// </summary>
        /// <typeparam name="TParam">Parameter type.</typeparam>
        /// <param name="input">The object passed to func.</param>
        /// <param name="asyncFunc">The function to call which operates on T.</param>
        /// <param name="cancellationToken">The cancellationToken to pass to asyncFunc</param>
        /// <returns>A Task</returns>
        public static async Task PipeAsync<TParam>(
            this Task<TParam> input,
            Func<TParam, CancellationToken, Task> asyncFunc,
            CancellationToken cancellationToken = default)
            => await asyncFunc(await input, cancellationToken);
    }
}
