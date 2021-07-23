using System;
using Requests;
using FakeRestAPI.DataTransferObject;
using System.Collections.Generic;

namespace FakeRestAPI
{
    public static class FakeRest
    {
        public static void Run()
        {
            var req = new Requests<List<FakeRestDTO>>();
            req.Get("http://10.201.8.2/Users/");
        }
    }
}
