﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using jmessage.util;
using jmessage.common;
using System.Diagnostics;
using Newtonsoft.Json;
using jmessage.message;
using jmessage.group;
using jmessage.cross;
using jmessage;
namespace example.CrossExamples
{
    class CrossAppsAddRemoveMembersExample
    {
        public static String app_key = "6be9204c30b9473e87bad4dc";
        public static String master_secret = "a19bef7870c55d7e51f4c4f0";
        public static void Main(string[] args)
        {
            CrossClient client = new CrossClient(app_key, master_secret);
            List<Hashtable> payloads = new List<Hashtable>();
            Hashtable payload1 = new Hashtable();
            payload1["appkey"] = "6be9204c30b9473e87bad4dc"; 
            List<string> add = new List<string> { "jmessage123" };
            payload1["add"] = add;
            payloads.Add(payload1);
            client.crossAppsAddRemoveMembers("19749893", payloads);
        }
    }
}