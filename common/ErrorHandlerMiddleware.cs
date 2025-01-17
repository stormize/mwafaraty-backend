﻿using Microsoft.AspNetCore.Http;
using Mwafaraty.Business.Common.Exceptions;
using Mwafaraty.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Mwafaraty.common
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                    if (error is ValidationException)
                {
                    // custom application error
                    response.StatusCode = (int)HttpStatusCode.OK;
                    ResponseVm<BaseVm> res = new ResponseVm<BaseVm>();
                    res.BuildErrorResponse(error.Message);
                    JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions();
                    jsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    await response.WriteAsync(JsonSerializer.Serialize(res, jsonSerializerOptions));
                    return;
                }


               else if (error is KeyNotFoundException)
                {
                    // not found error
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                }
                       
                 else
                {
                    // unhandled error
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                }       
                        
                 
                        
                       
                       
                

                var result = JsonSerializer.Serialize(new { message = error?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}
