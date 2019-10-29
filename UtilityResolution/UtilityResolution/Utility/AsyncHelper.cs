using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace UtilityResolution.Utility
{
    public class AsyncHelper
    {
        /// <summary>
        /// 同步转异步方法
        /// </summary>
        /// <typeparam name="T">返回值参数</typeparam>
        /// <param name="funcAction">方法调用</param>
        /// <param name="callBackAction">回调</param>
        public static void DoAsync<T>(Func<T> funcAction, Action<AsyncEventArgs<T>> callBackAction = null)
        {
            TaskScheduler syncContextScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Factory.StartNew<T>(funcAction, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).ContinueWith((ee) =>
            {
                var ex = ee.Exception != null ? ee.Exception.InnerException : null;

                T result = default(T);
                if (ex == null && ee.Result != null)
                    result = (T)ee.Result;

                AsyncEventArgs<T> args = new AsyncEventArgs<T>(ex, ee.IsCanceled, null, result);
                if (callBackAction != null)
                    callBackAction(args);

            }, syncContextScheduler);
        }

        /// <summary>
        /// 同步转异步方法
        /// </summary>
        /// <param name="action">方法调用</param>
        /// <param name="callBackAction">回调</param>
        public static void DoAsync(Action action, Action<AsyncCompletedEventArgs> callBackAction = null)
        {
            TaskScheduler syncContextScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Factory.StartNew(action, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).ContinueWith((ee) =>
            {
                var ex = ee.Exception != null ? ee.Exception.InnerException : null;
                AsyncCompletedEventArgs args = new AsyncCompletedEventArgs(ex, ee.IsCanceled, null);
                if (callBackAction != null)
                    callBackAction(args);

            }, syncContextScheduler);
        }

        /// <summary>
        /// 可取消异步操作
        /// </summary>
        public static CancellationTokenSource DoAsync<T>(Func<CancellationToken, T> funcAction, Action<AsyncEventArgs<T>> callBackAction = null)
        {
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;
            TaskScheduler syncContextScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Factory.StartNew<T>(() =>
            {
                var result = funcAction(token);
                if (token.IsCancellationRequested)
                    token.ThrowIfCancellationRequested();
                return result;

            }, token, TaskCreationOptions.None, TaskScheduler.Default).ContinueWith((ee) =>
            {
                var ex = ee.Exception != null ? ee.Exception.InnerException : null;
                T result = default(T);
                if (ex == null && !ee.IsCanceled && ee.Result != null)
                    result = (T)ee.Result;

                AsyncEventArgs<T> args = new AsyncEventArgs<T>(ex, ee.IsCanceled, null, result);
                if (callBackAction != null)
                    callBackAction(args);

            }, syncContextScheduler);
            return tokenSource;
        }

        /// <summary>
        /// 可取消异步操作
        /// </summary>
        public static CancellationTokenSource DoAsync(Action<CancellationToken> action, Action<AsyncCompletedEventArgs> callBackAction = null)
        {
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;
            TaskScheduler syncContextScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Factory.StartNew(() =>
            {
                action(token);
                if (token.IsCancellationRequested)
                    token.ThrowIfCancellationRequested();

            }, token, TaskCreationOptions.None, TaskScheduler.Default).ContinueWith((ee) =>
            {
                var ex = ee.Exception != null ? ee.Exception.InnerException : null;
                AsyncCompletedEventArgs args = new AsyncCompletedEventArgs(ex, ee.IsCanceled, null);
                if (callBackAction != null)
                    callBackAction(args);

            }, syncContextScheduler);
            return tokenSource;
        }

    }

    public class AsyncEventArgs<T> : AsyncCompletedEventArgs
    {
        public AsyncEventArgs(Exception error, bool cancelled, object userState, T result)
            : base(error, cancelled, userState)
        {
            this.Result = result;
        }
        public AsyncEventArgs(AsyncCompletedEventArgs args, T result, object userState)
            : base(args.Error, args.Cancelled, userState)
        {
            this.Result = result;
        }

        public T Result { get; private set; }
    }

    /// <summary>
    /// UI任务
    /// </summary>
    public static class UITask
    {
        /// <summary> 
        /// UI 线程
        /// </summary> 
        private static readonly TaskScheduler scheduler;
        static UITask()
        {
            scheduler = TaskScheduler.FromCurrentSynchronizationContext();
        }

        /// <summary>
        /// 新建UI任务
        /// </summary>
        /// <param name="action"></param>
        public static Task StartNewWithUI(Action action)
        {
            return Task.Factory.StartNew(action, CancellationToken.None, TaskCreationOptions.None, scheduler);
        }

        /// <summary>
        /// 同步新建UI任务
        /// </summary>
        /// <param name="action"></param>
        public static void StartNewWithUIWait(Action action)
        {
            StartNewWithUI(action).Wait();
        }

        /// <summary>
        /// 新建UI任务
        /// </summary>
        /// <param name="action"></param>
        public static Task<TResult> StartNewWithUI<TResult>(Func<TResult> funcAction)
        {
            return Task.Factory.StartNew<TResult>(funcAction, CancellationToken.None, TaskCreationOptions.None, scheduler);
        }

        /// <summary>
        /// 附加Task 
        /// </summary>
        public static Task ContinueWithUI(this Task task, Action action)
        {
            return task.ContinueWith(_ => action(), CancellationToken.None, TaskContinuationOptions.None, scheduler);
        }

        /// <summary>
        /// 附加Task 
        /// </summary>
        public static Task ContinueWithUI<TResult>(this Task<TResult> task, Action action)
        {
            return task.ContinueWith(_ => action(), CancellationToken.None, TaskContinuationOptions.None, scheduler);
        }



    }
}
