using Microsoft.IO;
using Newtonsoft.Json;
using ShopsService.Common;
using ShopsService.WebAPI.Constants;
using ContentType = ShopsService.WebAPI.Constants.ContentType;

namespace ShopsService.WebAPI.Middlewares
{

    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
         
        private readonly RecyclableMemoryStreamManager _streamManager;

        public RequestLoggingMiddleware(
            RequestDelegate next)
        {
            _next = next; 
            _streamManager = new RecyclableMemoryStreamManager();
        }

        public async Task Invoke(HttpContext context)
        {
            var requestMessage = await GetRequestMessage(context);
            var originalBodyStream = context.Response.Body;
            var responseStream = _streamManager.GetStream();
            context.Response.Body = responseStream;

            Exception exception = null;
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                exception = ex;

                var errorObject = CommonResult<object>.CreateError(System.Net.HttpStatusCode.BadRequest, ex.Message);
                var response = JsonConvert.SerializeObject(errorObject);
                context.Response.ContentType = ContentType.JSON;
                context.Response.WriteAsync(response);
            }

            var responseMessage = await GetResponseMessage(context);

            var message =
                String.Join(Environment.NewLine,
                    HttpRequestResponseInfo.StarLine,
                    $"RequestId: {context.TraceIdentifier}",
                    requestMessage,
                    (exception == null
                        ? null
                        : String.Join(Environment.NewLine,
                            "### Exception: ",
                            exception.Message)),
                    responseMessage,
                    HttpRequestResponseInfo.StarLine);


            await responseStream.CopyToAsync(originalBodyStream);
             
        }

        private async Task<string> GetRequestMessage(HttpContext context)
        {
            context.Request.EnableBuffering();
            var requestStream = _streamManager.GetStream();
            await context.Request.Body.CopyToAsync(requestStream);

            var message = string.Join(Environment.NewLine,
                HttpRequestResponseInfo.HttpRequestInformation,
                HttpRequestResponseInfo.CharpLine,
                String.Join(" ", HttpRequestResponseInfo.Schema, context.Request.Scheme),
                String.Join(" ", HttpRequestResponseInfo.Host, context.Request.Host),
                String.Join(" ", HttpRequestResponseInfo.Path, context.Request.Path),
                String.Join(" ", HttpRequestResponseInfo.QueryString, context.Request.QueryString),
                String.Join(" ", HttpRequestResponseInfo.ResponseBody, ReadStreamInChunks(requestStream)),
                $"Header Key: Authorization - Value: {context.Request.Headers["Authorization"]}");

            context.Request.Body.Position = 0;


            return message;
        }

        private async Task<string> GetResponseMessage(HttpContext context)
        {
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseText = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            var responseMessage = String.Join(Environment.NewLine,
                HttpRequestResponseInfo.HttpResponseInformation,
                HttpRequestResponseInfo.CharpLine,
                String.Join(" ", HttpRequestResponseInfo.Schema, context.Request.Scheme),
                String.Join(" ", HttpRequestResponseInfo.Host, context.Request.Host),
                String.Join(" ", HttpRequestResponseInfo.Path, context.Request.Path),
                String.Join(" ", HttpRequestResponseInfo.QueryString, context.Request.QueryString),
                String.Join(" ", HttpRequestResponseInfo.ResponseBody, responseText)
            );
            return responseMessage;
        }

        private static string ReadStreamInChunks(Stream stream)
        {
            const int readChunkBufferLenght = 4096;
            stream.Seek(0, SeekOrigin.Begin);
            var textWriter = new StringWriter();
            var reader = new StreamReader(stream);
            var readChunk = new char[readChunkBufferLenght];
            int readChunkLength;
            do
            {
                readChunkLength = reader.ReadBlock(readChunk,
                    0,
                    readChunkBufferLenght);
                textWriter.Write(readChunk, 0, readChunkLength);
            } while (readChunkLength > 0);

            return textWriter.ToString();
        }
         
         
    }
}
