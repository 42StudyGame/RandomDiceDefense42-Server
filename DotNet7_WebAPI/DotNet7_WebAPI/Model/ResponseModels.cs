namespace DotNet7_WebAPI.Model
{
    public class CommonResponseModel
    {
        public ErrorCode errorCode { get; set; }

    }
    public class RsRegister : CommonResponseModel { }
    public class RsLogin : CommonResponseModel { }
    public class RsMiddleWare : CommonResponseModel { }
}
