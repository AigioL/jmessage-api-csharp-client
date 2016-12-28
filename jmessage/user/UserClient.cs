﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jmessage.util;
using jmessage.common;
using System.Diagnostics;
using Newtonsoft.Json;

namespace jmessage.user
{
    public class UserClient : BaseHttpClient
    {
        private const String HOST_NAME_SSL = "https://api.im.jpush.cn";
        private const String ADMIN_PATH = "/v1/admins/";
        private const String GET_ADMIN_PATH = "/v1/admins?start=";
        private const String USER_PATH = "/v1/users/";
        private const String PUSH_PATH = "/v3/push";

        private String appKey;
        private String masterSecret;
        public UserClient(String appKey, String masterSecret)
        {
            Preconditions.checkArgument(!String.IsNullOrEmpty(appKey), "appKey should be set");
            Preconditions.checkArgument(!String.IsNullOrEmpty(masterSecret), "masterSecret should be set");
            this.appKey = appKey;
            this.masterSecret = masterSecret;
        }
        public ResponseWrapper registUser(List <UserPayload> payload)
        {
            Preconditions.checkArgument(payload != null, "pushPayload should not be empty");
            //payload.Check();
            String payloadJson = this.ToString(payload);
            return registUser(payloadJson);
        }
        public ResponseWrapper registUser(string payloadString)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(payloadString), "payloadString should not be empty");
            Console.WriteLine(payloadString);
            String url = HOST_NAME_SSL;
            url += USER_PATH;
            ResponseWrapper result = sendPost(url, Authorization(), payloadString);
            return result;
        }

        public ResponseWrapper registAdmin(UserPayload payload)
        {
            Preconditions.checkArgument(payload != null, "pushPayload should not be empty");
            payload.Check();
            String payloadJson = payload.ToString(payload);
            return registAdmin(payloadJson);
        }

        public ResponseWrapper registAdmin(string payloadString)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(payloadString), "payloadString should not be empty");

            String url = HOST_NAME_SSL;
            url += ADMIN_PATH;
            ResponseWrapper result = sendPost(url, Authorization(), payloadString);
            return result;
        }

        public ResponseWrapper getAdmin(int start ,int count)
        {
            String url = HOST_NAME_SSL;
            url += GET_ADMIN_PATH;
            url += start.ToString();
            url += "&count=";
            url += count.ToString();
            //GET /v1/admins?start={start}&count={count}
            ResponseWrapper result = sendGet(url, Authorization(),null);
            return result;
        }

        public ResponseWrapper getUser(string username)
        {
            String url = HOST_NAME_SSL;
            url += USER_PATH;
            url += username;
            ResponseWrapper result = sendGet(url, Authorization(), null);
            return result;
        }


        public ResponseWrapper putUser(UserPayload payload)
        {
            Preconditions.checkArgument(payload != null, "pushPayload should not be empty");
            //payload.Check();
            string username = payload.username;
            payload.username = null;
            String payloadJson = payload.ToString(payload);
            return putUser(payloadJson,username);
        }

        public ResponseWrapper putUser(string payloadString,string username)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(payloadString), "payloadString should not be empty");
            Console.WriteLine(payloadString);
            String url = HOST_NAME_SSL;
            url += USER_PATH;
            url += username;
            ResponseWrapper result = sendPut(url, Authorization(), payloadString);
            return result;
        }

        public ResponseWrapper getUserStat(UserPayload payload)
        {
            Preconditions.checkArgument(payload != null, "pushPayload should not be empty");
            //payload.Check();
            string username = payload.username;
            return getUserStat(username);
        }

        public ResponseWrapper getUserStat(string username)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(username), "payloadString should not be empty");
            String url = HOST_NAME_SSL;
            url += USER_PATH;
            url += username;
            url += "/userstat";
            ResponseWrapper result = sendGet(url, Authorization(), null);
            return result;
        }

        public string ToString(List<UserPayload> payload)
        {
            return JsonConvert.SerializeObject(payload,
                            Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
        }

        public  String Authorization()
        {

            Debug.Assert(!string.IsNullOrEmpty(this.appKey));
            Debug.Assert(!string.IsNullOrEmpty(this.masterSecret));

            String origin = this.appKey + ":" + this.masterSecret;
            return Base64.getBase64Encode(origin);
        }
    }
}
