﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jmessage.common;
using jmessage.util;
using jmessage.user;
using jmessage;


namespace example
{
    class AddSingleNodisturbExample:JMessageExampleBase
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("*****免打扰设置******");
            JMessageClient client = new JMessageClient(app_key, master_secret);
            String user = "jintian";
            List<String> users = new List<String> { "jintian1234" };
            client._messageClient.addSingleNodisturb(user, users);
            Console.ReadLine();
        }
    }
}
