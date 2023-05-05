using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    /// <summary>
    /// An abstract class representing a successful or a failed operation
    /// </summary>
    public abstract class Result
    {
        public bool Success { get; set; }
    }

    /// <summary>
    /// An abstract class representing a successful or a failed operation with a value
    /// </summary>
    public abstract class Result<T> : Result
    {
        public T Data { get; set; }

        public Result()
        {

        }

        public Result(T data)
        {
            Data = data;
        }
    }

    /// <summary>
    /// A class representing a successful operation
    /// </summary>
    public class SuccessResult : Result
    {
        public SuccessResult()
        {
            Success = true;
        }
    }

    /// <summary>
    /// A class representing a successful operation with a value
    /// </summary>
    public class SuccessResult<T> : Result<T>
    {
        public SuccessResult()
        {

        }

        public SuccessResult(T data) : base(data)
        {
            Success = true;
        }
    }

    /// <summary>
    /// A class representing a failed operation with a message and a collection of errors
    /// </summary>
    public class FailureResult : Result
    {
        public string Message { get; set; }
        public IReadOnlyCollection<Error> Errors { get; set; }

        public FailureResult() { }
        public FailureResult(string message) : this(message, new List<Error>()) { }

        public FailureResult(string message, IReadOnlyCollection<Error> errors)
        {
            Message = message;
            Success = false;
            Errors = errors ?? new List<Error>();
        }
    }

    /// <summary>
    /// A class representing a failed operation with a message and a collection of errors
    /// </summary>
    public class FailureResult<T> : Result<T>
    {
        public string Message { get; set; }
        public IReadOnlyCollection<Error> Errors { get; set; }

        public FailureResult(string message) : this(message, new List<Error>()) { }

        public FailureResult(string message, IReadOnlyCollection<Error> errors) : base(default)
        {
            Message = message;
            Success = false;
            Errors = errors ?? new List<Error>();
        }
    }

    /// <summary>
    /// A class representing an internal application error with error code and details
    /// </summary>
    public class Error
    {
        public string Code { get; set; }
        public string Details { get; set; }

        public Error(string details) : this(null, details) { }

        public Error(string code, string details)
        {
            Code = code;
            Details = details;
        }
    }
}
