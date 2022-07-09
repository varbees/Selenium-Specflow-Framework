using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polly;

namespace Selenium.Specflow.Automation.Utilities
{
    public static class FunctionRetrier
    {
        public static void RetryOnException<TException>(Action action, int retryCount = 3, int waitSeconds = 1)
            where TException : Exception
        {
            Policy
                .Handle<TException>()
                .WaitAndRetry(retryCount, count => TimeSpan.FromSeconds(waitSeconds))
                .Execute(action);
        }

        public static TResult RetryOnException<TResult, TException>(Func<TResult> function, int retryCount = 3, int waitSeconds = 1)
            where TException: Exception
        {
            return Policy
                .Handle<TException>()
                .WaitAndRetry(retryCount, count => TimeSpan.FromSeconds(waitSeconds))
                .Execute(function);
        }
        
        
        
    }
}
