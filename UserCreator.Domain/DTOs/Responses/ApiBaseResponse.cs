﻿using System.Net;

namespace UserCreator.Domain.DTOs.Responses
{
    public class ApiBaseResponse
    {
        public HttpStatusCode StatusCode { get; set; }

        public List<string> Errors { get; set; } = new List<string>();
    }
}
