using DotNet7_WebAPI.Model;
using DotNet7_WebAPI.Service;
using Microsoft.AspNetCore.Http;
using StackExchange.Redis;
using System.Text;
using System.Text.Json;

namespace DotNet7_WebAPI.Middleware
{
    public class AuthCheckMiddleware
    {
        private readonly RedisService _activeUserDb;
        private readonly RequestDelegate _next;

        public AuthCheckMiddleware(RequestDelegate next, RedisService redisDb)
        {
            _next = next;
            _activeUserDb = redisDb;
        }

        public async Task Invoke(HttpContext context)
        {
            // 경로 검증
            var formString = context.Request.Path.Value;
            if (string.Compare(formString, "/login", StringComparison.OrdinalIgnoreCase) == 0 ||
                string.Compare(formString, "/register", StringComparison.OrdinalIgnoreCase) == 0)
            {
                await _next(context);

                return;
            }
            // 아래의 context.Request.Body.Position= 0;하기 위해서 필요.
            context.Request.EnableBuffering();
            // 토큰 검색
            // http의 원시 바디는 한번 만 접근 할 수 있으므로, 스트림에 담아놓고 여러번 읽을 수 있게 함.
            using (var reader = new System.IO.StreamReader(context.Request.Body, System.Text.Encoding.UTF8, true, 4096, true))
            {
                //string? inputBody = reader.ReadToEnd();
                string? inputBody = await reader.ReadToEndAsync();
                // 바디 유효성 확인
                if (IsbodyFormValid(context, inputBody, out string? id, out string? token) == false)
                {
                    // 문제가 있으면 미들웨어에서 다음 delegate/middleware로 넘기지 않고 그냥 리턴해버림
                    // 클라에게 보낼 메세지는 체크 함수 내부에서 context.Response.Body.WriteAsync(...)에 담는다.
                    return;
                }
                if (id == null || token == null)
                {
                    return;
                }
                if (isTokenValid(context, id, token) == false)
                {
                    return;
                }
                // need to enable buffering.
                // 이걸 해줘야 아래의 await _next(context);도 성공함.
                context.Request.Body.Position= 0;
            }
            //context.Response.Body.Position= 0;
            //context.Request.Body.Position = 0;   
            // 문제 없으면 다음 delegate/middleware로 context를 넘김
            // 근데.. 뭐 성공해도 뭐 전치리 해서 담아서 옮겨야 하나?
            await _next(context);
            return;
        }
        private bool IsbodyFormValid(HttpContext context, string? body, out string? id, out string? token)
        {
            id = null;
            token = null;

            // body가 있는지
            if (string.IsNullOrEmpty(body))
            {
                // 실패시 사유를 context의 response body에 넣어준다.
                return false;
            }
            // json 형태로 토큰이랑 잘 왔는지
            // 일단 어떤 형태로 온 지 알 수 없으니, json으로 일단 파싱
            var document = JsonDocument.Parse(body);
            try
            {
                // 공통적으로 포함되어야 할 id와 token이 있는지 확인
                id = document.RootElement.GetProperty("id").GetString();
                token = document.RootElement.GetProperty("token").GetString();
                return true;
            }
            catch(Exception ex)
            {
                // 실패시 사유를 context의 response body에 넣어준다.
                return false;
            }
        }

        private bool isTokenValid(HttpContext context, string id, string token)
        {
            ActiveUserModel? userInfoInDb;
            string? userInfoJsonStr = RedisActiveUserService.GetActiveUserInfo(_activeUserDb.getRedisDB(), id);
            if (userInfoJsonStr == null)
            {
                // context
                return false;
            }
            try
            {
                userInfoInDb = JsonSerializer.Deserialize<ActiveUserModel>(userInfoJsonStr);
                if (userInfoInDb == null)
                {
                    // context
                    return false;
                }
            }
            catch (Exception ex)
            {
                // context
                return false;
            }
            if (userInfoInDb.Token != token)
            {
                // context
                return false;
            }
            // 토큰 유효기간 연장?
            return true;
        }
    }
}
