using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace PokemonAPI.BaseResponse
{

    // double check: TEntity is so it can be generic / anything? 
    // : classs, means it will be of a type from a class
    // TEntity generic type paramater, saying Data will later be a class (?)
    public class Result<TEntity> where TEntity : class
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public TEntity Data { get; set; }
        public string ErrorMessage { get; set; } // if this is not null or empty, set data to null

        // if theres no error message string, the request was succesfull
        // this way its read-only
        public bool RequestIsSuccssful
        {
            get
            {
                if (string.IsNullOrEmpty(ErrorMessage))
                {
                    return true;
                }
                return false;
            }
        }
    }
}
