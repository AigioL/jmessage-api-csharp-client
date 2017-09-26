﻿using Jiguang.JMessage.Model;
using Jiguang.JSMS.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Jiguang.JMessage
{
    /// <summary>
    /// 用户相关 API。
    /// <<para><see cref="https://docs.jiguang.cn/jmessage/server/rest_api_im/#_3"/></para>
    /// </summary>
    public class UserClient
    {
        /// <summary>
        /// <seealso cref="UserRegister(List{UserInfo})"/>
        /// </summary>
        public async Task<HttpResponse> RegisterAsync(List<UserInfo> userInfoList)
        {
            if (userInfoList == null)
                throw new ArgumentNullException(nameof(userInfoList));

            string json = JsonConvert.SerializeObject(userInfoList, Formatting.Indented);
            HttpContent httpContent = new StringContent(json, Encoding.UTF8);
            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.PostAsync("/v1/users/", httpContent).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 批量注册用户到极光 IM 服务器，一次批量注册最多支持 500 个用户。
        /// <para><see cref="https://docs.jiguang.cn/jmessage/server/rest_api_im/#_1"/></para>
        /// </summary>
        /// <param name="userInfoList">用户信息对象数组。</param>
        public HttpResponse Register(List<UserInfo> userInfoList)
        {
            Task<HttpResponse> task = Task.Run(() => RegisterAsync(userInfoList));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <seealso cref="AdminRegister(UserInfo)"/>
        /// </summary>
        public async Task<HttpResponse> RegisterAsAdminAsync(UserInfo userInfo)
        {
            if (userInfo == null)
                throw new ArgumentNullException(nameof(userInfo));

            HttpContent httpContent = new StringContent(userInfo.ToString(), Encoding.UTF8);
            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.PostAsync("/v1/admins/", httpContent).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 注册用户为管理员。
        /// <para><see cref="https://docs.jiguang.cn/jmessage/server/rest_api_im/#admin"/></para>
        /// </summary>
        /// <param name="userInfo">待注册为管理员的用户信息对象。</param>
        public HttpResponse RegisterAsAdmin(UserInfo userInfo)
        {
            Task<HttpResponse> task = Task.Run(() => RegisterAsAdminAsync(userInfo));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <seealso cref="GetAdminList(int, int)"/>
        /// </summary>
        public async Task<HttpResponse> GetAdminListAsync(int start, int count)
        {
            if (start < 0)
                throw new ArgumentOutOfRangeException(nameof(start));

            if (count > 500)
                throw new ArgumentOutOfRangeException(nameof(count));

            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.GetAsync($"/v1/admins?start={start}&count={count}").ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 获取管理员列表。
        /// <para><see cref="https://docs.jiguang.cn/jmessage/server/rest_api_im/#admin"/></para>
        /// </summary>
        /// <param name="start">起始记录位置，从 0 开始。</param>
        /// <param name="count">查询条数，最多支持 500 条。</param>
        public HttpResponse GetAdminList(int start, int count)
        {
            Task<HttpResponse> task = Task.Run(() => GetAdminList(start, count));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <seealso cref="GetUserList(int, int)"/>
        /// </summary>
        public async Task<HttpResponse> GetUserListAsync(int start, int count)
        {
            if (start < 0)
                throw new ArgumentOutOfRangeException(nameof(start));

            if (count > 500)
                throw new ArgumentOutOfRangeException(nameof(count));

            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.GetAsync($"/v1/users?start={start}&count={count}").ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 获取用户列表。
        /// </summary>
        /// <param name="start">起始记录位置，从 0 开始。</param>
        /// <param name="count">查询条数，最多支持 500 条。</param>
        public HttpResponse GetUserList(int start, int count)
        {
            Task<HttpResponse> task = Task.Run(() => GetUserListAsync(start, count));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <seealso cref="GetInfo(string)"/>
        /// </summary>
        public async Task<HttpResponse> GetInfoAsync(string username)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentException(nameof(username));

            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.GetAsync($"/v1/users/{username}").ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 获取用户信息。
        /// <para><see cref="https://docs.jiguang.cn/jmessage/server/rest_api_im/#_3"/></para>
        /// </summary>
        public HttpResponse GetInfo(string username)
        {
            Task<HttpResponse> task = Task.Run(() => GetInfoAsync(username));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <seealso cref="UpdateUserInfo(UserInfo)"/>
        /// </summary>
        public async Task<HttpResponse> UpdateInfoAsync(UserInfo userInfo)
        {
            if (userInfo == null)
                throw new ArgumentNullException(nameof(userInfo));

            HttpContent httpContent = new StringContent(userInfo.ToString(), Encoding.UTF8);
            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.PutAsync($"/v1/users/{userInfo.Username}", httpContent).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 更新用户信息。注意：该方法无法修改用户名和密码。
        /// <para>如果要修改密码，需要调用 <see cref="UpdatePassword(string, string)"/></para>
        /// <para><see cref="https://docs.jiguang.cn/jmessage/server/rest_api_im/#_3"/></para>
        /// </summary>
        /// <param name="userInfo">更新后的用户信息对象。</param>
        public HttpResponse UpdateInfo(UserInfo userInfo)
        {
            Task<HttpResponse> task = Task.Run(() => UpdateInfoAsync(userInfo));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <seealso cref="CheckStatus(string)"/>
        /// </summary>
        public async Task<HttpResponse> CheckStatusAsync(string username)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentException(username);

            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.GetAsync($"/v1/users/{username}/userstat").ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 查询用户在线状态。该接口不适用于多端在线，多端在线请用批量状态接口。
        /// <para><see cref="https://docs.jiguang.cn/jmessage/server/rest_api_im/#_3"/></para>
        /// </summary>
        /// <param name="username">待查询用户的用户名。</param>
        public HttpResponse CheckStatus(string username)
        {
            Task<HttpResponse> task = Task.Run(() => CheckStatusAsync(username));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <seealso cref="CheckStatus(List{string})"/>
        /// </summary>
        public async Task<HttpResponse> CheckStatusAsync(List<string> usernameList)
        {
            if (usernameList == null)
                throw new ArgumentNullException(nameof(usernameList));

            string jsonStr = JsonConvert.SerializeObject(usernameList, Formatting.Indented);
            HttpContent httpContent = new StringContent(jsonStr, Encoding.UTF8);
            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.PostAsync("/v1/users/userstat", httpContent).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 批量查询用户在线状态。
        /// <para><see cref="https://docs.jiguang.cn/jmessage/server/rest_api_im/#_3"/></para>
        /// </summary>
        /// <param name="usernameList">待查询用户的用户名列表。</param>
        public HttpResponse CheckUserStatus(List<string> usernameList)
        {
            Task<HttpResponse> task = Task.Run(() => CheckStatusAsync(usernameList));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <seealso cref="UpdatePassword(string, string)"/>
        /// </summary>
        public async Task<HttpResponse> UpdatePasswordAsync(string username, string newPassword)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(username);

            if (string.IsNullOrEmpty(newPassword))
                throw new ArgumentNullException(newPassword);

            JObject jObject = new JObject
            {
                { "new_password", newPassword }
            };

            HttpContent httpContent = new StringContent(jObject.ToString(), Encoding.UTF8);
            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.PostAsync($"/v1/users/{username}/password", httpContent).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 修改用户密码。
        /// <para><see cref="https://docs.jiguang.cn/jmessage/server/rest_api_im/#_3"/></para>
        /// </summary>
        /// <param name="username">待修改用户的用户名。</param>
        /// <param name="newPassword">新密码。</param>
        public HttpResponse UpdatePassword(string username, string newPassword)
        {
            Task<HttpResponse> task = Task.Run(() => UpdatePasswordAsync(username, newPassword));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <seealso cref="Delete(string)"/>
        /// </summary>
        public async Task<HttpResponse> DeleteAsync(string username)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(username);

            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.DeleteAsync($"/v1/users/{username}").ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 删除用户。
        /// <para><see cref="https://docs.jiguang.cn/jmessage/server/rest_api_im/#_3"/></para>
        /// </summary>
        /// <param name="username">待删除用户的用户名。</param>
        public HttpResponse Delete(string username)
        {
            Task<HttpResponse> task = Task.Run(() => DeleteAsync(username));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <seealso cref="Disable(string, bool)"/>
        /// </summary>
        public async Task<HttpResponse> DisableAsync(string username, bool isDisable)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            string url = $"/v1/users/{username}/forbidden?disable={isDisable}";
            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.PutAsync(url, null).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 禁用用户。
        /// </summary>
        /// <param name="username">目标用户用户名。</param>
        /// <param name="isDisable">true: 禁用；false：激活。</param>
        public HttpResponse Disable(string username, bool isDisable) 
        {
            Task<HttpResponse> task = Task.Run(() => DisableAsync(username, isDisable));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <seealso cref="AddToBlackList(string, List{string})"/>
        /// </summary>
        public async Task<HttpResponse> AddToBlackListAsync(string username, List<string> targetUsernameList)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            if (targetUsernameList == null)
                throw new ArgumentNullException(nameof(targetUsernameList));

            string jsonStr = JsonConvert.SerializeObject(targetUsernameList);
            HttpContent httpContent = new StringContent(jsonStr, Encoding.UTF8);
            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.PutAsync($"/v1/users/{username}/blacklist", httpContent).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 添加用户到指定用户的黑名单。
        /// </summary>
        /// <param name="username">要添加黑名单的用户。</param>
        /// <param name="targetUsernameList">被添加到黑名单中的用户名列表。</param>
        public HttpResponse AddToBlackList(string username, List<string> targetUsernameList)
        {
            Task<HttpResponse> task = Task.Run(() => AddToBlackListAsync(username, targetUsernameList));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <seealso cref="RemoveFromBlackList(string, List{string})"/>
        /// </summary>
        public async Task<HttpResponse> RemoveFromBlackListAsync(string username, List<string> targetUsernameList)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            if (targetUsernameList == null)
                throw new ArgumentNullException(nameof(targetUsernameList));

            string jsonStr = JsonConvert.SerializeObject(targetUsernameList, Formatting.Indented);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"/v1/users/{username}/blacklist"),
                Content = new StringContent(jsonStr, Encoding.UTF8, "application/json")
            };

            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.SendAsync(request).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 将用户从黑名单中移除。
        /// </summary>
        /// <param name="username">需要移除黑名单用户的用户名。</param>
        /// <param name="targetUsernameList">被移除用户的用户名列表。</param>
        public HttpResponse RemoveFromBlackList(string username, List<string> targetUsernameList)
        {
            Task<HttpResponse> task = Task.Run(() => RemoveFromBlackListAsync(username, targetUsernameList));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <seealso cref="GetBlackList(string)"/>
        /// </summary>
        public async Task<HttpResponse> GetBlackListAsync(string username)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            string url = $"/v1/users/{username}/blacklist";
            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.GetAsync(url).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 获取指定用户的黑名单列表。
        /// <para><see cref="https://docs.jiguang.cn/jmessage/server/rest_api_im/#_3"/></para>
        /// </summary>
        /// <param name="username">需要获取黑名单用户的用户名。</param>
        public HttpResponse GetBlackList(string username)
        {
            Task<HttpResponse> task = Task.Run(() => GetBlackListAsync(username));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <seealso cref="SetUserNoDisturbAsync(string, List{string}, bool)"/>
        /// </summary>
        public async Task<HttpResponse> SetUserNoDisturbAsync(string username, List<string> targetUsernameList, bool enable)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            if (targetUsernameList == null)
                throw new ArgumentNullException(nameof(targetUsernameList));

            JObject body = new JObject();
            JObject single = new JObject();

            if (enable)
            {
                single.Add("add", JArray.FromObject(targetUsernameList));
            } else
            {
                single.Add("remove", JArray.FromObject(targetUsernameList));
            }

            body.Add("single", single);

            string url = $"/v1/users/{username}/nodisturb";
            HttpContent httpContent = new StringContent(body.ToString(), Encoding.UTF8);

            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.PostAsync(url, httpContent).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 对指定用户设置免打扰。
        /// </summary>
        /// <param name="username">需要设置免打扰用户的用户名。</param>
        /// <param name="targetUsernameList">需要被设置为免打扰的目标用户用户名列表。</param>
        /// <param name="enable">true: 设置为免打扰；false: 解除免打扰。</param>
        public HttpResponse SetUserNoDisturb(string username, List<string> targetUsernameList, bool enable)
        {
            Task<HttpResponse> task = Task.Run(() => SetUserNoDisturbAsync(username, targetUsernameList, enable));
            task.Wait();
            return task.Result;
        }


        /// <summary>
        /// <seealso cref="SetGroupNoDisturbAsync(string, List{string}, bool)"/>
        /// </summary>
        public async Task<HttpResponse> SetGroupNoDisturbAsync(string username, List<int> targetGroupIdList, bool enable)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            if (targetGroupIdList == null)
                throw new ArgumentNullException(nameof(targetGroupIdList));

            JObject body = new JObject();
            JObject group = new JObject();

            if (enable)
            {
                group.Add("add", JArray.FromObject(targetGroupIdList));
            }
            else
            {
                group.Add("remove", JArray.FromObject(targetGroupIdList));
            }

            body.Add("single", group);

            string url = $"/v1/users/{username}/nodisturb";
            HttpContent httpContent = new StringContent(body.ToString(), Encoding.UTF8);

            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.PostAsync(url, httpContent).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 对指定用户设置免打扰。
        /// </summary>
        /// <param name="username">需要设置免打扰用户的用户名。</param>
        /// <param name="targetGroupIdList">需要被设置为免打扰的目标群组 Id 列表。</param>
        /// <param name="enable">true: 设置为免打扰；false: 解除免打扰。</param>
        public HttpResponse SetGroupNoDisturb(string username, List<int> targetGroupIdList, bool enable)
        {
            Task<HttpResponse> task = Task.Run(() => SetGroupNoDisturbAsync(username, targetGroupIdList, enable));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <seealso cref="SetGlobalNoDisturb(string, bool)"/>
        /// </summary>
        public async Task<HttpResponse> SetGlobalNoDisturbAsync(string username, bool enable)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            JObject body = new JObject();
            int global = enable ? 1 : 0;
            body.Add("global", global);

            string url = $"/v1/users/{username}/nodisturb";
            HttpContent httpContent = new StringContent(body.ToString(), Encoding.UTF8);

            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.PostAsync(url, httpContent).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 为指定用户设置全局免打扰。
        /// </summary>
        /// <param name="username">需要设置免打扰用户的用户名。</param>
        /// <param name="enable">true: 开启免打扰；false: 关闭免打扰。</param>
        public HttpResponse SetGlobalNoDisturb(string username, bool enable)
        {
            Task<HttpResponse> task = Task.Run(() => SetGlobalNoDisturbAsync(username, enable));
            task.Wait();
            return task.Result;
        }
    }
}
