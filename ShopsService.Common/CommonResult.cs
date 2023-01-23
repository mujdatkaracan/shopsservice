using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace ShopsService.Common
{
    /*! \brief
    * Generics list: \n
    * \n
    * T: Any class \n
    * \n
    * Actually this is just a wrapper class. \n
    * All services and some other utility classes uses this generic object for communicating each other \n
    */
    [ExcludeFromCodeCoverage]
    public class CommonResult<T> where T : class
    {
        public int? StatusCode { get; set; }

        /*!
         * If no error returns true
         */
        public bool Result => !IsError;

        /*!
         * Returns null if error occured
         */
        public T Data { get; internal set; }
        public string ErrorMessage { get; private set; }

        /// <summary>
        /// Returns TRUE if any status code exist and this value 
        /// </summary>
        public bool IsError => StatusCode.HasValue && StatusCode.Value != (int)HttpStatusCode.OK;

        public CommonResult(T data, /*!< Any class wrapped */
                                int? statusCode = null, /*!< Status codes */
                                 string errorMessage = null /*!< error message */
                                )
        {
            Data = data;
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
        }
         
          
        /*!
         * Factory method - Creates new instance of CommonResult passing http status code and error message \n
         * When you need to return error
         * \n
         * Generics list: \n
         * T: Any class
         */
        public static CommonResult<T> CreateError(HttpStatusCode statusCode, /*!< HttpStatusCode */
                                                    string errorMessage /*!< error message */
                                                    ) =>
            new CommonResult<T>(null, (int)statusCode, errorMessage);
 

        /*!
         * Factory method - Create new instance of CommonResult with 200 OK status code and passed object \n
         * \n
         * Generics list: \n
         * T: Any class
         */
        public static CommonResult<T> CreateResult(T data) =>
            new CommonResult<T>(data, 200);

    }
}
