﻿using System;
using jmessage.group;

namespace example.GroupExamples
{
    class GetGroupMembersExample
    {
        public static string app_key = "6be9204c30b9473e87bad4dc";
        public static string master_secret = "a19bef7870c55d7e51f4c4f0";

        public static void Main(string[] args)
        {
            GroupClient client = new GroupClient(app_key, master_secret);
            client.getGroupMembers(20292095);

            Console.ReadLine();
        }
    }
}
