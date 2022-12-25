namespace DotNet7_WebAPI
{
    public enum ErrorCode : UInt16
    {
        NoError = 0,
        // 잘못된 ID, 잘못된 PASS, 잘못된 토큰, 잘못된 레벨 요청
        // 만료된 토큰, 
        // 잘못된 요청 에러 : 101~200
        WrongID = 101,
        WrongPassword = 102,
        WrongToken = 103,
        WrongLevelReq = 104,
        DuplicatedID = 105,
        WrongRequest = 106,


        NotDefindedError = 999
    }
}
