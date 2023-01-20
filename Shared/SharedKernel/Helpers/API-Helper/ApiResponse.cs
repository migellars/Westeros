using Newtonsoft.Json;

namespace SharedKernel.Helpers.API_Helper
{
    public abstract class ApiResponseBase
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
        [JsonProperty("message")]
        public string? Message { get; set; }
    }

    public class ApiResponse<TData> : ApiResponseBase
    {
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public TData? Data { get; set; }
        [JsonProperty("Pagination", NullValueHandling = NullValueHandling.Ignore)]
        public object? Pagination { get; set; }
    }

    public class ApiResponse<TData, TPagination> : ApiResponseBase
    {
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public TData? Data { get; set; }
        [JsonProperty("Pagination", NullValueHandling = NullValueHandling.Ignore)]
        public TPagination? Pagination { get; set; }
    }

    public class ApiResponse : ApiResponseBase
    {
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public object? Data { get; set; }
        [JsonProperty("Pagination", NullValueHandling = NullValueHandling.Ignore)]
        public object? Pagination { get; set; }

        private const string FailedMessage = "Data not found";
        private const string SuccessMessage = "Data Retrieved";

        /// <summary>
        /// Response for Post
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public static ApiResponse GetSuccess( object? data = null, object? pagination = null, string message = SuccessMessage)
        {
            return new()
            {
                Success = true,
                Message = message,
                Data = data,
                Pagination = pagination
            };
        }

        //POSt

        public static ApiResponse GetFailed(object? data = null, object? pagination = null, string message = FailedMessage)
        {
            return new()
            {
                Success = false,
                Message = message,
                Data = data,
                Pagination = pagination
            };
        }

        /// <summary>
        /// Response for GET
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public static ApiResponse<TData?> GetSuccess<TData>( TData? data = default, string message =SuccessMessage, object? pagination = null)
        {
            return new()
            {
                Success = true,
                Message = message,
                Data = data,
                Pagination = pagination
            };
        }

        //GET

        public static ApiResponse<TData?> GetFailed<TData>(TData? data = default, string message = FailedMessage, object? pagination = null)
        {
            return new()
            {
                Success = false,
                Message = message,
                Data = data,
                Pagination = pagination
            };
        }

        public static ApiResponse<TData?, TPagination?> GetSuccess<TData, TPagination>( TData? data = default, TPagination? pagination = default, string message = SuccessMessage)
        {
            return new()
            {
                Success = true,
                Message = message,
                Data = data,
                Pagination = pagination
            };
        }

        public static ApiResponse<TData?, TPagination?> GetFailed<TData, TPagination>(TData? data = default, TPagination? pagination = default, string message = FailedMessage)
        {
            return new()
            {
                Success = false,
                Message = message,
                Data = data,
                Pagination = pagination
            };
        }
    }
}